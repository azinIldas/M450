using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingProjekt
{
    public class MeinService : IMeinService
    {
        private IVehicle _vehicle;

        // Der Konstruktor nimmt eine IVehicle-Implementierung entgegen.
        // Diese Abhängigkeit könnte auch durch Dependency Injection eingefügt werden.
        public MeinService(IVehicle vehicle)
        {
            _vehicle = vehicle;
        }

        public bool FühreAuftragAus(IOrder order)
        {
            // Hier würden Sie Ihre Logik zur Auftragsausführung implementieren.
            // Zum Beispiel könnte das Fahrzeug gestartet, die Ladung geladen und
            // dann zu einem bestimmten Ort gefahren werden, bevor es anhält.

            try
            {
                _vehicle.Start();
                bool loaded = _vehicle.LoadCargo(order.ContainerSize);
                if (!loaded)
                {
                    return false;
                }
                // Hier könnte weitere Logik stehen, z.B. Fahrzeug zu Zielort fahren.
                return true;
            }
            finally
            {
                _vehicle.Stop();
            }
        }
    }
}
