#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2018/2/2 18:02:56 
 * 文件名：FileDocumentEngine 
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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.General.WpfDocument
{

    /// <summary> 显示文本方式 </summary>
    public class FileDocumentEngine : DocumentEngine
    {
        /// <summary> 设置文本</summary>
        public void SetdData(FrameworkElement element, string path)
        {
            base.Init(element);

            if (string.IsNullOrEmpty(path)) return;

            sb.Clear();

            var collection = File.ReadAllLines(path, Encoding.Default);

            foreach (var item in collection)
            {
                this.ConvertLine(item);
            }

            this.CreateDocument(sb);
        }

        public void ConvertLine(string line)
        {
            TextBlock t = new TextBlock();
            t.Text = line;
            t.Style = detailTextBox;
            sb.Add(t);
        }

    }
}
