using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Image_Desktop_Widget
{
    public static class ColorUtility
    {

        //https://social.msdn.microsoft.com/Forums/vstudio/en-US/23cc280f-306e-444d-9577-3d6458094b99/converting-from-color-to-uint-and-vice-versa?forum=wpf

        private static uint ToUint(this Color c)
        {
            return (uint)(((c.A << 24) | (c.R << 16) | (c.G << 8) | c.B) & 0xffffffffL);
        }

        private static Color ToColor(this uint value)
        {
            return Color.FromArgb((byte)((value >> 24) & 0xFF),
                       (byte)((value >> 16) & 0xFF),
                       (byte)((value >> 8) & 0xFF),
                       (byte)(value & 0xFF));
        }
    }
}
