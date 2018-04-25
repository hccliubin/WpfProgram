#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2018/3/30 14:25:25 
 * 文件名：AddCaseViewModel 
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
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.MovieBrowser.Modules.MenuItem.ViewModel
{


    /// <summary> 说明 </summary>
    public partial class AddCaseViewModel
    {
        private string _caseName;
        /// <summary> 说明 </summary>
        public string CaseName
        {
            get { return _caseName; }
            set
            {
                _caseName = value;
                RaisePropertyChanged();
            }
        }

        private string _casePath;
        /// <summary> 说明 </summary>
        public string CasePath
        {
            get { return _casePath; }
            set
            {
                _casePath = value;
                RaisePropertyChanged();
            }
        }

    }

    partial class AddCaseViewModel : INotifyPropertyChanged
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
