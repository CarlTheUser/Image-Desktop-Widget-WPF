using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace UserInteraction
{
    /// <summary>
    /// Interaction logic for MessageBox.xaml
    /// </summary>
    public sealed partial class MessageBoxWindow : Window
    {


        public MessageBoxResult MessageBoxResult { get; private set; }

        internal MessageBoxWindow(string messageBoxText, string caption, MessageBoxButton messageBoxButtons)
        {
            InitializeComponent();

            this.Title = caption;

            Message.Text = messageBoxText;

            Button[] buttons = CreateMessageBoxButtons(messageBoxButtons);

            foreach (Button button in buttons)
            {
                ButtonContainer.Children.Add(button);
            }
        }

        private void ok(object sender, RoutedEventArgs e)
        {
            MessageBoxResult = MessageBoxResult.OK;
            this.Close();
        }

        private void cancel(object sender, RoutedEventArgs e)
        {
            MessageBoxResult = MessageBoxResult.Cancel;
            this.Close();
        }
        private void yes(object sender, RoutedEventArgs e)
        {
            MessageBoxResult = MessageBoxResult.Yes;
            this.Close();
        }

        private void no(object sender, RoutedEventArgs e)
        {
            MessageBoxResult = MessageBoxResult.No;
            this.Close();
        }

        private Button[] CreateMessageBoxButtons(MessageBoxButton messageBoxButtons)
        {

            Button[] buttons = null;

            Button okButton = null;
            Button cancelButton = null;
            Button yesButton = null;
            Button noButton = null;

            switch (messageBoxButtons)
            {
                case MessageBoxButton.OK:
                    okButton = new Button();
                    okButton.Content = "Ok";
                    okButton.Click += ok;
                    buttons = new Button[] { okButton };
                    break;
                case MessageBoxButton.OKCancel:
                    okButton = new Button();
                    okButton.Content = "Ok";
                    okButton.Click += ok;
                    cancelButton = new Button();
                    cancelButton.Content = "Cancel";
                    cancelButton.Click += cancel;
                    buttons = new Button[] { okButton, cancelButton };
                    break;
                case MessageBoxButton.YesNoCancel:
                    yesButton = new Button();
                    yesButton.Content = "Yes";
                    yesButton.Click += yes;
                    noButton = new Button();
                    noButton.Content = "No";
                    noButton.Click += no;
                    cancelButton = new Button();
                    cancelButton.Content = "Cancel";
                    cancelButton.Click += cancel;
                    buttons = new Button[] { yesButton, noButton, cancelButton };
                    break;
                case MessageBoxButton.YesNo:
                    yesButton = new Button();
                    yesButton.Content = "Yes";
                    yesButton.Click += yes;
                    noButton = new Button();
                    noButton.Content = "No";
                    noButton.Click += no;
                    buttons = new Button[] { yesButton, noButton };
                    break;
            }

            return buttons;
        }

        private class MessageBoxWindowViewModel : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            private MessageBoxResult messageBoxResult;

            public MessageBoxResult MessageBoxResult
            {
                get => messageBoxResult;
                set
                {
                    messageBoxResult = value;
                    OnPropertyChanged("MessageBoxResult");
                }
            }

            private string title;

            public string Title
            {
                get => title;
                set
                {
                    title = value;
                    OnPropertyChanged("Title");
                }
            }

            private string message;

            public string Message
            {
                get => message;
                set
                {
                    message = value;
                    OnPropertyChanged("Message");
                }
            }

            public MessageBoxWindowViewModel()
            {
                messageBoxResult = MessageBoxResult.None;
            }

            private void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
