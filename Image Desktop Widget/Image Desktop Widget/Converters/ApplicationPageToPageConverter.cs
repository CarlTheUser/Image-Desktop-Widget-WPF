using Image_Desktop_Widget.Pages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Image_Desktop_Widget.Converters
{
    public class ApplicationPageToPageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null)
            {
                if(value.GetType()==typeof(ApplicationPage))
                {
                    ApplicationPage page = (ApplicationPage)value;

                    switch(page)
                    {
                        case ApplicationPage.MainPage: return new MainPage();
                        case ApplicationPage.ThemePage: return new ThemePage();
                        case ApplicationPage.AboutPage: return new AboutPage();
                    }
                }
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(parameter != null)
            {
                if (parameter.GetType() == typeof(MainPage)) return ApplicationPage.MainPage;
                if (parameter.GetType() == typeof(AboutPage)) return ApplicationPage.AboutPage;
            }
            
            return Binding.DoNothing;
        }
    }
}
