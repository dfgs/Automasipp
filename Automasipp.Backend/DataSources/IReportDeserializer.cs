using Automasipp.Models;

namespace Automasipp.Backend.DataSources
{
    public interface IReportDeserializer
    {
        public Report Deserialize(string Line);
    }
}
