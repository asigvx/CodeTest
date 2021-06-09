using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using VechiclesInformation.Controllers;
using VechiclesInformation.Models;
using VechiclesInformation.Repositories;

namespace VehiclesInformationTests.Controller
{
    [TestClass]
    public class CustomerControllerTests
    {
        private MockRepository _mockRepository;
        private Fixture _fixture;
        private CustomerController _sut;
        private Mock<ICustomerRepository> _mockCustomerRepository;


        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new MockRepository(MockBehavior.Default);
            _mockCustomerRepository= _mockRepository.Create<ICustomerRepository>();
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

        [TestMethod()]
        public void AddCustomer_InsertsCustomer()
        {
            // Arrange
            _mockCustomerRepository.Setup(y => y.AddCustomer(It.IsAny<CustomerDetails>()));

            // Act
             _sut.AddCustomer(It.IsAny<CustomerDetails>());

            // Assert
            _mockCustomerRepository.Verify(m => m.AddCustomer(It.IsAny<CustomerDetails>()));
        }
    }

}


