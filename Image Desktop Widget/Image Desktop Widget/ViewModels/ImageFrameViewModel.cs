using Image_Desktop_Widget.Command;
using System;
using System.Windows.Input;
using System.ComponentModel;
using ViewComponent;
using Logging;
using Image_Desktop_Widget.Model;
using System.Collections.Generic;

namespace Image_Desktop_Widget.ViewModels
{
    public class ImageFrameViewModel : BaseViewModel
    {

        public static readonly string IMAGE_FRAME_MODEL_PARAMETER = "ImageFrameModel";

        private static readonly string DEFAULT_CAPTION = "Image frame";

        public event EventHandler ImageDisposeRequested = (s, e) => { };
        
        public IClosable Closable { get; set; }

        private ImageFrameModel imageFrameModel = null;

        public ImageFrameModel ImageFrameModel
        {
            get => imageFrameModel;
            set
            {
                imageFrameModel = value;
                OnPropertyChanged("ImageFrameModel");
            }
        }

        public ICommand SaveStateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand OpenImageCommand { get; }
        public ICommand ShowImageInExplorerCommand { get; }
        public ICommand OpenSettingsCommand { get; }
        public ICommand ShowMainWindowCommand { get; }

        private MessagePopupNotification notification;

        private IViewLauncher mainViewLauncher;
        
        public ImageFrameViewModel()
        {
            SaveStateCommand = new RelayCommand(SaveCurrentState);
            DeleteCommand = new RelayCommand(Delete);
            OpenImageCommand = new RelayCommand(OpenImage);
            ShowImageInExplorerCommand = new RelayCommand(ShowImageInExplorer);
            OpenSettingsCommand = new RelayCommand(ShowSettings);          
            ShowMainWindowCommand = new RelayCommand(ShowMainWindow);
            notification = new MessagePopupNotification() { Caption = DEFAULT_CAPTION };
            mainViewLauncher = new MainWindowViewLauncher();
        }

        protected override void OnParametersReceived(IDictionary<string, object> param)
        {
            base.OnParametersReceived(param);
            if(param.TryGetValue(IMAGE_FRAME_MODEL_PARAMETER, out object imageFrame))
            {
                ImageFrameModel = (ImageFrameModel)imageFrame;
                ImageFrameModel.ErrorOccured += ImageFrameModel_ErrorOccured;
                ImageFrameModel.Deleted += ImageFrameModel_Deleted;
                imageFrameModel.FrameShadow.PropertyChanged += FrameShadow_PropertyChanged;
            }
        }

        private void FrameShadow_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged("ImageFrameModel");
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

        private void ShowSettings()
        {
            new SettingsViewLauncher(ImageFrameModel).Launch();
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
            if(ImageFrameModel.State != Model.Model.ModelState.Deleted) ImageFrameModel.Save();
        }

        private void Delete()
        {
            ImageFrameModel.Delete();
        }


        //wrapper class for settings view (don't let the ViewModel directly know view specifics)
        private class SettingsViewLauncher : IViewLauncher
        {
            ImageFrameModel ImageFrameModel { get; set; }

            public SettingsViewLauncher(ImageFrameModel imageFrameModel)
            {
                ImageFrameModel = imageFrameModel;
            }

            public void Launch()
            {
                ImageFrameSettings imageFrameSettings = new ImageFrameSettings();
                imageFrameSettings.GetModel().Parameters = new Dictionary<string, object>
                {
                    { ImageFrameSettingViewModel.IMAGE_FRAME_MODEL_PARAMETER, ImageFrameModel },
                    { ImageFrameSettingViewModel.CLOSABLE_PARAMETER, imageFrameSettings }
                };
                imageFrameSettings.ShowDialog();
            }
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
