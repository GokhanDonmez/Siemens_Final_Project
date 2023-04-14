using Arduino.API.Controllers;
using Arduino.Business.Abstract;
using Arduino.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Arduino.API.Test
{
    #region Mocking




    //[TestFixture]
    //public class DatasControllerTests
    //{
    //    private IArduinoService _arduinoService;
    //    private DatasController _datasController;
    //    private Mock mockService;
    //    [SetUp]
    //    public void SetUp()
    //    {
    //        // _arduinoService = new ArduinoService(); //TODO: ArduinoService interface'i nedir?
    //        //_datasController = new DatasController(_arduinoService);
    //         mockService = new Mock<IArduinoService>();
    //    }
    //    [Test]
    //    public async Task GetAllDataTest()
    //    {
    //        // Arrange

    //        mockService.Setup(service => service.GetDataList()).ReturnsAsync(new List<Data>());

    //        var controller = new DatasController(mockService.Object);

    //        // Act
    //        var result = await controller.Get();

    //        // Assert
    //        Assert.IsInstanceOf<OkObjectResult>(result);
    //        var okResult = (OkObjectResult)result;
    //        var datas = (List<Data>)okResult.Value;
    //        Assert.AreEqual(0, datas.Count);
    //    }

    //    [Test]
    //    public async Task GetDataByIdTest()
    //    {
    //        // Arrange
    //        var mockService = new Mock<IArduinoService>();
    //        mockService.Setup(service => service
    //            .GetDataById(1))
    //            .ReturnsAsync(new Data { Id = 1,
    //                                     Fuel = "Test",
    //                                     Lat = "Test",
    //                                     Lon = "Test"
    //            });

    //        var controller = new DatasController(mockService.Object);

    //        // Act
    //        var result = await controller.GetDataById(1);

    //        // Assert
    //        Assert.IsInstanceOf<OkObjectResult>(result);
    //        var okResult = (OkObjectResult)result;
    //        var data = (Data)okResult.Value;
    //        Assert.AreEqual(1, data.Id);
    //        Assert.AreEqual("Test", data.Fuel);
    //        Assert.AreEqual("Test", data.Lat);
    //        Assert.AreEqual("Test", data.Lon);
    //    }

    //}
    #endregion

    #region Mocking_V2

    [TestFixture]
    public class DatasControllerTests
    {
        private Mock<IArduinoService> _arduinoServiceMock;
        private DatasController _datasController;

        [SetUp]
        public void SetUp()
        {
            _arduinoServiceMock = new Mock<IArduinoService>();
            _datasController = new DatasController(_arduinoServiceMock.Object);
        }

        [Test]
        public async Task Get_ReturnsAllDatas()
        {
            //Arrange
            var expectedDatas = new List<Data>
            {
                new Data { Id = 1, Fuel = "Fuel1", Lat = "Lat1",Lon = "Lon1"},
                new Data { Id = 2, Fuel = "Fuel2", Lat = "Lat2",Lon = "Lon2"},
            };

            _arduinoServiceMock.Setup(i => i.GetDataList()).ReturnsAsync(expectedDatas);

            //Act
            var result = await _datasController.Get();
            var okResult = result as OkObjectResult;
            var datas = okResult.Value as List<Data>;

            //Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(expectedDatas.Count, datas.Count);
            for (int i = 0; i < expectedDatas.Count; i++)
            {
                Assert.AreEqual(expectedDatas[i].Id, datas[i].Id);
                Assert.AreEqual(expectedDatas[i].Fuel, datas[i].Fuel);
                Assert.AreEqual(expectedDatas[i].Lat, datas[i].Lat);
                Assert.AreEqual(expectedDatas[i].Lon, datas[i].Lon);
            }
        }

        [Test]
        public async Task GetDataById_WithValidId_ReturnsData()
        {
            //Arrange
            int id = 1;
            var expectedData = new Data { Id = 1, Fuel = "Fuel1", Lat = "Lat1", Lon = "Lon1" };
            _arduinoServiceMock.Setup(i => i.GetDataById(id)).ReturnsAsync(expectedData);
            //Act
            var result = await _datasController.GetDataById(id);
            var okResult = result as OkObjectResult;
            var data = okResult.Value as Data;

            //Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(expectedData.Id, data.Id);
            Assert.AreEqual(expectedData.Fuel, data.Fuel);
            Assert.AreEqual(expectedData.Lat, data.Lat);
            Assert.AreEqual(expectedData.Lon, data.Lon);
        }

        [Test]
        public async Task GetDataById_WithInvalidId_ReturnsNotFound()
        {
            //Arrange
            int id = 2;

            //Act
            var result = await _datasController.GetDataById(id);
            var notFoundResult = result as NotFoundObjectResult;

            //Assert
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        

        [Test]
        public async Task CreateData_ShouldReturnCreatedData()
        {
            // Arrange
            
            var createdData = new Data {  Fuel = "Fuel1", Lat = "Lat1", Lon = "Lon1" };

            _arduinoServiceMock.Setup(s => s.CreateData(It.IsAny<Data>())).ReturnsAsync(createdData);

            // Act
            var result = await _datasController.CreateData(createdData) as CreatedAtActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status201Created, result.StatusCode);
            Assert.AreEqual("Get", result.ActionName);
            Assert.AreEqual(createdData.Id, result.RouteValues["id"]);

            var returnedVeri = result.Value as Data;
            Assert.IsNotNull(returnedVeri);
            
            Assert.AreEqual(createdData.Fuel, returnedVeri.Fuel);
            Assert.AreEqual(createdData.Lat, returnedVeri.Lat);
            Assert.AreEqual(createdData.Lon, returnedVeri.Lon);
            
        }
    }

    

    #endregion
}