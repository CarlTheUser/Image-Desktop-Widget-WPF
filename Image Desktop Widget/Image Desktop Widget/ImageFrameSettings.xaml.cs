using Image_Desktop_Widget.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ViewComponent;

namespace Image_Desktop_Widget
{
    /// <summary>
    /// Interaction logic for ImageFrameSettings.xaml
    /// </summary>
    public partial class ImageFrameSettings : Window, IApplicationView, IClosable
    {
        public ImageFrameSettings()
        {
            InitializeComponent();
            
        }

        public BaseViewModel GetModel()
        {
            return VM;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        
    }
}
