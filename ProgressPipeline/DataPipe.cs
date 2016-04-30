using System.Collections.Generic;
using CommonResources;

namespace ControlPipeline
{
    public class DataPipe
    {
        public event DataEventHandler DataEvent;

        private static DataPipe instance;
        public static DataPipe Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataPipe();
                }
                return instance;
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
