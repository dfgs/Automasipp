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
    public class SessionControllerUnitTest
    {

        #region GetSessions using scenario name

        [TestMethod]
        public void GetSessionsUsingScenarionNameShouldReturnCode404IfDirectoryIsNotFound()
        {
            ILogger<SessionController> logger;
            ISessionDataSource dataSource;
            SessionController controller;
            ActionResult<Session[]> result;
            NotFoundObjectResult? koResult;

            logger = Mock.Of<ILogger<SessionController>>();
            Mock.Get(logger).Setup(m => m.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>())).Verifiable(Times.Once());

            dataSource = Mock.Of<ISessionDataSource>();
            Mock.Get(dataSource).Setup(m => m.GetSessions(It.IsAny<string>())).Returns(Result.Fail<Session[]>(new DirectoryNotFoundException("error content")));

            controller = new SessionController(logger, dataSource);

            result = controller.GetSessions("anyname");
            koResult = result.Result as NotFoundObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(koResult);
            Assert.AreEqual("Session folder was not found", koResult.Value);
            Mock.VerifyAll();
        }
        

        [TestMethod]
        public void GetSessionsUsingScenarionNameShouldReturnCode400IfScenarioNameIsNotProvided()
        {
            ILogger<SessionController> logger;
            ISessionDataSource dataSource;
            SessionController controller;
            ActionResult<Session[]> result;
            BadRequestObjectResult? koResult;

            logger = Mock.Of<ILogger<SessionController>>();
            Mock.Get(logger).Setup(m => m.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>())).Verifiable(Times.Once());

            dataSource = Mock.Of<ISessionDataSource>();
            Mock.Get(dataSource).Setup(m => m.GetSessions(It.IsAny<string>())).Returns(Result.Fail<Session[]>(new FileNotFoundException("error content")));

            controller = new SessionController(logger, dataSource);

