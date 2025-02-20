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
    public class ReportsPage : RESTPage
    {
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(Report), typeof(ReportsPage), new PropertyMetadata(null));
        public Report SelectedItem
        {
            get { return (Report)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }



        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register("Items", typeof(List<Report>), typeof(ReportsPage), new PropertyMetadata(null));
        public List<Report> Items
        {
            get { return (List<Report>)GetValue(ItemsProperty); }
            private set { SetValue(ItemsProperty, value); }
        }



        public override string Name => "Reports";
        private string scenarioName;
        private int pid;

        public ReportsPage(string ScenarioName,int PID) : base()
        {
            this.scenarioName = ScenarioName;this.pid = PID;

        }
        protected override async Task<bool> OnLoadAsync()
        {
            if (PageManager == null) throw new ArgumentException("Page manager is not defined");

            
            IResult<Report[]> response = await GetAsync<Report[]>($"Report/{scenarioName}/{pid}");
            return response.Match((items) => Items = new List<Report>(items), (ex) => throw ex);
        }


       

    }
}
