using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using GeneralMerchandise.UI.ViewModel;

namespace GeneralMerchandise.UI.Pages
{
    public abstract class BasePage : Page, IApplicationView
    {
        public abstract ViewModel.ViewModel GetViewModel();
    }
}
