#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/11/29 13:38:11 
 * 文件名：IntergrationToolViewModel 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HeBianGu.General.ModuleManager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IntergrationToolModule.ViewModel
{

    /// <summary> 说明 </summary>
    [Export]
   public partial class IntergrationToolViewModel
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
    }

    partial class IntergrationToolViewModel : INotifyPropertyChanged
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
