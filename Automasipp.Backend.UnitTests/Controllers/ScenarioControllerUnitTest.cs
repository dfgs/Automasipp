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
    public class ScenarioDataSourceUnitTest
    {
        #region GetScenarioNames
        [TestMethod]
        public void GetScenarioNamesShouldReturnCode500IfDataSourceFails()
        {
            ILogger<ScenarioController> logger;
            IScenarioDataSource dataSource;
            ScenarioController controller;
            ActionResult<string[]> result;
            InternalServerError? koResult;

            logger = Mock.Of<ILogger<ScenarioController>>();
            Mock.Get(logger).Setup(m => m.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>())).Verifiable(Times.Once());

            dataSource = Mock.Of<IScenarioDataSource>();
            Mock.Get(dataSource).Setup(m => m.GetScenarioNames()).Returns(Result.Fail<string[]>(new InvalidOperationException("error content")));

            controller = new ScenarioController(logger, dataSource);
            
            result=controller.GetScenarioNames();
            koResult=result.Result as InternalServerError;

            Assert.IsNotNull(result);
            Assert.IsNotNull(koResult);
            Assert.AreEqual("error content", koResult.Content);
            Mock.VerifyAll();
        }

        [TestMethod]
        public void GetScenarioNamesShouldReturnCode200IfDataSourceSucceed()
        {
            ILogger<ScenarioController> logger;
            IScenarioDataSource dataSource;
            ScenarioController controller;
            ActionResult<string[]> result;
            OkObjectResult? okResult;
            string[]? values;

            logger = Mock.Of<ILogger<ScenarioController>>();
            dataSource = Mock.Of<IScenarioDataSource>();
            Mock.Get(dataSource).Setup(m => m.GetScenarioNames()).Returns(Result.Success(new string[] { "item1", "item2", "item3", } ));

            controller = new ScenarioController(logger, dataSource);

            result = controller.GetScenarioNames();
            okResult = result.Result as OkObjectResult;
            values = okResult?.Value as string[];

            Assert.IsNotNull(result);
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200,okResult.StatusCode);
            Assert.IsNotNull(values);
            Assert.AreEqual(3,values.Length);            

        }
        #endregion

        #region GetScenario
        [TestMethod]
        public void GetScenarioShouldReturnCode500IfDataSourceFails()
        {
            ILogger<ScenarioController> logger;
            IScenarioDataSource dataSource;
            ScenarioController controller;
            ActionResult<Scenario> result;
            InternalServerError? koResult;

            logger = Mock.Of<ILogger<ScenarioController>>();
            Mock.Get(logger).Setup(m => m.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>())).Verifiable(Times.Once());

            dataSource = Mock.Of<IScenarioDataSource>();
            Mock.Get(dataSource).Setup(m => m.GetScenario(It.IsAny<string>())).Returns(Result.Fail<Scenario>(new InvalidOperationException("error content")));

            controller = new ScenarioController(logger, dataSource);

            result = controller.GetScenario("anyname");
            koResult = result.Result as InternalServerError;

            Assert.IsNotNull(result);
            Assert.IsNotNull(koResult);
            Mock.VerifyAll();
        }
        [TestMethod]
        public void GetScenarioShouldReturnCode404IfFileIsNotFound()
        {
            ILogger<ScenarioController> logger;
            IScenarioDataSource dataSource;
            ScenarioController controller;
            ActionResult<Scenario> result;
            NotFoundObjectResult? koResult;

            logger = Mock.Of<ILogger<ScenarioController>>();
            Mock.Get(logger).Setup(m => m.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>())).Verifiable(Times.Once());

            dataSource = Mock.Of<IScenarioDataSource>();
            Mock.Get(dataSource).Setup(m => m.GetScenario(It.IsAny<string>())).Returns(Result.Fail<Scenario>(new FileNotFoundException("error content")));

            controller = new ScenarioController(logger, dataSource);

            result = controller.GetScenario("anyname");
            koResult = result.Result as NotFoundObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(koResult);
            Assert.AreEqual("Scenario anyname was not found", koResult.Value);
            Mock.VerifyAll();
        }
        [TestMethod]
        public void GetScenarioShouldReturnCode404IfDirectoryIsNotFound()
        {
            ILogger<ScenarioController> logger;
            IScenarioDataSource dataSource;
            ScenarioController controller;
            ActionResult<Scenario> result;
            NotFoundObjectResult? koResult;

            logger = Mock.Of<ILogger<ScenarioController>>();
            Mock.Get(logger).Setup(m => m.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>())).Verifiable(Times.Once());

            dataSource = Mock.Of<IScenarioDataSource>();
            Mock.Get(dataSource).Setup(m => m.GetScenario(It.IsAny<string>())).Returns(Result.Fail<Scenario>(new DirectoryNotFoundException("error content")));

            controller = new ScenarioController(logger, dataSource);

            result = controller.GetScenario("anyname");
            koResult = result.Result as NotFoundObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(koResult);
            Assert.AreEqual("Scenario anyname was not found", koResult.Value);
            Mock.VerifyAll();
        }
        [TestMethod]
        public void GetScenarioShouldReturnCode400IfNameIsNotProvided()
        {
            ILogger<ScenarioController> logger;
            IScenarioDataSource dataSource;
            ScenarioController controller;
            ActionResult<Scenario> result;
            BadRequestObjectResult? koResult;

            logger = Mock.Of<ILogger<ScenarioController>>();
            Mock.Get(logger).Setup(m => m.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>())).Verifiable(Times.Once());

            dataSource = Mock.Of<IScenarioDataSource>();
            Mock.Get(dataSource).Setup(m => m.GetScenario(It.IsAny<string>())).Returns(Result.Fail<Scenario>(new FileNotFoundException("error content")));

            controller = new ScenarioController(logger, dataSource);

