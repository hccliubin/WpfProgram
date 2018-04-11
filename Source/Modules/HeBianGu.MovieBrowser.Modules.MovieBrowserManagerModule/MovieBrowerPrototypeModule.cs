#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2018/3/29 16:01:14 
 * 文件名：Class1 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HeBianGu.General.ModuleManager.ModuleManager;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.MovieBrowser.Modules.MovieBrowserManagerModule
{
    [ModuleExport(typeof(MovieBrowerPrototypeModule))]
    public class MovieBrowerPrototypeModule : IModule
    {
        [System.ComponentModel.Composition.Import]
        public Microsoft.Practices.Prism.Regions.IRegionManager RegionManager;

        public void Initialize()
        {
            // todo: 11 - Pre-populating regions with items
            // Items may be placed in regions prior to navigating to them.  In this case, since we're registering
            // the type CalendarNavigationItemView, it will be created and added to the region when the MainNavigationRegion 
            // is loaded.  Even though it is created when the control associated with the region becomes visible,
            // the view itself won't be visible until it's navigated to. 
            //
            // Anything placed in a region in this manner will not be added to the navigation journal.
            this.RegionManager.RegisterViewWithRegion(RegionNames.MainNavigationRegion, typeof(View.MenuToolButton));
        }
    }
}
