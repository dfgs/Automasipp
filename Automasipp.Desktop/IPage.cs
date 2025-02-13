using ResultTypeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automasipp.Desktop
{
    public interface IPage:IDisposable
    {

        IPageManager? PageManager 
        { 
            get;
            set;
        }

        PageState State
        {
            get;
        }
        string Name
        { 
            get;
        }

        Task<IResult<bool>> LoadAsync();
        Task<IResult<bool>> CloseAsync();
        Task<IResult<T>> RunAsync<T>(Task<T> Func);
        Task<IResult<bool>> RunAsync(Task Action);




    }
}
