using Automasipp.Models;
using ResultTypeLib;

namespace Automasipp.backend.DataSources
{
    public class ScenarioDataSource : DataSource, IScenarioDataSource
    {
        private string scenariosFolder;
        public ScenarioDataSource(ILogger Logger, string ScenariosFolder):base(Logger) 
        { 
            if (ScenariosFolder==null) throw new ArgumentNullException(nameof(ScenariosFolder));    
            this.scenariosFolder = ScenariosFolder;
        }


        public IResult<string[]> GetScenarioNames()
        {
            Log(LogLevel.Information, $"List all scenarios in folder {scenariosFolder}");
            return Try(() => Directory.GetFiles(scenariosFolder, "*.xml").Select(fullPath => Path.GetFileNameWithoutExtension(fullPath)).ToArray());
        }


        public IResult<Scenario> GetScenario(string Name)
        {
            Log(LogLevel.Information, $"Load scenario {Name} in folder {scenariosFolder}");
            return Result.Fail<Scenario>(new FileNotFoundException());
        }



    }
}
