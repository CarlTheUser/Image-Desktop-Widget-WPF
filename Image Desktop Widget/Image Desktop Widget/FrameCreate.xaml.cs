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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ViewComponent;

namespace Image_Desktop_Widget
{
    /// <summary>
    /// Interaction logic for FrameCreate.xaml
    /// </summary>
    public partial class FrameCreate : Window, IApplicationView, IClosable
    {
        public FrameCreate()
        {
            InitializeComponent();
        }

        public BaseViewModel GetModel()
        {
            return VM;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Keyboard.ClearFocus();
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
    }
}
