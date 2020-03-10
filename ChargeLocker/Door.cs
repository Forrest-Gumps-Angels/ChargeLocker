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

        public bool DoorLocked { get; set; }

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
            DoorLocked = true;
            Console.WriteLine("Door is locked");
        }

        public void UnlockDoor()
        {
            DoorLocked = false;
            Console.WriteLine("Door is unlocked");
        }

        public virtual void OnDoorOpened()
        {
            DoorOpenEvent?.Invoke(this, EventArgs.Empty);
        }

        public virtual void OnDoorClosed()
        {
            DoorCloseEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}

