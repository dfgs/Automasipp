using Automasipp.backend.Controllers;
using Automasipp.backend.DataSources;
using Automasipp.Backend.Controllers;
using Automasipp.Models;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using Moq;
using ResultTypeLib;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Automasipp.Backend.UnitTests.DataSources
{
    [TestClass]
    public class ScenarioDataSourceUnitTest
    {

        #region GetScenarioNames
        [TestMethod]
        public void GetScenarioNamesShouldFailAndLogErrorIfSourceFolderIsInvalid()
        {
            ILogger logger;
            IScenarioDataSource dataSource;
            IResult<string[]> result;

 
            logger = Mock.Of<ILogger>();
            Mock.Get(logger).Setup(m => m.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>())).Verifiable(Times.Once());


            dataSource =new ScenarioDataSource(logger,"invalidfolder");

            result=dataSource.GetScenarioNames();

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Succeeded());
            result.Match((val) => Assert.Fail(), (ex) => Assert.IsInstanceOfType<DirectoryNotFoundException>(ex));
            
            Mock.VerifyAll();

        }
        #endregion

        #region GetScenario

        [TestMethod]
        public void GetScenarioShouldFailAndLogErrorIfDirectoryIsNotFound()
        {
            ILogger logger;
            IScenarioDataSource dataSource;
            IResult<Scenario> result;


            logger = Mock.Of<ILogger>();
            Mock.Get(logger).Setup(m => m.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>())).Verifiable(Times.Once());


            dataSource = new ScenarioDataSource(logger, "invalidfolder");

            result = dataSource.GetScenario("invalidname");

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Succeeded());
            result.Match((val) => Assert.Fail(), (ex) => Assert.IsInstanceOfType<DirectoryNotFoundException>(ex));

            Mock.VerifyAll();

        }
        [TestMethod]
        public void GetScenarioShouldFailAndLogErrorIfFileIsNotFound()
        {
            ILogger logger;
            IScenarioDataSource dataSource;
            IResult<Scenario> result;


            logger = Mock.Of<ILogger>();
            Mock.Get(logger).Setup(m => m.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>())).Verifiable(Times.Once());


            dataSource = new ScenarioDataSource(logger, ".");

            result = dataSource.GetScenario("invalidname");

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Succeeded());
            result.Match((val) => Assert.Fail(), (ex) => Assert.IsInstanceOfType<FileNotFoundException>(ex));

            Mock.VerifyAll();

        }

        [TestMethod]
        public void GetScenarioShouldFailAndLogErrorIfNameIsNull()
        {
            ILogger logger;
            IScenarioDataSource dataSource;
            IResult<Scenario> result;


            logger = Mock.Of<ILogger>();
            Mock.Get(logger).Setup(m => m.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>())).Verifiable(Times.Once());


            dataSource = new ScenarioDataSource(logger, "invalidfolder");

