#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/11/29 16:13:41 
 * 文件名：FavoriteViewModel 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using FavoriteModule.Provider;
using FavoriteModule.View;
using HeBianGu.Base.Util;
using HeBianGu.General.ModuleManager;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FavoriteModule.ViewModel
{

    /// <summary> 说明 </summary>
    [System.ComponentModel.Composition.Export]
    public partial class FavoriteViewModel
    {
        #region - 成员 -

        private ObservableCollection<FileBindModel> _commonSource = new ObservableCollection<FileBindModel>();

        /// <summary> 常用文件集合 </summary>
        public ObservableCollection<FileBindModel> CommonSource
        {
            get { return _commonSource; }
            set
            {
                _commonSource = value;
                RaisePropertyChanged("CommonSource");
            }
        }

        private FileBindModel _current;
        /// <summary> 选中项 </summary>
        public FileBindModel Current
        {
            get { return _current; }
            set
            {
                _current = value;
                RaisePropertyChanged();
            }
        }


        private bool _isBusyFlag = true;
        /// <summary> 说明 </summary>
        public bool IsBusyFlag
        {
            get { return _isBusyFlag; }
            set
            {
                _isBusyFlag = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public FavoriteViewModel()
        {

            _deleteFastCommad = new DelegateCommand(DeleteCurrent);
            _copyCommad = new DelegateCommand(Copy_AddFolder);
        }

        /// <summary> 删除当前文件 </summary>
        public void DeleteCurrent()
        {
            if (this.Current == null) return;

            File.Delete(this.Current.FilePath);

            this.CommonSource.Remove(this.Current);

           

           
        }

        private DelegateCommand _deleteFastCommad;
        /// <summary> 删除常用文件 </summary>
        public DelegateCommand DeleteCommad
        {
            get
            {

                return _deleteFastCommad;
            }
        }

        private DelegateCommand _copyCommad;
        /// <summary> 粘贴添加常用文件 </summary>
        public DelegateCommand CopyCommad
        {
            get
            {
                return _copyCommad;
            }
        }

        void Copy_AddFolder()
        {
            // HTodo  ：复制的文件路径 
            string text = System.Windows.Clipboard.GetText();

            if (string.IsNullOrEmpty(text)) return;

            //if (text.IsURL()||text.IsHTML
            if (Uri.IsWellFormedUriString(text, UriKind.RelativeOrAbsolute))
            {
                string favorite = Environment.GetFolderPath(Environment.SpecialFolder.Favorites);

                ParseFavoriteWindow w = new ParseFavoriteWindow(text, text);

                if (w.ShowDialog() != null && w.DialogResult.Value)
                {
                    string path = System.IO.Path.Combine(favorite, w.FileName + ".url");

                    if (!File.Exists(path))
                    {
                        string urlstr = string.Format(@"[InternetShortcut]
URL = {0}", w.Url);
                        File.WriteAllText(path, urlstr);
                    }
                }
            }

            // HTodo  ：复制的文件 
            System.Collections.Specialized.StringCollection list = System.Windows.Clipboard.GetFileDropList();

            foreach (var item in list)
            {

                if (System.IO.Path.GetExtension(item).ToUpper() == ".URL")
                {
                    string favorite = Environment.GetFolderPath(Environment.SpecialFolder.Favorites);

                    string path = System.IO.Path.Combine(favorite, System.IO.Path.GetFileName(item));

                    if (!File.Exists(path))
                    {
                        File.Copy(item, path);
                    }
                }
            }

            this.RefreshFavoritesSource();
                

        }

        /// <summary> 收藏文件夹 </summary>
        public void RefreshFavoritesSource()
        {
            string recent = Environment.GetFolderPath(Environment.SpecialFolder.Favorites);

            var files = recent.GetAllFile();

            this.CommonSource.Clear();

            foreach (var item in files)
            {
                FileBindModel f = new FileBindModel(item);

                if (string.IsNullOrEmpty(f.FilePath)) continue;

                this.CommonSource.Add(f);

            }
        }


    }

    partial class FavoriteViewModel : INotifyPropertyChanged
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
