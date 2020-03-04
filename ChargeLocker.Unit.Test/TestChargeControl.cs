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
            _usbCharger = new UsbChargerSimulator();
            _uut = Substitute.For<IChargeControl>(_usbCharger);
            _display = Substitute.For<IDisplay>();
            _door    = Substitute.For<IDoor>();
            _reader  = Substitute.For<IRfidReader>();


            _stCTRL = new StationControl(_uut, _display,_door, _reader);

        }

        [Test]
        public void ChargeController_StartCharging_When_RFIDDetected()
        {
            _reader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs { id = 1403 });

            _uut.Received().StartCharge();
        }

        [Test]
        public void ctor_CurentValueIsZero()
        {
            //Assert.That(_uut.CurrentValue, Is.Zero);
        }
    }
}
