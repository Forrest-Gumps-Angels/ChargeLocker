using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargeLocker
{
    interface IDoor
    {
        event EventHandler LockDoor;
        event EventHandler UnlockDoor;
    }
}
