using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargeLocker
{
    public class RfidDetectedEventArgs : EventArgs
    {
        public int id { get; set; }
    }
    public interface IRfidReader
    {
        // Event handlers
        event EventHandler<RfidDetectedEventArgs> RfidDetectedEvent;
        void RfidRead(int _id);
    }

}
