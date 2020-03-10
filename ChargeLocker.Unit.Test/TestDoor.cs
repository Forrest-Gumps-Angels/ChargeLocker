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
        public void Door_OpenDoor_EventFired()
        {
            var wasCalled = false;
            _uut.DoorOpenEvent += (sender, args) => wasCalled = true;

            _uut.OpenDoor();
            Assert.True(wasCalled);
        }

        [Test]
        public void Door_ClosedDoor_EventFired()
        {
            var wasCalled = false;
            _uut.DoorCloseEvent += (sender, args) => wasCalled = true;

            _uut.CloseDoor();
            Assert.True(wasCalled);
        }

        [Test]
        public void Door_LockedDoor()
        {
            _uut.LockDoor();
            Assert.True(_uut.DoorLocked());
        }

        [Test]
        public void Door_UnlockedDoor()
        {
            _uut.UnlockDoor();
            Assert.False(_uut.DoorLocked());
        }
    }
}
