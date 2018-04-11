#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2018/2/2 18:03:34 
 * 文件名：BindDocumentEngine 
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
using System.Windows.Documents;

namespace HeBianGu.General.WpfDocument
{

    /// <summary> 显示模板和绑定模型方式 </summary>
    public class BindDocumentEngine : DocumentEngine
    {
        public void SetData(FrameworkElement element, DocumentModel model, string path, string title)
        {
            if (string.IsNullOrEmpty(path)) return;

            if (model == null) return;

            base.Init(element);

            underLineRun = element.FindResource("UnderLineRun") as Style;
            titleTextBox = element.FindResource("TitleTextBox") as Style;
            centerTextBox = element.FindResource("CenterTextBox") as Style;
            secondTitleTextBox = element.FindResource("SecondTitleTextBox") as Style;
            detailTextBox = element.FindResource("DetailTextBox") as Style;
            selecteRun = element.FindResource("SelecteRun") as Style;


            List<object> sb = new List<object>();


            this.SetTitle(sb, model, title);

            var collection = File.ReadAllLines(path, Encoding.Default);

            foreach (var item in collection)
            {
                this.ConvertDetail(sb, item, model);
            }

            this.SetEnd(sb, model);

            this.CreateDocument(sb);
        }

        /// <summary> 设置抬头 </summary>
        void SetTitle(List<object> sb, DocumentModel model, string title)
        {

            {
                TextBlock t = new TextBlock();
                t.Text = title;
                t.Style = titleTextBox;
                sb.Add(t);

                t = new TextBlock();
                t.Text = string.Empty;
                sb.Add(t);
            }

            {

                // Todo ：        市           社区卫生服务中心或卫生院 
                TextBlock t = new TextBlock();
                //Run r = new Run(model.Country.ToDL());
                //r.Style = underLineRun;
                //t.Inlines.Add(r);

                //r = new Run("市");
                //t.Inlines.Add(r);

                //r = new Run(model.Community.ToDL());
                //r.Style = underLineRun;
                //t.Inlines.Add(r);

                //r = new Run("社区卫生服务中心或卫生院");
                //t.Inlines.Add(r);
                //t.Style = centerTextBox;
                //sb.Add(t);



                t.Text = model.AreaName;
                t.Style = centerTextBox;
                sb.Add(t);

                t = new TextBlock();
                t.Text = string.Empty;
                sb.Add(t);

            }

            {
                //string format = "     {0}     ";
                //format string.Format(format, str);

                // Todo ：监督投诉电话：                
                TextBlock t2 = new TextBlock();
                Run r = new Run("    监督投诉电话：");
                t2.Inlines.Add(r);
                r = new Run(model.Complain.ToDL().ToDL());
                r.Style = underLineRun;
                t2.Inlines.Add(r);

                r = new Run(".");
                t2.Inlines.Add(r);

                sb.Add(t2);
            }

            {
                bool flag = true;
                foreach (var item in model.Famliys)
                {
                    //甲方：姓名 性别         年龄 联系方式
                    TextBlock t = new TextBlock();
                    Run r = new Run();
                    if (flag)
                    {
                        r.Text = "    甲方：姓名";
                        flag = false;
                    }
                    else
                    {
                        r.Text = "               姓名";
                    }


                    t.Inlines.Add(r);
                    r = new Run(item.Item1.ToDL());
                    r.Style = underLineRun;
                    t.Inlines.Add(r);

                    r = new Run("性别");
                    t.Inlines.Add(r);

                    r = new Run(item.Item2.ToDL());
                    r.Style = underLineRun;
                    t.Inlines.Add(r);

                    r = new Run("年龄");
                    t.Inlines.Add(r);

                    r = new Run(item.Item3.ToDL());
                    r.Style = underLineRun;
                    t.Inlines.Add(r);

                    r = new Run("联系方式");
                    t.Inlines.Add(r);

                    r = new Run(item.Item4.ToDL());
                    r.Style = underLineRun;
                    t.Inlines.Add(r);

                    r = new Run(".");
                    t.Inlines.Add(r);

                    t.Style = detailTextBox;
                    sb.Add(t);

                }
            }

            {
                //乙方：医生团队负责人             经办人姓名             
                TextBlock t = new TextBlock();
                Run r = new Run("    乙方：医生团队负责人");
                t.Inlines.Add(r);
                r = new Run(model.LeaderB.ToDL());
                r.Style = underLineRun;
                t.Inlines.Add(r);

                r = new Run("经办人姓名");
                t.Inlines.Add(r);

                r = new Run(model.Operator.ToDL());
                r.Style = underLineRun;
                t.Inlines.Add(r);

                r = new Run(".");
                t.Inlines.Add(r);

                t.Style = detailTextBox;
                sb.Add(t);
            }

            {
                // Todo ：团队成员姓名 联系方式
                foreach (var item in model.Members)
                {
                    TextBlock t = new TextBlock();
                    Run r = new Run("               团队成员姓名");
                    t.Inlines.Add(r);
                    r = new Run(item.Item1.ToDL());
                    r.Style = underLineRun;
                    t.Inlines.Add(r);

                    r = new Run("联系方式");
                    t.Inlines.Add(r);

                    r = new Run(item.Item2.ToDL());
                    r.Style = underLineRun;
                    t.Inlines.Add(r);

                    r = new Run(".");
                    t.Inlines.Add(r);

                    t.Style = detailTextBox;
                    sb.Add(t);

                }
            }

        }

