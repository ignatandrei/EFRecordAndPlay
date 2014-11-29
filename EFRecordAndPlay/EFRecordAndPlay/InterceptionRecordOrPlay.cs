using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace EFRecordAndPlay
{
    public class InterceptionRecordOrPlay:IDbCommandInterceptor
    {
        public string FilePath { get; set; }
        public ModeInterception ModeInterception { get; set; }
        private Queue<InterceptionData> _dataForInterception;
        public int SaveAfterSteps = 1;
        public InterceptionRecordOrPlay(string filePath, ModeInterception modeInterception)
        {
            if (!Path.IsPathRooted(filePath))
                filePath = Path.GetFullPath(filePath);

            FilePath = filePath;
            ModeInterception = modeInterception;
            _dataForInterception=new Queue<InterceptionData>();
            var xs = new XmlSerializer(typeof(InterceptionData));
            
            if (ModeInterception == ModeInterception.Play)
            {
                var zipFIle = ZipFile.OpenRead(filePath);
                var serialized = new List<Tuple<int, InterceptionData>>(); 
                foreach (var entry in zipFIle.Entries)
                {
                    var nr =int.Parse(entry.Name.Replace(".txt",""));
                    using (var str = entry.Open())
                    {
                        var id = xs.Deserialize(str) as InterceptionData;
                        serialized.Add(new Tuple<int, InterceptionData>(nr,id));
                    }
                }

                foreach (var ser in serialized.OrderBy(it=>it.Item1))
                {
                    _dataForInterception.Enqueue(ser.Item2);
                }
                
            }
        }

        private void DecideIfSave()
        {
            if(_dataForInterception.Count % SaveAfterSteps == 0)
                Save();

        }
        public void Save()
        {
            var xs=new XmlSerializer(typeof(InterceptionData));
            var backupQueue=new Queue<InterceptionData>();
            string pathStore = Path.GetTempPath();
            pathStore = Path.Combine(pathStore, Assembly.GetEntryAssembly().GetName().Name);
            if(Directory.Exists(pathStore))
                Directory.Delete(pathStore,true);

            Directory.CreateDirectory(pathStore);
            int i = 0;
            while (_dataForInterception.Count>0)
            {
                i++;
                var data = _dataForInterception.Dequeue();
                    
                backupQueue.Enqueue(data);

                using (var ms = new MemoryStream())
                {
                    //formatter.Serialize(ms,data);
                    xs.Serialize(ms,data);
                    var bytes = ms.ToArray();
                    File.WriteAllBytes(Path.Combine(pathStore,i+".txt"),bytes);
                }
            }
            
            if(File.Exists(FilePath))
                File.Delete(FilePath);

            ZipFile.CreateFromDirectory(pathStore,FilePath,CompressionLevel.Fastest,false);
            
            _dataForInterception = backupQueue;
        }

        public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            if(ModeInterception != ModeInterception.Play)
                return;

            if(_dataForInterception.Count == 0)
                return;

            var data = _dataForInterception.Dequeue();
            if (data.ExceptionThrown != null)
                throw data.ExceptionThrown;

            interceptionContext.Result = data.NonQuery;

        }

        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            if (ModeInterception != ModeInterception.Record)
                return;

            _dataForInterception.Enqueue(new InterceptionData(command){ NonQuery = interceptionContext.Result, ExceptionThrown = interceptionContext.OriginalException});

            DecideIfSave();
        }

        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            if (ModeInterception != ModeInterception.Play)
                return;

            if (_dataForInterception.Count == 0)
                return;

            var data = _dataForInterception.Dequeue();

            if (data.ExceptionThrown != null)
                throw data.ExceptionThrown;

            interceptionContext.Result = data.Data.CreateDataReader();

        }

        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            if (ModeInterception != ModeInterception.Record)
                return;

            DataTable dataTable = null;
            
            if (interceptionContext.OriginalException == null)
            {
                dataTable=new DataTable {TableName = command.CommandText};
                dataTable.Load(interceptionContext.OriginalResult);

                interceptionContext.Result = dataTable.CreateDataReader();
            }

            _dataForInterception.Enqueue(new InterceptionData(command) {  Data = dataTable, ExceptionThrown = interceptionContext.OriginalException });
            DecideIfSave();
        }

        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            if (ModeInterception != ModeInterception.Play)
                return;

            if (_dataForInterception.Count == 0)
                return;

            var data = _dataForInterception.Dequeue();

            if (data.ExceptionThrown != null)
                throw data.ExceptionThrown;

            interceptionContext.Result = data.Scalar;

        }

        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            if (ModeInterception != ModeInterception.Record)
                return;

            _dataForInterception.Enqueue(new InterceptionData(command) { Scalar = interceptionContext.OriginalResult, ExceptionThrown = interceptionContext.OriginalException });
            DecideIfSave();
        }
    }
}