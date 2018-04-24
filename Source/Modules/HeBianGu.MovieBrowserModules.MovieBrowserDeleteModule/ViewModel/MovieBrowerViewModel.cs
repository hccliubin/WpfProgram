#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2018/3/29 16:17:05 
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
using HeBianGu.General.ModuleManager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using HeBianGu.General.ModuleManager.Service;
using System.Diagnostics;
using HeBianGu.Base.Util;
using System.IO;
using HeBianGu.General.ModuleManager.Model;
using HeBianGu.MovieBrower.UserControls;
using HeBianGu.MovieBrower.UserControls.DataManager;

namespace HeBianGu.MovieBrowserModules.MovieBrowserDeleteModule.ViewModel
{

    /// <summary> 说明 </summary>
    [System.ComponentModel.Composition.Export]
    public partial class MovieBrowerViewModel: MovieBroswerViewModelBase
    {
        public MovieBrowerViewModel()
        {
            this.Type = FileType.Delete;
        }
    }


}
