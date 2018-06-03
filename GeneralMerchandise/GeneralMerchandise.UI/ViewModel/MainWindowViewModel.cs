using GeneralMerchandise.UI.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.UI.ViewModel
{
    public class MainWindowViewModel : ViewModel
    {

        private static readonly string CURRENTPAGE_PROPERTY_NAME = "CurentPage";

        private ApplicationPage currentPage;

        public ApplicationPage CurrentPage
        {
            get => currentPage;
            set
            {
                if (currentPage == value) return;
                currentPage = value;
                OnPropertyChanged(CURRENTPAGE_PROPERTY_NAME);
            }
        }

        private string foo = "Initial Value";

        public string Foo
        {
            get => foo;
            set
            {
                foo = value;
                OnPropertyChanged("Foo");
            }
        }

        




    }
}
