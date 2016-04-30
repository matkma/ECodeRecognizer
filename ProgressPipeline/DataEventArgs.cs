using System;
using System.Collections.Generic;
using CommonResources;

namespace ControlPipeline
{
    public delegate void DataEventHandler(Object source, DataEventArgs e);

    public class DataEventArgs : EventArgs
    {
        private List<ECode> ecodes ;

        public DataEventArgs(List<ECode> arg)
        {
            ecodes = arg;
        }

        public List<ECode> GetInfo()
        {
            return ecodes;
        }
    }
}
