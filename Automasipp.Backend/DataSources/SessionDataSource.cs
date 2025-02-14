using Automasipp.Models;
using ResultTypeLib;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace Automasipp.backend.DataSources
{
    public class SessionDataSource : DataSource, ISessionDataSource
    {

        private static Regex sessionRegex = new Regex(@"(?<ScenarioName>.+)_(?<PID>\d+)");
        private string sessionsFolder;
        private string sippFolder;
        private string scenariosFolder;
        public SessionDataSource(ILogger Logger,string SippFolder, string SessionsFolder, string ScenariosFolder) :base(Logger) 
        {
            if (SessionsFolder == null) throw new ArgumentNullException(nameof(SessionsFolder));
            if (SippFolder == null) throw new ArgumentNullException(nameof(SippFolder));
            if (ScenariosFolder == null) throw new ArgumentNullException(nameof(ScenariosFolder));
            this.sippFolder= SippFolder;    
            this.sessionsFolder = SessionsFolder;
            this.scenariosFolder = ScenariosFolder;
        }


        public IResult<Session[]> GetSessions()
        {
            Log(LogLevel.Information, $"List all session in folder {sessionsFolder}");
            return Try(() => Directory.GetFiles(sessionsFolder, "*.session").Select(fullPath => CreateSessionFromFileName(fullPath)).ToArray());
        }
        public IResult<Session[]> GetSessions(string ScenarioName)
        {
            Log(LogLevel.Information, $"List all session from scenario {ScenarioName} in folder {sessionsFolder}");
            return Try(() => Directory.GetFiles(sessionsFolder, $"{ScenarioName}_*.session").Select(fullPath => CreateSessionFromFileName(fullPath)).ToArray());
        }

        public Session CreateSessionFromFileName(string FullPath)
        {
            string fileName = Path.GetFileNameWithoutExtension(FullPath);
            Match match=sessionRegex.Match(fileName);

            if (!match.Success) throw new FormatException($"Invalid session file name: {fileName}");

            return new Session() { ScenarioName = match.Groups["ScenarioName"].Value , PID = int.Parse(match.Groups["PID"].Value) };
        }

        public IResult<Session> StartSession(string ScenarioName)
        {
            return Try( () => 
            {
                Process process = new Process();
                process.StartInfo.FileName = Path.Combine(sippFolder,"sipp");
                process.StartInfo.Arguments = $"-s 1001 -sf {scenariosFolder}/{ScenarioName}.xml uac 10.0.1.11 -l 1 -m 1 -mi 10.0.1.133 -trace_stat";
                if (!process.Start()) throw new InvalidOperationException("Failed to start sipp process");
                Session session=new Session() { ScenarioName = ScenarioName, PID = process.Id };
                File.Create(Path.Combine(sessionsFolder, $"{session.ScenarioName}_{session.PID}"));

                return session;
            }
            );
        }
       


       


    }
}
