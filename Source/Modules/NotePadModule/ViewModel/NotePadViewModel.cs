#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/11/29 9:51:15 
 * 文件名：NotePadViewModel 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HeBianGu.General.ModuleManager;
using HeBianGu.General.ModuleManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NotePadModule
{
    /// <summary> 说明 </summary>
    [System.ComponentModel.Composition.Export]
    public partial class NotePadViewModel
    {
        #region - 成员 -

        private ObservableCollection<NotePadBindModel> _commonSource = new ObservableCollection<NotePadBindModel>();

        /// <summary> 常用文件集合 </summary>
        public ObservableCollection<NotePadBindModel> CommonSource
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
    }

    partial class NotePadViewModel : INotifyPropertyChanged
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
