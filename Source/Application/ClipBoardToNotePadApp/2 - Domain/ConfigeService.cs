using HeBianGu.Base.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipBoardToNotePadApp
{
    public class ConfigService
    {

        public static ConfigService Instance = new ConfigService();

        public string LocalDataPath
        {
            get
            {
                string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ClipBoardToNotePadApp", "config.txt");

                if (!System.IO.Path.GetDirectoryName(filePath).IsExistDirectory())
                {
                    System.IO.Path.GetDirectoryName(filePath).CreateDir();
                }

                return filePath;
            }
        }
    }
}
