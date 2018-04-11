#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/11/29 16:12:31 
 * 文件名：FavoriteProvider 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HeBianGu.Base.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FavoriteModule.ViewModel;
using HeBianGu.General.ModuleManager;
using System.IO;
using System.Collections.ObjectModel;
using FavoriteModule.View;

namespace FavoriteModule.Provider
{
    class FavoriteProvider : BaseFactory<FavoriteProvider>
    {
        internal FavoriteViewModel Create()
        {
            FavoriteViewModel fav = new FavoriteViewModel();

            string recent = Environment.GetFolderPath(Environment.SpecialFolder.Favorites);

            var files = recent.GetAllFile();

            fav.CommonSource.Clear();

            foreach (var item in files)
            {
                FileBindModel f = new FileBindModel(item);

                if (string.IsNullOrEmpty(f.FilePath)) continue;

                fav.CommonSource.Add(f);

            }

            return fav;
        }

        private FavoriteViewModel _current;
        /// <summary> 说明 </summary>
        public FavoriteViewModel Current
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
