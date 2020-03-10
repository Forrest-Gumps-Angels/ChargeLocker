using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargeLocker
{
    public interface IDoor
    {
        event EventHandler DoorOpenEvent;
        event EventHandler DoorCloseEvent;

        bool DoorLocked { get; set; }
        void LockDoor();
        void UnlockDoor();
        void OpenDoor();
        void CloseDoor();
    }
}
