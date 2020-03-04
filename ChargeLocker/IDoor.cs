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

        void LockDoor();
        void UnlockDoor();
    }
}
