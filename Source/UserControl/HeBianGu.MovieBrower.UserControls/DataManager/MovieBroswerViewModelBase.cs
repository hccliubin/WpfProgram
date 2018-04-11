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

        private MovieFileViewModel _current;
        /// <summary> 选中项 </summary>
        public MovieFileViewModel Current
        {
            get { return _current; }
            set
            {
                _current = value;
                RaisePropertyChanged();
            }
        }

        public MovieBroswerViewModelBase()
        {
            RelayCommand = new RelayCommand(new Action<object>(ButtonClickFunc));
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

        /// <summary>
        /// 按钮点击事件
        /// </summary>
        /// <param name="obj"></param>
        private void ButtonClickFunc(object obj)
        {
            string buttonName = obj as string;

            if (buttonName == "Open")
            {
                if (SelectItem == null) return;

                Process.Start(SelectItem.FilePath);

                this.SelectItem.LastTime = DateTime.Now;

                int count = this.SelectItem.Count + 1;

                this.SelectItem.Count = count;
            }

            else if (buttonName == "Favorite")
            {
                var favorite = MovieBrowserDataManager.Instance.ViewModelItem.Find(l => l.Type == FileType.Favorate);

                favorite.CommonSource.Add(this.SelectItem);

                this.CommonSource.Remove(this.SelectItem);

            }

            else if (buttonName == "Delete")
            {
                var delete = MovieBrowserDataManager.Instance.ViewModelItem.Find(l => l.Type == FileType.Delete);

                delete.CommonSource.Add(this.SelectItem);

                this.CommonSource.Remove(this.SelectItem);
            }

            else if (buttonName == "DeleteDeep")
            {
                string folderFullName = Path.GetDirectoryName(this.SelectItem.FilePath);

                var folderName = Path.GetFileNameWithoutExtension(folderFullName);

                if (folderName == Path.GetFileNameWithoutExtension(this.SelectItem.FilePath))
                {
                    Directory.Delete(folderFullName, true);
                }
                else
                {
                    File.Delete(this.SelectItem.FilePath);
                }

                this.CommonSource.Remove(this.SelectItem);
            }

            else if (buttonName == "ReLoad")
            {
                var noraml = MovieBrowserDataManager.Instance.ViewModelItem.Find(l => l.Type == FileType.Normal);

                noraml.CommonSource.Add(this.SelectItem);

                this.CommonSource.Remove(this.SelectItem);
            }
            else if (buttonName == "ShowImage")
            {

                var folder = Path.GetDirectoryName(this.SelectItem.FilePath);

                var collection = DirectoryHelper.GetAllFile(folder, l => l.Extension.EndsWith("jpg"));

                if (collection == null || collection.Count == 0)
                {
                    MessageWindow.ShowDialog("不存在预览图片！");
                    return;
                }

                foreach (var item in collection)
                {
                    Process.Start(item);
                }
            }
            else if (buttonName == "InsertImage")
            {

                string folder = Path.GetDirectoryName(this.SelectItem.FilePath);

                string imageName = Path.GetFileNameWithoutExtension(this.SelectItem.FilePath);

                string imagePath = Path.Combine(folder, imageName + ".jpg");

                // Todo ：如果存在生成随机图片名称
                if (File.Exists(imagePath))
                {
                    imagePath = Path.Combine(folder, imageName + Guid.NewGuid() + ".jpg");
                }


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
            }

            else if (buttonName == "Refresh")
            {

                var filters = this.FilterType.Split('/').ToList();

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

                List<MovieFileViewModel> itemsNew = items.FindAll(l => filters.Exists(k => match(l.Type))).ToList();

                foreach (var item in itemsNew)
                {
                    item.IsVisible = Visibility.Visible;
                }
            }


            // Todo ：全选 
            if (obj is bool)
            {
                foreach (var item in this.CommonSource)
                {
                    item.IsChecked = (bool)obj;

                }
            }

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

        private string _filterType=string.Empty;
        /// <summary> 说明 </summary>
        public string FilterType
        {
            get { return _filterType; }
            set
            {
                _filterType = value;
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
