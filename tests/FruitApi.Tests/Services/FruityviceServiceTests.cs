using FruitApi.Bussiness;
using FruitApi.Bussiness.Services;
using FruitApi.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FruitApi.Tests.Services
{
    [TestClass]
    public class FruityviceServiceTests
    {
        private MockRepository mockRepository;

        private Mock<ILogger<FruityviceService>> mockLogger;
        private Mock<IApiService> mockApiService;
        private Mock<IOptions<FruitApiSettings>> mockOptions;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Default);

            this.mockLogger = this.mockRepository.Create<ILogger<FruityviceService>>();
            this.mockApiService = this.mockRepository.Create<IApiService>();
            this.mockOptions = this.mockRepository.Create<IOptions<FruitApiSettings>>();

        }

        private IEnumerable<Fruits> GetDefaultFruits ()
        {
            return new List<Fruits>() { new Fruits() { Id = 1 } }.AsEnumerable();
        }
        private FruityviceService CreateService()
        {
            return new FruityviceService(
                this.mockLogger.Object,
                this.mockApiService.Object,
                this.mockOptions.Object);
        }

        [TestMethod]
        public async Task GetAllFruits_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);
            this.mockOptions.SetupGet(a => a.Value).Returns(new FruitApiSettings() { Baseurl = "test" });
            this.mockApiService.Setup(a => a.Configure(It.Is<string>(a => a == "test"))).Returns(this.mockApiService.Object);
            this.mockApiService.Setup(a => a.GetData<IEnumerable<Fruits>>(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task
                .FromResult(this.GetDefaultFruits()));
            // Act
            var result = await service.GetAllFruits(
                cancellationToken);

            // Assert
            Assert.AreEqual(result.Count(),1);
            Assert.AreEqual(result.FirstOrDefault().Id ,1);
            this.mockRepository.VerifyAll();
        }

        [TestMethod]
        public async Task RetriveAllFruitsForFamnily_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            string familyName = "Test";
            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);
            this.mockOptions.SetupGet(a => a.Value).Returns( new FruitApiSettings() { Baseurl = "test" });
            this.mockApiService.Setup(a => a.Configure(It.Is<string>(a => a == "test"))).Returns(this.mockApiService.Object);
            this.mockApiService.Setup(a => a.GetData<IEnumerable<Fruits>>(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task
                .FromResult(this.GetDefaultFruits()));
            // Act
            var result = await service.RetriveAllFruitsForFamnily(
                familyName,
                cancellationToken);

            // Assert
            Assert.AreEqual(result.Count(), 1);
            Assert.AreEqual(result.FirstOrDefault().Id, 1);
            this.mockRepository.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public async Task RetriveAllFruitsForFamnily_FamilyEmpty_Excception()
        {
            // Arrange
            var service = this.CreateService();
            string familyName = string.Empty;
            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);
            this.mockOptions.SetupGet(a => a.Value).Returns(new FruitApiSettings() { Baseurl = "test" });
            this.mockApiService.Setup(a=>a.Configure(It.Is<string>(a=>a == "test"))).Verifiable();
            
            // Act
            var result = await service.RetriveAllFruitsForFamnily(
                familyName,
                cancellationToken);

            // Assert
            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.FirstOrDefault().Id == 1);
            this.mockRepository.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public async Task RetriveAllFruitsForFamnily_FamilyNull_Excception()
        {
            // Arrange
            var service = this.CreateService();
            string familyName = null;
            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);
            this.mockOptions.SetupGet(a => a.Value).Returns(new FruitApiSettings() { Baseurl = "test" });
            this.mockApiService.Setup(a => a.Configure(It.Is<string>(a => a == "test"))).Verifiable();
            
            // Act
            var result = await service.RetriveAllFruitsForFamnily(
                familyName,
                cancellationToken);

            // Assert
            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.FirstOrDefault().Id == 1);
            this.mockRepository.VerifyAll();
        }
    }
}
