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
    public class UserManagementTests
    {
        private MockRepository _mockRepository;
        private Fixture _fixture;
        private UserManagementController _sut;
        private Mock<IUserRepository> _mockUserRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new MockRepository(MockBehavior.Default);
            _mockUserRepository = _mockRepository.Create<IUserRepository>();
            _sut = new UserManagementController(_mockUserRepository.Object);
            _fixture = new Fixture();
        }

        [TestMethod()]
        public void GetUser_ReturnsUser()
        {
            // Arrange
            var userResponse = _fixture.Build<List<AuthenticateModel>>()
                                .Create();
            _mockUserRepository.Setup(x => x.GetUsers())
                .Returns(() => userResponse);

            // Act
            var response = _sut.Get();

            // Assert
            Assert.IsInstanceOfType(response, typeof(List<AuthenticateModel>));
            Assert.IsNotNull(response);
        }

        [TestMethod()]
        public void AddUser_InsertsUser()
        {
            // Arrange
            _mockUserRepository.Setup(y => y.AddUser(It.IsAny<AuthenticateModel>()));

            // Act
            _sut.Post(It.IsAny<AuthenticateModel>());

            // Assert
            _mockUserRepository.Verify(m => m.AddUser(It.IsAny<AuthenticateModel>()));
        }
    }
}
