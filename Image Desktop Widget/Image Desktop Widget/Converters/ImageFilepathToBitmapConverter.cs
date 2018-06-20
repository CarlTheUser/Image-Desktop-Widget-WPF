using Logging;
using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

using ViewComponent;

namespace Image_Desktop_Widget.Converters
{
    public class ImageFilepathToBitmapConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null)
            {
                if(value.GetType() == typeof(string))
                {

                    string filePath = value as string;

                    if (filePath.Length > 0)
                    {

                        //I'll need to do this if I want to delete the image
                        //otherwise it'll throw an exception from an unreleased resource
                        BitmapImage bitmap = new BitmapImage();

                        try
                        {
                            bitmap.BeginInit();
                            bitmap.CacheOption = BitmapCacheOption.OnLoad;
                            bitmap.UriSource = new Uri(filePath);
                            bitmap.EndInit();
                        }
                        catch (FileNotFoundException ex)
                        {
                            new MessagePopupNotification().Notify("Image file not found for " + ((string)value));

                            return Binding.DoNothing;
                        }
                        catch (Exception ex)
                        {
                            new MessagePopupNotification().Notify("Some error occured. Unable to load file " + (string)value);

                            ExceptionLogger exceptionLogger = new ExceptionLogger(TextLogger.GetInstance(DataLayer.Configuration.Instance.TextLogsPath));

                            exceptionLogger.LogException(ex);

                            return Binding.DoNothing;
                        }
                        return bitmap;
                    }

                }
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private class MessagePopupNotification : INotification
        {
            public void Notify(string message)
            {
                UserInteraction.MessageBox.Show(message);
            }
        }

    }
}
