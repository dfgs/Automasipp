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

        #region constructor
        [TestMethod]
        public void ConstructorShouldFailAndLogErrorIfLoggerIsNull()
        {
            ILogger logger;

            logger = Mock.Of<ILogger>();
#pragma warning disable CS8625 // Impossible de convertir un litt�ral ayant une valeur null en type r�f�rence non-nullable.
            Assert.ThrowsException<ArgumentNullException>(() => new SessionDataSource(null, "invalidfolder", "invalidfolder", "invalidfolder", "invalidfolder"));
#pragma warning restore CS8625 // Impossible de convertir un litt�ral ayant une valeur null en type r�f�rence non-nullable.
        }
        [TestMethod]
        public void ConstructorShouldFailAndLogErrorIfSippFolderIsNull()
        {
            ILogger logger;

            logger = Mock.Of<ILogger>();
#pragma warning disable CS8625 // Impossible de convertir un litt�ral ayant une valeur null en type r�f�rence non-nullable.
            Assert.ThrowsException<ArgumentNullException>(() => new SessionDataSource(logger, null, "invalidfolder", "invalidfolder", "invalidfolder"));
#pragma warning restore CS8625 // Impossible de convertir un litt�ral ayant une valeur null en type r�f�rence non-nullable.
        }
        [TestMethod]
        public void ConstructorShouldFailAndLogErrorIfSessionsFolderIsNull()
        {
            ILogger logger;

            logger = Mock.Of<ILogger>();
#pragma warning disable CS8625 // Impossible de convertir un litt�ral ayant une valeur null en type r�f�rence non-nullable.
            Assert.ThrowsException<ArgumentNullException>(() => new SessionDataSource(logger, "invalidfolder", null, "invalidfolder", "invalidfolder"));
#pragma warning restore CS8625 // Impossible de convertir un litt�ral ayant une valeur null en type r�f�rence non-nullable.


        }
        [TestMethod]
        public void ConstructorShouldFailAndLogErrorIfScenariosFolderIsNull()
        {
            ILogger logger;

            logger = Mock.Of<ILogger>();
#pragma warning disable CS8625 // Impossible de convertir un litt�ral ayant une valeur null en type r�f�rence non-nullable.
            Assert.ThrowsException<ArgumentNullException>(() => new SessionDataSource(logger, "invalidfolder", "invalidfolder", null, "invalidfolder"));
#pragma warning restore CS8625 // Impossible de convertir un litt�ral ayant une valeur null en type r�f�rence non-nullable.
        }
        [TestMethod]
        public void ConstructorShouldFailAndLogErrorIfReportsFolderIsNull()
        {
            ILogger logger;

            logger = Mock.Of<ILogger>();
#pragma warning disable CS8625 // Impossible de convertir un litt�ral ayant une valeur null en type r�f�rence non-nullable.
            Assert.ThrowsException<ArgumentNullException>(() => new SessionDataSource(logger, "invalidfolder", "invalidfolder", "invalidfolder", null));
#pragma warning restore CS8625 // Impossible de convertir un litt�ral ayant une valeur null en type r�f�rence non-nullable.
        }
        #endregion


        #region CreateSessionFromFileName
        [TestMethod]
        public void CreateSessionFromFileNameShouldThrowExceptionIfFileNameIsInvalid()
        {
            ILogger logger;
            SessionDataSource dataSource;


            logger = Mock.Of<ILogger>();

            dataSource = new SessionDataSource(logger, "invalidfolder", "invalidfolder", "invalidfolder", "invalidfolder");

            Assert.ThrowsException<FormatException>(() => dataSource.CreateSessionFromFileName("invalidFileName"));
        }
        [TestMethod]
        public void CreateSessionFromFileNameShouldThrowExceptionIfFileNameIsNull()
        {
            ILogger logger;
            SessionDataSource dataSource;


            logger = Mock.Of<ILogger>();

            dataSource = new SessionDataSource(logger, "invalidfolder", "invalidfolder", "invalidfolder", "invalidfolder");

#pragma warning disable CS8625 // Impossible de convertir un litt�ral ayant une valeur null en type r�f�rence non-nullable.
            Assert.ThrowsException<ArgumentNullException>(() => dataSource.CreateSessionFromFileName(null));
#pragma warning restore CS8625 // Impossible de convertir un litt�ral ayant une valeur null en type r�f�rence non-nullable.
        }

        [TestMethod]
        public void CreateSessionFromFileNameShouldSuccess()
        {
            ILogger logger;
            SessionDataSource dataSource;
            Session session;

            logger = Mock.Of<ILogger>();

            dataSource = new SessionDataSource(logger, "invalidfolder", "invalidfolder", "invalidfolder", "invalidfolder");

            session = dataSource.CreateSessionFromFileName(@"c:\folder1\demo1_1234.session");
            Assert.AreEqual("demo1", session.ScenarioName);
            Assert.AreEqual(1234, session.PID);
            Assert.IsFalse(session.IsRunning);

            session = dataSource.CreateSessionFromFileName(@"c:\folder1\Folder 2\demo_test_1234.session");
            Assert.AreEqual("demo_test", session.ScenarioName);
            Assert.AreEqual(1234, session.PID);
            Assert.IsFalse(session.IsRunning);

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


            dataSource =new SessionDataSource(logger,"invalidfolder","invalidfolder","invalidfolder", "invalidfolder");

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

            dataSource = new SessionDataSource(logger,"invalidfolder", @".\Sessions","invalidfolder", "invalidfolder");

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

        [TestMethod]
        public void GetSessionsWithScenarioNameShouldSuccess()
        {
            ILogger logger;
            ISessionDataSource dataSource;
            IResult<Session[]> result;


            logger = Mock.Of<ILogger>();

            dataSource = new SessionDataSource(logger, "invalidfolder", @".\Sessions", "invalidfolder", "invalidfolder");

            result = dataSource.GetSessions("demo1");

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
        [TestMethod]
        public void GetSessionsWithInvalidScenarioNameShouldSuccess()
        {
            ILogger logger;
            ISessionDataSource dataSource;
            IResult<Session[]> result;


            logger = Mock.Of<ILogger>();

            dataSource = new SessionDataSource(logger, "invalidfolder", @".\Sessions", "invalidfolder", "invalidfolder");

            result = dataSource.GetSessions("demo2");

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Succeeded());
            result.Match((val) =>
            {
                Assert.AreEqual(0, val.Length);
            }
            , (ex) => Assert.Fail());

        }

        [TestMethod]
        public void GetSessionsWithScenarioNameShouldFailAndLogErrorIfScenariosFolderIsNull()
        {
            ILogger logger;
            ISessionDataSource dataSource;
            IResult<Session[]> result;


            logger = Mock.Of<ILogger>();
            Mock.Get(logger).Setup(m => m.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>())).Verifiable(Times.Once());

            dataSource = new SessionDataSource(logger, "invalidfolder", @".\Sessions", "invalidfolder", "invalidfolder");

#pragma warning disable CS8625 // Impossible de convertir un litt�ral ayant une valeur null en type r�f�rence non-nullable.
            result = dataSource.GetSessions(null);
#pragma warning restore CS8625 // Impossible de convertir un litt�ral ayant une valeur null en type r�f�rence non-nullable.

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Succeeded());
            result.Match((val) => Assert.Fail(),
            (ex) =>
            {
                Assert.IsNotNull(result);
                Assert.IsFalse(result.Succeeded());
                result.Match((val) => Assert.Fail(), (ex) => Assert.IsInstanceOfType<ArgumentNullException>(ex));
            }

            );

            Mock.VerifyAll();


        }
        #endregion

        #region DeleteSession
        [TestMethod]
        public void DeleteSessionShouldFailAndLogErrorIfSessionsFolderIsInvalid()
        {
            ILogger logger;
            ISessionDataSource dataSource;
            IResult<bool> result;


            logger = Mock.Of<ILogger>();
            Mock.Get(logger).Setup(m => m.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>())).Verifiable(Times.Once());


            dataSource = new SessionDataSource(logger, "invalidfolder", "invalidfolder", "invalidfolder", @".\Reports");

            result = dataSource.DeleteSession("scenario",1234);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Succeeded());
            result.Match((val) => Assert.Fail(), (ex) => Assert.IsInstanceOfType<DirectoryNotFoundException>(ex));

            Mock.VerifyAll();

        }
        [TestMethod]
        public void DeleteSessionShouldFailAndLogErrorIfReportsFolderIsInvalid()
        {
            ILogger logger;
            ISessionDataSource dataSource;
            IResult<bool> result;


            logger = Mock.Of<ILogger>();
            Mock.Get(logger).Setup(m => m.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>())).Verifiable(Times.Once());


            dataSource = new SessionDataSource(logger, "invalidfolder", @".\Sessions", "invalidfolder", "invalidfolder");

            result = dataSource.DeleteSession("scenario", 1234);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Succeeded());
            result.Match((val) => Assert.Fail(), (ex) => Assert.IsInstanceOfType<DirectoryNotFoundException>(ex));

            Mock.VerifyAll();

        }

        [TestMethod]
        public void DeleteSessionShouldFailAndLogErrorIfScenarioNameIsInvalid()
        {
            ILogger logger;
            ISessionDataSource dataSource;
            IResult<bool> result;


            logger = Mock.Of<ILogger>();
            Mock.Get(logger).Setup(m => m.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>())).Verifiable(Times.Once());


            dataSource = new SessionDataSource(logger, "invalidfolder", @".\Temp", "invalidfolder", @".\Temp");

            result = dataSource.DeleteSession("invalidscenario", 1234);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Succeeded());
            result.Match((val) => Assert.Fail(), (ex) => Assert.IsInstanceOfType<FileNotFoundException>(ex));

            Mock.VerifyAll();

        }
   

        [TestMethod]
        public void DeleteSessionShouldSuccess()
        {
            ILogger logger;
            ISessionDataSource dataSource;
            IResult<bool> result;
            string sessionFileName = @".\Temp\deletescenario_1234.session";
            string reportFileName = @".\Temp\deletescenario_1234_.csv";

            logger = Mock.Of<ILogger>();

            dataSource = new SessionDataSource(logger, "invalidfolder", @".\Temp", "invalidfolder", @".\Temp");

            if (!File.Exists(sessionFileName)) using (var sw=File.CreateText(sessionFileName)) { sw.WriteLine("test"); } ;
            if (!File.Exists(reportFileName)) using (var sw = File.CreateText(reportFileName)) { sw.WriteLine("test"); } ;

            result = dataSource.DeleteSession("deletescenario", 1234);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Succeeded());
            result.Match((val) => Assert.IsTrue(val), (ex) => Assert.Fail());


        }

        #endregion


    }
}