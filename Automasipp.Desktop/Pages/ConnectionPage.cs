using RestSharp;
using RestSharp.Serializers;
using RestSharp.Serializers.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Automasipp.Desktop.Pages
{
    public class ConnectionPage:Page
    {

        private RestClient? client;
        public RestClient? Client
        {
            get => client;
        }

        public static readonly DependencyProperty PortProperty = DependencyProperty.Register("Port", typeof(int), typeof(ConnectionPage), new PropertyMetadata(5000));
        public int Port
        {
            get { return (int)GetValue(PortProperty); }
            set { SetValue(PortProperty, value); }
        }



        public static readonly DependencyProperty HostnameProperty = DependencyProperty.Register("Hostname", typeof(string), typeof(ConnectionPage), new PropertyMetadata("10.0.10.12"));
        public string Hostname
        {
            get { return (string)GetValue(HostnameProperty); }
            set { SetValue(HostnameProperty, value); }
        }




        public static readonly DependencyProperty ConnectCommandProperty = DependencyProperty.Register("ConnectCommand", typeof(PageCommand), typeof(ConnectionPage), new PropertyMetadata(null));
        public PageCommand ConnectCommand
        {
            get { return (PageCommand)GetValue(ConnectCommandProperty); }
            set { SetValue(ConnectCommandProperty, value); }
        }



        public override string Name => "Connection settings";

        public ConnectionPage():base()
        {          
            ConnectCommand=new PageCommand(this,(_)=>true,(_) => ConnectCommandExecutedAsync());
        }

        protected override async Task OnLoadAsync()
        {
            await Task.Yield();
        }

        private async Task ConnectCommandExecutedAsync()
        {
            if (PageManager==null) return;

            RestClientOptions options;
            options = new RestClientOptions($"https://{Hostname}:{Port}/") { FollowRedirects=true, RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true };
            // json serializer doesn't work with nested class (Command)
            client = new RestClient(options, configureSerialization: s => s.UseXml());

            await PageManager.OpenPageAsync(new ScenariosPage());
        }

        
        protected override void OnDispose()
        {
            base.OnDispose();
            if (client!=null) client.Dispose();
        }

    }
}
