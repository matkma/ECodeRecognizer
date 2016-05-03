using System;

namespace ControlPipeline
{
    public class ErrorPipe
    {
        public event ErrorEventHandler ErrorEvent;

        private static ErrorPipe _instance;
        public static ErrorPipe Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ErrorPipe();
                }
                return _instance;
            }
        }

        public void ErrorMessage(Object sender, string message)
        {
            var error = "Wystąpił błąd w " + sender.GetType().Name + ": " + message;
            var handler = ErrorEvent;
            if (handler != null)
            {
                handler(this, new ErrorEventArgs(error, DateTime.Now));
            }
        }
    }
}
