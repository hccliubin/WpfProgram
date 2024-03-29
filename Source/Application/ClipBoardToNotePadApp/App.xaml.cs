﻿using HeBianGu.General.Logger;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ClipBoardToNotePadApp
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {

        public App()
        {
            DispatcherUnhandledException += App_DispatcherUnhandledException;

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() => MessageBox.Show("当前应用程序遇到一些问题，该操作已经终止，请进行重试，如果问题继续存在，请联系管理员", "意外的操作"));

        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() => MessageBox.Show(e.Exception.Message));

            e.Handled = true;
        }


        protected override void OnStartup(StartupEventArgs e)
        {
            string exeFileFullPath = Assembly.GetEntryAssembly().Location;
            string exeName = System.IO.Path.GetFileNameWithoutExtension(exeFileFullPath);
            string binPath = System.IO.Path.GetDirectoryName(exeFileFullPath);

            binPath = System.IO.Path.GetDirectoryName(binPath);
            string logFilePath = System.IO.Path.GetDirectoryName(binPath);

            //  初始化日志
            Log4Servcie.Instance.InitLogger(logFilePath, System.Diagnostics.Process.GetCurrentProcess().ProcessName);
        }
    }
}
