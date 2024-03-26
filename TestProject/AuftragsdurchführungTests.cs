using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class AuftragsdurchführungTests
    {
        private Mock<IMeinService> _mockService;
        private Mock<IOrder> _mockOrder;
        private Mock<IVehicle> _mockVehicle;

        public AuftragsdurchführungTests()
        {
            // Mock-Objekte für den Service und Abhängigkeiten einrichten
            _mockService = new Mock<IMeinService>();
            _mockOrder = new Mock<IOrder>();
            _mockVehicle = new Mock<IVehicle>();

            // Mock-Einstellungen für das IOrder Interface
            _mockOrder.Setup(o => o.StartLocation).Returns("Lager");
            _mockOrder.Setup(o => o.EndLocation).Returns("Hafen");
            _mockOrder.Setup(o => o.ContainerSize).Returns(20);

            // Mock-Einstellungen für das IVehicle Interface
            _mockVehicle.Setup(v => v.LoadCargo(It.IsAny<int>())).Returns(true);
        }

        [Fact]
        public void AuftragErfolgreichDurchgeführt()
        {
            _mockService.Setup(s => s.FühreAuftragAus(_mockOrder.Object)).Returns(true);
            bool ergebnis = _mockService.Object.FühreAuftragAus(_mockOrder.Object);
            Assert.True(ergebnis);
        }

        [Fact]
        public void AuftragNichtDurchgeführtBeiNichtVerfügbarkeit()
        {
            _mockService.Setup(s => s.FühreAuftragAus(_mockOrder.Object)).Returns(false);
            bool ergebnis = _mockService.Object.FühreAuftragAus(_mockOrder.Object);
            Assert.False(ergebnis);
        }

        [Fact]
        public void AuftragNichtDurchgeführtBeiUngültigenDaten()
        {
            _mockService.Setup(s => s.FühreAuftragAus(It.IsAny<IOrder>())).Throws(new InvalidDataException());
            Assert.Throws<InvalidDataException>(() => _mockService.Object.FühreAuftragAus(_mockOrder.Object));
        }
    }
}
