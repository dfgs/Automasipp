using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automasipp.Desktop
{
    public interface IPageManager
    {
        IPage CurrentPage
        {
            get;
        }
    }
}
