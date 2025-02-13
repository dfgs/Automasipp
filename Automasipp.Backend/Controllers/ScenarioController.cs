using Automasipp.Models;
using Automasipp.backend.DataSources;
using Microsoft.AspNetCore.Mvc;
using ResultTypeLib;
using System;
using Automasipp.Backend.Controllers;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net.Mime;

namespace Automasipp.backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScenarioController : ControllerBase, ILogController
    {
       

        private readonly ILogger<ScenarioController> logger;
        ILogger ILogController.Logger
        {
            get => logger;
        }

        private IScenarioDataSource dataSource;

        public ScenarioController(ILogger<ScenarioController> Logger, IScenarioDataSource DataSource)
        {
            this.logger = Logger;
            this.dataSource = DataSource;
        }

        [HttpGet("names")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<string[]> GetScenarioNames()
        {
            return dataSource.GetScenarioNames().SelectActionResult(
                (items) => Ok(items),
                (ex) => new InternalServerError(ex.Message)
            );//*/
        }
        
        [HttpGet("{Name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Scenario> GetScenario(string Name)
        {
            if (Name == null) return this.CreateErrorAction<Scenario>(LogLevel.Error, "Scenario name must be provided",(m)=>this.BadRequest(m));

            return dataSource.GetScenario(Name).SelectActionResult(
                (item) => Ok(item) ,
                (ex) =>
                {
                    switch (ex)
                    {
                        case FileNotFoundException: return this.CreateErrorAction<Scenario>(LogLevel.Warning, $"Scenario {Name} was not found", (m) => this.NotFound(m)); ;
                        case DirectoryNotFoundException: return this.CreateErrorAction<Scenario>(LogLevel.Warning, $"Scenario {Name} was not found", (m) => this.NotFound(m)); ;
                        default: return this.CreateErrorAction<Scenario>(LogLevel.Error, "An internal server error occured", (m) => new InternalServerError(m)); ;
                    }
                }
            );
        }//*/

        [HttpPut("{Name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<bool> PutScenario(string Name, [FromBody] Scenario Scenario)
        {
            if (Name == null) return this.CreateErrorAction<bool>(LogLevel.Error, "Scenario name must be provided", (m) => this.BadRequest(m));
            if (Scenario == null) return this.CreateErrorAction<bool>(LogLevel.Error, "Scenario content must be provided", (m) => this.BadRequest(m));

            return dataSource.PutScenario(Name,Scenario).SelectActionResult(
                (item) => Ok(true),
                (ex) =>
                {
                    switch (ex)
                    {
                        case FileNotFoundException: return this.CreateErrorAction<bool>(LogLevel.Warning, $"Scenario {Name} was not found", (m) => this.NotFound(m)); ;
                        case DirectoryNotFoundException: return this.CreateErrorAction<bool>(LogLevel.Warning, $"Scenario {Name} was not found", (m) => this.NotFound(m)); ;
                        default: return this.CreateErrorAction<bool>(LogLevel.Error, "An internal server error occured", (m) => new InternalServerError(m)); ;
                    }
                }
            );
        }//*/


    }
}
