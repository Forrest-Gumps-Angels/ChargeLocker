using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargeLocker
{
    class Door : IDoor
    {
        public event EventHandler DoorOpenEvent;
        public event EventHandler DoorCloseEvent;

        public void LockDoor()
        { 

        }
        public void UnlockDoor()
        { 
        }
    }
}

