using Ladeskab;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsbSimulator;
using ChargeLocker;
using NSubstitute;

namespace ChargeLocker.Unit.Test
{
    [TestFixture]
    public class TestChargeControl
    {
        private StationControl _stCTRL;
        private IChargeControl _uut;
        private IDisplay _display;
        private IDoor _door;
        private IRfidReader _reader;
        private UsbChargerSimulator _usbCharger;

        [SetUp]
        public void Setup()
        {
            _usbCharger = new UsbChargerSimulator();
            _uut = new ChargeControl(_usbCharger);
            _display = Substitute.For<IDisplay>();
            _door    = Substitute.For<IDoor>();
            _reader  = Substitute.For<IRfidReader>();


            _stCTRL = new StationControl(_uut, _display,_door, _reader);

        }

        [Test]
        public void ChargeController_StartCharging_When_valid_RFIDDetected()
        {
            _door.DoorCloseEvent += Raise.Event();
            _usbCharger.SimulateConnected(true);
            _reader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs { id = 1403 });
           

            // Make sure that usbcharger has been started
            Assert.Greater(_usbCharger.CurrentValue, 0);
        }

        [Test]
        public void ChargeController_NotCharging_When_no_RFIDDetected()
        {
            _door.DoorCloseEvent += Raise.Event();
            _usbCharger.SimulateConnected(true);

            // Make sure that usbcharger has been started
            Assert.AreEqual(_usbCharger.CurrentValue, 0);
        }

        [Test]
        public void ChargeController_IsConnected_When_UsbChargerConnected()
        {
            // Typisk brugsscenarie
            _door.DoorCloseEvent += Raise.Event();
            _usbCharger.SimulateConnected(true);
            _reader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs { id = 1403 });

            // Make sure that usbcharger has been started
            Assert.AreEqual(_uut.IsConnected(), true);
        }

        [Test]
        public void ChargeController_not_validating_connection_When_no_phone_connected()
        {
            // Typisk brugsscenarie
            _door.DoorCloseEvent += Raise.Event();
            _usbCharger.SimulateConnected(false);
            _reader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs { id = 1403 });

            // Make sure that usbcharger has been started
            Assert.AreEqual(_uut.IsConnected(), false);
        }
    }
}
