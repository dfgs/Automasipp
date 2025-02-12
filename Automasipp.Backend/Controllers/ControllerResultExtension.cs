using Microsoft.AspNetCore.Mvc;
using ResultTypeLib;

namespace Automasipp.backend.Controllers
{
    public static class ControllerResultExtension
    {
        public static ActionResult<TIn> SelectActionResult<TIn>(this IResult<TIn> SourceResult, Func<TIn, ActionResult<TIn>> OnSuccess, Func<Exception, ActionResult<TIn>> OnFail)
        {
            ActionResult<TIn>? result = default;

            SourceResult.Match(
                (inVal) => result = OnSuccess(inVal),
                (ex) => result = OnFail(ex)
            );

            return result!;
        }//*/
    }
}
