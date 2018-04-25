#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2018/4/25 16:46:44 
 * 文件名：SettingCaseViewModel 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.MovieBrowser.Modules.MenuItem.ViewModel
{
    class SettingCaseViewModel : NotifyPropertyChanged
    {
        private Visibility _baseVisible = Visibility.Collapsed;
        /// <summary> 说明 </summary>
        public Visibility BaseVisible
        {
            get { return _baseVisible; }
            set
            {
                _baseVisible = value;
                RaisePropertyChanged();
            }
        }

        private Visibility _caseVisible = Visibility.Visible;
        /// <summary> 说明 </summary>
        public Visibility CaseVisible
        {
            get { return _caseVisible; }
            set
            {
                _caseVisible = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand RelayCommand { get; set; }

        public SettingCaseViewModel()
        {
            RelayCommand = new RelayCommand(new Action<object>(ButtonClickFunc));
        }

        private void ButtonClickFunc(object obj)
        {
            string buttonName = obj as string;

            switch (buttonName)
            {
                case "BaseSetClick":
                    {
                        this.BaseVisible = Visibility.Visible;
                        this.CaseVisible = Visibility.Collapsed;
                    }
                    break;

                case "CaseSetClick":
                    {
                        this.BaseVisible = Visibility.Collapsed;
                        this.CaseVisible = Visibility.Visible;
                    }
                    break;
                case "Btn_Sumit":
                    {
                        


                    }
                    break;
                case "Case2":
                    {

                    }
                    break;
                case "Case3":
                    {

                    }
                    break;
                case "Case4":
                    {

                    }
                    break;
                case "Case5":
                    {

                    }
                    break;
                case "Case6":
                    {

                    }
                    break;
                case "Case7":
                    {

                    }
                    break;
                case "Case8":
                    {

                    }
                    break;
                case "Case9":
                    {

                    }
                    break;
                case "Case10":
                    {

                    }
                    break;
                case "Case11":
                    {

                    }
                    break;
                case "Case12":
                    {

                    }
                    break;
                default:
                    break;
            }
        }
    }
}
