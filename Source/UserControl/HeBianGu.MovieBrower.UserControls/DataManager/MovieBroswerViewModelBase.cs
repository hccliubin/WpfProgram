#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2018/4/4 15:30:35 
 * 文件名：MovieBroswerViewModelBase 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HeBianGu.Base.Util;
using HeBianGu.Base.WpfBase;
using HeBianGu.General.Logger;
using HeBianGu.General.ModuleManager.Model;
using HeBianGu.General.ModuleManager.Service;
using HeBianGu.General.WpfControlLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace HeBianGu.MovieBrower.UserControls.DataManager
{
    public partial class MovieBroswerViewModelBase
    {
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


        public RelayCommand RelayCommand { get; set; }


        private ObservableCollection<MovieFileViewModel> _commonSource = new ObservableCollection<MovieFileViewModel>();

        /// <summary> 常用文件集合 </summary>
        public ObservableCollection<MovieFileViewModel> CommonSource
        {
            get { return _commonSource; }
            set
            {
                _commonSource = value;
                RaisePropertyChanged("CommonSource");
            }
        }

        public MovieBroswerViewModelBase()
        {
            RelayCommand = new RelayCommand(new Action<object>(ButtonClickFunc));

            ClipBoardRegisterService.Instance.ClipBoardChanged += Instance_ClipBoardChanged;

        }

        private void Instance_ClipBoardChanged()
        {
            //Todo  ：复制的图片 
            BitmapSource bit = System.Windows.Clipboard.GetImage();

            if (bit != null)
            {
                if (this.SelectItem != null && this.IsActived)
                {
                    this.ButtonClickFunc("InsertImage");

                    this.ButtonClickFunc("ShowImage");

                    Log4Servcie.Instance.Info("保存图片成功！" + this.Type);
                }
            }
        }

        FileType _type = FileType.Normal;

        public FileType Type { get => _type; set => _type = value; }

        ICaseItem _caseItem;

        public ICaseItem CaseItem { get => _caseItem; set => _caseItem = value; }

        private MovieFileViewModel _selectItem;
        /// <summary> 说明 </summary>
        public MovieFileViewModel SelectItem
        {
            get { return _selectItem; }
            set
            {
                _selectItem = value;
                RaisePropertyChanged();
            }
        }

        /// <summary> 按钮点击事件 </summary>
        private void ButtonClickFunc(object obj)
        {
            string buttonName = obj as string;

            // Todo ：打开 
            if (buttonName == "Open")
            {
                if (SelectItem == null) return;

                if (!File.Exists(SelectItem.FilePath)) return;

                Process.Start(SelectItem.FilePath);

                this.SelectItem.LastTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

                int count = this.SelectItem.Count + 1;

                this.SelectItem.Count = count;

                Log4Servcie.Instance.Info("打开文件："+this.SelectItem.FilePath );
            }

            // Todo ：收藏 
            else if (buttonName == "Favorite")
            {
                if (this.SelectItem == null) return;

                var favorite = MovieBrowserDataManager.Instance.ViewModelItem.Find(l => l.Type == FileType.Favorate);

                favorite.CommonSource.Add(this.SelectItem);

                Log4Servcie.Instance.Info("收藏文件：" + this.SelectItem.FilePath);

                this.CommonSource.Remove(this.SelectItem);

                favorite.RefreshCount();

           

            }

            // Todo ：删除 
            else if (buttonName == "Delete")
            {
                if (this.SelectItem == null) return;

                var delete = MovieBrowserDataManager.Instance.ViewModelItem.Find(l => l.Type == FileType.Delete);

                delete.CommonSource.Add(this.SelectItem);

                Log4Servcie.Instance.Info("删除文件：" + this.SelectItem.FilePath);

                this.CommonSource.Remove(this.SelectItem);

                delete.RefreshCount();

              
            }

            // Todo ：彻底删除 
            else if (buttonName == "DeleteDeep")
            {
                if (this.SelectItem == null) return;

                string folderFullName = Path.GetDirectoryName(this.SelectItem.FilePath);

                var folderName = Path.GetFileNameWithoutExtension(folderFullName);

                if (folderName == Path.GetFileNameWithoutExtension(this.SelectItem.FilePath))
                {

                    if (Directory.Exists(folderFullName))
                    {

                        Directory.Delete(folderFullName, true);
                    }
                }
                else
                {
                    if (File.Exists(this.SelectItem.FilePath))
                    {
                        File.Delete(this.SelectItem.FilePath);
                    }
                }

                this.CommonSource.Remove(this.SelectItem);

                Log4Servcie.Instance.Info("彻底删除：" + this.SelectItem.FilePath);
            }

            // Todo ：还原 
            else if (buttonName == "ReLoad")
            {
                var noraml = MovieBrowserDataManager.Instance.ViewModelItem.Find(l => l.Type == FileType.Normal);

                noraml.CommonSource.Add(this.SelectItem);

                this.CommonSource.Remove(this.SelectItem);

                noraml.RefreshCount();

                Log4Servcie.Instance.Info("重新加载：" + this.SelectItem.FilePath);
            }

            // Todo ：图片预览 
            else if (buttonName == "ShowImage")
            {

                if (this.SelectItem == null) return;

                var folder = Path.GetDirectoryName(this.SelectItem.FilePath);

                var collection = DirectoryHelper.GetAllFile(folder, l => l.Extension.EndsWith("jpg"));

                collection.Reverse();

                List<string> cs = new List<string>();

                if (collection == null || collection.Count == 0)
                {
                    Log4Servcie.Instance.Info("不存在预览图片");
                    this.ImagePath = new ObservableCollection<string>();
                    return;
                }

                this.ImagePath = null;

                ObservableCollection<string> ss = new ObservableCollection<string>();

                foreach (var item in collection)
                {
                    ss.Add(item);
                }

                this.ImagePath = ss;

                Log4Servcie.Instance.Info("图片预览：" + this.SelectItem.FilePath);
            }

            // Todo ：插入图片 
            else if (buttonName == "InsertImage")
            {
                if (this.SelectItem == null) return;

                string folder = Path.GetDirectoryName(this.SelectItem.FilePath);

                string imageName = Path.GetFileNameWithoutExtension(this.SelectItem.FilePath);

                string imagePath = Path.Combine(folder, imageName + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpg");

                if (!Directory.Exists(Path.GetDirectoryName(this.SelectItem.FilePath))) return;

                var image = Clipboard.GetImage();

                if (image == null)
                {
                    MessageWindow.ShowDialog("不存在图片文件");
                    return;
                }

                MemoryStream ms = new MemoryStream();
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(image));
                enc.Save(ms);
                Bitmap img = new Bitmap(ms);
                ms.Flush();
                ms = new MemoryStream();
                img.Save(ms, ImageFormat.Jpeg);

                FileStream fs = new FileStream(imagePath, FileMode.Create);
                fs.Write(ms.ToArray(), 0, ms.ToArray().Length);
                fs.Flush();
                fs.Dispose();
                ms = null;

                this.SelectItem.RefreshImage();

                Log4Servcie.Instance.Info("插入图片：" + image);

            }

            // Todo ：刷新 
            else if (buttonName == "Refresh")
            {
                var filters = this.FilterType.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                List<MovieFileViewModel> items = new List<MovieFileViewModel>();

                if (this.OrderByIndex == 0)
                {
                    items = this.CommonSource.OrderBy(l => l.FileName).ToList();

                }

                else if (this.OrderByIndex == 1)
                {
                    items = this.CommonSource.OrderByDescending(l => l.LastTime).ToList();

                }
                else if (this.OrderByIndex == 2)
                {
                    items = this.CommonSource.OrderByDescending(l => l.Score).ToList();
                }
                else if (this.OrderByIndex == 3)
                {
                    items = this.CommonSource.OrderByDescending(l => l.Type).ToList();
                }
                else if (this.OrderByIndex == 4)
                {
                    items = this.CommonSource.OrderByDescending(l => l.Size).ToList();
                }

                // Todo ：匹配规则 
                Predicate<string> match = l =>
                  {
                      // Todo ：没选过滤规则 
                      if (filters == null || filters.Count == 0) return true;

                      // Todo ：当前项是空 
                      if (l == null) return false;

                      foreach (var item in filters)
                      {
                          if (!l.Contains(item)) return false;
                      }

                      return true;
                  };


                this.CommonSource.Clear();

                foreach (var item in items)
                {
                    item.IsVisible = Visibility.Collapsed;

                    this.CommonSource.Add(item);
                }

                var matchIitems = items.FindAll(l => filters.Count == 0 || filters.Exists(k => match(l.Type))).ToList();

                //var matchIitems = MovieBrowserDataManager.Instance.AllFileCatche.FindAll(l => filters.Exists(k => match(l.Type))).ToList()

                foreach (var item in matchIitems)
                {
                    item.IsVisible = Visibility.Visible;
                }

                this.RefreshCount();

                Log4Servcie.Instance.Info("刷新完成");
            }

            // Todo ：删除图片 
            else if (buttonName == "DeleteImage")
            {
                this.ImagePath.Remove(this.SelectImage);

                File.Delete(this.SelectImage);

                ObservableCollection<string> collection = new ObservableCollection<string>();

                foreach (var item in this.ImagePath)
                {
                    collection.Add(item);
                }

                this.ImagePath = collection;

                Log4Servcie.Instance.Info("删除图片成功：" + this.SelectImage);

            }

            // Todo ：清空数据 
            else if (buttonName == "Clear")
            {
                this.ImagePath = null;

                this.CommonSource.Clear();


                Log4Servcie.Instance.Info("清空数据成功：" + this.SelectImage);
            }

            // Todo ：清空数据 
            else if (buttonName == "SetDefault")
            {
                this.RefreshFilter();

                if (this.CommonSource == null || this.CommonSource.Count == 0)
                {
                    this.ButtonClickFunc("Clear");
                    return;
                }

                this.SelectItem = this.CommonSource[0];

                this.ButtonClickFunc("ShowImage");

               


                Log4Servcie.Instance.Info("设置默认项成功：" + this.SelectItem.FilePath);
            }

            // Todo ：打开路径 
            else if (buttonName == "OpenFolder")
            {
                if (this.SelectItem == null) return;

                if (File.Exists(this.SelectItem.FilePath))
                {
                    Process.Start(Path.GetDirectoryName(this.SelectItem.FilePath));
                }

                Log4Servcie.Instance.Info("打开文件路径成功：" + this.SelectItem.FilePath);
            }

            // Todo ：全选 
            if (obj is bool)
            {
                foreach (var item in this.CommonSource)
                {
                    item.IsChecked = (bool)obj;

                }
            }

            this.RefreshCount();


        }

        /// <summary> 触发命令 </summary>
        public void DoCommandWith(object obj)
        {
            this.RelayCommand.Execute(obj);
        }

        private int _orderByIndex = 2;
        /// <summary> 说明 </summary>
        public int OrderByIndex
        {
            get { return _orderByIndex; }
            set
            {
                _orderByIndex = value;
                RaisePropertyChanged();
            }
        }

        private string _filterType = string.Empty;
        /// <summary> 筛选的类型 </summary>
        public string FilterType
        {
            get { return _filterType; }
            set
            {
                _filterType = value;
                RaisePropertyChanged();
            }
        }

        private string _count;
        /// <summary> 说明 </summary>
        public string Count
        {
            get { return _count; }
            set
            {
                _count = value;
                RaisePropertyChanged();
            }
        }

        private string _visibleCount;
        /// <summary> 说明 </summary>
        public string VisibleCount
        {
            get { return _visibleCount; }
            set
            {
                _visibleCount = value;
                RaisePropertyChanged();
            }
        }


        private ObservableCollection<string> _imagePath = new ObservableCollection<string>();
        /// <summary> 说明 </summary>
        public ObservableCollection<string> ImagePath
        {
            get { return _imagePath; }
            set
            {
                _imagePath = value;
                RaisePropertyChanged();
            }
        }

        private string _selectImage;
        /// <summary> 说明 </summary>
        public string SelectImage
        {
            get { return _selectImage; }
            set
            {
                _selectImage = value;
                RaisePropertyChanged();
            }
        }

        public void RefreshCount()
        {
            this.Count = this.CommonSource.Count.ToString();

            this.VisibleCount = this.CommonSource.ToList().FindAll(l=>l.IsVisible==Visibility.Visible).Count.ToString();
        }

        /// <summary> 此方法的说明 </summary>
        public void RefreshFilter()
        {
            this.Types = CaseNotifyService.MovieTypes;
        }


        private bool _isActived;
        /// <summary> 是否激活 </summary>
        public bool IsActived
        {
            get { return _isActived; }
            set { _isActived = value; }
        }

        private List<string> _types;
        /// <summary> 类型配置 </summary>
        public List<string> Types
        {
            get { return _types; }
            set
            {
                _types = value;
                RaisePropertyChanged();
            }
        }

     
    }

    partial class MovieBroswerViewModelBase : INotifyPropertyChanged
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
