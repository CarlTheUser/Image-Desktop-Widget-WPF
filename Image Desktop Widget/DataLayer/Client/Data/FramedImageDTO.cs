
namespace DataLayer.Client.Data
{
    public sealed class FramedImageDTO
    {

        public int Id { get; set; }

        
        public string ImageFilepath { get; set; }

        public double Witdh { get; set; }
        public double Height { get; set; }
        public double LocationX { get; set; }
        public double LocationY { get; set; }
        public int FrameThickness { get; set; }
        public int RotateAngle { get; set; }
        public string Caption { get; set; }
        public bool EnableCaption { get; set; }

        public bool EnableShadow { get; set; }
        public double ShadowOpacity { get; set; }
        public double ShadowDepth { get; set; }
        public double ShadowDirection { get; set; }
        public double ShadowBlurRadius { get; set; }
    }
}
