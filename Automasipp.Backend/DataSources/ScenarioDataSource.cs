using Automasipp.Models;
using ResultTypeLib;
using System.Xml;
using System.Xml.Serialization;

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

        private IResult<Scenario> LoadScenario(string FileName)
        {
            return Try( () => 
            {
                using (FileStream stream = new FileStream(FileName, FileMode.Open))
                {
                    XmlTextReader reader = new XmlTextReader(stream) { DtdProcessing = DtdProcessing.Ignore };
                    XmlSerializer serialiser = new XmlSerializer(typeof(Scenario));
                    Scenario? scenario=serialiser.Deserialize(reader) as Scenario;
                    if (scenario != null) return scenario;
                    throw new InvalidOperationException("Failed to deserialize scenarion object");
                }
            }
            );
        }

        public IResult<Scenario> GetScenario(string Name)
        {
            if (Name == null) return Result.Fail<Scenario>(new ArgumentNullException(nameof(Name))) ;

            Log(LogLevel.Information, $"Load scenario {Name} in folder {scenariosFolder}");

            return LoadScenario(Path.Combine(scenariosFolder, $"{Name}.xml")).Select((s)=>s,(ex)=>ex.InnerException??ex);
        }



    }
}
