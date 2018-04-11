#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2018/3/29 18:16:48 
 * 文件名：MovieConfig 
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;
using HeBianGu.Base.Util;
using HeBianGu.General.ModuleManager.Service;

namespace HeBianGu.MovieBrowser.Modules.MovieBrowserManagerModule.Config
{
    public class MoviePrivider
    {
        ///// <summary> 保存 </summary>
        //public void Save(ObservableCollection<MovieFileViewModel> models, string caseName)
        //{
        //    string s = models.SerializeJson<ObservableCollection<MovieFileViewModel>>();

        //    System.IO.File.WriteAllText(CaseNotifyService.Instance.LocalCasePath(caseName), s);
        //}



        //public ObservableCollection<MovieFileViewModel> GetCaseData(string caseName)
        //{
        //    string filePath = CaseNotifyService.Instance.LocalCasePath(caseName);

        //    string json = File.ReadAllText(filePath);

        //    var models = json.SerializeDeJson<ObservableCollection<MovieFileViewModel>>();

        //    return models;
        //}
    }
}
