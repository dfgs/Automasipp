using Automasipp.Desktop.ViewModels;
using Automasipp.Models;
using RestSharp;
using ResultTypeLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Automasipp.Desktop.Pages
{
    public class SessionsPage : RESTPage
    {
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(Session), typeof(SessionsPage), new PropertyMetadata(null));
        public Session SelectedItem
        {
            get { return (Session)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }



        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register("Items", typeof(ObservableCollection<Session>), typeof(SessionsPage), new PropertyMetadata(null));
        public ObservableCollection<Session> Items
        {
            get { return (ObservableCollection<Session>)GetValue(ItemsProperty); }
            private set { SetValue(ItemsProperty, value); }
        }




        public static readonly DependencyProperty OpenReportsCommandProperty = DependencyProperty.Register("OpenReportsCommand", typeof(PageCommand), typeof(SessionsPage), new PropertyMetadata(null));
        public PageCommand OpenReportsCommand
        {
            get { return (PageCommand)GetValue(OpenReportsCommandProperty); }
            set { SetValue(OpenReportsCommandProperty, value); }
        }

        public static readonly DependencyProperty DeleteSessionCommandProperty = DependencyProperty.Register("DeleteSessionCommand", typeof(PageCommand), typeof(SessionsPage), new PropertyMetadata(null));
        public PageCommand DeleteSessionCommand
        {
            get { return (PageCommand)GetValue(DeleteSessionCommandProperty); }
            set { SetValue(DeleteSessionCommandProperty, value); }
        }


        public override string Name => "Sessions";
        private string scenarioName;

        public SessionsPage(string ScenarioName) : base()
        {
            this.scenarioName = ScenarioName;
            OpenReportsCommand = new PageCommand(this, (_) => SelectedItem != null, (_) => OpenReportsAsync());
            DeleteSessionCommand = new PageCommand(this, (_) => SelectedItem != null, (_) => DeleteSessionAsync());

        }
        protected override async Task<bool> OnLoadAsync()
        {
            if (PageManager == null) throw new ArgumentException("Page manager is not defined");

            
            IResult<Session[]> response = await GetAsync<Session[]>($"Session/{scenarioName}");
            return response.Match((items) => Items = new ObservableCollection<Session>(items), (ex) => throw ex);
        }

        private async Task OpenReportsAsync()
        {
            ReportsPage reportsPage;

            if (PageManager == null) return;

            reportsPage = new ReportsPage(SelectedItem.ScenarioName,SelectedItem.PID);
            await PageManager.OpenPageAsync(reportsPage);
        }
        private async Task DeleteSessionAsync()
        {
            IResult<bool> result;

            if (PageManager == null) return;

            result = await DeleteAsync<bool>($"Session/{SelectedItem.ScenarioName}/{SelectedItem.PID}");
            if (!result.Succeeded())
            {
                ErrorMessage = "Cannot delete session";
                return;
            }
            else Items.Remove(SelectedItem);
        }

    }
}
