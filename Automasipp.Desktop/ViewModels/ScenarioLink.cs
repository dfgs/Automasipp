using Automasipp.Desktop.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Automasipp.Desktop.ViewModels
{
    public class ScenarioLink 
    {
        public required string ScenarioName { get; set; }

        public PageCommand OpenCommand
        {
            get;
            private set;
        }

        private IPageManager pageManager;

        public ScenarioLink(IPageManager PageManager)
        {
            OpenCommand = new PageCommand((_) => true, Open);
            this.pageManager = PageManager; 
        }

        private async void Open(object? Parameter)
        {
            await pageManager.OpenPageAsync(new ScenarioPage(ScenarioName));

        }

    }
}
