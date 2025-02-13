using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
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

        [XmlIgnore]
        public required string Message
        {
            get; set;
        }

       
        // must serialize message as CDataContent
        [XmlText]
        public XmlNode[] CDataContent
        {
            get
            {
                var doc = new XmlDocument();
                return new XmlNode[] { doc.CreateCDataSection(Message) };
            }
            set
            {
                if (value == null)
                {
                    Message = "";
                    return;
                }
                if (value.Length != 1) throw new InvalidOperationException(String.Format("Invalid array length {0}", value.Length));
                string? message=value[0].Value;
                if (message== null)
                {
                    Message = "";
                    return;
                }
                Message = message;
            }
        }


    }
}
