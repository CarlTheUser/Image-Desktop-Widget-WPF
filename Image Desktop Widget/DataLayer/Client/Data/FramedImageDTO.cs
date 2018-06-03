
namespace DataLayer.Client.Data
{
    public sealed class FramedImageDTO
    {
        //typical DTO here
        //DTOs are use to transfer data
        //from a layer to another layer
        //without directly exposing
        //business objects to the client side.
        //imagine in PHP / ASP.NET
        //wherein you pass on JSON objects
        //over the net and then
        //the client side recieves
        //pure data without behaviors using JS.
        //unless you directly echo html elements from the server
        // <expanding brain meme here> which violates
        //the separation of UI and the business logic.

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
    }
}
