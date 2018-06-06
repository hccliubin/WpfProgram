#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/11/29 9:53:29 
 * 文件名：NotePadPrivider 
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
using HeBianGu.General.ModuleManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotePadModule
{
    class NotePadPrivider:BaseFactory<NotePadPrivider>
    {

        private string _configerPath;
        /// <summary> 配置文件路径 </summary>
        public string ConfigerPath
        {
            get
            {
                string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), SysTemConfiger.ConfigerFolder);

                _configerPath = System.IO.Path.Combine(filePath, "notepad.dat");

                Directory.CreateDirectory(filePath);

                if (!File.Exists(_configerPath))
                {
                    File.WriteAllText(_configerPath, string.Empty);
                }

                return _configerPath;
            }
        }

        /// <summary> 此方法的说明 </summary>
        public NotePadViewModel Create()
        {
            NotePadViewModel n = new NotePadViewModel();

            if (!File.Exists(ConfigerPath)) return n;

            string s = File.ReadAllText(ConfigerPath);

            ObservableCollection<NotePadBindModel> b = s.SerializeDeJson<ObservableCollection<NotePadBindModel>>();

            if (b == null || b.Count == 0) return n;

            n.CommonSource = b;


            return n;
        }

        private NotePadViewModel _current;
        /// <summary> 说明 </summary>
        public NotePadViewModel Current
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
