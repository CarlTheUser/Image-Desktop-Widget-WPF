using Image_Desktop_Widget.Command;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;

namespace Image_Desktop_Widget.ViewModels
{
    public class ThemePageViewModel : BaseViewModel
    {

        public ObservableCollection<Color> ThemeColors { get; private set; }

        public ICommand SetColorCommand { get; private set; }

        public ThemePageViewModel()
        {
            ThemeColors = new ObservableCollection<Color>(ThemeOption.GetThemeColors());

            SetColorCommand = new RelayParameterizedCommand(SetColor);
        }

        private void SetColor(object value)
        {
            if(value != null)
            {

                if(value.GetType() == typeof(Color))
                {
                    Properties.Settings.Default.ThemeColor = (Color)value;
                    Properties.Settings.Default.Save();
                }

            }
        }
    }
}
