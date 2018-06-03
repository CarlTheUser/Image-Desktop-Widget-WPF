using Image_Desktop_Widget.Command;
using Image_Desktop_Widget.Pages;
using System.Windows.Input;

namespace Image_Desktop_Widget.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        
        private ApplicationPage currentPage = ApplicationPage.MainPage;

        public ApplicationPage CurrentPage
        {
            get { return currentPage; }
            set
            {
                currentPage = value;
                OnPropertyChanged("CurrentPage");
            }
        }

        public ICommand ChangePageCommand { get; private set; }

        public MainWindowViewModel()
        {
            ChangePageCommand = new RelayParameterizedCommand(ChangePage);
        }

        private void ChangePage(object newPage)
        {
            if (newPage != null)
            {
                if (newPage.GetType() == typeof(ApplicationPage))
                {
                    ApplicationPage page = (ApplicationPage)newPage;
                    if (CurrentPage != page) CurrentPage = page; 
                }
            }
        }



    }
}
