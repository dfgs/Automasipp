using Automasipp.Models;
using ResultTypeLib;

namespace Automasipp.backend.DataSources
{
    public interface IScenarioDataSource:IDataSource
    {
        IResult<string[]> GetScenarioNames();
        IResult<Scenario> GetScenario(string Name);
    }
}
