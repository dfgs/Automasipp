using Automasipp.Desktop.ViewModels;
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
    public class SessionsPage : RESTPage
    {
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(Session), typeof(SessionsPage), new PropertyMetadata(null));
        public Session SelectedItem
        {
            get { return (Session)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }



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
        protected override async Task<bool> OnLoadAsync()
        {
            if (PageManager == null) throw new ArgumentException("Page manager is not defined");

            
            IResult<Session[]> response = await GetAsync<Session[]>($"Session/{scenarioName}");
            return response.Match((items) => Items = new List<Session>(items), (ex) => throw ex);
        }


       

    }
}
