using Automasipp.Models;
using ResultTypeLib;

namespace Automasipp.backend.DataSources
{
    public interface ISessionDataSource:IDataSource
    {
        IResult<Session[]> GetSessions();
        IResult<Session[]> GetSessions(string ScenarioName);
        IResult<Session> StartSession(string ScenarioName);
        IResult<bool> DeleteSession(string ScenarioName,int PID);
    }
}
