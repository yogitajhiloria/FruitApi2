using FruitApi.Bussiness;
using FruitApi.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace FruitApi.Tests.Services
{
    [TestClass]
    public class BaseApiServiceTests
    {
        private MockRepository mockRepository;

        private Mock<ILogger<BaseApiService>> mockLogger;
        private Mock<IHttpClientFactory> mockHttpClientFactory;
        private Mock<HttpClient> mockHttpClient;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Default);

            this.mockLogger = this.mockRepository.Create<ILogger<BaseApiService>>();
            this.mockHttpClientFactory = this.mockRepository.Create<IHttpClientFactory>();
            this.mockHttpClient = this.mockRepository.Create<HttpClient>();
        }

        private BaseApiService CreateService()
        {
            return new BaseApiService(
                this.mockLogger.Object,
                this.mockHttpClientFactory.Object);
        }

        [TestMethod]
        public void AddAdditionalHeaders_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            Dictionary<string,string> headers = new Dictionary<string, string>() { { "Test","value1" } };

            // Act
            var result = service.AddAdditionalHeaders(
                headers);

            // Assert
            Assert.AreEqual(result, service);
            this.mockRepository.VerifyAll();
        }
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void AddAdditionalHeaders_HeaderNull_ExpectedException()
        {
            // Arrange
            var service = this.CreateService();
            Dictionary<string, string> headers = null;

            // Act
            var result = service.AddAdditionalHeaders(
                headers);

            // Assert
            
            this.mockRepository.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void AddAdditionalHeaders_HeaderEmpty_ExpectedException()
        {
            // Arrange
            var service = this.CreateService();
            Dictionary<string, string> headers = new Dictionary<string, string>();

            // Act
            var result = service.AddAdditionalHeaders(
                headers);

            // Assert

            this.mockRepository.VerifyAll();
        }

        [TestMethod]
        public void Configure_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            string baseurl = "http://www.test.com";

            // Act
            var result = service.Configure(
                baseurl);

            // Assert
            Assert.AreEqual(result, service);
            this.mockRepository.VerifyAll();
        }
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Configure_UrlNull_ExpectedException()
        {
            // Arrange
            var service = this.CreateService();
            string baseurl = null;

            // Act
            var result = service.Configure(
                baseurl);

            // Assert
           
            this.mockRepository.VerifyAll();
        }
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Configure_UrlEmpty_ExpectedException()
        {
            // Arrange
            var service = this.CreateService();
            string baseurl = "";

            // Act
            var result = service.Configure(
                baseurl);

            // Assert
            
            this.mockRepository.VerifyAll();
        }

        [TestMethod]
        public async Task GetData_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            string subUrl = "user/test";
            var handlerMock = new Mock<HttpMessageHandler>();
            var httpClient = new HttpClient(handlerMock.Object);
            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);
            this.mockHttpClientFactory.Setup(a => a.CreateClient(Options.DefaultName)).Returns(httpClient);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"id\":1}")
                })
                .Verifiable();
            // Act
            service.Configure("http://localhost/api");
            var result = await service.GetData<Fruits>(
                subUrl,
                cancellationToken);

            // Assert
            Assert.AreEqual(result.Id, 1);
            this.mockRepository.VerifyAll();
        }

        [TestMethod]
        public async Task PostData_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            Fruits response = new Fruits() { Id = 1 };
            FruitFilter request = new FruitFilter { FruitFamily="test"};
            string subUrl = "user/test";
            var handlerMock = new Mock<HttpMessageHandler>();
            var httpClient = new HttpClient(handlerMock.Object);
            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);
            this.mockHttpClientFactory.Setup(a => a.CreateClient(Options.DefaultName)).Returns(httpClient);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"id\":1}")
                })
                .Verifiable();

            // Act
            service.Configure("http://www.test.com");
            var result = await service.PostData<FruitFilter,Fruits>(
                request,
                subUrl,
                cancellationToken);

            // Assert
            Assert.AreEqual(result.Id, 1);
            this.mockRepository.VerifyAll();
        }
    }
}
