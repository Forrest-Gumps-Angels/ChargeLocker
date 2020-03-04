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

        public void LockDoor()
        {
            Console.WriteLine("Door is locked");
            OnDoorClosed();
        }
        public void UnlockDoor()
        {
            Console.WriteLine("Door is unlocked");
            OnDoorOpened();
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

