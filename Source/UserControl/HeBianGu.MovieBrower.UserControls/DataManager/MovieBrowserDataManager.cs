#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2018/4/4 15:27:08 
 * 文件名：MovieBrowserDataManager 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HeBianGu.Base.Util;
using HeBianGu.General.ModuleManager.Model;
using HeBianGu.General.ModuleManager.Service;
using HeBianGu.MovieBrower.UserControls.DataManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.MovieBrower.UserControls
{
    public class MovieBrowserDataManager
    {

        public static MovieBrowserDataManager Instance = new MovieBrowserDataManager();

        private List<MovieBroswerViewModelBase> _viewModelItem = new List<MovieBroswerViewModelBase>();
        /// <summary> 说明 </summary>
        public List<MovieBroswerViewModelBase> ViewModelItem
        {
            get { return _viewModelItem; }
            set { _viewModelItem = value; }
        }


        public void Register(MovieBroswerViewModelBase vm)
        {
            _viewModelItem.Add(vm);
        }

        public MovieBrowserDataManager()
        {
            CaseNotifyService.Instance.CaseChanged += Instance_CaseChanged;

            CaseNotifyService.Instance.BeforeSaveCase += Instance_SaveCase;
        }

        private void Instance_SaveCase(General.ModuleManager.Model.CaseModel obj)
        {
            // Todo ：保存 
            foreach (var item in ViewModelItem)
            {
                List<MovieFileModel> models = new List<MovieFileModel>();

                foreach (var it in item.CommonSource)
                {
                    MovieFileModel m = new MovieFileModel();
                    m.CopyFromObj(it);
                    m.FileType = item.Type;
                    models.Add(m);
                }

                if (item.CaseItem == null) continue;

                item.CaseItem.Collection = models;
            }
        }

        private void Instance_CaseChanged(General.ModuleManager.Model.CaseModel obj)
        {
            // Todo ：加载数据 
            foreach (var item in ViewModelItem)
            {
                var caseItem = CaseNotifyService.Instance.CaseItems.Find(l => l.FileType == item.Type);

                item.CaseItem = caseItem;

                item.CommonSource.Clear();

                foreach (var it in caseItem.Collection) 
                {
                    MovieFileViewModel vm = new MovieFileViewModel(it);

                    item.CommonSource.Add(vm);
                }
            }
        }






    }

    public static class LocalConfiger
    {
        private static List<string> _types = new List<string>();

        public static List<string> Types
        {
            get
            {
                if (_types == null || _types.Count == 0)
                {
                    _types = CaseNotifyService.MovieTypes;

                }
                return _types;
            }
            set { _types = value; }
        }
    }
}
