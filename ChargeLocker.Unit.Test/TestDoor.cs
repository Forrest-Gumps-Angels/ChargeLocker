using NSubstitute;
using NUnit.Framework;
using System;


namespace ChargeLocker.Unit.Test
{
    [TestFixture]
    public class TestDoor
    {
        private IDoor _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new Door();
        }

        [Test]
        public void Door_UnlockDoor_EventFired()
        {
            var wasCalled = false;
            _uut.DoorOpenEvent += (sender, args) => wasCalled = true;

            _uut.DoorOpenEvent += Raise.Event();
            Assert.True(wasCalled);
        }

        [Test]
        public void Door_LockDoor_EventFired()
        {
            var wasCalled = false;
            _uut.DoorCloseEvent += (sender, args) => wasCalled = true;

            _uut.DoorCloseEvent += Raise.Event();
            Assert.True(wasCalled);
        }
    }
}
