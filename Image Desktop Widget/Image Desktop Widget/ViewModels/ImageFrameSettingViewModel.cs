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
        public IClosable Closable { get; set; }

        public ICommand ApplyEditCommand { get; private set; }
        public ICommand CancelEditCommand { get; private set; }

        public ImageFrameModel ImageFrameModel { get; set; }


        public ImageFrameSettingViewModel(ImageFrameModel imageFrameModel)
        {
            ImageFrameModel = imageFrameModel;
            ImageFrameModel.EditApplied += ImageFrameModel_EditApplied;
            ImageFrameModel.EditCancelled += ImageFrameModel_EditCancelled;
            ApplyEditCommand = new RelayCommand(ApplyEdit);
            CancelEditCommand = new RelayCommand(CancelEdit);
            imageFrameModel.BeginEdit();
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
            Closable.Close();
        }

        private void ImageFrameModel_EditApplied(object sender, EventArgs e)
        {
            Closable.Close();
        }

        #endregion

    }
}
