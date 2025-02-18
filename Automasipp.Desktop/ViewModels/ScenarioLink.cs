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

        

        private IPageManager pageManager;

        public ScenarioLink(IPageManager PageManager,IPage Page)
        {
            this.pageManager = PageManager; 
        }

        private async Task OpenAsync()
        {
            await pageManager.OpenPageAsync(new ScenarioPage(ScenarioName));

        }

    }
}
