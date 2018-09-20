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

            SetColorCommand = new RelayParameterizedCommand<Color>(SetColor);
        }

        private void SetColor(Color value)
        {
            if(value != null)
            {
                Properties.Settings.Default.ThemeColor = value;
                Properties.Settings.Default.Save();
            }
        }
    }
}
