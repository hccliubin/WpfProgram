#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2018/4/11 15:12:49 
 * 文件名：CaseViewModel 
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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MovieBrowserToolApp.ViewModel
{

    /// <summary> 案例绑定模型 </summary>
    partial class CaseViewModel
    {

        public RelayCommand RelayCommand { get; set; }


        public CaseViewModel(CaseModel model)
        {
            _model = model;

            RelayCommand = new RelayCommand(new Action<object>(ButtonClickFunc));

        }

        /// <summary>
        /// 按钮点击事件
        /// </summary>
        /// <param name="obj"></param>
        private void ButtonClickFunc(object obj)
        {
            string buttonName = obj as string;

            if (buttonName == "AddCase")
            {
               
            }

        }

        private CaseModel _model;
        /// <summary> 说明 </summary>
        public CaseModel Model
        {
            get { return _model; }
            set { _model = value; }
        }


        private string _caseName;
        /// <summary> 说明 </summary>
        public string CaseName
        {
            get { return _model.CaseName; }
            set
            {
                _model.CaseName = value;
                RaisePropertyChanged();
            }
        }

        private string _casePath;
        /// <summary> 说明 </summary>
        public string CasePath
        {
            get { return _model.CasePath; }
            set
            {
                _model.CasePath = value;
                RaisePropertyChanged();
            }
        }

        private bool _isOpen;
        /// <summary> 说明 </summary>
        public bool IsOpen
        {
            get { return _isOpen; }
            set
            {
                _isOpen = value;
                RaisePropertyChanged();


            }
        }


        private string _folderPath;
        /// <summary> 说明 </summary>
        public string FolderPath
        {
            get { return _model.FolderPath; }
            set
            {
                _model.FolderPath = value;
                RaisePropertyChanged();
            }
        }



        /// <summary> 图片路径 </summary>
        public Icon ImagePath
        {
            get { return IconHelper.Instance.GetSystemInfoIcon(CasePath); }
        }
    }

    partial class CaseViewModel : INotifyPropertyChanged
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
