#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/11/29 13:54:24 
 * 文件名：ProcessingBatchProvider 
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
using ProcessingBatchModule.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessingBatchModule.Provider
{
    class ProcessingBatchProvider : BaseFactory<ProcessingBatchProvider>
    {

        private string _configerPath;
        /// <summary> 配置文件路径 </summary>
        public string ConfigerPath
        {
            get
            {
                string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), SysTemConfiger.ConfigerFolder);

                _configerPath = System.IO.Path.Combine(filePath, "MulBat");


                Directory.CreateDirectory(filePath);

                Directory.CreateDirectory(_configerPath);

                if (!File.Exists(_configerPath))
                {
                    File.Create(_configerPath);
                }

                return _configerPath;
            }
        }

        public ProcessingBatchViewModel Create()
        {

            ProcessingBatchViewModel p = new ProcessingBatchViewModel();
            

            DirectoryInfo folder = Directory.CreateDirectory(ConfigerPath);

            var extends = folder.GetFiles();

            foreach (var item in extends)
            {
                if (!item.Extension.EndsWith("bat")) continue;

                FileBindModel fileBind = new FileBindModel(item);
                fileBind.FileName = item.Name;

                p.CommonSource.Add(fileBind);
            }

            return p;
        }


        private ProcessingBatchViewModel _current;
        /// <summary> 说明 </summary>
        public ProcessingBatchViewModel Current
        {
            get
            {
                if (_current == null)
                    _current = this.Create();
                return _current;
            }
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
