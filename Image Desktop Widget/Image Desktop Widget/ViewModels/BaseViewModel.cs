using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Image_Desktop_Widget.ViewModels
{

    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        private bool ParametersReceived = false;

        private IDictionary<string, object> parameters = null;

        public IDictionary<string, object> Parameters
        {
            protected get => parameters;
            set
            {
                if(!ParametersReceived)     //Parameters can only be set once
                {                           //and ignore succeeding  set unles null
                    if (value != null)
                    {
                        OnParametersReceived(parameters = value);
                        ParametersReceived = true;
                    }
                }
            }
        }


        protected virtual void OnParametersReceived(IDictionary<string, object> param)
        {

        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

    }
}