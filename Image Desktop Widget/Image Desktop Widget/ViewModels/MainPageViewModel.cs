using Image_Desktop_Widget.Command;
using Microsoft.Win32;
using System;
using System.Windows.Input;
using ViewComponent;
using Image_Desktop_Widget.Model;

namespace Image_Desktop_Widget.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {

        private bool allowOnStartup;

        public bool AllowOnStartup
        {
            get => allowOnStartup;
            set
            {
                allowOnStartup = value;
                OnPropertyChanged("AllowOnStartup");
            }
        }

        public ICommand OpenImageCommand { get; private set; }

        public ICommand AllowStartupCommand { get; private set; }
        
        #region Wrapper Items

        private StartupBehavior startupBehavior;

        private FrameCreateViewLauncher frameCreateViewLauncher;

        private INotification popupMessage;

        private INotification<string> imagePicker;

        #endregion

        public MainPageViewModel()
        {
            OpenImageCommand = new RelayCommand(PickImage);
            AllowStartupCommand = new RelayCommand(AllowStartup);

            startupBehavior = new WindowsStartupBehavior();
            startupBehavior.RunAtStartupSet += MainPageViewModel_RunAtStartupSet;
            
            allowOnStartup = Properties.Settings.Default.AllowStartup;

            popupMessage = new PopupMessageNotification();

            imagePicker = new ImagePicker();

            frameCreateViewLauncher = new FrameCreateViewLauncher();

        }

        private void MainPageViewModel_RunAtStartupSet(object sender, WindowsStartupBehavior.StartupBehaviorEventArgs e)
        {

            if (e.StartupEnabledChanged)
            {
                if (e.CurrentStartupValue)
                {
                    popupMessage.Notify("Startup changed successfuly.");
                    popupMessage.Notify("Setting this as a startup item writes key to registry; "
                    + "If you're planning to delete this application make sure to disable the startup setting first "
                    + @"to avoid invalid registry keys (or manually delete at Computer\HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run\Image Desktop Widget.)");
                }

            }
            else
            {
                AllowOnStartup = startupBehavior.IsRunAtStartup;
                popupMessage.Notify("Setting startup value failed :(\nClose this app and try running it as an Administrator.");
            }

        }

        private void AllowStartup()
        {
            startupBehavior.RunAtStartup(AllowOnStartup);
        }

        private void PickImage()
        {
            string imageFilePath = imagePicker.Notify("Select an image");

            OpenImage(imageFilePath);
            
        }

        public void OpenImage(String imageFilePath)
        {
            if (imageFilePath.Trim().Length > 0)
            {
                ImageFrameModel model = ImageFrameModel.NewInstance();

                model.ImagePath = imageFilePath;

                frameCreateViewLauncher.ViewModel = new FrameCreateViewModel(imageFilePath);

                frameCreateViewLauncher.Launch();
            }
        }


        //Wrapper classes

        private class FrameCreateViewLauncher : IViewLauncher
        {
            public FrameCreateViewModel ViewModel { get; set; }
            
            public FrameCreateViewLauncher() { }

            public void Launch()
            {
                FrameCreate frameCreate = new FrameCreate()
                {
                    DataContext = ViewModel
                };
                ViewModel.Closable = frameCreate;
                frameCreate.ShowDialog();
            }
        }

        private class ImagePicker : INotification<string>
        {
            public string Notify(string message)
            {
                string filePath = string.Empty;

                OpenFileDialog op = new OpenFileDialog();
                op.Title = message;
                op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                  "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                  "Portable Network Graphic (*.png)|*.png";
                if (op.ShowDialog() == true)
                {
                    filePath = op.FileName;
                }

                return filePath;
            }
        }

        private class PopupMessageNotification : INotification
        {
            public void Notify(string message)
            {
                UserInteraction.MessageBox.Show(message, "Image Desktop Widget");
            }
        }
    }
}
