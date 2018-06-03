using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.Data
{
    public sealed class Configuration
    {
        //CLR managed singleton
        private static readonly Configuration instance = new Configuration();

        public static Configuration Instance { get { return instance; } }

        private readonly string UserImageDirectoryName = "UserImages";

        private readonly string ProductImageDirectoryName = "Products";

        private readonly string AppDataFolderName = "General Merchandise Solution";

        private readonly string TextLogFileName = "Logs.txt";

        public readonly string AppDataFolderPath;

        public readonly string UserImageDirectoryPath;

        public readonly string ProductImageDirectoryPath;

        public readonly string TextLogsPath;

        private Configuration()
        {
            AppDataFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + AppDataFolderName;

            UserImageDirectoryPath = AppDataFolderPath + "\\" + UserImageDirectoryName;

            ProductImageDirectoryPath = AppDataFolderPath + "\\" + ProductImageDirectoryName;

            TextLogsPath = AppDataFolderPath + "\\" + TextLogFileName;

            //Creates directory only if it doesnt exists yet 
            Directory.CreateDirectory(AppDataFolderPath);

            Directory.CreateDirectory(UserImageDirectoryPath);

        }
    }
}
