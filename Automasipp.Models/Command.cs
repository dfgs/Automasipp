using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Automasipp.Models
{
    [Serializable, 
    XmlInclude(typeof(SendCommand)), JsonDerivedType(typeof(SendCommand)),
    XmlInclude(typeof(ReceiveCommand)), JsonDerivedType(typeof(ReceiveCommand)),
    XmlInclude(typeof(CallLengthRepartition)), JsonDerivedType(typeof(CallLengthRepartition)),
    XmlInclude(typeof(ResponseTimeRepartition)),JsonDerivedType(typeof(ResponseTimeRepartition)),
    XmlInclude(typeof(PauseCommand)),JsonDerivedType(typeof(PauseCommand)),
    ]
    public abstract class Command
    {
    }
}