#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
            result = controller.GetSessions(null);
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
            koResult = result.Result as BadRequestObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(koResult);
            Assert.AreEqual("Scenario name must be provided", koResult.Value);
            Mock.VerifyAll();
        }

        [TestMethod]
        public void GetSessionsUsingScenarionNameShouldReturnCode500IfDataSourceFails()
        {
            ILogger<SessionController> logger;
            ISessionDataSource dataSource;
            SessionController controller;
            ActionResult<Session[]> result;
            InternalServerError? koResult;

            logger = Mock.Of<ILogger<SessionController>>();
            Mock.Get(logger).Setup(m => m.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>())).Verifiable(Times.Once());

            dataSource = Mock.Of<ISessionDataSource>();
            Mock.Get(dataSource).Setup(m => m.GetSessions(It.IsAny<string>())).Returns(Result.Fail<Session[]>(new InvalidOperationException("error content")));

            controller = new SessionController(logger, dataSource);

            result = controller.GetSessions("anyname");
            koResult = result.Result as InternalServerError;

            Assert.IsNotNull(result);
            Assert.IsNotNull(koResult);
            Mock.VerifyAll();
        }


        [TestMethod]
        public void GetSessionsUsingScenarionNameShouldReturnCode200IfFilesExist()
        {
            ILogger<SessionController> logger;
            ISessionDataSource dataSource;
            SessionController controller;
            ActionResult<Session[]> result;
            OkObjectResult? okResult;
            Session[]? value;

            logger = Mock.Of<ILogger<SessionController>>();
            dataSource = Mock.Of<ISessionDataSource>();
            Mock.Get(dataSource).Setup(m => m.GetSessions(It.IsAny<string>())).Returns(Result.Success(new Session[] { new Session() { ScenarioName = "test",PID=1234 } }));

            controller = new SessionController(logger, dataSource);

            result = controller.GetSessions("test");
            okResult = result.Result as OkObjectResult;
            value = okResult?.Value as Session[];

            Assert.IsNotNull(result);
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsNotNull(value);
            Assert.AreEqual(1,value.Length);
        }
        #endregion

        #region GetSessions not using scenario name

        [TestMethod]
        public void GetSessionsShouldReturnCode404IfDirectoryIsNotFound()
        {
            ILogger<SessionController> logger;
            ISessionDataSource dataSource;
            SessionController controller;
            ActionResult<Session[]> result;
            NotFoundObjectResult? koResult;

            logger = Mock.Of<ILogger<SessionController>>();
            Mock.Get(logger).Setup(m => m.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>())).Verifiable(Times.Once());

            dataSource = Mock.Of<ISessionDataSource>();
            Mock.Get(dataSource).Setup(m => m.GetSessions()).Returns(Result.Fail<Session[]>(new DirectoryNotFoundException("error content")));

            controller = new SessionController(logger, dataSource);

            result = controller.GetSessions();
            koResult = result.Result as NotFoundObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(koResult);
            Assert.AreEqual("Session folder was not found", koResult.Value);
            Mock.VerifyAll();
        }

        [TestMethod]
        public void GetSessionsShouldReturnCode500IfDataSourceFails()
        {
            ILogger<SessionController> logger;
            ISessionDataSource dataSource;
            SessionController controller;
            ActionResult<Session[]> result;
            InternalServerError? koResult;

            logger = Mock.Of<ILogger<SessionController>>();
            Mock.Get(logger).Setup(m => m.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>())).Verifiable(Times.Once());

            dataSource = Mock.Of<ISessionDataSource>();
            Mock.Get(dataSource).Setup(m => m.GetSessions()).Returns(Result.Fail<Session[]>(new InvalidOperationException("error content")));

            controller = new SessionController(logger, dataSource);

            result = controller.GetSessions();
            koResult = result.Result as InternalServerError;

            Assert.IsNotNull(result);
            Assert.IsNotNull(koResult);
            Mock.VerifyAll();
        }


        [TestMethod]
        public void GetSessionsShouldReturnCode200IfFilesExist()
        {
            ILogger<SessionController> logger;
            ISessionDataSource dataSource;
            SessionController controller;
            ActionResult<Session[]> result;
            OkObjectResult? okResult;
            Session[]? value;

            logger = Mock.Of<ILogger<SessionController>>();
            dataSource = Mock.Of<ISessionDataSource>();
            Mock.Get(dataSource).Setup(m => m.GetSessions()).Returns(Result.Success(new Session[] { new Session() { ScenarioName = "test", PID = 1234 } }));

            controller = new SessionController(logger, dataSource);

            result = controller.GetSessions();
            okResult = result.Result as OkObjectResult;
            value = okResult?.Value as Session[];

            Assert.IsNotNull(result);
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsNotNull(value);
            Assert.AreEqual(1, value.Length);
        }
        #endregion

        #region StartSession

        [TestMethod]
        public void StartSessionShouldReturnCode500IfDataSourceFails()
        {
            ILogger<SessionController> logger;
            ISessionDataSource dataSource;
            SessionController controller;
            ActionResult<Session> result;
            InternalServerError? koResult;

            logger = Mock.Of<ILogger<SessionController>>();
            Mock.Get(logger).Setup(m => m.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>())).Verifiable(Times.Once());

            dataSource = Mock.Of<ISessionDataSource>();
            Mock.Get(dataSource).Setup(m => m.StartSession(It.IsAny<string>())).Returns(Result.Fail<Session>(new DirectoryNotFoundException("error content")));

            controller = new SessionController(logger, dataSource);

            result = controller.StartSession("anyname");
            koResult = result.Result as InternalServerError;

            Assert.IsNotNull(result);
            Assert.IsNotNull(koResult);
            Mock.VerifyAll();
        }

        [TestMethod]
        public void StartSessionShouldReturnCode400IfScenarioNameIsNotProvided()
        {
            ILogger<SessionController> logger;
            ISessionDataSource dataSource;
            SessionController controller;
            ActionResult<Session> result;
            BadRequestObjectResult? koResult;

            logger = Mock.Of<ILogger<SessionController>>();
            Mock.Get(logger).Setup(m => m.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>())).Verifiable(Times.Once());

            dataSource = Mock.Of<ISessionDataSource>();
            Mock.Get(dataSource).Setup(m => m.StartSession(It.IsAny<string>())).Returns(Result.Fail<Session>(new DirectoryNotFoundException("error content")));

            controller = new SessionController(logger, dataSource);

