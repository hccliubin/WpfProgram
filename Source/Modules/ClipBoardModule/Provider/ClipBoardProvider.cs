#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/11/29 10:47:34 
 * 文件名：ClipBoardProvider 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using ClipBoardModule.ViewModel;
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

namespace ClipBoardModule.Provider
{
    class ClipBoardProvider:BaseFactory<ClipBoardProvider>
    {

        private string _configerPath;
        /// <summary> 配置文件路径 </summary>
        public string ConfigerPath
        {
            get
            {
                string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), SysTemConfiger.ConfigerFolder);

                _configerPath = System.IO.Path.Combine(filePath, "clipboard.txt");

                Directory.CreateDirectory(filePath);

                if (!File.Exists(_configerPath))
                {
                    File.Create(_configerPath);
                }
                

                return _configerPath;
            }
        }

        public ClipBoardViewModel Create()
        {
            ClipBoardViewModel c = new ClipBoardViewModel();

            string s = File.ReadAllText(ConfigerPath).Trim('\0');

            if (string.IsNullOrEmpty(s)) return c;

            ObservableCollection<ClipBoradBindModel> b = s.SerializeDeJson<ObservableCollection<ClipBoradBindModel>>();

            if (b == null || b.Count == 0) return c;

            c.CommonSource = b;

            _current = c;

            return c;
        }

        private ClipBoardViewModel _current;
        /// <summary> 说明 </summary>
        public ClipBoardViewModel Current
        {
            get
            {
                if (_current == null)
                    _current = this.Create();
                return _current;
            }
        }

        int capital = 200;

        public void Save()
        {
            int temp = this.Current.CommonSource.Count;

            for (int i = capital; i < temp; i++)
            {
                Current.CommonSource.RemoveAt(capital);
            }

            string c = this.Current.CommonSource.SerializeJson<ObservableCollection<ClipBoradBindModel>>();

            File.WriteAllText(this.ConfigerPath, c);
        }
    }
}
