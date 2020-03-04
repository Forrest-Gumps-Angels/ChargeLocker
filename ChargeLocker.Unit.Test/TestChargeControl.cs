using Ladeskab;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsbSimulator;
using ChargeLocker;

namespace ChargeLocker.Unit.Test
{
    [TestFixture]
    public class TestUsbChargerSimulator
    {
        private UsbChargerSimulator _uut;
        [SetUp]
        public void Setup()
        {
            var _charger = new IChargeControl;
            var _display = new IDisplay;
            var _door = new IDoor;
            var _reader = new IRfidReader;

            _uut = new StationControl();
        }

        [Test]
        public void ctor_IsConnected()
        {
            Assert.That(_uut.Connected, Is.True);
        }

        [Test]
        public void ctor_CurentValueIsZero()
        {
            Assert.That(_uut.CurrentValue, Is.Zero);
        }
    }
}
