using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_Desktop_Widget
{
    // The purpose of this interface
    //is to further abstract the instantiation logic
    //and the type of the view to the view model incase we have to call a create view
    //from the view model
    //(the view model shouldnt be aware about the specifics 
    //of the view like if it is a winforms or what not
    //just that it knows there's a view. and that's why we bind to an interface)
    //so incase we're porting it to another platform
    //we only change the code about view specifics (windows -> winforms linux -> linuxform, etc)

    //TLDR: this is a wrapper class use to call Views
    public interface IViewLauncher
    {
        void Launch();
    }
}
