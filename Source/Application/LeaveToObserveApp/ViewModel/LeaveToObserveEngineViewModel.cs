#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2018/4/2 16:13:16 
 * 文件名：Class1 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using LeaveToObserveApp.Provider;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace LeaveToObserveApp.ViewModel
{

    /// <summary> 说明 </summary>
    partial class LeaveToObserveEngineViewModel
    {

        private ObservableCollection<LeaveToObserveItemViewModel> _collection = new ObservableCollection<LeaveToObserveItemViewModel>();
        /// <summary> 说明 </summary>
        public ObservableCollection<LeaveToObserveItemViewModel> Collection
        {
            get { return _collection; }
            set
            {
                if (_collection != value)
                {
                    _collection = value;
                    RaisePropertyChanged();
                }

            }
        }

        private ObservableCollection<string> _message = new ObservableCollection<string>();
        /// <summary> 提示信息 </summary>
        public ObservableCollection<string> Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged();
            }
        }



        private string _currentCode;
        /// <summary> 读到的编码 </summary>
        public string CurrentCode
        {
            get { return _currentCode; }
            set
            {
                _currentCode = value;
                RaisePropertyChanged();
            }
        }



        private LeaveToObserveItemViewModel _current;
        /// <summary> 说明 </summary>
        public LeaveToObserveItemViewModel Current
        {
            get { return _current; }
            set
            {
                _current = value;
                RaisePropertyChanged();
            }
        }



        public int _total;
        public int Total
        {
            get
            {
                return _total;
            }
            set
            {
                if (_total != value)
                {
                    _total = value;
                    RaisePropertyChanged();
                }
            }
        }


        private Visibility _isBuzy = Visibility.Hidden;
        /// <summary> 说明 </summary>
        public Visibility IsBuzy
        {
            get { return _isBuzy; }
            set
            {
                _isBuzy = value;
                RaisePropertyChanged();
            }
        }


        public LeaveToObserveEngineViewModel()
        {

            ScanningPrivder.Instance.CallBackScanning += Instance_CallBackScanning;
            ScanningPrivder.Instance.StartEngine();

            Action action = () =>
            {

                this.IsBuzy = Visibility.Visible;

                Thread.Sleep(5000);

                this.IsBuzy = Visibility.Hidden;
            };

            action.DoTask();


        }

        private void Instance_CallBackScanning(string obj)
        {
            Action action = () =>
              {
                  this.IsBuzy = Visibility.Visible;

                  this.CurrentCode = obj;

                  ServiceProvider.Instance.DoWork(obj,this);

                  Thread.Sleep(2000);

                  this.CurrentCode = string.Empty;

                  this.IsBuzy = Visibility.Hidden;
              };

            action.DoTask();

        }



    }

    partial class LeaveToObserveEngineViewModel : INotifyPropertyChanged
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
