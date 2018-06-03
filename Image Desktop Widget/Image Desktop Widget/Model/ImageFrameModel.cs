using DataLayer.Client.Data;
using DataLayer.Client.Service;
using System;

namespace Image_Desktop_Widget.Model
{
    public sealed class ImageFrameModel : Model
    {
        #region Constants

        public static readonly double MIN_HEIGHT = 112.5;

        public static readonly double MIN_WIDTH = 150;

        public static readonly double INITIAL_WIDTH = 300;

        public static readonly double INITIAL_HEIGHT = 200;
        
        public static readonly int MIN_FRAME_THICKNESS = 0;

        #endregion

        private readonly FramedImageOperation ImageFrameService = new FramedImageOperation();

        #region FactoryMethods

        public static ImageFrameModel FromPersistentStorage(int id, string imagePath, string caption, bool captionEnabled, double width, double height, int frameThickness, double locationX, double locationY, int rotationAngle)
        {
            return new ImageFrameModel(id, imagePath, caption, captionEnabled, width, height, frameThickness, locationX, locationY, rotationAngle)
            {
                State = ModelState.Old
            };
        }

        public static ImageFrameModel NewInstance()
        {
            return new ImageFrameModel()
            {
                State = ModelState.New
            };
        }

        #endregion

        #region Constructors

        private ImageFrameModel() { }

        private ImageFrameModel(int id, string imagePath, string caption, bool captionEnabled, double width, double height, int frameThickness, double locationX, double locationY, int rotationAngle) : this()
        {
            Id = id;
            ImagePath = imagePath;
            Caption = caption;
            CaptionEnabled = captionEnabled;
            Width = width;
            Height = height;
            FrameThickness = frameThickness;
            LocationX = locationX;
            LocationY = locationY;
            RotationAngle = rotationAngle;
        }

        #endregion

        #region Properties

        private int id = 0;

        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        private string imagePath = string.Empty; 

        public string ImagePath
        {
            get => imagePath;
            set
            {
                imagePath = value;
                OnPropertyChanged("ImagePath");
            }
        }

        private string caption = string.Empty;

        public string Caption
        {
            get => caption;
            set
            {
                caption = value;
                OnPropertyChanged("Caption");
            }
        }

        private bool captionEnabled = false;

        public bool CaptionEnabled
        {
            get => captionEnabled;
            set
            {
                captionEnabled = value;
                OnPropertyChanged("CaptionEnabled");
            }
        }

        private double width = INITIAL_WIDTH;

        public double Width
        {
            get => width;
            set
            {
                width = (value >= MIN_WIDTH) ? value : MIN_WIDTH;
                OnPropertyChanged("Width");
            }
        }

        private double height = INITIAL_HEIGHT;

        public double Height
        {
            get => height;
            set
            {
                height = (value >= MIN_HEIGHT) ? value : MIN_HEIGHT;
                OnPropertyChanged("Height");
            }
        }

        private int frameThickness = 10;

        public int FrameThickness
        {
            get => frameThickness;
            set
            {
                frameThickness = (value >= MIN_FRAME_THICKNESS) ? value : MIN_FRAME_THICKNESS;
                OnPropertyChanged("FrameThickness");
            }
        }

        private double location_x = 0;

        public double LocationX
        {
            get => location_x;
            set
            {
                location_x = value;
                OnPropertyChanged("LocationX");
            }
        }

        private double location_y = 0;

        public double LocationY
        {
            get => location_y;
            set
            {
                location_y = value;
                OnPropertyChanged("LocationY");
            }
        }

        private int rotationAngle = 0;

        public int RotationAngle
        {
            get => rotationAngle;
            set
            {
                rotationAngle = value;
                OnPropertyChanged("RotationAngle");
            }
        }

        #endregion

        #region OverridenBehaviors

        private string caption_backup = null;
        private int? frame_thickness_backup = null;
        private bool? caption_enabled_backup = null;
        private int? rotation_angle_backup = null;

        protected override void BackupProperties()
        {
            caption_backup = caption;
            frame_thickness_backup = frameThickness;
            caption_enabled_backup = captionEnabled;
            rotation_angle_backup = rotationAngle;
        }

        protected override void RestoreProperties()
        {
            Caption = caption_backup;
            FrameThickness = frame_thickness_backup.Value;
            CaptionEnabled = caption_enabled_backup.Value;
            RotationAngle = rotation_angle_backup.Value;
        }

        protected override void ClearPropertyBackups()
        {
            caption_backup = null;
            frame_thickness_backup = null;
            caption_enabled_backup = null;
            rotation_angle_backup = null;
        }

        protected override void SaveMethod()
        {
            FramedImageDTO framedImage = ToDTOForm();
            ImageFrameService.Save(framedImage);
            Id = framedImage.Id;
            ImagePath = framedImage.ImageFilepath;
        }

        protected override void EditMethod()
        {
            ImageFrameService.Edit(ToDTOForm());
        }

        protected override void DeleteMethod()
        {
            ImageFrameService.Delete(ToDTOForm());
        }

        #endregion

        private FramedImageDTO ToDTOForm()
        {
            return new FramedImageDTO
            {
                Id = Id,
                ImageFilepath = ImagePath,
                Caption = caption,
                EnableCaption = captionEnabled,
                FrameThickness = frameThickness,
                Witdh = width,
                Height = height,
                LocationX = location_x,
                LocationY = location_y,
                RotateAngle = rotationAngle
            };
        }

    }
}
