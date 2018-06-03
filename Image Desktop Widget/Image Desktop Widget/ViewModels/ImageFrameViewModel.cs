using Image_Desktop_Widget.Command;
using System;
using System.Windows.Input;
using System.ComponentModel;
using ViewComponent;
using Logging;
using Image_Desktop_Widget.Model;

namespace Image_Desktop_Widget.ViewModels
{
    public class ImageFrameViewModel : BaseViewModel
    {
        public event EventHandler ImageDisposeRequested = (s, e) => { };
        
        public IClosable Closable { get; set; }

        public ImageFrameModel ImageFrameModel { get; private set; }

        public ICommand SaveStateCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand OpenImageCommand { get; private set; }
        public ICommand ShowImageInExplorerCommand { get; private set; }
        public ICommand OpenSettingsCommand { get; private set; }
        public ICommand ShowMainWindowCommand { get; private set; }

        

        private MessagePopupNotification notification;

        private IViewLauncher mainViewLauncher;

        private IViewLauncher settingsViewLauncher;

        private static readonly string DEFAULT_CAPTION = "Image frame";

        public ImageFrameViewModel(ImageFrameModel imageFrameModel)
        {
            ImageFrameModel = imageFrameModel;
            ImageFrameModel.ErrorOccured += ImageFrameModel_ErrorOccured;
            ImageFrameModel.Deleted += ImageFrameModel_Deleted;
            
            SaveStateCommand = new RelayCommand(SaveCurrentState);
            DeleteCommand = new RelayCommand(Delete);
            OpenImageCommand = new RelayCommand(OpenImage);
            ShowImageInExplorerCommand = new RelayCommand(ShowImageInExplorer);
            OpenSettingsCommand = new RelayCommand(OpenSettings);          
            ShowMainWindowCommand = new RelayCommand(ShowMainWindow);

            notification = new MessagePopupNotification() { Caption = DEFAULT_CAPTION };

            mainViewLauncher = new MainWindowViewLauncher();

            settingsViewLauncher = new SettingsViewLauncher(ImageFrameModel);
        }

        

        private void ShowMainWindow()
        {
            mainViewLauncher.Launch();
        }

        private void ImageFrameModel_Deleted(object sender, EventArgs e)
        {
            Closable.Close();
        }

        private void ImageFrameModel_ErrorOccured(object sender, Model.Model.ErrorOccuredEventArgs e)
        {
            notification.Caption = "An error occured";
            notification.Notify("Unable to save current state :(\n" + e.Message);
            notification.Caption = DEFAULT_CAPTION;
        }

        private void OpenSettings()
        {
            settingsViewLauncher.Launch();
        }

        private void OpenImage()
        {
            try
            {
                System.Diagnostics.Process.Start(ImageFrameModel.ImagePath);
            }
            catch(Win32Exception ex)
            {
                notification.Caption = "An error occured";
                notification.Notify("An error occured while opening the image. Image file not found in the directory.");
                notification.Caption = DEFAULT_CAPTION;
            }
            catch(Exception ex)
            {
                notification.Notify("Unknown error occured.");

                ILogger logger = TextLogger.GetInstance(DataLayer.Configuration.Instance.TextLogsPath);
                ExceptionLogger el = new ExceptionLogger(logger);
                el.LogException(ex);
            }
        }

        private void ShowImageInExplorer()
        {
            string imagePath = ImageFrameModel.ImagePath;
            if (!string.IsNullOrEmpty(imagePath.Trim()))
                System.Diagnostics.Process.Start("explorer.exe", "/select, \"" + imagePath + "\"");
        }

        private void SaveCurrentState()
        {
            if(ImageFrameModel.State != Model.Model.ModelState.Deleted) ImageFrameModel.ApplyEdit();
        }

        private void Delete()
        {
            ImageFrameModel.Delete();
        }

        private class SettingsViewLauncher : IViewLauncher
        {
            ImageFrameModel ImageFrameModel { get; set; }

            public SettingsViewLauncher(ImageFrameModel imageFrameModel)
            {
                ImageFrameModel = imageFrameModel;
            }

            public void Launch() => new ImageFrameSettings()
            {
                DataContext = new ImageFrameSettingViewModel(ImageFrameModel)
            }.ShowDialog();
        }

        private class MainWindowViewLauncher : IViewLauncher
        {
            public void Launch()
            {
                MainWindow.Instance.Show();
                MainWindow.Instance.Focus();
            }
        }

        private class MessagePopupNotification : INotification
        {
            public string Caption { get; set; }

            public void Notify(string message) => UserInteraction.MessageBox.Show(message, Caption);
        }

    }
}
