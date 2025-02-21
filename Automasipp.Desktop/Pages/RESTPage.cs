using RestSharp;
using RestSharp.Serializers;
using RestSharp.Serializers.Json;
using ResultTypeLib;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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

        protected async Task<IResult<T>> GetAsync<T>(string Resource)
        {
            RestClient? client = PageManager?.GetPage<ConnectionPage>()?.Client;
            if (client== null) throw new InvalidOperationException("Cannot get REST client");

            IResult<T?> result = await RunAsync(client.GetAsync<T>(Resource));
            
            return result.SelectResult((response) => response==null?Result.Fail<T>( new InvalidOperationException("Result is null")):Result.Success<T>(response)
                ,(ex) => ex );
            

        }

        protected async Task<IResult<T>> PutAsync<T>(string Resource,object Content)
        {
            RestClient? client = PageManager?.GetPage<ConnectionPage>()?.Client;
            if (client == null) throw new InvalidOperationException("Cannot get REST client");
            if (Content==null) throw new ArgumentNullException(nameof(Content));

            RestRequest request = new RestRequest(Resource, Method.Put);
            request.RequestFormat = DataFormat.Xml;
            request.AddBody(Content);

            IResult<T?> result = await RunAsync(client.PutAsync<T>(request));

            return result.SelectResult((response) => response == null ? Result.Fail<T>(new InvalidOperationException("Result is null")) : Result.Success<T>(response)
                , (ex) => ex);

        }

        protected async Task<IResult<T>> PostAsync<T>(string Resource, object Content)
        {
            RestClient? client = PageManager?.GetPage<ConnectionPage>()?.Client;
            if (client == null) throw new InvalidOperationException("Cannot get REST client");
            if (Content == null) throw new ArgumentNullException(nameof(Content));

            RestRequest request = new RestRequest(Resource, Method.Post);
            request.RequestFormat = DataFormat.Xml;
            request.AddBody(Content);

            IResult<T?> result = await RunAsync(client.PostAsync<T>(request));

            return result.SelectResult((response) => response == null ? Result.Fail<T>(new InvalidOperationException("Result is null")) : Result.Success<T>(response)
                , (ex) => ex);

        }
        protected async Task<IResult<T>> PostAsync<T>(string Resource)
        {
            RestClient? client = PageManager?.GetPage<ConnectionPage>()?.Client;
            if (client == null) throw new InvalidOperationException("Cannot get REST client");

            RestRequest request = new RestRequest(Resource, Method.Post);
            request.RequestFormat = DataFormat.Xml;
           
            IResult<T?> result = await RunAsync(client.PostAsync<T>(request));

            return result.SelectResult((response) => 
            response == null ? Result.Fail<T>(new InvalidOperationException("Result is null")) : Result.Success<T>(response)
                , (ex) => ex);

        }
        protected async Task<IResult<T>> DeleteAsync<T>(string Resource)
        {
            RestClient? client = PageManager?.GetPage<ConnectionPage>()?.Client;
            if (client == null) throw new InvalidOperationException("Cannot get REST client");

            RestRequest request = new RestRequest(Resource, Method.Delete);
            request.RequestFormat = DataFormat.Xml;

            IResult<T?> result = await RunAsync(client.DeleteAsync<T>(request));

            return result.SelectResult((response) =>
            response == null ? Result.Fail<T>(new InvalidOperationException("Result is null")) : Result.Success<T>(response)
                , (ex) => ex);

        }
    }
}
