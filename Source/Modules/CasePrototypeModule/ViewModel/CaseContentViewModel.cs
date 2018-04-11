#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/11/28 17:15:05 
 * 文件名：CaseContentViewModel 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HeBianGu.General.ModuleManager;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CasePrototypeModule.ViewModel
{

    /// <summary> 说明 </summary>
    [System.ComponentModel.Composition.Export]
    public partial class CaseContentViewModel
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

        ///// <summary> 刷新界面数据 </summary>
        //public void RefreshUI()
        //{
        //    var collection = this.CommonSource.OrderByDescending(l => l.LastTime);

        //    ObservableCollection<FileBindModel> temp = new ObservableCollection<FileBindModel>();

        //    foreach (var item in collection)
        //    {
        //        temp.Add(item);
        //    };

        //    this.CommonSource = temp;

        //}

        //public CaseContentViewModel()
        //{
        //    _showInfoCommand = new DelegateCommand(ShowInfo);
        //}
    }

    partial class CaseContentViewModel : INotifyPropertyChanged
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
