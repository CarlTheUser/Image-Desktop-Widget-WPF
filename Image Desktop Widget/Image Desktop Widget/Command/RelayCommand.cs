using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Image_Desktop_Widget.Command
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged = (s, e) => { };

        private Action action;

        public Func<bool> CanExecuteFunc { get; set; } = null;

        public RelayCommand(Action action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter)
        {
            return CanExecuteFunc == null ? true : CanExecuteFunc.Invoke();
        }

        public void Execute(object parameter)
        {
            action.Invoke();
        }
    }
}
