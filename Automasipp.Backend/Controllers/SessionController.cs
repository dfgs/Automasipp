using Automasipp.Models;
using Automasipp.backend.DataSources;
using Microsoft.AspNetCore.Mvc;
using ResultTypeLib;
using System;
using Automasipp.Backend.Controllers;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net.Mime;
using System.Xml.Linq;

namespace Automasipp.backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessionController : ControllerBase, ILogController
    {
       

        private readonly ILogger<SessionController> logger;
        ILogger ILogController.Logger
        {
            get => logger;
        }

        private ISessionDataSource dataSource;

        public SessionController(ILogger<SessionController> Logger, ISessionDataSource DataSource)
        {
            this.logger = Logger;
            this.dataSource = DataSource;
        }

        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Session[]> GetSessions()
        {
            return dataSource.GetSessions().SelectActionResult(
                (items) => Ok(items),
                (ex) =>
                {
                    switch (ex)
                    {
                        case DirectoryNotFoundException: return this.CreateErrorAction<Session[]>(LogLevel.Warning, $"Session folder was not found", (m) => this.NotFound(m));
                        default: return this.CreateErrorAction<Session[]>(LogLevel.Error, "An internal server error occured", (m) => new InternalServerError(m)); ;
                    }
                }
            );//*/
        }
        
        [HttpGet("{ScenarioName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Session[]> GetSessions(string ScenarioName)
        {
            if (ScenarioName == null) return this.CreateErrorAction<Session[]>(LogLevel.Error, "Scenario name must be provided",(m)=>this.BadRequest(m));

            return dataSource.GetSessions(ScenarioName).SelectActionResult(
                (item) => Ok(item) ,
                (ex) =>
                {
                    switch (ex)
                    {
                        case DirectoryNotFoundException: return this.CreateErrorAction<Session[]>(LogLevel.Warning, $"Session folder was not found", (m) => this.NotFound(m)); 
                        default: return this.CreateErrorAction<Session[]>(LogLevel.Error, "An internal server error occured", (m) => new InternalServerError(m)); ;
                    }
                }
            );
        }//*/

        [HttpPost("{ScenarioName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Session> StartSession(string ScenarioName)
        {

            if (ScenarioName == null) return this.CreateErrorAction<Session>(LogLevel.Error, "Scenario name must be provided", (m) => this.BadRequest(m));

            return dataSource.StartSession(ScenarioName).SelectActionResult(
                (item) => Ok(item),
                (ex) =>
                {
                    switch (ex)
                    {
                        //case FileNotFoundException: return this.CreateErrorAction<Session>(LogLevel.Warning, $"Scenawas not found", (m) => this.NotFound(m));
                        //case DirectoryNotFoundException: return this.CreateErrorAction<Session>(LogLevel.Warning, $"Session folder was not found", (m) => this.NotFound(m));
                        default: return this.CreateErrorAction<Session>(LogLevel.Error, "An internal server error occured", (m) => new InternalServerError(m)); ;
                    }
                }
            );
        }//*/

        

    }
}
