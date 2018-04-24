#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2018/3/29 18:02:06 
 * 文件名：Class1 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HeBianGu.Base.Util;
using HeBianGu.General.ModuleManager;
using HeBianGu.General.ModuleManager.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.MovieBrower.UserControls
{
    /// <summary> 文件绑定实体 </summary>
    [Serializable]
    public partial class MovieFileViewModel
    {

        public MovieFileViewModel()
        {

        }

        public MovieFileViewModel(System.IO.FileSystemInfo sysFile)
        {
            if (SysTemConfiger.ExceptShowFile.Exists(l => l == sysFile.Extension))
            {
                return;
            }

            if (sysFile is FileInfo)
            {
                FileInfo file = sysFile as FileInfo;
                this.FileName = sysFile.Name;
                this.FilePath = sysFile.FullName;
                this.IsFile = true;
                this.Size = file.Length;

            }
            else
            {
                this.FileName = sysFile.Name;
                this.FilePath = sysFile.FullName;
                this.IsFile = false;
            }

            this.LastTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
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

        private bool _isEnble;
        /// <summary> 文件是否村子 </summary>
        public bool IsEnble
        {
            get
            {
                return File.Exists(this.FilePath);
            }
        }

        private long _size;
        /// <summary> 文件大小 </summary>
        public long Size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value;

                this.SizeString = DirectoryHelper.ConvertBytes(value);

                RaisePropertyChanged();
            }
        }

        private string _sizeString;
        /// <summary> 说明 </summary>
        public string SizeString
        {
            get { return _sizeString; }
            set
            {
                _sizeString = value;
                RaisePropertyChanged();
            }

        }

        private bool _isFile;
        /// <summary> 说明 </summary>
        public bool IsFile
        {
            get { return _isFile; }
            set { _isFile = value; }
        }

        /// <summary> 图片路径 </summary>
        public System.Drawing.Icon ImagePath
        {
            get { return Base.Util.IconHelper.Instance.GetSystemInfoIcon(FilePath); }
        }

        private string _lastTime;
        /// <summary> 说明 </summary>
        public string LastTime
        {
            get { return _lastTime; }
            set
            {
                _lastTime = value;
                RaisePropertyChanged();
            }
        }


        private double _score = 2;
        /// <summary> 说明 </summary>
        public double Score
        {
            get { return _score; }
            set
            {
                _score = value;
                RaisePropertyChanged();
            }
        }


        private string _type;
        /// <summary> 说明 </summary>
        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
                RaisePropertyChanged();
            }
        }

        private int _count = 0;
        /// <summary> 说明 </summary>
        public int Count
        {
            get { return _count; }
            set
            {
                _count = value;
                RaisePropertyChanged();
            }
        }

        private List<string> _typeItems = new List<string>() { "0", "1", "2" };
        /// <summary> 说明 </summary>
        public List<string> TypeItems
        {
            get { return _typeItems; }
            set
            {
                _typeItems = value;
                RaisePropertyChanged();
            }
        }

        public MovieFileModel ToModel()
        {
            MovieFileModel model = new MovieFileModel();

            model.CopyFromObj(this);

            return model;
        }

        public MovieFileViewModel(MovieFileModel model)
        {
            this.CopyFromObj(model);

            if (!File.Exists(model.FilePath)) return;

            FileInfo file = new FileInfo(model.FilePath);

            this.Size = file.Length;
        }

        private bool _isChecked;
        /// <summary> 说明 </summary>
        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                RaisePropertyChanged();
            }
        }


        private Visibility _isVisible = Visibility.Visible;
        /// <summary> 说明 </summary>
        public Visibility IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                RaisePropertyChanged();
            }
        }


    }

    partial class MovieFileViewModel : INotifyPropertyChanged
    {
        #region - MVVM -

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
