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

            this.SaveCommand = new PageCommand((_) => true, SaveCommandExecuted);
        }


        protected override async Task OnLoadAsync()
        {
            Scenario response = await GetAsync<Scenario>($"Scenario/{scenarioName}");
            
            this.Scenario=response;
                        
        }
        private async void SaveCommandExecuted(object? parameter)
        {

        }


    }
}
