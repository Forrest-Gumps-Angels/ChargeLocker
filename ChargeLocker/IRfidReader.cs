using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargeLocker
{
    class RfidDetectedEventArgs : EventArgs
    {
        public int id;
    }
    interface IRfidReader
    {
        // Event handlers
        event EventHandler<RfidDetectedEventArgs> RfidDetectedEvent;
    }
}
