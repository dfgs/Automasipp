using Automasipp.Desktop.ViewModels;
using Automasipp.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Automasipp.Desktop.Pages
{
    public class SessionsPage : RESTPage
    {

        public static readonly DependencyProperty NamesProperty = DependencyProperty.Register("Items", typeof(List<Session>), typeof(SessionsPage), new PropertyMetadata(null));
        public List<Session> Items
        {
            get { return (List<Session>)GetValue(NamesProperty); }
            private set { SetValue(NamesProperty, value); }
        }



        public override string Name => "Sessions";
        private string scenarioName;

        public SessionsPage(string ScenarioName) : base()
        {
            this.scenarioName = ScenarioName;

        }


        protected override async Task OnLoadAsync()
        {
            if (PageManager == null) return;

            //await Task.Delay(10000);
            Session[] response = await GetAsync<Session[]>($"Session/{scenarioName}");
            Items = new List<Session>(response);
        }

       

    }
}
