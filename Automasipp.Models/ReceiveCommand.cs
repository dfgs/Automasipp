using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Automasipp.Models
{
    [XmlRoot("recv")]
    public class ReceiveCommand:Command
    {
        [XmlAttribute("response")]
        public ushort ResponseCode
        {
            get; set;
        }

        [XmlAttribute("optional")]
        public bool IsOptional
        {
            get; set;
        }


    }
}
