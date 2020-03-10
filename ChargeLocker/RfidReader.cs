using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargeLocker
{

    public class RfidReader : IRfidReader
    {
        public event EventHandler<RfidDetectedEventArgs> RfidDetectedEvent;

        public virtual void OnRfidRead(int id)
        {
            RfidDetectedEventArgs rfidDetectedEventArgs = new RfidDetectedEventArgs() { id = id };

            RfidDetectedEvent?.Invoke(this, rfidDetectedEventArgs);
        }
    }  
}
