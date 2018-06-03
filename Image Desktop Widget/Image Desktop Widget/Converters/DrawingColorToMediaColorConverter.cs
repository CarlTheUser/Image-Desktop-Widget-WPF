using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Image_Desktop_Widget.Converters
{
    public class DrawingColorToMediaColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null)
            {
                if(value.GetType() == typeof(System.Drawing.Color))
                {
                    System.Drawing.Color fromColor = (System.Drawing.Color)value;

                    System.Windows.Media.Color color = new System.Windows.Media.Color()
                    {
                        A = fromColor.A,
                        R = fromColor.R,
                        B = fromColor.B,
                        G = fromColor.G
                    };

                    return color;
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
