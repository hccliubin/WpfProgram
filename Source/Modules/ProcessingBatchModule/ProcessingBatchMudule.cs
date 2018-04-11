#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/11/29 13:55:10 
 * 文件名：ProcessingBatchMudule 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HeBianGu.General.ModuleManager.ModuleManager;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessingBatchModule
{

    [ModuleExport(typeof(ProcessingBatchMudule))]
    public class ProcessingBatchMudule : IModule
    {
        [Import]
        IRegionManager _regionManager;

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(RegionNames.MainNavigationRegion, typeof(View.NavigationBar));
        }
    }
}
