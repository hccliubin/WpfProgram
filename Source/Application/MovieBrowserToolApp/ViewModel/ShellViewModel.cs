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
using HeBianGu.General.WpfControlLib;
using HeBianGu.MovieBrower.UserControls;
using HeBianGu.MovieBrowser.Modules.MenuItem.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

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
                  System.Windows.Application.Current.Dispatcher.Invoke(() => this.Message = l);
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
                    model.FolderPath = CaseNotifyService.LocalCaseFolder;
                    CaseNotifyService.Instance.CreateCase(model);
                    var c = new CaseViewModel(model);
                    this.CaseSource.Add(c);
                    this.CurrentCase = c;
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
                bool result = MessageWindow.ShowDialog("删除无法恢复,确定要删除？");

                if (!result) return;

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
                this.SaveCase();
            }

            else if (buttonName == "ClearOrder")
            {
                this.SaveCase();

                // Todo ：整理到同级别
                CaseNotifyService.Instance.ClearOrder(CurrentCase.Model);

                this.ButtonClickFunc("OpenCase");
            }

            // Todo ：重新加载 
            else if (buttonName == "RefreshLoad")
            {

                this.SaveCase();

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

            // Todo ：打开案例 
            else if (buttonName == "OpenOutCase")
            {
                OpenFileDialog dialog = new OpenFileDialog();

                dialog.Title = "请选择案例文件";

                string format = "案例文件(*{0}) | *{0}| 所有文件(*.*) | *.*";

                dialog.Filter = string.Format(format, CaseNotifyService.extend);

                var result = dialog.ShowDialog();

                if (result != DialogResult.OK) return;

                string path = Path.GetDirectoryName(dialog.FileName);

                this.SaveCase();

                var caseModel = CaseNotifyService.Instance.LoadCase(path);

                CaseViewModel model = new CaseViewModel(caseModel);

                this.CaseSource.Add(model);

                this.CurrentCase = model;

            }

            // Todo ：另存为 
            else if (buttonName == "SaveOutCase")
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();

                var result = dialog.ShowDialog();

                if (result != DialogResult.OK) return;

                string path = dialog.SelectedPath;

                this.SaveCase();

                CaseNotifyService.Instance.SaveOutLoad(CurrentCase.Model, path);
            }

            // Todo ：移动路径 
            else if (buttonName == "MoveFolder")
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();

                var result = dialog.ShowDialog();

                if (result != DialogResult.OK) return;

                string path = dialog.SelectedPath;

                if (this.CurrentCase == null) return;

                this.SaveCase();

                CaseNotifyService.Instance.MoveFolderLoad(CurrentCase.Model, path);

                this.RefreshCurrent(this.CurrentCase.Model);
            }

            // Todo ：合并案例 
            else if (buttonName == "MergeCase")
            {
                var models = this.CaseSource.Select(l => l.Model).ToList();

                models.Remove(this.CurrentCase.Model);

                if (models == null || models.Count == 0) return;

                MergeCaseWindow merge = new MergeCaseWindow(models);

                var result = merge.ShowDialog();

                if (result == null || result.Value == false) return;

                var selection = merge.SelectItems();

                if (selection == null || selection.Count == 0) return;

                Predicate<List<CaseModel>> match = l =>
                  {
                      foreach (var item in selection)
                      {
                          if (item.CasePath.Contains(this.CurrentCase.CasePath) || this.CurrentCase.CasePath.Contains(item.CasePath))
                          {
                              return false;
                          }
                      }

                      return true;
                  };

                if (!match(selection))
                {
                    this.Message = "不满足合并要求，合并路径不能有父子关系！";
                    return;
                }

                this.SaveCase();

                CaseNotifyService.Instance.MergeCases(CurrentCase.Model, selection);

                this.RefreshCurrent(this.CurrentCase.Model);

            }
        }



        /// <summary> 此方法的说明 </summary>
        public void SaveCase()
        {
            if (_caseSource == null) return;

            if (CurrentCase == null) return;

            CaseNotifyService.Instance.OnSaveCase(CurrentCase.Model);
        }


        void RefreshCurrent(CaseModel caseModel)
        {
            // Todo ：将上一次为空 
            this._lastCase = null;

            this.CaseSource.Remove(CurrentCase);

            CaseViewModel model = new CaseViewModel(caseModel);

            this.CaseSource.Add(model);



            this.CurrentCase = model;
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
