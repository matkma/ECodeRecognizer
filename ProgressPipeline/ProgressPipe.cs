using System;

namespace ControlPipeline
{
    public class ProgressPipe
    {
        public event ProgressEventHandler ProgressEvent;

        private static ProgressPipe _instance;
        public static ProgressPipe Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ProgressPipe();
                }
                return _instance;
            }
        }

        public void ProgressMessage(string message)
        {
            var handler = ProgressEvent;
            if (handler != null)
            {
                handler(this, new ProgressEventArgs(message, DateTime.Now));
            }
        }
    }
}
