using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.UI.ViewModel
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool parameterSet = false;

        private IDictionary<int, object> parameters;

        public IDictionary<int, object> Parameters
        {
            protected get => parameters;
            set
            {
                if (!parameterSet)
                {
                    parameters = value;
                    parameterSet = true;
                }
            }
        }



    }
}
