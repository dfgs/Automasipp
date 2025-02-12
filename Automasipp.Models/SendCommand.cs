using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Automasipp.Models
{
    [XmlRoot("send")]
    public class SendCommand:Command
    {
        [XmlAttribute("retrans")]
        public int RetransmissionTimer
        {
            get;set;
        }

        [XmlText]
        public required string Message
        {
            get; set;
        }

    }
}
