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
        private IChargeControl _uut;
        private StationControl _stCTRL;
        private IChargeControl _charger;
        private IDisplay _display;
        private IDoor _door;
        private IRfidReader _reader;

        [SetUp]
        public void Setup()
        {
            _charger = Substitute.For<IChargeControl>();
            _display = Substitute.For<IDisplay>();
            _door    = Substitute.For<IDoor>();
            _reader  = Substitute.For<IRfidReader>();

            _stCTRL = new StationControl(_charger, _display,_door, _reader);

            _uut = new ChargeControl();
        }

        [Test]
        public void ChargeController_StartCharging_When_RFIDDetected()
        {
            _reader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs { id = 1403 });

            Assert.That(_uut.Connected, Is.True);
        }

        [Test]
        public void ctor_CurentValueIsZero()
        {
            Assert.That(_uut.CurrentValue, Is.Zero);
        }
    }
}
