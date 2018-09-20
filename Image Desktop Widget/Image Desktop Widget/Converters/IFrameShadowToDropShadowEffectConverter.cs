using Image_Desktop_Widget.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Image_Desktop_Widget.Converters
{
    class IFrameShadowToDropShadowEffectConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null && value.GetType() == typeof(ImageFrameModel.Shadow))
            {
                ImageFrameModel.Shadow shadow = (ImageFrameModel.Shadow)value;
                if (shadow.Enabled)
                {
                    return new DropShadowEffect()
                    {
                        Opacity = shadow.Opacity,
                        ShadowDepth = shadow.Depth,
                        Direction = shadow.Direction,
                        BlurRadius = shadow.BlurRadius,
                        Color = Colors.Black
                    };
                }
                else return null;
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
