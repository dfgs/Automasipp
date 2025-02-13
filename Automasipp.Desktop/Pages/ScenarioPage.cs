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


        private string scenarioName;

        public override string Name => "Scenario";

        public ScenarioPage(string ScenarioName):base()
        {
            this.scenarioName = ScenarioName;
        }


        protected override async Task OnLoadAsync()
        {
            Scenario response = await GetAsync<Scenario>($"Scenario/{scenarioName}");
            
            
        }


    }
}
