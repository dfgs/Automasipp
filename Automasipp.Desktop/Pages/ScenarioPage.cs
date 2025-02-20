using Automasipp.Models;
using RestSharp;
using ResultTypeLib;
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

        public static readonly DependencyProperty ScenarioProperty = DependencyProperty.Register("Scenario", typeof(Scenario), typeof(ScenarioPage), new PropertyMetadata(null));
        public Scenario Scenario
        {
            get { return (Scenario)GetValue(ScenarioProperty); }
            set { SetValue(ScenarioProperty, value); }
        }


        public static readonly DependencyProperty SaveCommandProperty = DependencyProperty.Register("SaveCommand", typeof(PageCommand), typeof(ScenarioPage), new PropertyMetadata(null));
        public PageCommand SaveCommand
        {
            get { return (PageCommand)GetValue(SaveCommandProperty); }
            set { SetValue(SaveCommandProperty, value); }
        }

       


        private string scenarioName;

        public override string Name => "Scenario";

        public ScenarioPage(string ScenarioName):base()
        {
            this.scenarioName = ScenarioName;

            this.SaveCommand = new PageCommand(this, (_) => true, (_) => SaveCommandExecutedAsync());
        }
        protected override async Task<bool> OnLoadAsync()
        {
            if (PageManager == null) throw new ArgumentException("Page manager is not defined");

            IResult<Scenario> response = await GetAsync<Scenario>($"Scenario/{scenarioName}");
            return response.Match((scenario) => this.Scenario = scenario, (ex) => throw ex);
        }


        private async Task SaveCommandExecutedAsync()
        {
            await  PutAsync<bool>($"Scenario/{scenarioName}", Scenario);
            await Task.Delay(1000);
        }

       

    }
}
