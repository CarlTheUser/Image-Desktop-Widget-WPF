using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_Desktop_Widget.Model
{
    class ImageFrameMemento : IImageFrameMemento
    {
        public string Caption { get; }
        public bool CaptionEnabled { get; }
        public int FrameThickness { get; }
        public int RotationAngle { get; }
        public bool ShadowEnabled { get; }
        public double ShadowOpacity { get; }
        public double ShadowDepth { get; }
        public double ShadowDirection { get; }
        public double ShadowBlurRadius { get; }

        public ImageFrameMemento(
            string caption, 
            bool captionEnabled, 
            int frameThickness, 
            int rotationAngle, 
            bool shadowEnabled, 
            double shadowOpacity, 
            double shadowDepth, 
            double shadowDirection, 
            double shadowBlurRadius)
        {
            Caption = caption;
            CaptionEnabled = captionEnabled;
            FrameThickness = frameThickness;
            RotationAngle = rotationAngle;
            ShadowEnabled = shadowEnabled;
            ShadowOpacity = shadowOpacity;
            ShadowDepth = shadowDepth;
            ShadowDirection = shadowDirection;
            ShadowBlurRadius = shadowBlurRadius;
        }
    }
}
