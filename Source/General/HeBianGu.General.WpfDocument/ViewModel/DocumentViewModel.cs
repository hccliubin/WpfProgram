#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2018/1/29 14:43:36 
 * 文件名：DocumentViewModel 
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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Controls.PrintWorkService.ViewModel
{

    /// <summary> 说明 </summary>
   public partial class DocumentViewModel
    {
        private ObservableCollection<ItemModel> _dataSource=new ObservableCollection<ItemModel>();
        /// <summary> 说明 </summary>
        public ObservableCollection<ItemModel> DataSource
        {
            get { return _dataSource; }
            set
            {
                _dataSource = value;
                RaisePropertyChanged();
            }
        }
    }

    partial class DocumentViewModel : INotifyPropertyChanged
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


    /// <summary> 说明 </summary>
   public  class ItemModel
    {
        private string _text;
        /// <summary> 说明 </summary>
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        private object _content;
        /// <summary> 说明 </summary>
        public object Content
        {
            get { return _content; }
            set { _content = value; }
        }


    }
}
