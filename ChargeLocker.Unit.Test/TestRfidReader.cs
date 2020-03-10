using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;

namespace ChargeLocker.Unit.Test
{
    [TestFixture]
    public class TestRfidReader
    {
        private IRfidReader _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new RfidReader();
        }

        [Test]
        public void RfidReader_OnRfidRead_EventFired()
        {
            int Argument = 0;
            var wasCalled = false;
            _uut.RfidDetectedEvent += ((sender, e) => { wasCalled = true;
                                                         Argument = e.id; });
            _uut.RfidRead(200);
            Assert.True(wasCalled);
            Assert.That(Argument, Is.EqualTo(200));
        }
    }
}