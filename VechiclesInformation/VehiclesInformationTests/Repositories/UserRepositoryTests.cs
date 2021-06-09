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
    public class UserRepositoryTests
    {
        private MockRepository _mockRepository;
        private Fixture _fixture;
        private IUserRepository _sut;
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
                context.UsersDetail.Add(new AuthenticateModel
                {
                    Username = "akankshasinha",
                    Password = "admin102",
                });
                context.SaveChanges();
            }
        }

        [TestMethod()]
        public void GetAllUsers_ReturnsUsers()
        {
            using (var context = new ApplicationContext(_options))
            {
                // Arrange
                _sut = new UserRepository(context);

                // Act
                var response = _sut.GetUsers();

                // Assert
                Assert.IsNotNull(response);
                Assert.IsInstanceOfType(response, typeof(IEnumerable<AuthenticateModel>));
                Assert.AreEqual(response.Count(), 1);
                Assert.AreEqual("akankshasinha", response.First().Username);
                Assert.AreEqual("admin102", response.First().Password);  
            }
        }

        [TestMethod()]
        public void AddCustomer_InsertsCustomer()
        {
            using (var context = new ApplicationContext(_options))
            {
                // Arrange
                var user = _fixture.Build<AuthenticateModel>()
                                    .Create();

                // Arrange
                _sut = new UserRepository(context);

                // Act
                _sut.AddUser(user);
                var response = _sut.GetUsers();

                // Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(response.Count(), 2);
            }
        }
    
}
}
