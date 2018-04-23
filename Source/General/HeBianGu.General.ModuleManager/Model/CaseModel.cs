#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2018/3/30 14:31:21 
 * 文件名：CaseModel 
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
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.General.ModuleManager.Model
{
    [Serializable]
   public class CaseModel
    {
        private string _caseName;
        /// <summary> 说明 </summary>
        public string CaseName
        {
            get { return _caseName; }
            set { _caseName = value; }
        }

        private string _casePath;
        /// <summary> 说明 </summary>
        public string CasePath
        {
            get { return _casePath; }
            set { _casePath = value; }
        }


        /// <summary> 图片路径 </summary>
        public Icon ImagePath
        {
            get { return Base.Util.IconHelper.Instance.GetSystemInfoIcon(CasePath); }
        }


        private string _listJson;
        /// <summary> 说明 </summary>
        public string ListJson
        {
            get { return _listJson; }
            set { _listJson = value; }
        }

        [NonSerialized]
        private string _folderPath;
        /// <summary> 说明 </summary>
        
        public string FolderPath
        {
            get { return _folderPath; }
            set { _folderPath = value; }
        }



        //private string _favJson;
        ///// <summary> 说明 </summary>
        //public string FavJson
        //{
        //    get { return _favJson; }
        //    set { _favJson = value; }
        //}

        //private string _deleteJson;
        ///// <summary> 说明 </summary>
        //public string DeleteJson
        //{
        //    get { return _deleteJson; }
        //    set { _deleteJson = value; }
        //}

        //private bool _isOpen;
        ///// <summary> 说明 </summary>
        //public bool IsOpen
        //{
        //    get { return _isOpen; }
        //    set { _isOpen = value; }
        //}



    }
}
