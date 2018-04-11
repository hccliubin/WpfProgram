#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/11/29 10:49:52 
 * 文件名：Class1 
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
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.General.ModuleManager.Model
{
    /// <summary> 文件绑定实体 </summary>
    [Serializable]
    public class ClipBoradBindModel
    {
        ClipBoardType _type;

        string _detial;
        public ClipBoradBindModel(string detial, ClipBoardType type)
        {
            Type = type;
            _detial = detial;
            this._createTime = DateTime.Now;
        }

        /// <summary> 说明 </summary>
        public string Title
        {
            get
            {
                switch (Type)
                {
                    case ClipBoardType.FileSystem:
                        return Path.GetFileName(_detial);
                    case ClipBoardType.Image:
                        return _detial;
                    case ClipBoardType.Text:
                        return _detial;
                    default:
                        return _detial;
                }
            }
        }

        public string Detial
        {
            get
            {
                return _detial;
            }
            set
            {
                _detial = value;
            }
        }

        /// <summary> 图片路径 </summary>
        public Icon ImagePath
        {
            get
            {
                switch (Type)
                {
                    case ClipBoardType.FileSystem:
                        return IconHelper.Instance.GetSystemInfoIcon(_detial);
                    case ClipBoardType.Image:
                        return null;
                    case ClipBoardType.Text:
                        return null;
                    default:
                        return null;
                }

                return null;

            }
        }

        private DateTime _createTime;
        /// <summary> 复制的时间 </summary>
        public string CreateTime
        {
            get { return _createTime.ToString("yyyy-MM-dd hh:mm:ss"); }
        }

        public ClipBoardType Type
        {
            get
            {
                return _type;
            }

            set
            {
                _type = value;
            }
        }
    }

    /// <summary> 剪贴板内容样式 </summary>
   public  enum ClipBoardType : int
    {
        FileSystem = 0, Text, Image
    }
}
