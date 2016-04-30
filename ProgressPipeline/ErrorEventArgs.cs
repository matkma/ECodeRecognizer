using System;
using CommonResources;

namespace ControlPipeline
{
    public delegate void ErrorEventHandler(Object source, ErrorEventArgs e);

    public class ErrorEventArgs
    {
        EventOutput output = new EventOutput();
        
        public ErrorEventArgs(string text, DateTime dt)
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
