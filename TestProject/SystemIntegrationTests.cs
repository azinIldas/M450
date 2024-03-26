using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingProjekt;


namespace TestProject
{
    public class SystemIntegrationTests
    {
        private readonly Mock<IVehicle> _mockVehicle;
        private readonly MeinService _meinService;  
        private readonly Mock<IOrder> _mockOrder;   

        public SystemIntegrationTests()
        {
            // Mock-Objekte erstellen
            _mockVehicle = new Mock<IVehicle>();
            _mockOrder = new Mock<IOrder>();

            // IOrder Mock konfigurieren
            _mockOrder.Setup(o => o.StartLocation).Returns("Start");
            _mockOrder.Setup(o => o.EndLocation).Returns("Ziel");
            _mockOrder.Setup(o => o.ContainerSize).Returns(20);

            // MeinService mit den Mocks initialisieren
            _meinService = new MeinService(_mockVehicle.Object);
        }

        [Fact]
        public void ServiceVerarbeitetAuftrag()
        {
            // Aktion: Der Service versucht, den Auftrag zu verarbeiten
            _meinService.FühreAuftragAus(_mockOrder.Object);

            // Assertions: Überprüfen, ob die erwarteten Methoden aufgerufen wurden
            _mockVehicle.Verify(v => v.LoadCargo(It.IsAny<int>()), Times.Once());
            // Hier würden Sie weitere Assertions hinzufügen, um das Verhalten zu verifizieren
        }

        // Weitere Integrationstests können hier hinzugefügt werden...
    }
}