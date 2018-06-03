
namespace DataLayer.Model
{
    internal class FramedImageModel : BaseModel<int>
    {

        //the primary key / id in this case is just a plain ol int32
        public override int Id { get; set; }

        //note: you can add your own custom attributes here
        //if you're planning to use reflection as
        //an auto mapper for properties of your model

        //from the model we save Filename only
        //we do some processing on this layer
        //so the client will receive a full path
        public string Filename { get; set; }
        public double Witdh { get; set; }
        public double Height { get; set; }
        public double LocationX { get; set; }
        public double LocationY { get; set; }
        public int FrameThickness { get; set; }
        public int RotateAngle { get; set; }
        public string Caption { get; set; }
        public bool EnableCaption { get; set; }
    }
}
