using Automasipp.Models;
using ResultTypeLib;

namespace Automasipp.backend.DataSources
{
    public interface IReportDataSource:IDataSource
    {
        
        IResult<Report[]> GetReports(string ScenarioName,int PID);
    }
}
