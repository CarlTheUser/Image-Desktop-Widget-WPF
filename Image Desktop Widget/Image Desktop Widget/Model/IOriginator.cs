using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_Desktop_Widget.Model
{
    public interface IOriginator<T>
    {
        T WriteState();

        void ReadState(T memento);
    }
}
