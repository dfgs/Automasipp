using Automasipp.backend.Controllers;
using Automasipp.backend.DataSources;
using Automasipp.Backend.Controllers;
using Automasipp.Models;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using Moq;
using ResultTypeLib;

namespace Automasipp.Backend.UnitTests.Controllers
{
    [TestClass]
    public class ReportDataSourceUnitTest
    {

        #region GetReports using scenario name

        [TestMethod]
        public void GetReportsUsingScenarionNameShouldReturnCode404IfDirectoryIsNotFound()
        {
            ILogger<ReportController> logger;
            IReportDataSource dataSource;
            ReportController controller;
            ActionResult<Report[]> result;
            NotFoundObjectResult? koResult;

            logger = Mock.Of<ILogger<ReportController>>();
            Mock.Get(logger).Setup(m => m.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>())).Verifiable(Times.Once());

            dataSource = Mock.Of<IReportDataSource>();
            Mock.Get(dataSource).Setup(m => m.GetReports(It.IsAny<string>(), It.IsAny<int>())).Returns(Result.Fail<Report[]>(new DirectoryNotFoundException("error content")));

            controller = new ReportController(logger, dataSource);

            result = controller.GetReports("anyname",1234);
            koResult = result.Result as NotFoundObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(koResult);
            Assert.AreEqual("Reports folder was not found", koResult.Value);
            Mock.VerifyAll();
        }
        

        [TestMethod]
        public void GetReportsUsingScenarionNameShouldReturnCode400IfScenarioNameIsNotProvided()
        {
            ILogger<ReportController> logger;
            IReportDataSource dataSource;
            ReportController controller;
            ActionResult<Report[]> result;
            BadRequestObjectResult? koResult;

            logger = Mock.Of<ILogger<ReportController>>();
            Mock.Get(logger).Setup(m => m.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>())).Verifiable(Times.Once());

            dataSource = Mock.Of<IReportDataSource>();
            Mock.Get(dataSource).Setup(m => m.GetReports(It.IsAny<string>(), It.IsAny<int>())).Returns(Result.Fail<Report[]>(new FileNotFoundException("error content")));

            controller = new ReportController(logger, dataSource);

#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
            result = controller.GetReports(null,1234);
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
            koResult = result.Result as BadRequestObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(koResult);
            Assert.AreEqual("Scenario name must be provided", koResult.Value);
            Mock.VerifyAll();
        }

        [TestMethod]
        public void GetReportsUsingScenarionNameShouldReturnCode500IfDataSourceFails()
        {
            ILogger<ReportController> logger;
            IReportDataSource dataSource;
            ReportController controller;
            ActionResult<Report[]> result;
            InternalServerError? koResult;

            logger = Mock.Of<ILogger<ReportController>>();
            Mock.Get(logger).Setup(m => m.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>())).Verifiable(Times.Once());

            dataSource = Mock.Of<IReportDataSource>();
            Mock.Get(dataSource).Setup(m => m.GetReports(It.IsAny<string>(), It.IsAny<int>())).Returns(Result.Fail<Report[]>(new InvalidOperationException("error content")));

            controller = new ReportController(logger, dataSource);

            result = controller.GetReports("anyname", 1234);
            koResult = result.Result as InternalServerError;

            Assert.IsNotNull(result);
            Assert.IsNotNull(koResult);
            Mock.VerifyAll();
        }


        [TestMethod]
        public void GetReportsUsingScenarionNameShouldReturnCode200IfFilesExist()
        {
            ILogger<ReportController> logger;
            IReportDataSource dataSource;
            ReportController controller;
            ActionResult<Report[]> result;
            OkObjectResult? okResult;
            Report[]? value;

            logger = Mock.Of<ILogger<ReportController>>();
            dataSource = Mock.Of<IReportDataSource>();
            Mock.Get(dataSource).Setup(m => m.GetReports(It.IsAny<string>(), It.IsAny<int>())).Returns(Result.Success(new Report[] { new Report(), new Report(), new Report(), new Report(), }));

            controller = new ReportController(logger, dataSource);

            result = controller.GetReports("test", 1234);
            okResult = result.Result as OkObjectResult;
            value = okResult?.Value as Report[];

            Assert.IsNotNull(result);
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsNotNull(value);
            Assert.AreEqual(4,value.Length);
        }
        #endregion

    

    }
}