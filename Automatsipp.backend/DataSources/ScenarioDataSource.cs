using ResultTypeLib;

namespace Automatsipp.backend.DataSources
{
    public class ScenarioDataSource:DataSource,IScenarioDataSource
    {
        private string scenariosFolder;
        public ScenarioDataSource(string ScenariosFolder) 
        { 
            if (ScenariosFolder==null) throw new ArgumentNullException(nameof(ScenariosFolder));    
            this.scenariosFolder = ScenariosFolder;
        }

        public IResult<string[]> GetScenarioNames()
        {
            return Try(() => Directory.GetFiles(scenariosFolder, "*.xml").Select(fullPath => Path.GetFileNameWithoutExtension(fullPath)).ToArray());
        }

    }
}
