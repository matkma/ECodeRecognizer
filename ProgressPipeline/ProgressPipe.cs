using System;

namespace ControlPipeline
{
    public class ProgressPipe
    {
        public event ProgressEventHandler ProgressEvent;

        private static ProgressPipe instance;
        public static ProgressPipe Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProgressPipe();
                }
                return instance;
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
