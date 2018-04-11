#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/11/29 15:54:37 
 * 文件名：SystemFolderProvider 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HeBianGu.Base.Util;
using HeBianGu.General.ModuleManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemFolderModule.ViewModel;

namespace SystemFolderModule.Provider
{
    class SystemFolderProvider:BaseFactory<SystemFolderProvider>
    {

        public SystemFolderViewModel Create()
        {
            SystemFolderViewModel s = new SystemFolderViewModel();

            var names = Enum.GetNames(typeof(Environment.SpecialFolder));

            s.CommonSource.Clear();

            foreach (var item in names)
            {
                Environment.SpecialFolder e = item.GetEnumByNameOrValue<Environment.SpecialFolder>();

                //string recent = Environment.GetFolderPath(e);

                string recent = WinSysHelper.Instance.GetSystemPath(e);

                FileBindModel f = new FileBindModel(recent);
                f.FileName = item;
                if (string.IsNullOrEmpty(f.FilePath)) continue;
                s.CommonSource.Add(f);
            }

            return s;
        }

        private SystemFolderViewModel _current;
        /// <summary> 说明 </summary>
        public SystemFolderViewModel Current
        {
            get
            {
                if (_current == null)
                    _current = this.Create();
                return _current;
            }
        }
    }
}
