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



        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(ScenarioLink), typeof(ScenarioPage), new PropertyMetadata(null));
        public ScenarioLink SelectedItem
        {
            get { return (ScenarioLink)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }




        public static readonly DependencyProperty NamesProperty = DependencyProperty.Register("Items", typeof(List<ScenarioLink>), typeof(ScenariosPage), new PropertyMetadata(null));
        public List<ScenarioLink> Items
        {
            get { return (List<ScenarioLink>)GetValue(NamesProperty); }
            private set { SetValue(NamesProperty, value); }
        }



        public static readonly DependencyProperty OpenSessionsCommandProperty = DependencyProperty.Register("OpenSessionsCommand", typeof(PageCommand), typeof(ScenariosPage), new PropertyMetadata(null));
        public PageCommand OpenSessionsCommand
        {
            get { return (PageCommand)GetValue(OpenSessionsCommandProperty); }
            set { SetValue(OpenSessionsCommandProperty, value); }
        }
        public static readonly DependencyProperty OpenScenarioCommandProperty = DependencyProperty.Register("OpenScenarioCommand", typeof(PageCommand), typeof(ScenariosPage), new PropertyMetadata(null));
        public PageCommand OpenScenarioCommand
        {
            get { return (PageCommand)GetValue(OpenScenarioCommandProperty); }
            set { SetValue(OpenScenarioCommandProperty, value); }
        }




        public override string Name => "Scenarios";

        public ScenariosPage():base()
        {
            OpenScenarioCommand = new PageCommand(this, (_) => SelectedItem != null, (parameter) => OpenScenarioAsync());
            OpenSessionsCommand = new PageCommand(this,(_) => SelectedItem != null, (_) => OpenSessionAsync());
        }


        protected override async Task OnLoadAsync()
        {
            if (PageManager == null) return;

            //await Task.Delay(10000);
            string[] response = await GetAsync<string[]>("Scenario/names");
            Items = new List<ScenarioLink>(response.Select(item=> new ScenarioLink(PageManager,this) { ScenarioName=item}));
        }
        private async Task OpenSessionAsync()
        {
            SessionsPage sessionsPage;

            if (PageManager == null) return;

            sessionsPage = new SessionsPage(SelectedItem.ScenarioName);
            await PageManager.OpenPageAsync(sessionsPage);
        }
        private async Task OpenScenarioAsync()
        {
            ScenarioPage scenarioPage;

            if (PageManager == null) return;

            scenarioPage = new ScenarioPage(SelectedItem.ScenarioName);
            await PageManager.OpenPageAsync(scenarioPage);
        }

    }
}
