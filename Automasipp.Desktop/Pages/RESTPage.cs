using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automasipp.Desktop.Pages
{
    public abstract class RESTPage:Page
    {
        private static readonly RestClient client;
        private static bool disposed;

        static RESTPage()
        {
            RestClientOptions options;
            options = new RestClientOptions("http://localhost:5000/");
            client = new RestClient(options);
        }

        public RESTPage(IPageManager PageManager):base(PageManager)
        {          

        }

        protected async Task<T> GetAsync<T>(string Resource)
        {
            T? response = await client.GetAsync<T>(Resource);
            if (response == null) throw new InvalidOperationException("Result is null");
            return response;

        }

        protected override void OnDispose()
        {
            base.OnDispose();
            if (!disposed)
            {
                disposed = true;
                client.Dispose();
            }
        }

    }
}
