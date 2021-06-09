using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using VechiclesInformation.Controllers;
using VechiclesInformation.Integrations;
using VechiclesInformation.Models;
using VechiclesInformation.Repositories;

namespace VehiclesInformationTests.Controller
{
    [TestClass]
    public class VehicleControllerTests
    {
        private MockRepository _mockRepository;
        private Fixture _fixture;
        private VehicleController _sut;
        private Mock<IVehicleRepository> _mockVehicleRepository;
        private Mock<ISyncService> _mockSyncService;


        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new MockRepository(MockBehavior.Default);
            _mockVehicleRepository = _mockRepository.Create<IVehicleRepository>();
            _mockSyncService = _mockRepository.Create<ISyncService>();
            _sut = new VehicleController(_mockVehicleRepository.Object, _mockSyncService.Object);
            _fixture = new Fixture();
        }

        [TestMethod()]
        public void GetVehicles_ReturnsVehicles()
        {
            // Arrange
            var vehicleResponse = _fixture.Build<List<CustomerDetails>>()
                                .Create();
            _mockVehicleRepository.Setup(x => x.GetVechiles())
                .Returns(() => vehicleResponse);

            // Act
            var response = _sut.GetVehicles();

            // Assert
            Assert.IsInstanceOfType(response, typeof(List<CustomerDetails>));
            Assert.IsNotNull(response);
        }

        //[TestMethod()]
        //public void AddCustomer_InsertsCustomer()
        //{
        //    // Arrange
        //    _mockVehicleRepository.Setup(y => y.AddCustomer(It.IsAny<CustomerDetails>()));

        //    // Act
        //    _sut.AddCustomer(It.IsAny<CustomerDetails>());

        //    // Assert
        //    _mockVehicleRepository.Verify(m => m.AddCustomer(It.IsAny<CustomerDetails>()));
        //}

        [TestMethod()]
        public void GetVehiclesByCustomer_ReturnsVehicles()
        {
            // Arrange
            var vehicleResponse = _fixture.Build<CustomerDetails>()
                                .Create();
            _mockVehicleRepository.Setup(x => x.GetVehiclesByCustomer(It.IsAny<string>()))
                .Returns(() => vehicleResponse);

            // Act
            var response = _sut.GetVehiclesForCustomer(It.IsAny<string>());

            // Assert
            Assert.IsInstanceOfType(response, typeof(CustomerDetails));
            Assert.IsNotNull(response);
        }

        [TestMethod()]
        public void GetVehiclesByStatus_ReturnsVehicles()
        {
            // Arrange
            var vehicleResponse = _fixture.Build<List<CustomerDetails>>()
                                .Create();
            _mockVehicleRepository.Setup(x => x.GetVehiclesByStatus(It.IsAny<bool>()))
                .Returns(() => vehicleResponse);

            // Act
            var response = _sut.GetVehiclesByStatus(It.IsAny<bool>());

            // Assert
            Assert.IsInstanceOfType(response, typeof(List<CustomerDetails>));
            Assert.IsNotNull(response);
        }
    }
}
