#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/11/29 10:04:54 
 * 文件名：Class1 
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

namespace HeBianGu.General.ModuleManager.Model
{
    /// <summary> 记事本绑定实体 </summary>
    [Serializable]
    public class NotePadBindModel
    {

        private string _title = string.Empty;
        /// <summary> 说明 </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private string _detial = string.Empty;
        /// <summary> 说明 </summary>
        public string DetialMin
        {
            get
            {
                return _detial;
            }
            set { _detial = value; }
        }

        /// <summary> 说明 </summary>
        public string Detial
        {
            get
            {
                return _detial;
            }
            set { _detial = value; }
        }

        private int _level = 1;
        /// <summary> 说明 </summary>
        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }

        private DateTime _cTime = DateTime.Now;
        /// <summary> 说明 </summary>
        public DateTime CreateTime
        {
            get { return _cTime; }
            set { _cTime = value; }
        }

        public string CTime
        {
            get { return _cTime.ToString("yyyy-MM-dd"); }
        }

        private DateTime _notifyTime = DateTime.Now;
        /// <summary> 说明 </summary>
        public DateTime NotifyTime
        {
            get { return _notifyTime; }
            set { _notifyTime = value; }
        }

        /// <summary> 图片路径 </summary>
        public string ImagePath
        {
            get
            {
                switch (this.Level)
                {
                    case 1:
                        return "&#xe629;";
                    case 2:
                        return "&#xe629;";
                    case 3:
                        return "&#xe629;";
                    case 4:
                        return "&#xe629;";
                    case 5:
                        return "&#xe629;";
                    default:
                        return "&#xe629;";
                }
            }
        }


        private List<int> _levelSource = new List<int>() { 1, 2, 3, 4, 5 };
        /// <summary> 说明 </summary>
        public List<int> LevelSource
        {
            get { return _levelSource; }
        }


    }
}
