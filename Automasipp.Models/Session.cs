using System.Xml.Serialization;

namespace Automasipp.Models
{
    [Serializable]
    public class Session
    {
        [XmlAttribute]
        public required int PID
        {
            get;
            set;
        }

        [XmlAttribute()]
        public required string ScenarioName
        {
            get;
            set;
        }

        [XmlAttribute()]
        public bool IsRunning
        {
            get;
            set;
        }

        public Session()
        {
            
        }

    }

}
