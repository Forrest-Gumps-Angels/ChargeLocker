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
            _display = Substitute.For<IDisplay>();
            _uut = new ChargeControl(_usbCharger, _display);
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
            _display.Received(0).DisplayStatus(Arg.Any<string>());
        }

        [Test]
        public void ChargeController_SetValue_on_Currentchanged()
        {
            _door.CloseDoor();
            _usbCharger.SimulateOverload(false);
            _usbCharger.SimulateConnected(true);
            _usbCharger.StartCharge();



            Assert.That(_uut.Current, Is.EqualTo(_usbCharger.CurrentValue));

        }

        [Test]
        public void ChargeController_PrintMessage_fullycharged()
        {
            _door.CloseDoor();
            _usbCharger.SimulateOverload(false);
            _usbCharger.SimulateConnected(true);
            _usbCharger.StartCharge();

            System.Threading.Thread.Sleep(61000);

            _display.Received().DisplayStatus("Telefonen er fuldt opladet!");

        }

        [Test]
        public void ChargeController_PrintMessage_on_charging()
        {
            _door.CloseDoor();
            _usbCharger.SimulateOverload(false);
            _usbCharger.SimulateConnected(true);
            _usbCharger.StartCharge();

            _display.Received(1).DisplayStatus("Ladningen foregår! Current is at: " + _uut.Current);

        }

        [Test]
        public void ChargeController_PrintMessage_on_overload()
        {
            _door.CloseDoor();
            _usbCharger.SimulateOverload(true);
            _usbCharger.SimulateConnected(true);
            _usbCharger.StartCharge();

            _display.Received(1).DisplayStatus("Hov! Der gik noget galt. Frakobl straks dit ringe apparat!");

        }



    }
}
