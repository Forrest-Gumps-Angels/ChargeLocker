using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargeLocker
{
    public class Door : IDoor
    {
        public event EventHandler DoorOpenEvent;
        public event EventHandler DoorCloseEvent;

        public Door() 
        { }

        public void CloseDoor()
        {
            Console.WriteLine("Door is closed");
            OnDoorClosed();
        }
        public void OpenDoor()
        {
            Console.WriteLine("Door is open");
            OnDoorOpened();
        }

        public void LockDoor()
        {
            Console.WriteLine("LockDoor was called");
        }

        public void UnlockDoor()
        {
            Console.WriteLine("UnlockDoor was called");
        }

        protected virtual void OnDoorOpened()
        {
            DoorOpenEvent?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnDoorClosed()
        {
            DoorCloseEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}

