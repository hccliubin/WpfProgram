#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/11/28 10:46:52 
 * 文件名：CommonProvider 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeBianGu.Base.Util;
using CommonDocumentModule.ViewModel;
using System.IO;
using HeBianGu.General.ModuleManager;
using System.Collections.ObjectModel;
using HeBianGu.Base.Util;

namespace CommonDocumentModule
{
    class CommonProvider : BaseFactory<CommonProvider>
    {
        private string _configerPath;
        /// <summary> 配置文件路径 </summary>
        public string ConfigerPath
        {
            get
            {
                string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), SysTemConfiger.ConfigerFolder);

                _configerPath = System.IO.Path.Combine(filePath, "myDoc.configer");

                Directory.CreateDirectory(filePath);

                if (!File.Exists(_configerPath))
                {
                    File.WriteAllText(_configerPath, string.Empty);
                }

                return _configerPath;
            }
        }

        /// <summary> 从本地缓存文件读取数据 </summary>
        public CommonContentViewModel Create()
        {
            CommonContentViewModel c = new CommonContentViewModel();

            if (!File.Exists(ConfigerPath)) return c;

            string s = File.ReadAllText(ConfigerPath);

            ObservableCollection<FileBindModel> b = s.SerializeDeJson<ObservableCollection<FileBindModel>>();

            if (b == null || b.Count == 0) return c;

            c.CommonSource = b;

            return c;

        }

        private CommonContentViewModel _current;
        /// <summary> 说明 </summary>
        public CommonContentViewModel Current
        {
            get
            {
                if (_current == null)
                {
                    _current = this.Create();
                }

                return _current;
            }
           
        }


        /// <summary> 保存配置文件 </summary>
        public void Save(CommonContentViewModel c)
        {
            string s = c.CommonSource.SerializeJson<ObservableCollection<FileBindModel>>();

            File.WriteAllText(_configerPath, s);
        }


    }
}
