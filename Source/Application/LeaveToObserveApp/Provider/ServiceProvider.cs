#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2018/4/4 9:48:28 
 * 文件名：ServiceProvider 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HeBianGu.Base.Util;
using HeBianGu.General.Logger;
using HeBianGu.General.WpfControlLib;
using LeaveToObserveApp.Model;
using LeaveToObserveApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LeaveToObserveApp.Provider
{
    class ServiceProvider
    {

        public static ServiceProvider Instance = new ServiceProvider();
        /// <summary> 加载列表 </summary>
        public List<LeaveToObserveItem> GetList()
        {
            List<LeaveToObserveItem> collection = new List<LeaveToObserveItem>();

            for (int i = 0; i < 190; i++)
            {
                LeaveToObserveItem model = new LeaveToObserveItem();


                collection.Add(model);
            }

            return collection;
        }


        /// <summary> 扫描出来一个条形码后，我们这边一个接口，然后返回真实的条形码 </summary>
        string GetServiceCode(string str)
        {
            if (this.Random(10)) return string.Empty;

            return str;
        }


        /// <summary> 再去后台数据库读取儿童信息 </summary>
        ChildInfo GetChild(string code)
        {
            if (this.Random(10)) return null;

            return new ChildInfo();
        }


        /// <summary> 再去写一条挂号数据 </summary>
        void AddLeaveData(RegisterInfo info)
        {

        }


        /// <summary> 执行工作流 </summary>
        public void DoWork(string str, LeaveToObserveEngineViewModel viewModel)
        {
            string code = this.GetServiceCode(str);

            if (string.IsNullOrEmpty(code))
            {
                MessageProvider.Instance.ShowWithLog("儿童预防接种本条码扫描失败！");

                return;
            }

            ChildInfo child = this.GetChild(code);


            if (child == null)
            {
                MessageProvider.Instance.ShowWithLog("儿童预防接种本条码扫描失败！");

                return;
            }

            child.ID = str;
            child.Name = str;
            // Todo ：没有登记
            if (!this.IsRegister(child))
            {
                MessageProvider.Instance.ShowWithLog("儿童预防接种本信息未登记，请先到预检台进行登记；");
                return;
            }

            var item = viewModel.Collection.ToList().Find(l => l.Seq == child.ID);

            if (item != null)
            {
                if (item.State == LeaveState.Running)
                {
                    if (item.CreateTime == "0")
                    {
                        item.State = LeaveState.Ending;
                        item.LeaveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        //MessageProvider.Instance.ShowWithLog("儿童：" + child.Name + "预防接种留观结束");

                        Application.Current.Dispatcher.Invoke(() => viewModel.Message.Insert(0, "儿童：" + child.Name + "预防接种留观结束"));
                      ;
                    }
                    else
                    {
                        MessageProvider.Instance.ShowWithLog("儿童预防接种留观还未结束，请继续等待");
                    }
                    return;
                }

                if (item.State == LeaveState.Ending)
                {
                    item.LeaveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    MessageProvider.Instance.ShowWithLog("儿童预防接种留观已结束，感谢您的配合！");
                    return;
                }
            }
            else
            {
                // Todo ：更新列表 
                this.DownLoadList(viewModel);
            }

            // Todo ：开始登记
            this.Register(child, viewModel);

        }

        Random r = new Random();

        /// <summary> 系统检测用户当天无预防接种信息 </summary>
        bool Random(int v = 2)
        {
            return r.Next(v) == 1;
        }


        /// <summary> 是否挂号 </summary>
        bool IsRegister(ChildInfo c)
        {
            return !this.Random(10);
        }

        void Register(ChildInfo c, LeaveToObserveEngineViewModel viewModel)
        {
            // Todo ：发送登记信息 
            Application.Current.Dispatcher.Invoke(() =>
            {
                viewModel.Message.Insert(0, "儿童：" + c.Name + "预防接种登记成功");

                this.DownLoadList(viewModel);

                // Todo ：更新列表 
                LeaveToObserveItemViewModel observeitem = new LeaveToObserveItemViewModel();
                observeitem.Seq = c.ID;
                observeitem.Name = c.Name;
                observeitem.Sex = "男";
                observeitem.StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                observeitem.CreateTime = "30";
                viewModel.Collection.Add(observeitem);


            });

            //MessageProvider.Instance.ShowWithLog("儿童：" + c.Name + "预防接种登记成功");

        }

        void DownLoadList(LeaveToObserveEngineViewModel viewModel)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var collection = viewModel.Collection.OrderBy(l => l.CreateTime.ToInt()).ToList();

                viewModel.Collection.Clear();

                foreach (var item in collection)
                {
                    viewModel.Collection.Add(item);
                }
            });
        }
    }
}
