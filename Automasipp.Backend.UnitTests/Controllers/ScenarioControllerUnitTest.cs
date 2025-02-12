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

namespace Automasipp.Backend.UnitTests.Controllers
{
    [TestClass]
    public class ScenarioDataSourceUnitTest
    {
        [TestMethod]
        public void ShouldReturnCode500IfDataSourceFails()
        {
            ILogger<ScenarioController> logger;
            IScenarioDataSource dataSource;
            ScenarioController controller;
            ActionResult<string[]> result;
            InternalServerError? koResult;

            logger = Mock.Of<ILogger<ScenarioController>>();
            dataSource=Mock.Of<IScenarioDataSource>();
            Mock.Get(dataSource).Setup(m => m.GetScenarioNames()).Returns(Result.Fail<string[]>(new InvalidOperationException("error content")));

            controller = new ScenarioController(logger, dataSource);
            
            result=controller.GetScenarioNames();
            koResult=result.Result as InternalServerError;

            Assert.IsNotNull(result);
            Assert.IsNotNull(koResult);
            Assert.AreEqual("error content", koResult.Content);
        }

        [TestMethod]
        public void ShouldReturnCode200IfDataSourceSucceed()
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


    }
}