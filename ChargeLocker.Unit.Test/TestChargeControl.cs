using Ladeskab;
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
    public class TestUsbChargerSimulator
    {
        private UsbChargerSimulator _uut;
        [SetUp]
        public void Setup()
        {
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
