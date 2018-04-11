#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/11/29 15:54:03 
 * 文件名：SystemFolderModule 
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

namespace SystemFolderModule
{
    [ModuleExport(typeof(SystemFolderModule))]
    public class SystemFolderModule : IModule
    {
        [Import]
        IRegionManager _regionManager;

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(RegionNames.MainNavigationRegion, typeof(View.NavigationBar));
        }
    }
}
