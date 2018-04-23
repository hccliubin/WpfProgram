#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2018/3/30 14:17:45 
 * 文件名：ShellViewModel 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HeBianGu.Base.WpfBase;
using HeBianGu.General.Logger;
using HeBianGu.General.ModuleManager.Model;
using HeBianGu.General.ModuleManager.Service;
using HeBianGu.MovieBrower.UserControls;
using HeBianGu.MovieBrowser.Modules.MenuItem.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MovieBrowserToolApp.ViewModel
{

    /// <summary> 说明 </summary>
    partial class ShellViewModel
    {
        public RelayCommand RelayCommand { get; set; }

        public ShellViewModel()
        {
            RelayCommand = new RelayCommand(new Action<object>(ButtonClickFunc));

            this.LoadData();


            // Todo ：注册运行日志 
            Log4Servcie.Instance.RunLog += l =>
              {
                  Application.Current.Dispatcher.Invoke(() => this.Message = l);
              };
        }


        /// <summary> 此方法的说明 </summary>
        public void LoadData()
        {
            var collection = CaseNotifyService.Instance.LoadAllCase();

            if (collection == null || collection.Count == 0) return;

            foreach (var item in collection)
            {
                this.CaseSource.Add(new CaseViewModel(item));
            }

            // Todo ：默认打开第一个 
            this.ButtonClickFunc("ShowDefault");

        }

        private string _message;
        /// <summary> 说明 </summary>
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged();
            }
        }




        /// <summary>
        /// 按钮点击事件
        /// </summary>
        /// <param name="obj"></param>
        private void ButtonClickFunc(object obj)
        {
            string buttonName = obj as string;


            // Todo ：增加案例 
            if (buttonName == "AddCase")
            {
                AddCaseWindow addWindow = new AddCaseWindow();
                var result = addWindow.ShowDialog();

                if (result.HasValue && result.Value)
                {
                    CaseModel model = new CaseModel();
                    model.CaseName = addWindow.ViewModel.CaseName;
                    model.CasePath = addWindow.ViewModel.CasePath;
                    CaseNotifyService.Instance.CreateCase(model);
                    this.CaseSource.Add(new CaseViewModel(model));
                }
            }

            // Todo ：打开案例 
            else if (buttonName == "OpenCase")
            {
                if (_caseSource == null) return;

                foreach (var item in CaseSource)
                {
                    item.IsOpen = false;
                }

                if (this.CurrentCase != null)
                {
                    this.CurrentCase.IsOpen = true;

                }

                if (_lastCase != null && _lastCase != CurrentCase)
                {
                    // Todo ：保存上一次案例 
                    CaseNotifyService.Instance.OnSaveCase(_lastCase.Model);
                }

                // Todo ：加载本次案例 
                CaseNotifyService.Instance.OnCaseChanged(CurrentCase == null ? null : CurrentCase.Model);

                _lastCase = CurrentCase;

            }

            // Todo ：删除案例 
            else if (buttonName == "DeleteCase")
            {
                if (CurrentCase == _lastCase) _lastCase = null;

                CaseNotifyService.Instance.DeleteCase(CurrentCase.Model);

                this.CaseSource.Remove(CurrentCase);

                // Todo ：默认打开第一个 
                this.ButtonClickFunc("ShowDefault");
            }

            // Todo ：重命名 
            else if (buttonName == "ReNameCase")
            {

            }

            // Todo ：保存案例 
            else if (buttonName == "SaveCase")
            {
                if (_caseSource == null) return;

                CaseNotifyService.Instance.OnSaveCase(CurrentCase.Model);
            }

            else if (buttonName == "ClearOrder")
            {
                // Todo ：整理到同级别
                CaseNotifyService.Instance.ClearOrder(CurrentCase.Model);

                this.ButtonClickFunc("OpenCase");
            }

            // Todo ：重新加载 
            else if (buttonName == "RefreshLoad")
            {
                // Todo ：整理到同级别
                CaseNotifyService.Instance.RefreshLoad(CurrentCase.Model);

                this.ButtonClickFunc("OpenCase");
            }

            else if (buttonName == "ShowDefault")
            {
                if (this.CaseSource == null || this.CaseSource.Count == 0)
                {
                    this.CurrentCase = null;
                }
                else
                {
                    // Todo ：默认打开第一个 
                    this.CurrentCase = this.CaseSource[0];
                }
            }
        }

        private CaseViewModel _currentCase;
        /// <summary> 说明 </summary>
        public CaseViewModel CurrentCase
        {
            get { return _currentCase; }
            set
            {
                _currentCase = value;


                // Todo ：打开案例 
                this.ButtonClickFunc("OpenCase");

                RaisePropertyChanged();
            }
        }

        CaseViewModel _lastCase;

        private ObservableCollection<CaseViewModel> _caseSource = new ObservableCollection<CaseViewModel>();
        /// <summary> 说明 </summary>
        public ObservableCollection<CaseViewModel> CaseSource
        {
            get { return _caseSource; }
            set
            {
                _caseSource = value;
                RaisePropertyChanged();
            }
        }
    }

    partial class ShellViewModel : INotifyPropertyChanged
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