#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
            result = dataSource.GetScenario(null);
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Succeeded());
            result.Match((val) => Assert.Fail(), (ex) => Assert.IsInstanceOfType<ArgumentNullException>(ex));

            Mock.VerifyAll();

        }

        [TestMethod]
        public void GetScenarioShouldSucceed()
        {
            ILogger logger;
            IScenarioDataSource dataSource;
            IResult<Scenario> result;
            Scenario? scenario=null;
            SendCommand? send;
            ReceiveCommand? receive;

            logger = Mock.Of<ILogger>();
  

            dataSource = new ScenarioDataSource(logger, @".\Scenarios");

            result = dataSource.GetScenario("sippexample");

            Assert.IsNotNull(result);
            result.Match((val) => scenario=val, (ex) => Assert.Fail(ex.Message));

            if (scenario == null) return;
            Assert.AreEqual("Basic Sipstone UAC", scenario.Name);
            Assert.AreEqual(10, scenario.Commands.Count);

            send = scenario.Commands[0] as SendCommand;
            Assert.IsNotNull(send);
            Assert.AreEqual(500, send.RetransmissionTimer);
            Assert.IsNotNull(send.Message);
            Assert.IsTrue(send.Message.Contains("INVITE sip:[service]@[remote_ip] SIP/2.0"));

            receive = scenario.Commands[1] as ReceiveCommand;
            Assert.IsNotNull(receive);
            Assert.AreEqual(100, receive.ResponseCode);
            Assert.IsTrue(receive.IsOptional);
        }
        #endregion

        #region PutScenario

        [TestMethod]
        public void PutScenarioShouldFailAndLogErrorIfDirectoryIsNotFound()
        {
            ILogger logger;
            IScenarioDataSource dataSource;
            IResult<bool> result;


            logger = Mock.Of<ILogger>();
            Mock.Get(logger).Setup(m => m.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>())).Verifiable(Times.Once());


            dataSource = new ScenarioDataSource(logger, "invalidfolder");

            result = dataSource.PutScenario("invalidname",new Scenario() { Name="testscenario" });

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Succeeded());
            result.Match((val) => Assert.Fail(), (ex) => Assert.IsInstanceOfType<DirectoryNotFoundException>(ex));

            Mock.VerifyAll();

        }
        [TestMethod]
        public void PutScenarioShouldFailAndLogErrorIfFileIsNotFound()
        {
            ILogger logger;
            IScenarioDataSource dataSource;
            IResult<bool> result;


            logger = Mock.Of<ILogger>();
            Mock.Get(logger).Setup(m => m.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>())).Verifiable(Times.Once());


            dataSource = new ScenarioDataSource(logger, ".");

            result = dataSource.PutScenario("invalidname",new Scenario() { Name = "testscenario" });

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Succeeded());
            result.Match((val) => Assert.Fail(), (ex) => Assert.IsInstanceOfType<FileNotFoundException>(ex));

            Mock.VerifyAll();

        }

        [TestMethod]
        public void PutScenarioShouldFailAndLogErrorIfNameIsNull()
        {
            ILogger logger;
            IScenarioDataSource dataSource;
            IResult<bool> result;


            logger = Mock.Of<ILogger>();
            Mock.Get(logger).Setup(m => m.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>())).Verifiable(Times.Once());


            dataSource = new ScenarioDataSource(logger, "invalidfolder");

#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
            result = dataSource.PutScenario(null, new Scenario() { Name = "testscenario" });
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Succeeded());
            result.Match((val) => Assert.Fail(), (ex) => Assert.IsInstanceOfType<ArgumentNullException>(ex));

            Mock.VerifyAll();

        }
        [TestMethod]
        public void PutScenarioShouldFailAndLogErrorIfContentIsNull()
        {
            ILogger logger;
            IScenarioDataSource dataSource;
            IResult<bool> result;


            logger = Mock.Of<ILogger>();
            Mock.Get(logger).Setup(m => m.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>())).Verifiable(Times.Once());


            dataSource = new ScenarioDataSource(logger, "invalidfolder");

#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
            result = dataSource.PutScenario("filename", null);
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Succeeded());
            result.Match((val) => Assert.Fail(), (ex) => Assert.IsInstanceOfType<ArgumentNullException>(ex));

            Mock.VerifyAll();

        }

        [TestMethod]
        public void PutScenarioShouldFailAndLogErrorIfFileDoesntExists()
        {
            ILogger logger;
            IScenarioDataSource dataSource;
            IResult<bool> result;
            Scenario scenario;


            logger = Mock.Of<ILogger>();
            Mock.Get(logger).Setup(m => m.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>())).Verifiable(Times.Once());


            dataSource = new ScenarioDataSource(logger, @".\Scenarios");

            scenario = new Scenario() { Name = "Test" };
            result = dataSource.PutScenario("filename", scenario);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Succeeded());
            result.Match((val) => Assert.Fail(), (ex) => Assert.IsInstanceOfType<FileNotFoundException>(ex));
            Assert.IsFalse(File.Exists(@".\Scenarios\filename.xml"));
            Mock.VerifyAll();

        }

        [TestMethod]
        public void PutScenarioShouldSucceed()
        {
            string fileName = @".\Scenarios\TestPut.xml";
            ILogger logger;
            IScenarioDataSource dataSource;
            IResult<bool> result;
            Scenario scenario ;

            logger = Mock.Of<ILogger>();

            using (File.Create(fileName)) { };
            
            Assert.AreEqual(0, new FileInfo(fileName).Length);

            dataSource = new ScenarioDataSource(logger, @".\Scenarios");

            scenario =new Scenario() { Name= "Test" };

            result = dataSource.PutScenario("TestPut",scenario);

            Assert.IsNotNull(result);
            result.Match((val) => Assert.IsTrue(val), (ex) => Assert.Fail(ex.Message));
            Assert.AreNotEqual(0, new FileInfo(fileName).Length);

            File.Delete(fileName);

        }
        #endregion

    }
}