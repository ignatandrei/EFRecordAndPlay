using System;
using System.Data;
using System.Data.Common;

namespace EFRecordAndPlay
{
    [Serializable]
    public class InterceptionData
    {
        public InterceptionData():this(null)
        {
            
        }

        public InterceptionData(DbCommand command)
        {
            if(command == null)
                return;
            CommandText = command.CommandText;
            foreach (DbParameter parameter in command.Parameters)
            {
                CommandParameters += parameter.ParameterName + "=" + parameter.Value + " ";
            }
        }
        public string CommandText { get; set; }

        public string CommandParameters { get; set; }

        public DataTable Data { get; set; }

        public int NonQuery { get; set; }

        public object Scalar { get; set; }

        
        
    }
}