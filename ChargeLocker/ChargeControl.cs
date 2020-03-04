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
        private IUsbCharger _charger;
        private double _current;

        public ChargeControl(IUsbCharger charger)
        {
            _charger = charger;
        }

        public bool IsConnected()
        {
            return _charger.Connected;
        }
        public void StartCharge()
        {
            _charger.StartCharge();
        }
        public void StopCharge()
        {
            _charger.StopCharge();
        }
        public void currentChangedEventHandler(object sender, CurrentChangedEventArgs e)
        {
            _current = e.Current;
        }
    }
}
