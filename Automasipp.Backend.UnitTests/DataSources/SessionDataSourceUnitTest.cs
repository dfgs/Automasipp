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
    public class SessionDataSourceUnitTest
    {
        #region CreateSessionFromFileName
        [TestMethod]
        public void CreateSessionFromFileNameShouldThrowExceptionIfFileNameIsInvalid()
        {
            ILogger logger;
            SessionDataSource dataSource;


            logger = Mock.Of<ILogger>();

            dataSource = new SessionDataSource(logger, "invalidfolder", "invalidfolder", "invalidfolder");

            Assert.ThrowsException<FormatException>(() => dataSource.CreateSessionFromFileName("invalidFileName"));
        }
        [TestMethod]
        public void CreateSessionFromFileNameShouldSuccess()
        {
            ILogger logger;
            SessionDataSource dataSource;
            Session session;

            logger = Mock.Of<ILogger>();

            dataSource = new SessionDataSource(logger, "invalidfolder", "invalidfolder", "invalidfolder");

            session = dataSource.CreateSessionFromFileName(@"c:\folder1\demo1_1234.session");
            Assert.AreEqual("demo1", session.ScenarioName);
            Assert.AreEqual(1234, session.PID);
           
            session = dataSource.CreateSessionFromFileName(@"c:\folder1\Folder 2\demo_test_1234.session");
            Assert.AreEqual("demo_test", session.ScenarioName);
            Assert.AreEqual(1234, session.PID);

        }
        #endregion


        #region GetSessions
        [TestMethod]
        public void GetSessionsShouldFailAndLogErrorIfSourceFolderIsInvalid()
        {
            ILogger logger;
            ISessionDataSource dataSource;
            IResult<Session[]> result;

 
            logger = Mock.Of<ILogger>();
            Mock.Get(logger).Setup(m => m.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>())).Verifiable(Times.Once());


            dataSource =new SessionDataSource(logger,"invalidfolder","invalidfolder","invalidfolder");

            result=dataSource.GetSessions();

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Succeeded());
            result.Match((val) => Assert.Fail(), (ex) => Assert.IsInstanceOfType<DirectoryNotFoundException>(ex));
            
            Mock.VerifyAll();

        }
        [TestMethod]
        public void GetSessionsShouldSuccess()
        {
            ILogger logger;
            ISessionDataSource dataSource;
            IResult<Session[]> result;


            logger = Mock.Of<ILogger>();

            dataSource = new SessionDataSource(logger,"invalidfolder", @".\Sessions","invalidfolder");

            result = dataSource.GetSessions();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Succeeded());
            result.Match((val) =>
            {
                Assert.AreEqual(1, val.Length);
                Assert.AreEqual("demo1", val[0].ScenarioName);
                Assert.AreEqual(1234, val[0].PID);
            }
            , (ex) => Assert.Fail());

        }
        #endregion

        

    }
}