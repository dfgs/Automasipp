using RestSharp;
using RestSharp.Serializers;
using RestSharp.Serializers.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automasipp.Desktop.Pages
{
    public abstract class RESTPage:Page
    {

        

        public RESTPage():base()
        {
 
        }

        protected async Task<T> GetAsync<T>(string Resource)
        {
            RestClient? client = PageManager?.GetPage<ConnectionPage>()?.Client;
            if (client== null) throw new InvalidOperationException("Cannot get REST client");

            T? response = await client.GetAsync<T>(Resource);
            if (response == null) throw new InvalidOperationException("Result is null");
            return response;

        }

       

    }
}
