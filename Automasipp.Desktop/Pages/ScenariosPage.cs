using Automasipp.Desktop.ViewModels;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Automasipp.Desktop.Pages
{
    public class ScenariosPage : RESTPage
    {

        public static readonly DependencyProperty NamesProperty = DependencyProperty.Register("Items", typeof(List<ScenarioLink>), typeof(ScenariosPage), new PropertyMetadata(null));
        public List<ScenarioLink> Items
        {
            get { return (List<ScenarioLink>)GetValue(NamesProperty); }
            private set { SetValue(NamesProperty, value); }
        }



        public override string Name => "Scenarios";

        public ScenariosPage():base()
        {
        }


        protected override async Task OnLoadAsync()
        {
            if (PageManager == null) return;

            //await Task.Delay(10000);
            string[] response = await GetAsync<string[]>("Scenario/names");
            Items = new List<ScenarioLink>(response.Select(item=> new ScenarioLink(PageManager,this) { ScenarioName=item}));
        }

       

    }
}
