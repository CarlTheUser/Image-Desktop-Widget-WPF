using Image_Desktop_Widget.Command;
using Image_Desktop_Widget.Model;
using Logging;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using ViewComponent;

namespace Image_Desktop_Widget.ViewModels
{
    
    public class FrameCreateViewModel : BaseViewModel
    {
        public static readonly string IMAGE_FILE_PATH_PARAMETER = "ImageFilePath";

        public static readonly string CLOSABLE_PARAMETER = "Closable";

        private IClosable Closable { get; set; }
        
        public ImageFrameModel ImageFrameModel { get; private set; }
        
        public ICommand SaveCommand { get; private set; }
        
        private INotification notification;

        public FrameCreateViewModel()
        {
            ImageFrameModel = ImageFrameModel.NewInstance();
            ImageFrameModel.Saved += ImageFrameModel_Saved;
            ImageFrameModel.ErrorOccured += ImageFrameModel_ErrorOccured;
            SaveCommand = new RelayCommand(Save);
            notification = new MessagePopupNotification();
        }

        protected override void OnParametersReceived(IDictionary<string, object> param)
        {
            base.OnParametersReceived(param);
            if(param.TryGetValue(IMAGE_FILE_PATH_PARAMETER, out object imagePath))
            {
                ImageFrameModel.ImagePath = (string)imagePath;
            }
            if(param.TryGetValue(CLOSABLE_PARAMETER, out object closable))
            {
                Closable = (IClosable)closable;
            }
        }
        
        private void ImageFrameModel_Saved(object sender, EventArgs e)
        {
            new ImageFrameViewLauncher(ImageFrameModel).Launch();
            Closable.Close();
        }

        private void ImageFrameModel_ErrorOccured(object sender, Model.Model.ErrorOccuredEventArgs e)
        {
            notification.Notify(e.Message);
        }

        private void Save()
        {
            ImageFrameModel.Save();
        }

        private class ImageFrameViewLauncher : IViewLauncher
        {
            public ImageFrameModel ImageFrameModel { get; set; }

            public ImageFrameViewLauncher(ImageFrameModel model)
            {
                ImageFrameModel = model;
            }
            
            public void Launch()
            {
                ImageFrame imageFrame = new ImageFrame();
                imageFrame.GetModel().Parameters = new Dictionary<string, object>
                {
                    {ImageFrameViewModel.IMAGE_FRAME_MODEL_PARAMETER, ImageFrameModel }
                };
                imageFrame.Show();
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
