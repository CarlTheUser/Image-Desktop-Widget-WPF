using Image_Desktop_Widget.Command;
using Image_Desktop_Widget.Pages;
using System.Windows.Input;

namespace Image_Desktop_Widget.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {

        private ApplicationPage currentPage;

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
            CurrentPage = ApplicationPage.MainPage;
            ChangePageCommand = new RelayParameterizedCommand<ApplicationPage>(ChangePage);
        }

        private void ChangePage(ApplicationPage newPage)
        {
            if (newPage != CurrentPage)
            {
                CurrentPage = newPage;
            }
        }
    }
}
