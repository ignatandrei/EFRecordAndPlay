using System;

namespace EFRecordAndPlay
{
    [Serializable]
    public class ExceptionSerializable
    {
        public static explicit operator Exception(ExceptionSerializable element)
        {
            var message = element.ExceptionMessage;
            string exceptionType = element.ExceptionType;
            Exception ex;
            try
            {

                ex = Activator.CreateInstance(Type.GetType(exceptionType), message) as Exception;
            }
            catch
            {
                ex = new Exception(exceptionType + "=>"+message);
            }
            //TODO: load inner stack, and so on
            
            return ex;
        }

        public static explicit operator ExceptionSerializable(Exception exception)
        {

            if (exception == null)
            {
                return null;
            }

            return new ExceptionSerializable(exception);
        }

        public ExceptionSerializable()
        {
            
        }
        public ExceptionSerializable(Exception ex)
        {
            if(ex==null)
                return;
            ExceptionType = ex.GetType().FullName;
            ExceptionMessage = ex.Message;
        }
        public string ExceptionType { get; set; }
        public string ExceptionMessage { get; set; }
       
        
    }
}