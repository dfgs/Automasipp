using ResultTypeLib;

namespace Automasipp.backend.DataSources
{
    public class DataSource:IDataSource
    {
        private ILogger logger;

        public DataSource(ILogger Logger) 
        { 
            this.logger = Logger;
        }

        protected void Log(LogLevel LogLevel,string Message,params object?[] args)
        {
            logger.Log(LogLevel, Message, args);
        }


        public IResult<T> Try<T>(Func<T> Func)
        {
            try
            {
                T value=Func();
                return Result.Success(value);
            }
            catch (Exception ex)
            {
                Log(LogLevel.Error, $"Operation failure ({ex.Message})", ex);
                return Result.Fail<T>(ex);
            }

        }

    }
}
