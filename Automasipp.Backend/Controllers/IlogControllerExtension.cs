using Microsoft.AspNetCore.Mvc;

namespace Automasipp.Backend.Controllers
{
    public static class IlogControllerExtension
    {
        public static ActionResult<T> CreateErrorAction<T>(this ILogController LogController,LogLevel LogLevel,string Message,Func<string,ActionResult<T>> ActionFactory)
        {
            LogController.Logger.Log(LogLevel,Message);
            return ActionFactory(Message);
        }


    }
}
