using System;
using CommonResources;

namespace ControlPipeline
{
    public delegate void ProgressEventHandler(object source, ProgressEventArgs e);

    public class ProgressEventArgs : EventArgs
    {
        private EventOutput _output = new EventOutput();
        
        public ProgressEventArgs(string text, DateTime dt)
        {
            _output.EventInfo = text;
            _output.BeginDateTime = dt;
        }

        public EventOutput GetInfo()
        {
            return _output;
        }
    }
}