#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
            result = controller.GetScenario(null);
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
            koResult = result.Result as BadRequestObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(koResult);
            Assert.AreEqual("Scenario name must be provided", koResult.Value);
            Mock.VerifyAll();
        }

        [TestMethod]
        public void GetScenarioShouldReturnCode200IfFileExists()
        {
            ILogger<ScenarioController> logger;
            IScenarioDataSource dataSource;
            ScenarioController controller;
            ActionResult<Scenario> result;
            OkObjectResult? okResult;
            Scenario? value;

            logger = Mock.Of<ILogger<ScenarioController>>();
            dataSource = Mock.Of<IScenarioDataSource>();
            Mock.Get(dataSource).Setup(m => m.GetScenario(It.IsAny<string>())).Returns(Result.Success(new Scenario() { Name="test" }));

            controller = new ScenarioController(logger, dataSource);

            result = controller.GetScenario("anyname");
            okResult = result.Result as OkObjectResult;
            value = okResult?.Value as Scenario;

            Assert.IsNotNull(result);
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsNotNull(value);

        }
        #endregion

        #region PutScenario
        [TestMethod]
        public void PutScenarioShouldReturnCode500IfDataSourceFails()
        {
            ILogger<ScenarioController> logger;
            IScenarioDataSource dataSource;
            ScenarioController controller;
            ActionResult<bool> result;
            InternalServerError? koResult;
            Scenario scenario;

            logger = Mock.Of<ILogger<ScenarioController>>();
            Mock.Get(logger).Setup(m => m.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>())).Verifiable(Times.Once());

            dataSource = Mock.Of<IScenarioDataSource>();
            Mock.Get(dataSource).Setup(m => m.PutScenario(It.IsAny<string>(), It.IsAny<Scenario>())).Returns(Result.Fail<bool>(new InvalidOperationException("error content")));

            controller = new ScenarioController(logger, dataSource);

            scenario = new Scenario() { Name = "testscenario" };
            result = controller.PutScenario("anyname", scenario);
            koResult = result.Result as InternalServerError;

            Assert.IsNotNull(result);
            Assert.IsNotNull(koResult);
            Mock.VerifyAll();
        }
        [TestMethod]
        public void PutScenarioShouldReturnCode404IfFileIsNotFound()
        {
            ILogger<ScenarioController> logger;
            IScenarioDataSource dataSource;
            ScenarioController controller;
            ActionResult<bool> result;
            NotFoundObjectResult? koResult;
            Scenario scenario;

            logger = Mock.Of<ILogger<ScenarioController>>();
            Mock.Get(logger).Setup(m => m.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>())).Verifiable(Times.Once());

            dataSource = Mock.Of<IScenarioDataSource>();
            Mock.Get(dataSource).Setup(m => m.PutScenario(It.IsAny<string>(), It.IsAny<Scenario>())).Returns(Result.Fail<bool>(new FileNotFoundException("error content")));

            controller = new ScenarioController(logger, dataSource);

            scenario = new Scenario() { Name = "testscenario" };
            result = controller.PutScenario("anyname",scenario);
            koResult = result.Result as NotFoundObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(koResult);
            Assert.AreEqual("Scenario anyname was not found", koResult.Value);
            Mock.VerifyAll();
        }
        [TestMethod]
        public void PutScenarioShouldReturnCode404IfDirectoryIsNotFound()
        {
            ILogger<ScenarioController> logger;
            IScenarioDataSource dataSource;
            ScenarioController controller;
            ActionResult<bool> result;
            NotFoundObjectResult? koResult;
            Scenario scenario;

            logger = Mock.Of<ILogger<ScenarioController>>();
            Mock.Get(logger).Setup(m => m.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>())).Verifiable(Times.Once());

            dataSource = Mock.Of<IScenarioDataSource>();
            Mock.Get(dataSource).Setup(m => m.PutScenario(It.IsAny<string>(), It.IsAny<Scenario>())).Returns(Result.Fail<bool>(new DirectoryNotFoundException("error content")));

            controller = new ScenarioController(logger, dataSource);

            scenario = new Scenario() { Name = "testscenario" };
            result = controller.PutScenario("anyname",scenario);
            koResult = result.Result as NotFoundObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(koResult);
            Assert.AreEqual("Scenario anyname was not found", koResult.Value);
            Mock.VerifyAll();
        }
        [TestMethod]
        public void PutScenarioShouldReturnCode400IfNameIsNotProvided()
        {
            ILogger<ScenarioController> logger;
            IScenarioDataSource dataSource;
            ScenarioController controller;
            ActionResult<bool> result;
            BadRequestObjectResult? koResult;
            Scenario scenario;

            logger = Mock.Of<ILogger<ScenarioController>>();
            Mock.Get(logger).Setup(m => m.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>())).Verifiable(Times.Once());

            dataSource = Mock.Of<IScenarioDataSource>();
            Mock.Get(dataSource).Setup(m => m.PutScenario(It.IsAny<string>(), It.IsAny<Scenario>())).Returns(Result.Fail<bool>(new FileNotFoundException("error content")));

            controller = new ScenarioController(logger, dataSource);
            scenario = new Scenario() { Name = "testscenario" };

