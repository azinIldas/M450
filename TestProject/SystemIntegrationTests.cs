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

        [Fact]
        public void ServiceVerarbeitetAuftragNichtWennLadungNichtGeladenWerdenKann()
        {
            // Fahrzeug-Setup so konfigurieren, dass es false zurückgibt, was bedeutet,
            // dass das Fahrzeug die Ladung nicht laden kann
            _mockVehicle.Setup(v => v.LoadCargo(It.IsAny<int>())).Returns(false);

            // Aktion: Der Service versucht, den Auftrag zu verarbeiten
            var result = _meinService.FühreAuftragAus(_mockOrder.Object);

            // Assertion: Überprüfen, ob die Methode false zurückgibt
            Assert.False(result);

            // Überprüfen, ob die erwarteten Methoden aufgerufen wurden
            _mockVehicle.Verify(v => v.LoadCargo(It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public void ServiceWirftAusnahmeBeiUngültigemAuftrag()
        {
            // IOrder Mock so konfigurieren, dass eine Ausnahme geworfen wird, wenn versucht wird,
            // die ContainerSize-Eigenschaft zu erhalten
            _mockOrder.Setup(o => o.ContainerSize).Throws(new InvalidOperationException("Ungültiger Auftrag"));

            // Assertion: Überprüfen, ob eine Ausnahme geworfen wird, wenn die Methode aufgerufen wird
            Assert.Throws<InvalidOperationException>(() => _meinService.FühreAuftragAus(_mockOrder.Object));

            // Es wird nicht erwartet, dass LoadCargo aufgerufen wird, da eine Ausnahme geworfen wird
            _mockVehicle.Verify(v => v.LoadCargo(It.IsAny<int>()), Times.Never());
        }
    }
}