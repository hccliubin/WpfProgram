﻿#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2018/4/10 14:41:58 
 * 文件名：MessageProvider 
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LeaveToObserveApp.Provider
{
    class MessageProvider:BaseFactory<MessageProvider>
    {

        public void ShowWithLog(string message)
        {
            Log4Servcie.Instance.Info(message);

            Application.Current.Dispatcher.Invoke(() => MessageWindow.ShowSumit(message, "系统提示！", 10));
        }
    }
}
