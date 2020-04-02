using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargeLocker
{
    public interface IDisplay
    {
        void Display(string message);
        void DisplayStatus(string message);
    }
}
