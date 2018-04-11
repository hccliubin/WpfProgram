#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2018/2/2 18:44:15 
 * 文件名：DocDocumentEngine 
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

    /// <summary> 格式化规则的模板   </summary>
    public class DocDocumentEngine : DocumentEngine
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

        List<string> formatIndex = new List<string>();

        public void ConvertLine(string line)
        {
            // Todo ：此处需要添加对line 格式化的解析规则  如：颜色 大小写 大小 下划线等 可以解析Run规则 
            TextBlock t = new TextBlock();
            t.Text = line;
            t.Style = detailTextBox;
            sb.Add(t);
        }

        public void Save(string path)
        {
            // Todo ：保存编辑后的模板 
        }
    }
}