        /// <summary> 设置结束 </summary>
        void SetEnd(List<object> sb, DocumentModel model)
        {
            {
                //甲方签字：                             乙方签字：                  
                TextBlock t = new TextBlock();
                t.Text = string.Empty;
                sb.Add(t);

                t = new TextBlock();
                t.Text = string.Empty;
                sb.Add(t);

                t = new TextBlock();
                Run r = new Run("甲方签字：");
                t.Inlines.Add(r);
                r = new Run(model.Contract.ToDL());
                r.Style = underLineRun;
                t.Inlines.Add(r);

                r = new Run("                   乙方签字：");
                t.Inlines.Add(r);

                r = new Run(model.Operator.ToDL());
                r.Style = underLineRun;
                t.Inlines.Add(r);

                r = new Run(".");
                t.Inlines.Add(r);

                t.Style = centerTextBox;
                sb.Add(t);
            }
        }

        /// <summary> 每行的转换规则 </summary>
        void ConvertDetail(List<object> sb, string str, DocumentModel model)
        {
            string[] collection = { "     一", "     二", "     三", "     四", "     五", "     六", "     七", "     八", "     九" };

            string[] collection1 = { "        70", "        71", "        72" };



            bool isSelect = collection1.ToList().Exists(l => str.StartsWith(l));

            bool isTitle = collection.ToList().Exists(l => str.StartsWith(l));

            if (isTitle)
            {
                TextBlock t = new TextBlock();
                t.Text = str;
                t.Style = secondTitleTextBox;
                sb.Add(t);
            }
            else if (isSelect)
            {
                // Todo ：过滤 
                return;

                int len = 20;

                var splits = str.Split(new char[] { ' ', '：' }, StringSplitOptions.RemoveEmptyEntries);

                TextBlock t = new TextBlock();
                if (splits.Length == 2)
                {
                    Run r = new Run();
                    r.Text = "        ";
                    t.Inlines.Add(r);

                    r = new Run();
                    r.Text = splits[0] + ":" + splits[1];

                    if (model.Result.Contains(splits[0]))
                    {
                        r.Style = selecteRun;
                        r.Text += "√";
                    }
                    t.Inlines.Add(r);

                }
                else if (splits.Length == 4)
                {
                    Run r = new Run();
                    r.Text = "        ";
                    t.Inlines.Add(r);

                    r = new Run();
                    r.Text = splits[0] + ":" + splits[1];
                    if (model.Result.Contains(splits[0]))
                    {
                        r.Style = selecteRun;
                        r.Text += "√";
                    }
                    t.Inlines.Add(r);

                    r = new Run();
                    r.Text = "            ";
                    t.Inlines.Add(r);


                    r = new Run();
                    r.Text = splits[2] + ":" + splits[3];

                    if (model.Result.Contains(splits[2]))
                    {
                        r.Style = selecteRun;
                        r.Text += "√";
                    }
                    t.Inlines.Add(r);
                }

                t.Style = detailTextBox;
                sb.Add(t);
            }
            else if (str.Contains("基本公卫服务包, 小服务包"))
            {
                // Todo ：加粗下划线 
                TextBlock t = new TextBlock();

                Run r = new Run();
                r.Text = "            ";
                t.Inlines.Add(r);

                r = new Run();
                r.Text = str.Trim();
                r.Style = selecteRun;
                t.Inlines.Add(r);
                sb.Add(t);
            }
            else if (str.Contains("有效期为1年"))
            {
                var arr = str.Split(new char[] { '1' }, StringSplitOptions.RemoveEmptyEntries);

                if (arr.Length != 2) return;

                TextBlock t = new TextBlock();
                Run r = new Run();
                r.Text = arr[0];
                t.Inlines.Add(r);

                // Todo ：加粗下划线 
                r = new Run(); ;
                r.Text = " " + model.SignYear + " ";
                r.Style = selecteRun;
                t.Inlines.Add(r);

                r = new Run();
                r.Text = arr[1];
                t.Inlines.Add(r);
                t.Style = detailTextBox;
                sb.Add(t);
            }
            else
            {
                TextBlock t = new TextBlock();
                t.Text = str;
                t.Style = detailTextBox;
                sb.Add(t);
            }
        }
    }
}
