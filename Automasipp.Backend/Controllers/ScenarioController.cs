using Automasipp.Models;
using Automatsipp.backend.DataSources;
using Microsoft.AspNetCore.Mvc;
using ResultTypeLib;

namespace Automatsipp.backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScenarioController : ControllerBase
    {
       

        private readonly ILogger<ScenarioController> logger;
        private IScenarioDataSource dataSource;

        public ScenarioController(ILogger<ScenarioController> Logger, IScenarioDataSource DataSource)
        {
            this.logger = Logger;
            this.dataSource = DataSource;
        }

        [HttpGet(Name = "GetScenarioNames")]
        public ActionResult<string[]> GetScenarioNames()
        {
            return dataSource.GetScenarioNames().SelectActionResult(
                (items) => Ok(items),
                (ex) =>  BadRequest()
            );//*/
        }
    }
}
