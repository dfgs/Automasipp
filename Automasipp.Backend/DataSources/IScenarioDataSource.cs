using ResultTypeLib;

namespace Automatsipp.backend.DataSources
{
    public interface IScenarioDataSource:IDataSource
    {
        IResult<string[]> GetScenarioNames();
    }
}
