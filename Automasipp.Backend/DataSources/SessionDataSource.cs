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
    public class SessionDataSource : DataSource, ISessionDataSource
    {

        private static Regex sessionRegex = new Regex(@"(?<ScenarioName>.+)_(?<PID>\d+)");
        private string sessionsFolder;
        private string sippFolder;
        private string scenariosFolder;
        private string reportsFolder;

        public SessionDataSource(ILogger Logger,string SippFolder, string SessionsFolder, string ScenariosFolder,string ReportsFolder) :base(Logger) 
        {
            if (Logger == null) throw new ArgumentNullException(nameof(Logger));
            if (SessionsFolder == null) throw new ArgumentNullException(nameof(SessionsFolder));
            if (SippFolder == null) throw new ArgumentNullException(nameof(SippFolder));
            if (ScenariosFolder == null) throw new ArgumentNullException(nameof(ScenariosFolder));
            if (ReportsFolder == null) throw new ArgumentNullException(nameof(ReportsFolder));
            this.sippFolder= SippFolder;    
            this.sessionsFolder = SessionsFolder;
            this.scenariosFolder = ScenariosFolder;
            this.reportsFolder = ReportsFolder;
        }


        public IResult<Session[]> GetSessions()
        {
            Log(LogLevel.Information, $"List all session in folder {sessionsFolder}");
            return Try(() => Directory.GetFiles(sessionsFolder, "*.session").Select(fullPath => CreateSessionFromFileName(fullPath)).ToArray());
        }
        public IResult<Session[]> GetSessions(string ScenarioName)
        {
            if (ScenarioName == null) return Result.Fail<Session[]>(new ArgumentNullException(nameof(ScenarioName)));
  
            Log(LogLevel.Information, $"List all session from scenario {ScenarioName} in folder {sessionsFolder}");
            return Try(() => Directory.GetFiles(sessionsFolder, $"{ScenarioName}_*.session").Select(fullPath => CreateSessionFromFileName(fullPath)).ToArray());
        }

        public Session CreateSessionFromFileName(string FullPath)
        {
            int pid;
            bool isRunning=false;

            if (FullPath == null) throw new ArgumentNullException(nameof(FullPath));

            string fileName = Path.GetFileNameWithoutExtension(FullPath);
            Match match=sessionRegex.Match(fileName);

            if (!match.Success) throw new FormatException($"Invalid session file name: {fileName}");
            
            pid = int.Parse(match.Groups["PID"].Value);
            try
            {
                Process process=Process.GetProcessById(pid);
                isRunning = true;
            }
            catch
            {
                isRunning = false;
            }


            return new Session() { ScenarioName = match.Groups["ScenarioName"].Value , PID= pid,IsRunning=isRunning };
        }
        
        private Process CreateProcess(string ScenarioName)
        {

            // -bg run in background => pid is incorrect when using this option
            // -stf define report file name 

            Process process = new Process();

            process.StartInfo.FileName = Path.Combine(sippFolder, "sipp");
            process.StartInfo.WorkingDirectory = reportsFolder;
            process.StartInfo.Arguments = $"10.0.1.11 -s 1001 -sf {scenariosFolder}/{ScenarioName}.xml -l 1 -m 1 -mi 10.0.1.133 -trace_stat";
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = true;

            /*process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.EnableRaisingEvents = true;
            process.OutputDataReceived += Process_OutputDataReceived; ;
            process.ErrorDataReceived += Process_ErrorDataReceived;
            //*/

            return process; 
        }
        private Session RunSession(string ScenarioName)
        {
            Process process = CreateProcess(ScenarioName);

            if (!process.Start())
            {
                Log(LogLevel.Error, $"Failed to start sipp process");
                throw new InvalidOperationException("Failed to start sipp process");
            }

            /*process.BeginErrorReadLine();
            process.BeginOutputReadLine();//*/

           
            if (process.HasExited)
            {
                Log(LogLevel.Information, $"Process exited with code {process.ExitCode}");

                // Upon exit(on fatal error or when the number of asked calls (-m option) is reached, sipp exits with one of the following exit code:
                // 0: All calls were successful
                // 1: At least one call failed
                // 97: Exit on internal command.Calls may have been processed
                // 99: Normal exit without calls processed
                // 253: RTP validation failure
                // -1: Fatal error
                // -2: Fatal error binding a socket
                if (process.ExitCode < 0) throw new InvalidOperationException($"Process exited with result {process.ExitCode}");
            }

            Session session = new Session() { ScenarioName = ScenarioName, PID = process.Id ,IsRunning=true};
            System.IO.File.Create(Path.Combine(sessionsFolder, $"{session.ScenarioName}_{session.PID}.session"));

            return session;
        }
       
        public  IResult<Session> StartSession(string ScenarioName)
        {
            if (ScenarioName == null) return Result.Fail<Session>(new ArgumentNullException(nameof(ScenarioName)));

            Log(LogLevel.Information, $"Start session for scenario {ScenarioName} in folder {sippFolder}");

            return Try<Session>(()=>RunSession(ScenarioName));

        }

        private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data!=null) Log(LogLevel.Debug, e.Data);

        }
        private void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null) Log(LogLevel.Error, e.Data);

        }


        public IResult<bool> DeleteSession(string ScenarioName, int PID)
        {
            string scenarioFileName, reportFileName;


            if (ScenarioName == null) return Result.Fail<bool>(new ArgumentNullException(nameof(ScenarioName)));

            scenarioFileName = Path.Combine(sessionsFolder, $"{ScenarioName}_{PID}.session");
            reportFileName = Path.Combine(reportsFolder, $"{ScenarioName}_{PID}_.csv");

            if (!System.IO.Directory.Exists(sessionsFolder)) return Result.Fail<bool>(new DirectoryNotFoundException($"Directory {sessionsFolder} was not found"));
            if (!System.IO.Directory.Exists(reportsFolder)) return Result.Fail<bool>(new DirectoryNotFoundException($"Directory {reportsFolder} was not found"));
            if (!System.IO.File.Exists(scenarioFileName)) return Result.Fail<bool>(new FileNotFoundException($"File {scenarioFileName} was not found"));


            return
                Try<bool>(() => 
                {
                    Log(LogLevel.Information, $"Delete session from scenario {ScenarioName}, PID {PID} in folder {sessionsFolder}");
                    System.IO.File.Delete(scenarioFileName);return true; 
                }
                ).Then(Try(() => 
                {
                    Log(LogLevel.Information, $"Delete session reports from scenario {ScenarioName}, PID {PID} in folder {reportsFolder}");
                    System.IO.File.Delete(reportFileName); return true; 
                }
                ));

        }



    }
}
