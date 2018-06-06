#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/11/29 11:19:37 
 * 文件名：SaveBoardProvider 
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
    class SaveBoardProvider:BaseFactory<SaveBoardProvider>
    {

        private string _configerPath;
        /// <summary> 配置文件路径 </summary>
        public string ConfigerPath
        {
            get
            {
                string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), SysTemConfiger.ConfigerFolder);

                _configerPath = System.IO.Path.Combine(filePath, "saveboard.txt");

                Directory.CreateDirectory(filePath);

                if (!File.Exists(_configerPath))
                {
                    File.WriteAllText(_configerPath, string.Empty);
                }


                return _configerPath;
            }
        }


        public SaveBoardViewModel Create()
        {
            SaveBoardViewModel c = new SaveBoardViewModel();

            string s = File.ReadAllText(ConfigerPath);

            ObservableCollection<ClipBoradBindModel> b = s.SerializeDeJson<ObservableCollection<ClipBoradBindModel>>();

            if (b == null || b.Count == 0) return c;

            c.CommonSource = b;

            _current = c;

            return c;
        }

        public void Add(ClipBoradBindModel model)
        {
            if (_current == null) return;

            _current.CommonSource.Insert(0,model);

            this.Save();
        }

        private SaveBoardViewModel _current;
        /// <summary> 说明 </summary>
        public SaveBoardViewModel Current
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
            string c = this.Current.CommonSource.SerializeJson<ObservableCollection<ClipBoradBindModel>>();

            File.WriteAllText(this.ConfigerPath, c);
        }
    }
}
