using Image_Desktop_Widget.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Image_Desktop_Widget.Converters
{
    class ImageFrameShadowOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value.GetType() == typeof(ImageFrameModel.Shadow))
            {
                ImageFrameModel.Shadow shadow = (ImageFrameModel.Shadow)value;
                if (shadow.Enabled)
                {
                    return shadow.Opacity;
                }
                else return 0;
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
