using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using VechiclesInformation.Models;
using VechiclesInformation.Repositories;

namespace VehiclesInformationTests.Repositories
{
    [TestClass]
    public class VehicleRepositoryTests
    {
        private MockRepository _mockRepository;
        private Fixture _fixture;
        private IVehicleRepository _sut;
        const string connectionString = "Data Source=VehiclesTests.db";
        private DbContextOptions<ApplicationContext> _options;

        [TestInitialize]
        public void TestInitialize()
        {
            _options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseSqlite(connectionString).Options;
            _mockRepository = new MockRepository(MockBehavior.Default);
            _fixture = new Fixture();

            using (var context = new ApplicationContext(_options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.CustomerDetails.Add(new CustomerDetails
                {
                    CustomerId = 101,
                    CustomerName = "First Customer",
                    Address = "First Address",
                    Vehicles = new VehicleDetails[]
                        {
                            new VehicleDetails { VehicleId = "123", RegistrationNumber = "456", VehicleStatus = true },
                            new VehicleDetails { VehicleId = "789", RegistrationNumber = "111", VehicleStatus = false },
                            new VehicleDetails { VehicleId = "222", RegistrationNumber = "777", VehicleStatus = true }
                        }
                });
                context.CustomerDetails.Add(new CustomerDetails
                {
                    CustomerId = 102,
                    CustomerName = "Second Customer",
                    Address = "Second Address",
                    Vehicles = new VehicleDetails[]
                        {
                            new VehicleDetails { VehicleId = "888", RegistrationNumber = "877", VehicleStatus = true },
                            new VehicleDetails { VehicleId = "200", RegistrationNumber = "789", VehicleStatus = true }
                        }
                });
                context.SaveChanges();
            }
        }

        [TestMethod()]
        public void GetAllVehicles_ReturnsVehicles()
        {
            using (var context = new ApplicationContext(_options))
            {
                // Arrange
                _sut = new VehicleRepository(context);

                // Act
                var response = _sut.GetVechiles();

                // Assert
                Assert.IsNotNull(response);
                Assert.IsInstanceOfType(response, typeof(IEnumerable<CustomerDetails>));
                Assert.AreEqual(response.Count(), 2);
                Assert.AreEqual(101, response.First().CustomerId);
                Assert.AreEqual("First Customer", response.First().CustomerName);
                Assert.AreEqual("First Address", response.First().Address);
                Assert.AreEqual("123", response.First().Vehicles.First().VehicleId);
                Assert.AreEqual("456", response.First().Vehicles.First().RegistrationNumber);
                Assert.AreEqual(true, response.First().Vehicles.First().VehicleStatus);
            }
        }

        [TestMethod()]
        public void GetVehiclesByCustomer_ReturnsVehicles()
        {
            using (var context = new ApplicationContext(_options))
            {
                // Arrange
                _sut = new VehicleRepository(context);

                // Act
                var response = _sut.GetVehiclesByCustomer("First Customer");

                // Assert
                Assert.IsNotNull(response);
                Assert.IsInstanceOfType(response, typeof(CustomerDetails));
                Assert.AreEqual(response.Vehicles.Count(), 3);
                Assert.AreEqual(101, response.CustomerId);
                Assert.AreEqual("First Customer", response.CustomerName);
                Assert.AreEqual("First Address", response.Address);
                Assert.AreEqual("123", response.Vehicles.First().VehicleId);
                Assert.AreEqual("456", response.Vehicles.First().RegistrationNumber);
                Assert.AreEqual(true, response.Vehicles.First().VehicleStatus);
            }
        }

        [TestMethod()]
        public void GetVehiclesByStatus_ReturnsVehicles()
        {
            using (var context = new ApplicationContext(_options))
            {
                // Arrange
                _sut = new VehicleRepository(context);

                // Act
                var response = _sut.GetVehiclesByStatus(false);

                // Assert
                Assert.IsNotNull(response);
                Assert.IsInstanceOfType(response, typeof(IEnumerable<CustomerDetails>));
                Assert.AreEqual(response.Count(), 1);
                Assert.AreEqual(101, response.First().CustomerId);
                Assert.AreEqual("First Customer", response.First().CustomerName);
                Assert.AreEqual("First Address", response.First().Address);
                Assert.AreEqual(response.First().Vehicles.Count(), 1);
                Assert.AreEqual("789", response.First().Vehicles.First().VehicleId);
                Assert.AreEqual("111", response.First().Vehicles.First().RegistrationNumber);
                Assert.AreEqual(false, response.First().Vehicles.First().VehicleStatus);
            }
        }
    }
}
