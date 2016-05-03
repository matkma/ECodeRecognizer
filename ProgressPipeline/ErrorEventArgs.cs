using System;
using CommonResources;

namespace ControlPipeline
{
    public delegate void ErrorEventHandler(object source, ErrorEventArgs e);

    public class ErrorEventArgs
    {
        private EventOutput _output = new EventOutput();
        
        public ErrorEventArgs(string text, DateTime dt)
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
