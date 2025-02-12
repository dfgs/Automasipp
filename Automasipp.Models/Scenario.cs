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
    }

}
