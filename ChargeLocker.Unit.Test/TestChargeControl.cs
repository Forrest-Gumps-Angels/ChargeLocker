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
        private IUsbCharger _usbCharger;

        [SetUp]
        public void Setup()
        {
            _usbCharger = Substitute.For<IUsbCharger>();
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
            _usbCharger.Connected.Returns(true);
            _reader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs { id = 1403 });
            _usbCharger.ReceivedWithAnyArgs().StartCharge();
        }

        [Test]
        public void ChargeController_NotCharging_When_no_RFIDDetected()
        {
            _door.DoorCloseEvent += Raise.Event();
            _usbCharger.Connected.Returns(true);
            _usbCharger.DidNotReceiveWithAnyArgs().StartCharge();
        }

        [Test]
        public void ChargeController_IsConnected_When_UsbChargerConnected()
        {
            _door.DoorCloseEvent += Raise.Event();
            _usbCharger.Connected.Returns(true);
            Assert.AreEqual(_uut.IsConnected(), true);
        }

        [Test]
        public void ChargeController_not_validating_connection_When_no_phone_connected()
        {
            // Typisk brugsscenarie
            _door.DoorCloseEvent += Raise.Event();
            _usbCharger.Connected.Returns(false);
            Assert.AreEqual(_uut.IsConnected(), false);
        }

        [TestCase(501, 501)]
        [TestCase(500, 500)]
        [TestCase(250, 250)]
        [TestCase(0, 0)]
        public void ChargeController_SetValue_on_CurrentChanged(double current, double expected)
        {
            _door.CloseDoor();
            _usbCharger.Connected.Returns(true);
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentChangedEventArgs {Current = current} );
            Assert.That(_uut.Current, Is.EqualTo(expected));

        }

        [TestCase(0.01, "Telefonen er fuldt opladet!")]
        [TestCase(2.5, "Telefonen er fuldt opladet!")]
        [TestCase(5, "Telefonen er fuldt opladet!")]
        public void ChargeController_PrintMessage_fullycharged(double current, string expected)
        {
            _door.CloseDoor();

            _usbCharger.Connected.Returns(true);
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentChangedEventArgs { Current = current });

            _display.Received().DisplayStatus(expected);

        }

        [TestCase(5.01, "Ladningen foregår! Current is at: 5,01")]
        [TestCase(250, "Ladningen foregår! Current is at: 250")]
        [TestCase(500, "Ladningen foregår! Current is at: 500")]
        public void ChargeController_PrintMessage_on_charging(double current, string expected)
        {
            _door.CloseDoor();

            _usbCharger.Connected.Returns(true);
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentChangedEventArgs { Current = current });

            _display.Received().DisplayStatus(expected);


        }

        [TestCase(500.01, "Hov! Der gik noget galt. Frakobl straks dit ringeapparat!")]
        [TestCase(750, "Hov! Der gik noget galt. Frakobl straks dit ringeapparat!")]
        [TestCase(1000, "Hov! Der gik noget galt. Frakobl straks dit ringeapparat!")]
        public void ChargeController_PrintMessage_on_overload(double current, string expected)
        {
            _door.CloseDoor();

            _usbCharger.Connected.Returns(true);
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentChangedEventArgs { Current = current });

            _display.Received(1).DisplayStatus(expected);

        }
    }
}
