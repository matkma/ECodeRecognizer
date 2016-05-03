using System;
using System.Collections.Generic;
using CommonResources;

namespace ControlPipeline
{
    public delegate void DataEventHandler(object source, DataEventArgs e);

    public class DataEventArgs : EventArgs
    {
        private List<ECode> _ecodes ;

        public DataEventArgs(List<ECode> arg)
        {
            _ecodes = arg;
        }

        public List<ECode> GetInfo()
        {
            return _ecodes;
        }
    }
}
