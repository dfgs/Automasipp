using Automasipp.backend.Controllers;
using Automasipp.backend.DataSources;
using Automasipp.Backend.Controllers;
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
        public void GetScenarioNamesShouldFailAndLogError()
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
            Mock.VerifyAll();

        }




    }
}