#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
            result = controller.PutScenario(null,scenario);
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
            koResult = result.Result as BadRequestObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(koResult);
            Assert.AreEqual("Scenario name must be provided", koResult.Value);
            Mock.VerifyAll();
        }

        [TestMethod]
        public void PutScenarioShouldReturnCode400IfContentIsNotProvided()
        {
            ILogger<ScenarioController> logger;
            IScenarioDataSource dataSource;
            ScenarioController controller;
            ActionResult<bool> result;
            BadRequestObjectResult? koResult;

            logger = Mock.Of<ILogger<ScenarioController>>();
            Mock.Get(logger).Setup(m => m.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>())).Verifiable(Times.Once());

            dataSource = Mock.Of<IScenarioDataSource>();
            Mock.Get(dataSource).Setup(m => m.PutScenario(It.IsAny<string>(), It.IsAny<Scenario>())).Returns(Result.Fail<bool>(new FileNotFoundException("error content")));

            controller = new ScenarioController(logger, dataSource);
 
#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
            result = controller.PutScenario("name", null);
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
            koResult = result.Result as BadRequestObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(koResult);
            Assert.AreEqual("Scenario content must be provided", koResult.Value);
            Mock.VerifyAll();
        }


        [TestMethod]
        public void PutScenarioShouldReturnCode200IfFileExists()
        {
            ILogger<ScenarioController> logger;
            IScenarioDataSource dataSource;
            ScenarioController controller;
            ActionResult<bool> result;
            OkObjectResult? okResult;
            Scenario scenario;

            logger = Mock.Of<ILogger<ScenarioController>>();
            dataSource = Mock.Of<IScenarioDataSource>();
            Mock.Get(dataSource).Setup(m => m.PutScenario(It.IsAny<string>(), It.IsAny<Scenario>())).Returns(Result.Success(true));

            controller = new ScenarioController(logger, dataSource);
            scenario = new Scenario() { Name = "testscenario" };

            result = controller.PutScenario("anyname",scenario);
            okResult = result.Result as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsNotNull(okResult.Value);
            Assert.IsTrue((bool)okResult.Value );

        }
        #endregion

    }
}