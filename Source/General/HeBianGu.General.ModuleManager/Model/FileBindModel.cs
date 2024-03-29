﻿#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/11/28 9:52:37 
 * 文件名：FileBindModel 
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
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HeBianGu.General.ModuleManager
{
    /// <summary> 文件绑定实体 </summary>
    [Serializable]
    public class FileBindModel
    {
        public FileBindModel(string path)
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

            this.LastTime = DateTime.Now;
        }

        public FileBindModel(System.IO.FileSystemInfo sysFile)
        {
            if (SysTemConfiger.ExceptShowFile.Exists(l => l == sysFile.Extension))
            {
                return;
            }

            if (sysFile is FileInfo)
            {
                this.FileName = sysFile.Name;
                this.FilePath = sysFile.FullName;
                this.IsFile = true;
            }
            else
            {
                this.FileName = sysFile.Name;
                this.FilePath = sysFile.FullName;
                this.IsFile = false;
            }

            this.LastTime = DateTime.Now;
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

        /// <summary> 图片路径 </summary>
        public Icon ImagePath
        {
            get { return Base.Util.IconHelper.Instance.GetSystemInfoIcon(FilePath); }
        }

        private DateTime _lastTime;
        /// <summary> 说明 </summary>
        public DateTime LastTime
        {
            get { return _lastTime; }
            set { _lastTime = value; }
        }

    }
}
