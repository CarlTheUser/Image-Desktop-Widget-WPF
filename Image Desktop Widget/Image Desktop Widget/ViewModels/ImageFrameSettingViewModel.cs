using Image_Desktop_Widget.Command;
using Image_Desktop_Widget.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Image_Desktop_Widget.ViewModels
{
    public class ImageFrameSettingViewModel : BaseViewModel
    {
        public static readonly string IMAGE_FRAME_MODEL_PARAMETER = "ImageFrameModel";

        public static readonly string CLOSABLE_PARAMETER = "Closable";

        #region Bindable Proberties

        public ICommand applyEditCommand;
        
        public ICommand ApplyEditCommand
        {
            get => applyEditCommand;
            private set
            {
                applyEditCommand = value;
                OnPropertyChanged("ApplyEditCommand");
            }
        }

        public ICommand cancelEditCommand;

        public ICommand CancelEditCommand
        {
            get => cancelEditCommand;
            private set
            {
                cancelEditCommand = value;
                OnPropertyChanged("CancelEditCommand");
            }
        }

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

        #endregion

        private IClosable Closable;

        public ImageFrameSettingViewModel() { }

        protected override void OnParametersReceived(IDictionary<string, object> param)
        {
            base.OnParametersReceived(param);
            if(param.ContainsKey(IMAGE_FRAME_MODEL_PARAMETER))
            {
                ImageFrameModel = (ImageFrameModel)param[IMAGE_FRAME_MODEL_PARAMETER];
                ImageFrameModel.EditApplied += ImageFrameModel_EditApplied;
                ImageFrameModel.EditCancelled += ImageFrameModel_EditCancelled;
                imageFrameModel.BeginEdit();
                ApplyEditCommand = new RelayCommand(ApplyEdit);
                CancelEditCommand = new RelayCommand(CancelEdit);
            }
            if(param.ContainsKey(CLOSABLE_PARAMETER))
            {
                Closable = (IClosable)param[CLOSABLE_PARAMETER];
            }
        }

        #region CommandImplementations

        private void ApplyEdit()
        {
            ImageFrameModel.ApplyEdit();
        }

        private void CancelEdit()
        {
            ImageFrameModel.CancelEdit();
        }

        #endregion

        #region EventHandlers

        private void ImageFrameModel_EditCancelled(object sender, EventArgs e)
        {
            Closable?.Close();
        }

        private void ImageFrameModel_EditApplied(object sender, EventArgs e)
        {
            Closable?.Close();
        }

        #endregion

    }
}
