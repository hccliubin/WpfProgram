#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/12/1 10:36:49 
 * 文件名：Class1 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HeBianGu.Base.WpfBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace HeBianGu.General.ModuleManager
{
    public static class ModuleStaticCommand
    {

        /// <summary> 运行文件 </summary>
        public static ICommand OpenFileCommand
        {
            get
            {
                Action<object> action = l =>
                {
                    if (l is ListBox)
                    {
                        ListBox listbox = l as ListBox;

                        FileBindModel f = listbox.SelectedItem as FileBindModel;

                        if (f == null) return;

                        f.LastTime = DateTime.Now;

                        // Todo ：打开文件 
                        Process.Start(f.FilePath);

                        // Todo ：按时间排序 
                        ObservableCollection<FileBindModel> source=  listbox.ItemsSource as ObservableCollection<FileBindModel>;

                        var collection = source.OrderByDescending(k => k.LastTime);

                        ObservableCollection<FileBindModel> temp = new ObservableCollection<FileBindModel>();

                        foreach (var item in collection)
                        {
                            temp.Add(item);
                        };

                        listbox.ItemsSource = temp;
                    }
                };

                return new RelayCommand(action);
            }
        }


        /// <summary> 删除文件 </summary>
        public static ICommand DeleteFileCommand
        {
            get
            {
                Action<object> action = l =>
                {
                    if (l is ListBox)
                    {
                        ListBox listbox = l as ListBox;

                        IList collection = listbox.ItemsSource as IList;

                        collection.Remove(listbox.SelectedItem);
                    }
                };

                return new RelayCommand(action);
            }
        }
    }
}
