using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace EFRecordAndPlay
{
    public class InterceptionRecordOrPlay : DbCommandInterceptor
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
                using var zipFIle = ZipFile.OpenRead(filePath);
                var serialized = new List<Tuple<int, InterceptionData>>(); 
                foreach (var entry in zipFIle.Entries)
                {
                    var nr =int.Parse(entry.Name.Replace(".txt",""));
                    using var str = entry.Open();
                    var id = xs.Deserialize(str) as InterceptionData;
                    serialized.Add(new Tuple<int, InterceptionData>(nr, id));
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
        private void Save()
        {
            var xs=new XmlSerializer(typeof(InterceptionData));
            var backupQueue=new Queue<InterceptionData>();
            string pathStore = Path.GetTempPath();
            var ass = (Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly());
            pathStore = Path.Combine(pathStore, ass.GetName().Name);
            if(Directory.Exists(pathStore))
                Directory.Delete(pathStore,true);

            Directory.CreateDirectory(pathStore);
            int i = 0;
            while (_dataForInterception.Count>0)
            {
                i++;
                var data = _dataForInterception.Dequeue();
                    
                backupQueue.Enqueue(data);

                using var ms = new MemoryStream();
                xs.Serialize(ms, data);
                var bytes = ms.ToArray();
                File.WriteAllBytes(Path.Combine(pathStore, i + ".txt"), bytes);
            }
            
            if(File.Exists(FilePath))
                File.Delete(FilePath);

            ZipFile.CreateFromDirectory(pathStore,FilePath,CompressionLevel.Fastest,false);
            
            _dataForInterception = backupQueue;
        }

        public override InterceptionResult<int> NonQueryExecuting(DbCommand command, 
            CommandEventData eventData, InterceptionResult<int> result)
        {
            if(ModeInterception != ModeInterception.Play)
                return base.NonQueryExecuting(command, eventData, result); ;

            if(_dataForInterception.Count == 0)
                return base.NonQueryExecuting(command, eventData, result); ;

            var data = _dataForInterception.Dequeue();
           
            return InterceptionResult<int>.SuppressWithResult( data.NonQuery);


        }
        public override int NonQueryExecuted(DbCommand command, CommandExecutedEventData eventData, int result)
        {
            if (ModeInterception != ModeInterception.Record)
                return base.NonQueryExecuted(command, eventData, result); ;

            _dataForInterception.Enqueue(new InterceptionData(command){ NonQuery = result});

            DecideIfSave();
            return result;
        }
        public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
        {
            if (ModeInterception != ModeInterception.Play)
                return base.ReaderExecuting(command, eventData, result);


            if (_dataForInterception.Count == 0)
                return base.ReaderExecuting(command, eventData, result);

            var data = _dataForInterception.Dequeue();

            return InterceptionResult<DbDataReader>.SuppressWithResult(data.Data.CreateDataReader());

        }
        public override DbDataReader ReaderExecuted(DbCommand command, CommandExecutedEventData eventData, DbDataReader result)
        {
            if (ModeInterception != ModeInterception.Record)
                return base.ReaderExecuted(command, eventData, result);

            var types = new Dictionary<string, Type>();
            for (var i = 0; i < result.FieldCount; i++)
            {
                var t = result.GetFieldType(i);
                var name = result.GetName(i);
                types.Add(name, t);
            }


            var dataTable = new DataTable { TableName = command.CommandText };
            dataTable.Load(result);
            foreach (var item in types)
            {
                var existingType = dataTable.Columns[item.Key].DataType;
                if (existingType.FullName != item.Value.FullName)
                {
                    dataTable.Columns[item.Key].DataType = item.Value;
                }
            }


            _dataForInterception.Enqueue(new InterceptionData(command) { Data = dataTable });
            DecideIfSave();
            return dataTable.CreateDataReader();
        }
        public override InterceptionResult<object> ScalarExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<object> result)
        {

            if (ModeInterception != ModeInterception.Play)
                return base.ScalarExecuting(command, eventData, result);
            

            if (_dataForInterception.Count == 0)
                return base.ScalarExecuting(command, eventData, result);
            

            var data = _dataForInterception.Dequeue();

            
            return InterceptionResult<object>.SuppressWithResult(data.Scalar);

        }

        public override object ScalarExecuted(DbCommand command, CommandExecutedEventData eventData, object result)
        {
            if (ModeInterception != ModeInterception.Record)
                return base.ScalarExecuted(command, eventData, result);
            

            _dataForInterception.Enqueue(new InterceptionData(command) { Scalar = result});
            DecideIfSave();
            return result;
        }
    }
}