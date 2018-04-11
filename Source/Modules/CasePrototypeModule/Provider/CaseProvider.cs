#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/11/28 17:26:22 
 * 文件名：CaseProvider 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using CasePrototypeModule.ViewModel;
using HeBianGu.Base.Util;
using HeBianGu.General.ModuleManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeBianGu.Base.Util;
using System.Collections.ObjectModel;
using System.Threading;

namespace CasePrototypeModule.Provider
{
    class CaseProvider : BaseFactory<CaseProvider>
    {
        private string _configerPath;
        /// <summary> 配置文件路径 </summary>
        public string ConfigerPath
        {
            get
            {
                string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), SysTemConfiger.ConfigerFolder);

                _configerPath = System.IO.Path.Combine(filePath, "Solutions");

                return _configerPath;
            }
        }

        /// <summary> 从本地缓存文件读取数据 </summary>
        public CaseContentViewModel Create()
        {

            CaseContentViewModel c = new CaseContentViewModel();

            if (!Directory.Exists(ConfigerPath)) return c;

            DirectoryInfo folder = Directory.CreateDirectory(ConfigerPath);

            foreach (var item in folder.GetDirectories())
            {
                var files = item.FindAll<FileInfo>(l => l.Extension.EndsWith("sln"));

                foreach (var it in files)
                {
                    if (it == null || !it.Exists) continue;

                    FileBindModel fileBind = new FileBindModel(it);
                    fileBind.FileName = it.Name;
                    c.CommonSource.Add(fileBind);
                }
            }

            Thread.Sleep(10000);

            return c;

        }

        private CaseContentViewModel _current;
        /// <summary> 说明 </summary>
        public CaseContentViewModel Current
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
