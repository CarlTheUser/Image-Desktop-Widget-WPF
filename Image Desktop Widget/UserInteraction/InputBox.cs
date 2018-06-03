
namespace UserInteraction
{
    public sealed class InputBox
    {
        

        public static string Show(string prompt, string caption, bool filterInput = false)
        {
            string returnString = string.Empty;

            InputBoxWindow inputBoxWindow = new InputBoxWindow(prompt, caption, filterInput);

            inputBoxWindow.ShowDialog();

            returnString = inputBoxWindow.UserInput;

            return returnString;
        }
        
        public static string Show(string prompt)
        {
            return Show(prompt, "");
        }




    }
}
