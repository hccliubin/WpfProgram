#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/11/29 13:38:30 
 * 文件名：IntergrationToolViewModel 
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
using IntergrationToolModule.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntergrationToolModule.Provider
{
    class IntergrationToolProvider : BaseFactory<IntergrationToolProvider>
    {


        private string _configerPath;
        /// <summary> 配置文件路径 </summary>
        public string ConfigerPath
        {
            get
            {
                string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), SysTemConfiger.ConfigerFolder);

                _configerPath = System.IO.Path.Combine(filePath, "Extend");


                Directory.CreateDirectory(filePath);
                Directory.CreateDirectory(_configerPath);

                return _configerPath;
            }
        }

        internal IntergrationToolViewModel Create()
        {

            IntergrationToolViewModel i = new IntergrationToolViewModel();

            string extendPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SysTemConfiger.ConfigerExtend);

            if (!Directory.Exists(ConfigerPath)) return i;

            DirectoryInfo folder = Directory.CreateDirectory(ConfigerPath);

            var extends = folder.GetDirectories();

            foreach (var item in extends)
            {
                var file = item.Find<FileInfo>(l => l.Extension.EndsWith("exe"));

                if (file == null) continue;

                FileBindModel fileBind = new FileBindModel(file);
                fileBind.FileName = item.Name;

                i.CommonSource.Add(fileBind);
            }

            return i;
        }

        private IntergrationToolViewModel _current;
        /// <summary> 说明 </summary>
        public IntergrationToolViewModel Current
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
