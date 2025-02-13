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

        private Func<object?,bool> canExecuteHandler;
        private Action<object?> executeHandler;

        public PageCommand( Func<object?,bool> CanExecuteHandler, Action<object?> ExecuteHandler )
        {
            this.canExecuteHandler= CanExecuteHandler;
            this.executeHandler= ExecuteHandler;
        }

  
        public bool CanExecute(object? parameter)
        {
            return canExecuteHandler(parameter);
        }

        public void Execute(object? parameter)
        {
            executeHandler(parameter);
        }


    }
}
