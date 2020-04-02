using ChargeLocker;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsbSimulator;

namespace Ladeskab
{
    public class StationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        private enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        // Her mangler flere member variable

        private LadeskabState _state;
        private IChargeControl _charger;
        private IDisplay _display;
        private IDoor _door;
        private IRfidReader _reader;


        private int _oldId;

        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        // We use constructor injection for all dependencies
        public StationControl(IChargeControl charger, IDisplay display, IDoor door, IRfidReader reader)
        {
            _charger = charger;
            _display = display;
            _door = door;
            _reader = reader;

            _reader.RfidDetectedEvent += RfidDetected;
            _door.DoorCloseEvent += DoorClosed;
            _door.DoorOpenEvent += DoorOpened;
        }

        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        private void RfidDetected(object sender, RfidDetectedEventArgs eventArgs)
        {
            int id = eventArgs.id;

            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_charger.IsConnected())
                    {
                        _door.LockDoor();
                        _charger.StartCharge();
                        _oldId = id;
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
                        }

                        _display.Display("Ladeskab optaget");
                        Console.WriteLine("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        _display.Display("Tilslutningsfejl");
                        Console.WriteLine("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
                    }

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (id == _oldId)
                    {
                        _charger.StopCharge();
                        _door.UnlockDoor();
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", id);
                        }
                        _display.Display("Fjern telefon");
                        Console.WriteLine("Tag din telefon ud af skabet og luk døren");
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        _display.Display("RFID fejl");
                        Console.WriteLine("Forkert RFID tag");
                    }

                    break;
            }
        }

        private void DoorOpened(object sender, EventArgs e)
        {
            _state = LadeskabState.DoorOpen;
            _display.Display("Tilslut telefon");
        }

        private void DoorClosed(object sender, EventArgs e)
        {
            _state = LadeskabState.Available;
            _display.Display("Indlæs RFID");
        }
    }
}
