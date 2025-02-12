using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Automasipp.Models
{
    [Serializable, 
    XmlInclude(typeof(SendCommand)), XmlInclude(typeof(ReceiveCommand)), 
    XmlInclude(typeof(CallLengthRepartition)), XmlInclude(typeof(ResponseTimeRepartition)),
    XmlInclude(typeof(PauseCommand)),
    ]
    public abstract class Command
    {
    }
}
