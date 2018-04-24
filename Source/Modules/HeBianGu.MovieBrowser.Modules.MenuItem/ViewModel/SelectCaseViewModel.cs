#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2018/4/24 16:34:31 
 * 文件名：Class1 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HeBianGu.General.ModuleManager.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.MovieBrowser.Modules.MenuItem.ViewModel
{
    /// <summary> 说明 </summary>
    public partial class SelectCaseViewModel
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
        
        /// <summary> 说明 </summary>
        public string CaseName
        {
            get { return _model.CaseName; }
        }

        private CaseModel _model;
        /// <summary> 说明 </summary>
        public CaseModel Model
        {
            get { return _model; }
            set
            {
                _model = value;
                RaisePropertyChanged();
            }
        }


        public SelectCaseViewModel(CaseModel model)
        {
            _model = model;
        }

    }

    partial class SelectCaseViewModel : INotifyPropertyChanged
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
