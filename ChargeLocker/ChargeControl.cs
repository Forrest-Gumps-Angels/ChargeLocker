using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsbSimulator;

namespace ChargeLocker
{
    public class ChargeControl : IChargeControl
    {
        private double _current;

        public bool IsConnected()
        {
         switch   
        }
        public void StartCharge()
        { 

        }
        public void StopCharge()
        { 

        }
        public void currentChangedEventHandler(object sender, CurrentChangedEventArgs e)
        {
            _current = e.Current;
        }
    }
}
