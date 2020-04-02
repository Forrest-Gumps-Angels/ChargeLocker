using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab;
using UsbSimulator;
using ChargeLocker;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Assemble your system here from all the classes
            Door door = new Door();
            RfidReader rfidReader = new RfidReader();
            ChargeControl charger = new ChargeControl(new UsbChargerSimulator(), new DisplayConsole());
            DisplayConsole display = new DisplayConsole();

            StationControl statCtrl = new StationControl(charger, display, door, rfidReader);


            bool finish = false;
            do
            {
                string input;
                System.Console.WriteLine("Indtast E, O, C, R: ");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

                switch (input[0])
                {
                    case 'E':
                        finish = true;
                        break;

                    case 'O':
                        door.OnDoorOpened();
                        break;

                    case 'C':
                        door.OnDoorClosed();
                        break;

                    case 'R':
                        System.Console.WriteLine("Indtast RFID id: ");
                        string idString = System.Console.ReadLine();

                        int id = Convert.ToInt32(idString);
                        rfidReader.OnRfidRead(id);
                        break;

                    default:
                        break;
                }

            } while (!finish);
        }
    }
}
