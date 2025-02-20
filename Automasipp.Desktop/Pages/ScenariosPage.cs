using Automasipp.Desktop.ViewModels;
using Automasipp.Models;
using RestSharp;
using ResultTypeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Session = Automasipp.Models.Session;

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




        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register("Items", typeof(List<ScenarioLink>), typeof(ScenariosPage), new PropertyMetadata(null));
        public List<ScenarioLink> Items
        {
            get { return (List<ScenarioLink>)GetValue(ItemsProperty); }
            private set { SetValue(ItemsProperty, value); }
        }



        public static readonly DependencyProperty OpenScenarioCommandProperty = DependencyProperty.Register("OpenScenarioCommand", typeof(PageCommand), typeof(ScenariosPage), new PropertyMetadata(null));
        public PageCommand OpenScenarioCommand
        {
            get { return (PageCommand)GetValue(OpenScenarioCommandProperty); }
            set { SetValue(OpenScenarioCommandProperty, value); }
        }

        public static readonly DependencyProperty StartSessionCommandProperty = DependencyProperty.Register("StartSessionCommand", typeof(PageCommand), typeof(ScenariosPage), new PropertyMetadata(null));
        public PageCommand StartSessionCommand
        {
            get { return (PageCommand)GetValue(StartSessionCommandProperty); }
            set { SetValue(StartSessionCommandProperty, value); }
        }

        public static readonly DependencyProperty OpenSessionsCommandProperty = DependencyProperty.Register("OpenSessionsCommand", typeof(PageCommand), typeof(ScenariosPage), new PropertyMetadata(null));
        public PageCommand OpenSessionsCommand
        {
            get { return (PageCommand)GetValue(OpenSessionsCommandProperty); }
            set { SetValue(OpenSessionsCommandProperty, value); }
        }



        public override string Name => "Scenarios";

        public ScenariosPage():base()
        {
            OpenScenarioCommand = new PageCommand(this, (_) => SelectedItem != null, (parameter) => OpenScenarioAsync());
            OpenSessionsCommand = new PageCommand(this, (_) => SelectedItem != null, (_) => OpenSessionsAsync());
            StartSessionCommand = new PageCommand(this, (_) => SelectedItem != null, (_) => StartSessionAsync());
        }


        protected override async Task<bool> OnLoadAsync()
        {
            if (PageManager == null) throw new ArgumentException("Page manager is not defined");

            IResult<string[]> response = await GetAsync<string[]>("Scenario/names");
            return response.Match((items) => Items = new List<ScenarioLink>(items.Select(item => new ScenarioLink(PageManager, this) { ScenarioName = item })), (ex) => throw ex);
        }

        private async Task OpenScenarioAsync()
        {
            ScenarioPage scenarioPage;

            if (PageManager == null) return;

            scenarioPage = new ScenarioPage(SelectedItem.ScenarioName);
            await PageManager.OpenPageAsync(scenarioPage);
        }

        private async Task OpenSessionsAsync()
        {
            SessionsPage sessionsPage;

            if (PageManager == null) return;

            sessionsPage = new SessionsPage(SelectedItem.ScenarioName);
            await PageManager.OpenPageAsync(sessionsPage);
        }


        private async Task StartSessionAsync()
        {
            SessionsPage sessionsPage;
            IResult<Session> result;

            if (PageManager == null) return;

            
            result=await PostAsync<Session>($"Session/{SelectedItem.ScenarioName}");
            if (!result.Succeeded())
            {
                ErrorMessage = "Cannot start session";
                return;
            }
                

            sessionsPage = new SessionsPage(SelectedItem.ScenarioName);
            await PageManager.OpenPageAsync(sessionsPage);
        }




    }
}
