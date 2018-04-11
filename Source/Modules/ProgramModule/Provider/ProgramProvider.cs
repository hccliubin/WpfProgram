#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/11/29 11:29:45 
 * 文件名：ProgramProvider 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HeBianGu.Base.Util;
using HeBianGu.General.ModuleManager;
using Microsoft.Win32;
using ProgramModule.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramModule.Provider
{
    class ProgramProvider : BaseFactory<ProgramProvider>
    {
        public ProgramViewModel Create()
        {
            ProgramViewModel program = new ProgramViewModel();

            //HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall 此键的子健为本机所有注册过的软件的卸载程序,通过此思路进行遍历安装的软件
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
            string[] key1 = key.GetSubKeyNames();//返回此键所有的子键名称
            List<string> key2 = key1.ToList<string>();//因为有的项木有"DisplayName"或"DisplayName"的键值的时候要把键值所在数组中的的元素进行删除
            RegistryKey subkey = null;

            for (int i = 0; i < key2.Count; i++)
            {

                //通过list泛型数组进行遍历,某款软件项下的子键
                subkey = key.OpenSubKey(key2[i]);

                if (subkey.GetValue("DisplayName") == null) continue;
                if (subkey.GetValue("DisplayIcon") == null) continue;


                string path = subkey.GetValue("DisplayIcon").ToString();
                //截取子键值的最后一位进行判断
                string SubPath = path.Substring(path.Length - 1, 1);

                //如果为o 就是ico 或 找不到exe的 表示为图标文件或只有个标识而没有地址的
                if (SubPath == "o" || path.IndexOf("exe") == -1)
                {
                    //首先删除数组中此索引的元素
                    key2.RemoveAt(i);
                    //把循环条件i的值进行从新复制,否则下面给listview的项的tag属性进行赋值的时候会报错
                    i -= 1;
                    continue;
                }

                //如果为e 就代表着是exe可执行文件,
                if (SubPath == "e")
                {
                    //则表示可以直接把地址赋给tag属性
                    FileBindModel p = new FileBindModel(path);
                    p.FileName = subkey.GetValue("DisplayName").ToString();
                    program.CommonSource.Add(p);
                    continue;
                }
                //因为根据观察 取的是DisplayIcon的值 表示为图片所在路径 如果为0或1,则是为可执行文件的图标  
                if (SubPath == "0" || SubPath == "1")
                {
                    //进行字符串截取,
                    path = path.Substring(0, path.LastIndexOf("e") + 1);
                    //则表示可以直接把地址赋给tag属性
                    FileBindModel p = new FileBindModel(path);
                    p.FileName = subkey.GetValue("DisplayName").ToString();
                    program.CommonSource.Add(p);
                    continue;
                }
            }

            return program;
        }

        private ProgramViewModel _current;
        /// <summary> 说明 </summary>
        public ProgramViewModel Current
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
