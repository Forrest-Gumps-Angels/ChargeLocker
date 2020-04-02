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
        private IDisplay _display;
        private double current;

        public double Current { get => current; set => current = value; }

        public IChargeControl IChargeControl
        {
            get => default;
            set
            {
            }
        }

        public ChargeControl(IUsbCharger charger, IDisplay display)
        {
            _display = display;
            _charger = charger;
            _charger.CurrentValueEvent += currentChangedEventHandler;
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
            Current = e.Current;
            switch (Current)
            {
                case double n when (n > 0 && n <= 5):
                    _display.DisplayStatus("Telefonen er fuldt opladet!");
                    break;
                case double n when (n > 5 && n <= 500):
                    _display.DisplayStatus("Ladningen foregår! Current is at: " + Current);
                    break;
                case double n when (n > 500):
                    _display.DisplayStatus("Hov! Der gik noget galt. Frakobl straks dit ringeapparat!");
                    break;
            }
        }
    }
}
