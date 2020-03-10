using Ladeskab;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsbSimulator;

namespace ChargeLocker.Unit.Test
{
    [TestFixture]
    public class TestDisplay
    {
        private StationControl _stCTRL;
        private IChargeControl _charger;
        private IDisplay _uut;
        private IDoor _door;
        private IRfidReader _reader;
        private UsbChargerSimulator _usbCharger;

        [SetUp]
        public void Setup()
        {
            _usbCharger = new UsbChargerSimulator();
            _charger = Substitute.For<ChargeControl>(_usbCharger);
            _uut = new DisplayConsole();
            _door = Substitute.For<IDoor>();
            _reader = Substitute.For<IRfidReader>();


            _stCTRL = new StationControl(_charger, _uut, _door, _reader);

        }

        [Test]
        public void Display_DoorOpened_Display_TilslutTelefon()
        {
            _door.OpenDoor();
            
        }

    }
}
