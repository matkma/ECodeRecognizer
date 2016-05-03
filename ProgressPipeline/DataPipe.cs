using System.Collections.Generic;
using CommonResources;

namespace ControlPipeline
{
    public class DataPipe
    {
        public event DataEventHandler DataEvent;

        private static DataPipe _instance;
        public static DataPipe Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DataPipe();
                }
                return _instance;
            }
        }

        public void DataMessage(List<ECode> ecodes)
        {
            var handler = DataEvent;
            if (handler != null)
            {
                handler(this, new DataEventArgs(ecodes));
            }
        }
    }
}
