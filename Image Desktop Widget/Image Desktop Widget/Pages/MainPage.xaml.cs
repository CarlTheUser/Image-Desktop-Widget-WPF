using Image_Desktop_Widget.ViewModels;
using System.Linq;
using System.Windows;

namespace Image_Desktop_Widget.Pages
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : BasePage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public override BaseViewModel GetModel()
        {
            return VM;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var imageFrames = Application.Current.Windows.OfType<ImageFrame>();
            
            if(imageFrames != null)
            {
                foreach (var frame in imageFrames) frame.Close();
            }
        }

        private void BasePage_DragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }

        private void BasePage_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string file in files) ((MainPageViewModel)DataContext).OpenImage(file);
            }
            
        }

        


    }
}
