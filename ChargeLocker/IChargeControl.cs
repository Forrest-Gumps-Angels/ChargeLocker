using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsbSimulator;

namespace ChargeLocker
{
    interface IChargeControl
    {
        bool IsConnected();
        void StartCharge();
        void StopCharge();
        void currentChangedEventHandler(CurrentChangedEventArgs e);
    }
}
