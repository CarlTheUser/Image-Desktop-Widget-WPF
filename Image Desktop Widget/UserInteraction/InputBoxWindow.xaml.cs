using System.Windows;

namespace UserInteraction
{
    /// <summary>
    /// Interaction logic for InputBoxWindow.xaml
    /// </summary>
    public sealed partial class InputBoxWindow : Window
    {

        public string UserInput => PromptMessageTextBlock.Text;

        internal InputBoxWindow(string prompt, string caption, bool filterInput = false)
        {
            InitializeComponent();
            this.Title = caption;
            PromptMessageTextBlock.Text = prompt;

        }

        private void ButtonAccept_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
