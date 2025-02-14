using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Automasipp.Models
{
    [XmlRoot("pause")]
    public class PauseCommand:Command
    {
        [XmlAttribute("milliseconds")]
        public int Duration
        {
            get;
            set;
        }

    }
}
