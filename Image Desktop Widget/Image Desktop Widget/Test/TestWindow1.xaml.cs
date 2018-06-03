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

namespace Image_Desktop_Widget.Test
{
    /// <summary>
    /// Interaction logic for TestWindow1.xaml
    /// </summary>
    public partial class TestWindow1 : Window
    {
        public TestWindow1()
        {
            InitializeComponent();
            this.DataContext = new TestViewModel();
        }


        private class TestViewModel : BaseViewModel
        {
            public string Title { get; set; } = "Custom title";
        }
    }
}
