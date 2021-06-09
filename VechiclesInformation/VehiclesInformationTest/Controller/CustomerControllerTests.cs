using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VechiclesInformation.Controllers;
using VechiclesInformation.Models;
using VechiclesInformation.Repositories;

namespace VehiclesInformationTest.Controller
{
    [TestClass]
    public class CustomerControllerTests
    {
        private MockRepository _mockRepository;
        private Fixture _fixture;
        private CustomerController _sut;
        const string connectionString = "Data Source=VehiclesTests.db";
        private DbContextOptions<ApplicationContext> _options;
        private Mock<ICustomerRepository> _mockCustomerRepository;


        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new MockRepository(MockBehavior.Default); 
            _sut = new CustomerController(_mockCustomerRepository.Object);
            _fixture = new Fixture();
        }

        [TestMethod()]
        public void GetCustomers_ReturnsCustomers()
        {
            // Arrange
            var customerResponse = _fixture.Build<List<CustomerDetails>>()
                                .Create();
            _mockCustomerRepository.Setup(x => x.GetCustomers())
                .Returns(() => customerResponse);

            // Act
            var response = _sut.GetCustomers();

            // Assert
            Assert.IsInstanceOfType(response, typeof(List<CustomerDetails>));
            Assert.IsNotNull(response);
        }
    }
}
