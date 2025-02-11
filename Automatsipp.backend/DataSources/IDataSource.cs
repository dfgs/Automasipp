using ResultTypeLib;

namespace Automatsipp.backend.DataSources
{
    public interface IDataSource
    {
        IResult<T> Try<T>(Func<T> Func);
    }
}
