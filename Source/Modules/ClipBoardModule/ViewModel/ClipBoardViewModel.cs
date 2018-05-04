#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/11/29 10:37:11 
 * 文件名：ClipBoardViewModel 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using ClipBoardModule.Provider;
using HeBianGu.Base.Util;
using HeBianGu.Base.WpfBase;
using HeBianGu.General.ModuleManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ClipBoardModule.ViewModel
{

    /// <summary> 说明 </summary>
    [System.ComponentModel.Composition.Export]
    public partial class ClipBoardViewModel
    {
        #region - 成员 -

        private ObservableCollection<ClipBoradBindModel> _commonSource = new ObservableCollection<ClipBoradBindModel>();

        /// <summary> 常用文件集合 </summary>
        public ObservableCollection<ClipBoradBindModel> CommonSource
        {
            get { return _commonSource; }
            set
            {
                _commonSource = value;
                RaisePropertyChanged("CommonSource");
            }
        }

        private ClipBoradBindModel _current;
        /// <summary> 选中项 </summary>
        public ClipBoradBindModel Current
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

        public RelayCommand ObjectCommond { get; set; }

        #endregion

        public ClipBoardViewModel()
        {
            ClipBoardRegisterService.Instance.ClipBoardChanged += OnClipboardChanged;
            ClipBoardRegisterService.Instance.ClipBoardChanged += OnClipboardTextFile;
            ClipBoardRegisterService.Instance.ClipBoardChanged += OnClipboardTextUrl;

            ObjectCommond = new RelayCommand(RunMethod);

            DoubleClickCommond = new RelayCommand(DoubleClickMethod);
        }

        /// <summary> 此方法的说明 </summary>
        public void DoubleClickMethod(object obj)
        {
            this.RunMethod("Open");
        }

        public ICommand DoubleClickCommond { get; set; }

        /// <summary> 此方法的说明 </summary>
        public void RunMethod(object obj)
        {

            if (_current == null) return;


            // Todo ：复制 
            if (obj.ToString() == "Copy")
            {
                if (_current.Type == ClipBoardType.FileSystem)
                {
                    // HTodo  ：添加到剪贴板中 
                    StringCollection c = new StringCollection();
                    c.Add(_current.Detial);
                    System.Windows.Clipboard.SetFileDropList(c);

                }
                else if (_current.Type == ClipBoardType.Text)
                {
                    System.Windows.Clipboard.SetText(_current.Detial);
                }
                else
                {
                    throw new Exception("未实现该功能");
                    //Process.Start("mspaint", m.Detial);
                }
            }
            // Todo ：删除 
            else if (obj.ToString() == "Delete")
            {
                this.CommonSource.Remove(_current);

                ClipBoardProvider.Instance.Save();
            }

            // Todo ：归档 
            else if (obj.ToString() == "Save")
            {
                SaveBoardProvider.Instance.Add(this.Current);
            }

            // Todo ：打开 
            else if (obj.ToString() == "Open")
            {

                if (_current.Type == ClipBoardType.FileSystem)
                {
                    Process.Start(_current.Detial);
                }
                else if (_current.Type == ClipBoardType.Text)
                {

                    string temp = System.Environment.GetEnvironmentVariable("TEMP");

                    string tempFile = Path.Combine(temp
                        , "WindowStartTool.txt");

                    File.WriteAllText(tempFile, _current.Detial);

                    Process.Start(tempFile);
                }
                else
                {
                    throw new Exception("未实现该功能");
                    //Process.Start("mspaint", m.Detial);
                }
            }
        }

        /// <summary> 剪贴板内容改变 </summary>
        internal void OnClipboardChanged()
        {
            // HTodo  ：复制的文件路径 
            string text = System.Windows.Clipboard.GetText().Trim();

            if (!string.IsNullOrEmpty(text))
            {
                if (this.CommonSource.Count > 0)
                {
                    ClipBoradBindModel last = this.CommonSource.First();

                    if (last.Detial != text)
                    {
                        ClipBoradBindModel f = new ClipBoradBindModel(text, ClipBoardType.Text);
                        this.CommonSource.Insert(0, f);
                    }
                }
                else
                {
                    ClipBoradBindModel f = new ClipBoradBindModel(text, ClipBoardType.Text);
                    this.CommonSource.Insert(0, f);
                }
            }


            // HTodo  ：复制的文件 
            System.Collections.Specialized.StringCollection list = System.Windows.Clipboard.GetFileDropList();

            foreach (var item in list)
            {
                if (Directory.Exists(item) || File.Exists(item))
                {
                    if (this.CommonSource.Count > 0)
                    {
                        ClipBoradBindModel last = this.CommonSource.First();

                        if (last.Detial != item)
                        {
                            ClipBoradBindModel f = new ClipBoradBindModel(item, ClipBoardType.FileSystem);
                            this.CommonSource.Insert(0, f);
                        }
                    }
                    else
                    {
                        ClipBoradBindModel f = new ClipBoradBindModel(item, ClipBoardType.FileSystem);
                        this.CommonSource.Insert(0, f);
                    }


                }
            }

            //// HTodo  ：复制的图片 
            //BitmapSource bit = System.Windows.Clipboard.GetImage();

            //if (bit != null)
            //{
            //    if (this._viewModel.ClipBoradSource.Count > 0)
            //    {
            //        ClipBoradBindModel last = this._viewModel.ClipBoradSource.First();

            //        if (last.Detial != bit.ToString())
            //        {
            //            ClipBoradBindModel f = new ClipBoradBindModel(bit.ToString(), ClipBoardType.Image);
            //            this._viewModel.ClipBoradSource.Insert(0, f);
            //        }
            //    }
            //    else
            //    {
            //        ClipBoradBindModel f = new ClipBoradBindModel(bit.ToString(), ClipBoardType.Image);
            //        this._viewModel.ClipBoradSource.Insert(0, f);
            //    }


            //}
            ClipBoardProvider.Instance.Save();
        }

        /// <summary> 自动打开文件 </summary>
        internal void OnClipboardTextFile()
        {
            // HTodo  ：复制的文件路径 
            string text = System.Windows.Clipboard.GetText();

            if (string.IsNullOrEmpty(text)) return;

            string temp = text.Trim();


            if (Directory.Exists(temp))
            {
                Process.Start(temp);
            }

            if (File.Exists(temp))
            {
                Process.Start(temp);
            }
        }

        /// <summary> 自动打开URL </summary>
        internal void OnClipboardTextUrl()
        {
            // HTodo  ：复制的文件路径 
            string text = System.Windows.Clipboard.GetText();

            if (string.IsNullOrEmpty(text)) return;

            string temp = text.Trim();


            if (temp.IsURL())
            {
                Process.Start(temp);
            }
        }

    }

    partial class ClipBoardViewModel : INotifyPropertyChanged
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
