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
            switch (_current)
            {
                case 0:
                        return false;
                    
                case double current when (0.0 < current && current <= 5):
                    return true;

                case double current when (5.0 < current && current <= 500):
                    return true;

                case double current when (current > 500):
                    return true;

                default:
                    return false;

                    // Simple if statement would do...
            }
            
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
