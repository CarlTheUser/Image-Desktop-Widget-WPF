using Image_Desktop_Widget.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewComponent
{

    //this namespace will contain
    //abstractions the view should use
    //to make code less specific
    //or to say the goal is
    //the viewmodel should be platform agnostic
    //and nor directly refer to a platform specific
    //implementation (Winforms etc)

    
        //TLDR : These are wrapper interfaces for platform specific UI prompt elements like Messagebox, InputBox, etc.


    //any prompts to user that needs
    //user input (messagebox, inputbox, etc)
    //non generic
    public interface INotification
    {
        void Notify(string message);
    }

    //any prompts to user that needs
    //user input (messagebox, inputbox, etc)
    //can return any type (generic)
    public interface INotification<T>
    {
        T Notify(string message);
    }

    //implemented by view (Winform, Page, etc)
    //that will use a viewmodel
    //might as well create a viewmodel in the constrcutor
    //so we can assume all class that inherit can not have a null vm
    public interface IView<T> where T: BaseViewModel, new()
    {
        T GetCurrentModel();
    }


}
