using ResultTypeLib;

namespace Automatsipp.backend.DataSources
{
    public class DataSource:IDataSource
    {
        public IResult<T> Try<T>(Func<T> Func)
        {
            try
            {
                T value=Func();
                return Result.Success(value);
            }
            catch (Exception ex)
            {
                return Result.Fail<T>(ex);
            }

        }

    }
}
