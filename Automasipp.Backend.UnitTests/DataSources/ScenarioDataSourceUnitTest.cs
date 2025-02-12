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

        [TestMethod]
        public void GetScenarioShouldFailAndLogErrorIsFileIsNotFound()
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
            result.Match((val) => Assert.Fail(), (ex) => Assert.IsInstanceOfType<FileNotFoundException>(ex));

            Mock.VerifyAll();

        }

        [TestMethod]
        public void GetScenarioShouldFailAndLogErrorIsNameIsNull()
        {
            ILogger logger;
            IScenarioDataSource dataSource;
            IResult<Scenario> result;


            logger = Mock.Of<ILogger>();
            Mock.Get(logger).Setup(m => m.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>())).Verifiable(Times.Once());


            dataSource = new ScenarioDataSource(logger, "invalidfolder");

#pragma warning disable CS8625 // Impossible de convertir un litt�ral ayant une valeur null en type r�f�rence non-nullable.
            result = dataSource.GetScenario(null);
#pragma warning restore CS8625 // Impossible de convertir un litt�ral ayant une valeur null en type r�f�rence non-nullable.

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Succeeded());
            result.Match((val) => Assert.Fail(), (ex) => Assert.IsInstanceOfType<ArgumentNullException>(ex));

            Mock.VerifyAll();

        }

    }
}