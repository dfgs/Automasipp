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
    public class ScenarioPage : RESTPage
    {
 
      


        public override string Name => "Scenario";

        public ScenarioPage(IPageManager PageManager):base(PageManager)
        {
        }


        protected override async Task OnLoadAsync()
        {
            Scenario response = await GetAsync<Scenario>($"Scenario/names/test");
            
            
        }


    }
}
