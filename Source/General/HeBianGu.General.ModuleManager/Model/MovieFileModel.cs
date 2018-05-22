#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2018/3/30 17:06:17 
 * 文件名：Class1 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.General.ModuleManager.Model
{
    [Serializable]
    public partial class MovieFileModel
    {
        public MovieFileModel()
        {

        }
        public MovieFileModel(string path)
        {
            if (Path.HasExtension(path))
            {
                if (SysTemConfiger.ExceptShowFile.Exists(l => l == Path.GetExtension(path)))
                {
                    return;
                }

                this.FileName = Path.GetFileNameWithoutExtension(path);
                this.FilePath = path;
                this.IsFile = true;
            }
            else
            {
                this.FileName = Path.GetFileName(path);
                this.FilePath = path;
                this.IsFile = false;
            }

            this.LastTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private string _fileName;
        /// <summary> 说明 </summary>
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        private string _filePath;
        /// <summary> 说明 </summary>
        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; }
        }

        private bool _isFile;
        /// <summary> 说明 </summary>
        public bool IsFile
        {
            get { return _isFile; }
            set { _isFile = value; }
        }

        private string _lastTime;
        /// <summary> 说明 </summary>
        public string LastTime
        {
            get { return _lastTime; }
            set
            {
                _lastTime = value;
            }
        }

        private double _score = 0;
        /// <summary> 说明 </summary>
        public double Score
        {
            get { return _score; }
            set { _score = value; }
        }


        private string _type;
        /// <summary> 说明 </summary>
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }


        private int _count = 0;
        /// <summary> 说明 </summary>
        public int Count
        {
            get { return _count; }
            set
            {
                _count = value;
            }
        }

        private FileType _fileType;
        /// <summary> 说明 </summary>
        public FileType FileType
        {
            get { return _fileType; }
            set { _fileType = value; }
        }




    }

    public enum FileType
    {
        Normal=0,Delete,Favorate
    }

}
