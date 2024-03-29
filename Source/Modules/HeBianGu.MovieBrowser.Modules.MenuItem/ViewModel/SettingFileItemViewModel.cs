﻿
#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2018/4/25 14:26:13 
 * 文件名：SettingFileViewModel 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.MovieBrowser.Modules.MenuItem.ViewModel
{
    /// <summary> 说明 </summary>
    partial class SettingFileItemViewModel : NotifyPropertyChanged
    {
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

        private string _fileName;
        /// <summary> 说明 </summary>
        public string FileName
        {
            get { return _fileName; }
            set
            {
                _fileName = value;
                RaisePropertyChanged();
            }
        }

        public SettingFileItemViewModel(string name)
        {
            this.FileName = name;

            RelayCommand = new RelayCommand(new Action<object>(ButtonClickFunc));

        }

        /// <summary>
        /// 按钮点击事件
        /// </summary>
        /// <param name="obj"></param>
        private void ButtonClickFunc(object obj)
        {
            string buttonName = obj.ToString();

            if (buttonName == "DeleteItem")
            {
                if(DeleteAction!=null)
                {
                    DeleteAction.Invoke();
                }
            }
        }


        public Action DeleteAction { get; set; } 


        public RelayCommand RelayCommand { get; set; }
    }
}
