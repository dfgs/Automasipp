using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Automasipp.Desktop
{
    public class PageCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private IPage page;

        private Func<object?,bool> canExecuteHandler;
        private Func<Task> executeHandler;

        public PageCommand(IPage Page, Func<object?,bool> CanExecuteHandler,Func<Task> ExecuteHandler )
        {
            this.page = Page;
            this.canExecuteHandler= CanExecuteHandler;
            this.executeHandler= ExecuteHandler;
        }

  
        public bool CanExecute(object? parameter)
        {
            return canExecuteHandler(parameter);
        }

        public async void Execute(object? parameter)
        {
            await page.RunAsync(executeHandler());
        }


    }
}
