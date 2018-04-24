#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2018/4/3 15:06:39 
 * 文件名：MovieBrowerViewModel 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HeBianGu.Base.Util;
using HeBianGu.Base.WpfBase;
using HeBianGu.General.ModuleManager.Model;
using HeBianGu.General.ModuleManager.Service;
using HeBianGu.MovieBrower.UserControls;
using HeBianGu.MovieBrower.UserControls.DataManager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.MovieBrowser.Modules.MovieBrowserfavoriteModule.ViewModel
{
    /// <summary> 说明 </summary>
    [System.ComponentModel.Composition.Export]
    public partial class MovieBrowerViewModel:MovieBroswerViewModelBase
    {
        public MovieBrowerViewModel()
        {
            this.Type = FileType.Favorate;
        }
        
    }

}
