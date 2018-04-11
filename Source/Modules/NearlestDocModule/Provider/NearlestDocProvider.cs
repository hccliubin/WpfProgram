#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/11/29 11:54:19 
 * 文件名：NearlestDocProvider 
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
using NearlestDocModule.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NearlestDocModule.Provider
{
    class NearlestDocProvider:BaseFactory<NearlestDocProvider>
    {

        public NearlestDocViewModel Create()
        {
            NearlestDocViewModel n = new NearlestDocViewModel();

            string recent = Environment.GetFolderPath(Environment.SpecialFolder.Recent);

            var files = Directory.GetFiles(recent);

            files = files.OrderByDescending(l => Directory.GetLastAccessTime(l)).ToArray();

            files = files.Take(100).ToArray();

            n.CommonSource.Clear();

            foreach (var item in files)
            {
                FileBindModel f = new FileBindModel(item);

                if (string.IsNullOrEmpty(f.FilePath)) continue;

                n.CommonSource.Add(f);
            }

            return n;
        }

        private NearlestDocViewModel _current;
        /// <summary> 说明 </summary>
        public NearlestDocViewModel Current
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
