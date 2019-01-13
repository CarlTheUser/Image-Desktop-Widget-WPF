using DataLayer.Client.Data;
using DataLayer.Client.Service;
using System;
using System.ComponentModel;

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

        #region FactoryMethods

        public static ImageFrameModel Existing(
            int id, 
            string imagePath, 
            string caption, 
            bool captionEnabled, 
            double width, 
            double height, 
            int frameThickness, 
            double locationX, 
            double locationY, 
            int rotationAngle,
            bool shadowEnabled,
            double shadowOpacity,
            double shadowDepth,
            double shadowDirection,
            double shadowBlurRadius)
        {
            return new ImageFrameModel(id, imagePath, caption, captionEnabled, width, height, frameThickness, locationX, locationY, rotationAngle, shadowEnabled, shadowOpacity, shadowDepth, shadowDirection, shadowBlurRadius)
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

        private ImageFrameModel(
            int id, 
            string imagePath, 
            string caption, 
            bool captionEnabled, 
            double width, 
            double height, 
            int frameThickness, 
            double locationX, 
            double locationY, 
            int rotationAngle,
            bool shadowEnabled,
            double shadowOpacity,
            double shadowDepth,
            double shadowDirection,
            double shadowBlurRadius) : this()
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
            FrameShadow.Enabled = shadowEnabled;
            FrameShadow.Opacity = shadowOpacity;
            FrameShadow.Depth = shadowDepth;
            FrameShadow.Direction = shadowDirection;
            FrameShadow.BlurRadius = shadowBlurRadius;
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

        private Shadow shadow = new Shadow();

        public Shadow FrameShadow { get => shadow; }


        #endregion

        #region OverridenBehaviors

        //private string caption_backup = null;
        //private int? frame_thickness_backup = null;
        //private bool? caption_enabled_backup = null;
        //private int? rotation_angle_backup = null;

        //private bool? shadow_enabled_backup = null;
        //private double? shadow_opacity_backup = null;
        //private double? shadow_depth_backup = null;
        //private double? shadow_direction_backup = null;
        //private double? shadow_blur_radius_backup = null;


        protected override void BackupProperties()
        {
            //caption_backup = caption;
            //frame_thickness_backup = frameThickness;
            //caption_enabled_backup = captionEnabled;
            //rotation_angle_backup = rotationAngle;

            //shadow_enabled_backup = FrameShadow?.Enabled ?? false;
            //shadow_opacity_backup = FrameShadow?.Opacity ?? 0;
            //shadow_depth_backup = FrameShadow?.Depth ?? 0;
            //shadow_direction_backup = FrameShadow?.Direction ?? 0;
            //shadow_blur_radius_backup = FrameShadow?.BlurRadius ?? 0;

            PropertyBackups["Caption"] = caption;
            PropertyBackups["FrameThickness"] = frameThickness;
            PropertyBackups["CaptionEnabled"] = captionEnabled;
            PropertyBackups["RotationAngle"] = rotationAngle;

            if(FrameShadow != null)
            {
                PropertyBackups["ShadowEnabled"] = FrameShadow.Enabled;
                PropertyBackups["ShadowOpacity"] = FrameShadow.Opacity;
                PropertyBackups["ShadowDepth"] = FrameShadow.Depth;
                PropertyBackups["ShadowDirection"] = FrameShadow.Direction;
                PropertyBackups["ShadowBlurRadius"] = FrameShadow.BlurRadius;
            }
        }

        protected override void RestoreProperties()
        {
            //Caption = caption_backup;
            //FrameThickness = frame_thickness_backup.Value;
            //CaptionEnabled = caption_enabled_backup.Value;
            //RotationAngle = rotation_angle_backup.Value;

            //if (FrameShadow != null)
            //{
            //    FrameShadow.Enabled = shadow_enabled_backup.Value;
            //    FrameShadow.Opacity = shadow_opacity_backup.Value;
            //    FrameShadow.Depth = shadow_depth_backup.Value;
            //    FrameShadow.Direction = shadow_direction_backup.Value;
            //    FrameShadow.BlurRadius = shadow_blur_radius_backup.Value;
            //}

            if(PropertyBackups.TryGetValue("Caption", out object caption_backup)) Caption = (string)caption_backup;
            if (PropertyBackups.TryGetValue("FrameThickness", out object frame_thickness_backup)) FrameThickness = (int)frame_thickness_backup;
            if (PropertyBackups.TryGetValue("CaptionEnabled", out object caption_enabled_backup)) CaptionEnabled = (bool)caption_enabled_backup;
            if (PropertyBackups.TryGetValue("RotationAngle", out object rotation_angle_backup)) RotationAngle = (int)rotation_angle_backup;

            if (FrameShadow != null)
            {
                if (PropertyBackups.TryGetValue("ShadowEnabled", out object shadow_enabled_backup)) FrameShadow.Enabled = (bool)shadow_enabled_backup;
                if (PropertyBackups.TryGetValue("ShadowOpacity", out object shadow_opacity_backup)) FrameShadow.Opacity = (double)shadow_opacity_backup;
                if (PropertyBackups.TryGetValue("ShadowDepth", out object shadow_depth_backup)) FrameShadow.Depth = (double)shadow_depth_backup;
                if (PropertyBackups.TryGetValue("ShadowDirection", out object shadow_direction_backup)) FrameShadow.Direction = (double)shadow_direction_backup;
                if (PropertyBackups.TryGetValue("ShadowBlurRadius", out object shadow_blur_radius_backup)) FrameShadow.BlurRadius = (double)shadow_blur_radius_backup;
            }

        }

        protected override void ClearPropertyBackups()
        {
            //caption_backup = null;
            //frame_thickness_backup = null;
            //caption_enabled_backup = null;
            //rotation_angle_backup = null;
            //shadow_enabled_backup = null;
            //shadow_opacity_backup = shadow_depth_backup = shadow_direction_backup = shadow_blur_radius_backup = null;
            PropertyBackups.Clear();

        }

        protected override void SaveMethod()
        {
            FramedImageDTO framedImage = ToDTOForm();
            new FramedImageOperation().Save(framedImage);
            Id = framedImage.Id;
            ImagePath = framedImage.ImageFilepath;
        }

        protected override void EditMethod()
        {
            new FramedImageOperation().Edit(ToDTOForm());
        }

        protected override void DeleteMethod()
        {
            new FramedImageOperation().Delete(ToDTOForm());
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
                RotateAngle = rotationAngle,
                EnableShadow = FrameShadow?.Enabled ?? false,
                ShadowOpacity = FrameShadow ?.Opacity ?? 0,
                ShadowDepth = FrameShadow ?.Depth ?? 0,
                ShadowDirection = FrameShadow ?.Direction ?? 0,
                ShadowBlurRadius = FrameShadow ?.BlurRadius ?? 0
            };
        }
        
        public sealed class Shadow : INotifyPropertyChanged
        {

            private static readonly bool DEFAULT_ENABLE = true;

            private static readonly double DEFAULT_OPACITY = .4;

            private static readonly double DEFAULT_DEPTH = 3;

            private static readonly double DEFAULT_DIRECTION = 270;

            private static readonly double DEFAULT_BLUR_RADIUS = 6;

            public event PropertyChangedEventHandler PropertyChanged;

            private bool enabled = DEFAULT_ENABLE;

            public bool Enabled
            {
                get => enabled;
                set
                {
                    enabled = value;
                    OnPropertyChanged("Enabled");
                }
            }

            private double opacity = DEFAULT_OPACITY;

            public double Opacity
            {
                get => opacity;
                set
                {
                    opacity = value;
                    OnPropertyChanged("Opacity");
                }
            }

            private double depth = DEFAULT_DEPTH;

            public double Depth
            {
                get => depth;
                set
                {
                    depth = value;
                    OnPropertyChanged("Depth");
                }
            }

            private double direction = DEFAULT_DIRECTION;

            public double Direction
            {
                get => direction;
                set
                {
                    direction = value;
                    OnPropertyChanged("Direction");
                }
            }

            private double blurRadius = DEFAULT_BLUR_RADIUS;

            public double BlurRadius
            {
                get => blurRadius;
                set
                {
                    blurRadius = value;
                    OnPropertyChanged("BlurRadius");
                }
            }

            public Shadow()
            {

            }

            public Shadow(bool enabled, double opacity, double depth, double direction, double blurRadius)
            {
                Enabled = enabled;
                Opacity = opacity;
                Depth = depth;
                Direction = direction;
                BlurRadius = blurRadius;
            }

            private void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            
        }
    }
}
