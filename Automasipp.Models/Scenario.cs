using System.Xml.Serialization;

namespace Automasipp.Models
{
    [Serializable, XmlRoot("scenario")]
    public class Scenario
    {
        [XmlAttribute(AttributeName ="name")]
        public required string Name
        {
            get;
            set;
        }

        [XmlElementAttribute("send", typeof(SendCommand))]
        [XmlElementAttribute("recv", typeof(ReceiveCommand))]
        [XmlElementAttribute("CallLengthRepartition", typeof(CallLengthRepartition))]
        [XmlElementAttribute("ResponseTimeRepartition", typeof(ResponseTimeRepartition))]
        [XmlElementAttribute("pause", typeof(PauseCommand))]
        public List<Command> Commands
        {
            get;
            set;
        }

        public Scenario()
        {
            this.Commands = new List<Command>();
        }

    }

}
