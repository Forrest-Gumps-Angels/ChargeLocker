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
    public class TestStationControl
    {
        private StationControl _uut;
        private IChargeControl _charger;
        private IDisplay _display;
        private IDoor _door;
        private IRfidReader _reader;
        private UsbChargerSimulator _usbCharger;

        [SetUp]
        public void Setup()
        {
            _usbCharger = new UsbChargerSimulator();
            _charger = Substitute.For<ChargeControl>(_usbCharger);
            _display = Substitute.For<IDisplay>();
            _door = Substitute.For<IDoor>();
            _reader = Substitute.For<IRfidReader>();


            _uut = new StationControl(_charger, _display, _door, _reader);

        }

        [Test]
        public void StationController_SendsMessage_On_DoorOpened()
        {
            _door.DoorOpenEvent += Raise.Event();

            _display.Received(1).Display("Tilslut telefon");
        }

        [Test]
        public void StationController_SendsMessage_On_DoorClosed()
        {
            _door.DoorCloseEvent += Raise.Event();

            _display.Received(1).Display("Indlæs RFID");
        }


        [Test]
        public void StationController_ChecksConnection_when_RfidDetected()
        {
            _door.DoorCloseEvent += Raise.Event();
            _reader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs { id = 1403 });

            _charger.Received(1).IsConnected();
        }

        [Test]
        public void StationController_SendsMessage_when_IsConnected_is_false()
        {
            _door.DoorCloseEvent += Raise.Event();
            _reader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs { id = 1403 });
            _usbCharger.SimulateConnected(false);

           // _display.ReceivedWithAnyArgs(1).Display(default);
            _display.Received(1).Display("Tilslutningsfejl");
        }

        [Test]
        public void StationController_StartCharge_when_IsConnected_is_true()
        {
            _door.DoorCloseEvent += Raise.Event();
            _reader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs { id = 1403 });
            _usbCharger.SimulateConnected(false);

            _charger.Received(1).StartCharge();
        }

        [Test]
        public void StationController_LockDoor_when_IsConnected_is_true()
        {
            _door.DoorCloseEvent += Raise.Event();
            _reader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs { id = 1403 });
            _usbCharger.SimulateConnected(false);

            _door.Received(1).LockDoor();
        }

        [Test]
        public void StationController_SendsMessage_when_IsConnected_is_true()
        {
            _door.DoorCloseEvent += Raise.Event();
            _reader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs { id = 1403 });
            _usbCharger.SimulateConnected(false);

            _display.Received(1).Display("Ladeskab optaget");
            //
        }

        [Test]
        public void StationController_SendsMessage_when_incorrect_RfidDetected()
        {
            _usbCharger.SimulateConnected(true);
            _door.DoorCloseEvent += Raise.Event();
            _reader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs { id = 1403 });
            // New RFID wih incorrect id
            _reader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs { id = 0 });

            _display.Received(1).Display("RFID fejl");
        }

        [Test]
        public void StationController_StopCharge_when_correct_RfidDetected()
        {
            _usbCharger.SimulateConnected(true);
            _door.DoorCloseEvent += Raise.Event();
            _reader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs { id = 1403 });
            // New RFID wih incorrect id
            _reader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs { id = 1403 });

            _charger.Received(1).StopCharge();
        }

        [Test]
        public void StationController_UnlockDoor_when_correct_RfidDetected()
        {
            _usbCharger.SimulateConnected(true);
            _door.DoorCloseEvent += Raise.Event();
            _reader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs { id = 1403 });
            // New RFID wih incorrect id
            _reader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs { id = 1403 });

            _door.Received(1).UnlockDoor();
        }

        [Test]
        public void StationController_SendMessage_when_correct_RfidDetected()
        {
            _usbCharger.SimulateConnected(true);
            _door.DoorCloseEvent += Raise.Event();
            _reader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs { id = 1403 });
            // New RFID wih incorrect id
            _reader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs { id = 1403 });

            _display.Received(1).Display("Fjern telefon");
        }

    }
}
