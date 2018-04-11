#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2018/1/29 14:45:46 
 * 文件名：DocumentEngine 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using Controls.PrintWorkService.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace HeBianGu.General.WpfDocument
{

    /// <summary> 文本ViewModel抽象基类 </summary>
    public abstract partial class DocumentEngine
    {

        List<ItemModel> _collection = new List<ItemModel>();

        int pageCapicity = 32;

        public void CreateDocument(List<object> collection)
        {
            _collection.Clear();
            _pages.Clear();

            foreach (var item in collection)
            {
                ItemModel t = new ItemModel();
                t.Text = item.ToString();

                if (item is string)
                {
                    TextBlock tt = new TextBlock();
                    tt.Text = item.ToString();
                    t.Content = tt;
                }
                else if (item is TextBlock)
                {
                    t.Content = item;
                }


                _collection.Add(t);
            }

            int total = this.GetPageCount;

            for (int i = 0; i < total; i++)
            {
                DocumentViewModel d = this.GetPageAt(i);

                this.Pages.Add(d);
            }
        }


        /// <summary> 获取指定页 </summary>
        public DocumentViewModel GetPageAt(int index)
        {
            int f = index * pageCapicity;
            int t = index * pageCapicity + pageCapicity;
            var cs = _collection.TakeFromTo(f, t).ToList();

            DocumentViewModel d = new DocumentViewModel();

            for (int i = 0; i < this.pageCapicity; i++)
            {
                if (i < cs.Count)
                {
                    d.DataSource.Add(cs[i]);
                }
                else
                {
                    ItemModel item = new ItemModel();
                    d.DataSource.Add(item);
                }


            }

            foreach (var item in cs)
            {

            }

            return d;
        }

        private ObservableCollection<DocumentViewModel> _pages = new ObservableCollection<DocumentViewModel>();
        /// <summary> 所有页 </summary>
        public ObservableCollection<DocumentViewModel> Pages
        {
            get { return _pages; }
            set
            {
                _pages = value;
                RaisePropertyChanged();
            }
        }

        /// <summary> 总页数 </summary>
        public int GetPageCount
        {
            get
            {
                if (_collection.Count % pageCapicity == 0)
                {
                    return _collection.Count / pageCapicity;
                }
                else
                {
                    return _collection.Count / pageCapicity + 1;
                }
            }
        }

        public void TestDemo()
        {
            List<object> sb = new List<object>();

            string format = @"{0}．提供如下人群的健康管理、疾病管理：";


            for (int i = 0; i < 100; i++)
            {
                sb.Add(string.Format(format, i));
            }

            this.CreateDocument(sb);
        }

        internal Style underLineRun;
        internal Style titleTextBox;
        internal Style centerTextBox;
        internal Style secondTitleTextBox;
        internal Style detailTextBox;
        internal Style selecteRun;

        protected void Init(FrameworkElement element)
        {
            underLineRun = element.FindResource("UnderLineRun") as Style;
            titleTextBox = element.FindResource("TitleTextBox") as Style;
            centerTextBox = element.FindResource("CenterTextBox") as Style;
            secondTitleTextBox = element.FindResource("SecondTitleTextBox") as Style;
            detailTextBox = element.FindResource("DetailTextBox") as Style;
            selecteRun = element.FindResource("SelecteRun") as Style;
        }


        internal List<object> sb = new List<object>();

   


    }

    partial class DocumentEngine : INotifyPropertyChanged
    {
        #region - MVVM -

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
