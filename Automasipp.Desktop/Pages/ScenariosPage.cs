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



        public static readonly DependencyProperty NamesProperty = DependencyProperty.Register("Names", typeof(List<string>), typeof(ScenariosPage), new PropertyMetadata(null));
        public List<string> Names
        {
            get { return (List<string>)GetValue(NamesProperty); }
            private set { SetValue(NamesProperty, value); }
        }



        public override string Name => "Scenarios";

        public ScenariosPage(IPageManager PageManager):base(PageManager)
        {
        }


        protected override async Task OnLoadAsync()
        {
            string[] response = await GetAsync<string[]>("Scenario/names");
            Names = new List<string>(response);
        }

       

    }
}
