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
    public class ReportController : ControllerBase, ILogController
    {
       

        private readonly ILogger<ReportController> logger;
        ILogger ILogController.Logger
        {
            get => logger;
        }

        private IReportDataSource dataSource;

        public ReportController(ILogger<ReportController> Logger, IReportDataSource DataSource)
        {
            this.logger = Logger;
            this.dataSource = DataSource;
        }

       
        [HttpGet("{ScenarioName}/{PID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Report[]> GetReports(string ScenarioName,int PID)
        {
            if (ScenarioName == null) return this.CreateErrorAction<Report[]>(LogLevel.Error, "Scenario name must be provided",(m)=>this.BadRequest(m));

            return dataSource.GetReports(ScenarioName,PID).SelectActionResult(
                (item) => Ok(item) ,
                (ex) =>
                {
                    switch (ex)
                    {
                        case DirectoryNotFoundException: return this.CreateErrorAction<Report[]>(LogLevel.Warning, $"Reports folder was not found", (m) => this.NotFound(m)); 
                        default: return this.CreateErrorAction<Report[]>(LogLevel.Error, "An internal server error occured", (m) => new InternalServerError(m)); ;
                    }
                }
            );
        }//*/

       

        

    }
}
