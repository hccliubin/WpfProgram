
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HeBianGu.General.WpfDocument
{
    /// <summary>
    /// DocumnetControl.xaml 的交互逻辑
    /// </summary>
    public partial class DocumnetControl : UserControl
    {

       
        public DocumnetControl()
        {
            InitializeComponent();
        }

        public void Print()
        {
            //PrintProvider.Instance.CheckPrintShow();

            //temp.Clear();

            //this.FindVisualChildItem(this);

            //temp.Reverse();

            //foreach (var item in temp)
            //{
            //    item.Print();
            //}
        }


        List<DocumentPage> temp = new List<DocumentPage>();
        private void FindVisualChildItem(DependencyObject obj)
        {

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);

                if (child != null && child is DocumentPage)
                    temp.Add((DocumentPage)child);
                else
                {
                    FindVisualChildItem(child);
                }
            }
        }

        private static ChildItem FindVisualChildItem<ChildItem>(DependencyObject obj, string name) where ChildItem : FrameworkElement
        {
            if (null != obj)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(obj, i);

                    if (child != null && child is ChildItem && (child as ChildItem).Name.Equals(name))
                    {
                        return (ChildItem)child;
                    }
                    else
                    {
                        ChildItem childOfChild = FindVisualChildItem<ChildItem>(child, name);

                        if (childOfChild != null && childOfChild is ChildItem && (childOfChild as ChildItem).Name.Equals(name))
                        {
                            return childOfChild;
                        }
                    }
                }
            }
            return null;
        }

        public UserControl AsControl()
        {
            return this;
        }
    }




}
