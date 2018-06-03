using Image_Desktop_Widget.Command;
using System.Windows.Input;

namespace Image_Desktop_Widget.ViewModels
{
    public class AboutPageViewModel  : BaseViewModel
    {
        private readonly string link = @"https://www.facebook.com/carlalwen.andres";

        public ICommand OpenLinkCommand { get; private set; }

        public AboutPageViewModel()
        {
            OpenLinkCommand = new RelayCommand(openLink);
        }

        private void openLink()
        {
            System.Diagnostics.Process.Start(link);
        }
    }
}
