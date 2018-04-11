#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/11/28 10:11:09 
 * 文件名：CommonContentViewModel 
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using HeBianGu.General.ModuleManager;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using System.IO;
using System.Windows.Forms;
using HeBianGu.Base.WpfBase;

namespace CommonDocumentModule.ViewModel
{
    /// <summary> 常用文件 </summary>
    [System.ComponentModel.Composition.Export]
    public partial class CommonContentViewModel
    {
        public CommonContentViewModel()
        {
            _showInfoCommand = new RelayCommand(OpenCurrent);
            _addFolderCommand = new DelegateCommand(AddFolderItem);
            _addFileCommand = new DelegateCommand(AddFileItem);
            _deleteFastCommad = new DelegateCommand(DeleteCurrent);
            _copyCommad = new DelegateCommand(Copy_AddFolder);
        }

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

        #endregion

        #region - 命令 -

        RelayCommand _showInfoCommand;

        /// <summary> 打开文件 </summary>
        public RelayCommand ShowInfoCommand
        {
            get
            {
                return _showInfoCommand;
            }
        }

        /// <summary> 打开文件 </summary>
        public void OpenCurrent(object sender)
        {
            if (this.Current == null) return;

            this.Current.LastTime = DateTime.Now;

            System.Diagnostics.Process.Start(this.Current.FilePath);

            this.RefreshUI();
        }

        private DelegateCommand _addFolderCommand;
        /// <summary> 添加文件夹 </summary>
        public DelegateCommand AddFolderCommand
        {
            get
            {
                return _addFolderCommand;
            }
        }

        /// <summary> 添加文件夹 </summary>
        public void AddFolderItem()
        {
            FolderBrowserDialog open = new FolderBrowserDialog();

            if (open.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            string filePath = open.SelectedPath;

            FileBindModel m = new FileBindModel(filePath);
            if (string.IsNullOrEmpty(m.FilePath)) return;
            this.CommonSource.Add(m);

            this.RefreshUI();

            this.SaveToFile();
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
        
        /// <summary> 删除当前文件 </summary>
        public void DeleteCurrent()
        {
            if (this.Current == null) return;

            this.CommonSource.Remove(this.Current);

            this.RefreshUI();

            this.SaveToFile();
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

            if (!string.IsNullOrEmpty(text))
            {
                if (Directory.Exists(text))
                {
                    FileBindModel f = new FileBindModel(Directory.CreateDirectory(text));
                    this.CommonSource.Add(f);
                }

                if (File.Exists(text))
                {
                    FileInfo file = new FileInfo(text);
                    FileBindModel f = new FileBindModel(file);

                    this.CommonSource.Add(f);
                }
            }


            // HTodo  ：复制的文件 
            System.Collections.Specialized.StringCollection list = System.Windows.Clipboard.GetFileDropList();

            foreach (var item in list)
            {

                if (Directory.Exists(item))
                {
                    FileBindModel f = new FileBindModel(Directory.CreateDirectory(item));
                    this.CommonSource.Add(f);
                }

                if (File.Exists(item))
                {
                    FileInfo file = new FileInfo(item);
                    FileBindModel f = new FileBindModel(file);

                    this.CommonSource.Add(f);
                }
            }

            this.RefreshUI();

            this.SaveToFile();

        }

        private DelegateCommand _addFileCommand;
        /// <summary> 添加常用文件 </summary>
        public DelegateCommand AddFileCommand
        {
            get
            {
                return _addFileCommand;
            }
        }


        /// <summary> 此方法的说明 </summary>
        public void AddFileItem()
        {
            System.Windows.Forms.OpenFileDialog open = new System.Windows.Forms.OpenFileDialog();

            if (open.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            string filePath = open.FileName;

            FileBindModel m = new FileBindModel(filePath);
            if (string.IsNullOrEmpty(m.FilePath)) return;
            this.CommonSource.Add(m);

            this.RefreshUI();

            this.SaveToFile();
        }

        #endregion

        #region - 方法 -

        /// <summary> 刷新界面数据 按时间排序 </summary>
        public void RefreshUI()
        {
            var collection = this.CommonSource.OrderByDescending(l => l.LastTime);

            ObservableCollection<FileBindModel> temp = new ObservableCollection<FileBindModel>();


            foreach (var item in collection)
            {
                temp.Add(item);
            };

            this.CommonSource = temp;

        }


        /// <summary> 保存配置文件 </summary>
        public void SaveToFile()
        {
            CommonProvider.Instance.Save(this);
        }

        #endregion

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
    }

    partial class CommonContentViewModel : INotifyPropertyChanged
    {
        #region - MVVM -

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
