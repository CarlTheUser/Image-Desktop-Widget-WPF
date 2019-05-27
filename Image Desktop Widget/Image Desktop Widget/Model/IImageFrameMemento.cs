using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_Desktop_Widget.Model
{
    public interface IImageFrameMemento
    {
        string Caption { get; }
        bool CaptionEnabled { get; }
        int FrameThickness { get; }
        int RotationAngle { get; }
        bool ShadowEnabled { get; }
        double ShadowOpacity { get; }
        double ShadowDepth { get; }
        double ShadowDirection { get; }
        double ShadowBlurRadius { get; }
    }
}
