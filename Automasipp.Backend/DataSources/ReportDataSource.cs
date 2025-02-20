using Automasipp.Backend.DataSources;
using Automasipp.Models;
using Microsoft.AspNetCore.Mvc;
using ResultTypeLib;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Automasipp.backend.DataSources
{
    public class ReportDataSource : DataSource, IReportDataSource
    {

        private IReportDeserializer reportDeserializer;
        private string reportsFolder;

        public ReportDataSource(ILogger Logger,IReportDeserializer ReportDeserializer, string ReportsFolder) :base(Logger) 
        {
            if (Logger == null) throw new ArgumentNullException(nameof(Logger));
            if (ReportDeserializer == null) throw new ArgumentNullException(nameof(ReportDeserializer));
            if (ReportsFolder == null) throw new ArgumentNullException(nameof(ReportsFolder));
            this.reportsFolder = ReportsFolder;
            this.reportDeserializer= ReportDeserializer;
        }


        public IEnumerable<Report> CreateReportsFromFileName(string ScenarioName,int PID)
        {
            List<Report> reports;
            Report report;
            string? line;

            if (ScenarioName == null) throw new ArgumentNullException(nameof(ScenarioName));

            if (!Directory.Exists(reportsFolder)) throw new DirectoryNotFoundException($"Directory {reportsFolder} was not found");

            reports = new List<Report>();
            using (FileStream stream = new FileStream(Path.Combine(reportsFolder, $"{ScenarioName}_{PID}.csv"),FileMode.Open))
            {
                StreamReader reader=new StreamReader(stream);
                // skip first line
                line=reader.ReadLine();
                while(true)
                {
                    line = reader.ReadLine();
                    if (line == null) break;
                    report=reportDeserializer.Deserialize(line);
                    reports.Add(report);
                }
            }

            return reports;
        }

        public IResult<Report[]> GetReports(string ScenarioName,int PID)
        {
            if (ScenarioName == null) return Result.Fail<Report[]>(new ArgumentNullException(nameof(ScenarioName)));
  
            Log(LogLevel.Information, $"List all reports from scenario {ScenarioName}, PID {PID} in folder {reportsFolder}");
            return Try(() => CreateReportsFromFileName(ScenarioName,PID).ToArray());
        }

        
    }
}
