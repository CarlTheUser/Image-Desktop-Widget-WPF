using Image_Desktop_Widget.Command;
using Image_Desktop_Widget.Model;
using System;
using System.Windows.Input;
using ViewComponent;

namespace Image_Desktop_Widget.ViewModels
{
    
    public class FrameCreateViewModel : BaseViewModel
    {
        public IClosable Closable { get; set; }
        
        public ImageFrameModel ImageFrameModel { get; private set; }
        
        public ICommand SaveCommand { get; private set; }

        private ImageFrameViewLauncher imageFrameViewLauncher;

        private INotification notification;

        public FrameCreateViewModel(string imageFilePath)
        {
            ImageFrameModel = ImageFrameModel.NewInstance();
            ImageFrameModel.ImagePath = imageFilePath;
            ImageFrameModel.Saved += ImageFrameModel_Saved;
            ImageFrameModel.ErrorOccured += ImageFrameModel_ErrorOccured;
            SaveCommand = new RelayCommand(save);
            imageFrameViewLauncher = new ImageFrameViewLauncher();
            notification = new MessagePopupNotification();
        }

        
        private void ImageFrameModel_Saved(object sender, EventArgs e)
        {
            Closable.Close();
            imageFrameViewLauncher.ViewModel = new ImageFrameViewModel(ImageFrameModel);
            imageFrameViewLauncher.Launch();
        }

        private void ImageFrameModel_ErrorOccured(object sender, Model.Model.ErrorOccuredEventArgs e)
        {
            notification.Notify(e.Message);
        }

        private void save()
        {
            ImageFrameModel.Save();
        }
        private class ImageFrameViewLauncher : IViewLauncher
        {
            public BaseViewModel ViewModel { get; set; }
            
            public void Launch()
            {
                new ImageFrame
                {
                    DataContext = ViewModel
                }.Show();
            }
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
