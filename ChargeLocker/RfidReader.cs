﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargeLocker
{

    public class RfidReader : IRfidReader
    {
        public event EventHandler<RfidDetectedEventArgs> RfidDetectedEvent;

        public void RfidRead(int _id)
        {
            Console.WriteLine("RfidRead was called");
            OnRfidRead(_id);
        }

        public virtual void OnRfidRead(int _id)
        {
            RfidDetectedEventArgs rfidDetectedEventArgs = new RfidDetectedEventArgs() { id = _id };

            RfidDetectedEvent?.Invoke(this, rfidDetectedEventArgs);
        }
    }  
}
