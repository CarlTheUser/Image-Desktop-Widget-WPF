using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    internal static class FileProperty
    {

        private static readonly List<string> jpg;
        private static readonly List<string> bmp;
        private static readonly List<string> gif;
        private static readonly List<string> png;
        private static readonly List<List<string>> imgTypes;

        static FileProperty()
        {
            jpg = new List<string> { "FF", "D8" };
            bmp = new List<string> { "42", "4D" };
            gif = new List<string> { "47", "49", "46" };
            png = new List<string> { "89", "50", "4E", "47", "0D", "0A", "1A", "0A" };
            imgTypes = new List<List<string>> { jpg, bmp, gif, png };
        }

        public static bool IsImage(this Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            
            List<string> bytesIterated = new List<string>();

            for (int i = 0; i < 8; i++)
            {
                string bit = stream.ReadByte().ToString("X2");
                bytesIterated.Add(bit);

                bool isImage = imgTypes.Any(img => !img.Except(bytesIterated).Any());
                if (isImage)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
