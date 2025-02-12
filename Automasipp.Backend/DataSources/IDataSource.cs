using ResultTypeLib;

namespace Automasipp.backend.DataSources
{
    public interface IDataSource
    {
        IResult<T> Try<T>(Func<T> Func);
    }
}
