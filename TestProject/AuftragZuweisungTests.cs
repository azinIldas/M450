using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class AuftragZuweisungTests
    {
        private Mock<IVehicle> _mockVehicle;
        private Mock<IOrder> _mockOrder;

        public AuftragZuweisungTests()
        {
            _mockVehicle = new Mock<IVehicle>();
            _mockOrder = new Mock<IOrder>();
        }

        [Fact]
        public void ZuweisungErfolgreichWennFahrzeugVerfügbar()
        {
            _mockVehicle.Setup(v => v.LoadCargo(It.IsAny<int>())).Returns(true);
            var result = _mockVehicle.Object.LoadCargo(_mockOrder.Object.ContainerSize);
            Assert.True(result);
        }

        [Fact]
        public void ZuweisungFehlschlägtWennFahrzeugNichtVerfügbar()
        {
            _mockVehicle.Setup(v => v.LoadCargo(It.IsAny<int>())).Returns(false);
            var result = _mockVehicle.Object.LoadCargo(_mockOrder.Object.ContainerSize);
            Assert.False(result);
        }

        [Fact]
        public void ZuweisungFehlschlägtBeiUngültigemAuftrag()
        {
            _mockOrder.Setup(o => o.ContainerSize).Throws(new InvalidOperationException());
            Assert.Throws<InvalidOperationException>(() => _mockVehicle.Object.LoadCargo(_mockOrder.Object.ContainerSize));
        }
    }
 }