#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
            result = controller.StartSession(null);
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
            koResult = result.Result as BadRequestObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(koResult);
            Assert.AreEqual("Scenario name must be provided", koResult.Value);
            Mock.VerifyAll();
        }

        [TestMethod]
        public void StartSessionShouldReturnCode200()
        {
            ILogger<SessionController> logger;
            ISessionDataSource dataSource;
            SessionController controller;
            ActionResult<Session> result;
            OkObjectResult? okResult;
            Session? value;

            logger = Mock.Of<ILogger<SessionController>>();
            dataSource = Mock.Of<ISessionDataSource>();
            Mock.Get(dataSource).Setup(m => m.StartSession(It.IsAny<string>())).Returns(Result.Success( new Session() { ScenarioName = "test", PID = 1234 } ));

            controller = new SessionController(logger, dataSource);

            result = controller.StartSession("test");
            okResult = result.Result as OkObjectResult;
            value = okResult?.Value as Session;

            Assert.IsNotNull(result);
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsNotNull(value);
            Assert.AreEqual(1234, value.PID);
        }
        #endregion

        #region DeleteSession

        [TestMethod]
        public void DeleteSessionShouldReturnCode404IfDirectoryIsNotFound()
        {
            ILogger<SessionController> logger;
            ISessionDataSource dataSource;
            SessionController controller;
            ActionResult<bool> result;
            NotFoundObjectResult? koResult;

            logger = Mock.Of<ILogger<SessionController>>();
            Mock.Get(logger).Setup(m => m.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>())).Verifiable(Times.Once());

            dataSource = Mock.Of<ISessionDataSource>();
            Mock.Get(dataSource).Setup(m => m.DeleteSession(It.IsAny<string>(), It.IsAny<int>())).Returns(Result.Fail<bool>(new DirectoryNotFoundException("error content")));

            controller = new SessionController(logger, dataSource);

            result = controller.DeleteSession("anyname",124);
            koResult = result.Result as NotFoundObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(koResult);
            Assert.AreEqual("Session folder was not found", koResult.Value);
            Mock.VerifyAll();
        }
        [TestMethod]
        public void DeleteSessionShouldReturnCode404IfFileIsNotFound()
        {
            ILogger<SessionController> logger;
            ISessionDataSource dataSource;
            SessionController controller;
            ActionResult<bool> result;
            NotFoundObjectResult? koResult;

            logger = Mock.Of<ILogger<SessionController>>();
            Mock.Get(logger).Setup(m => m.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>())).Verifiable(Times.Once());

            dataSource = Mock.Of<ISessionDataSource>();
            Mock.Get(dataSource).Setup(m => m.DeleteSession(It.IsAny<string>(), It.IsAny<int>())).Returns(Result.Fail<bool>(new FileNotFoundException("error content")));

            controller = new SessionController(logger, dataSource);

            result = controller.DeleteSession("anyname", 124);
            koResult = result.Result as NotFoundObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(koResult);
            Assert.AreEqual("Session file was not found", koResult.Value);
            Mock.VerifyAll();
        }

        [TestMethod]
        public void DeleteSessionShouldReturnCode400IfScenarioNameIsNotProvided()
        {
            ILogger<SessionController> logger;
            ISessionDataSource dataSource;
            SessionController controller;
            ActionResult<bool> result;
            BadRequestObjectResult? koResult;

            logger = Mock.Of<ILogger<SessionController>>();
            Mock.Get(logger).Setup(m => m.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>())).Verifiable(Times.Once());

            dataSource = Mock.Of<ISessionDataSource>();
            Mock.Get(dataSource).Setup(m => m.DeleteSession(It.IsAny<string>(), It.IsAny<int>())).Returns(Result.Fail<bool>(new FileNotFoundException("error content")));

            controller = new SessionController(logger, dataSource);

#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
            result = controller.DeleteSession(null,1234);
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
            koResult = result.Result as BadRequestObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(koResult);
            Assert.AreEqual("Scenario name must be provided", koResult.Value);
            Mock.VerifyAll();
        }

        [TestMethod]
        public void DeleteSessionShouldReturnCode500IfDataSourceFails()
        {
            ILogger<SessionController> logger;
            ISessionDataSource dataSource;
            SessionController controller;
            ActionResult<bool> result;
            InternalServerError? koResult;

            logger = Mock.Of<ILogger<SessionController>>();
            Mock.Get(logger).Setup(m => m.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>())).Verifiable(Times.Once());

            dataSource = Mock.Of<ISessionDataSource>();
            Mock.Get(dataSource).Setup(m => m.DeleteSession(It.IsAny<string>(), It.IsAny<int>())).Returns(Result.Fail<bool>(new InvalidOperationException("error content")));

            controller = new SessionController(logger, dataSource);

            result = controller.DeleteSession("anyname",1234);
            koResult = result.Result as InternalServerError;

            Assert.IsNotNull(result);
            Assert.IsNotNull(koResult);
            Mock.VerifyAll();
        }


        [TestMethod]
        public void DeleteSessionShouldReturnCode200IfFilesExist()
        {
            ILogger<SessionController> logger;
            ISessionDataSource dataSource;
            SessionController controller;
            ActionResult<bool> result;
            OkObjectResult? okResult;
            bool value;

            logger = Mock.Of<ILogger<SessionController>>();
            dataSource = Mock.Of<ISessionDataSource>();
            Mock.Get(dataSource).Setup(m => m.DeleteSession(It.IsAny<string>(), It.IsAny<int>())).Returns(Result.Success(true));

            controller = new SessionController(logger, dataSource);

            result = controller.DeleteSession("test", 1234);
            okResult = result.Result as OkObjectResult;


            Assert.IsNotNull(result);
            Assert.IsNotNull(okResult);
            Assert.IsNotNull(okResult.Value);
            value = ((bool)okResult.Value);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsTrue(value);
        }
        #endregion

    }
}