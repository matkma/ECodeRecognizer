using System;
using CommonResources;

namespace ControlPipeline
{
    public delegate void ProgressEventHandler(Object source, ProgressEventArgs e);

    public class ProgressEventArgs : EventArgs
    {
        EventOutput output = new EventOutput();
        
        public ProgressEventArgs(string text, DateTime dt)
        {
            output.eventInfo = text;
            output.beginDateTime = dt;
        }

        public EventOutput GetInfo()
        {
            return output;
        }
    }
}
