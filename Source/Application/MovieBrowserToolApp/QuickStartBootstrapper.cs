#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2018/3/29 15:48:17 
 * 文件名：Class1 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HeBianGu.MovieBrower.UserControls;
using HeBianGu.MovieBrowser.Modules.MovieBrowserfavoriteModule;
using HeBianGu.MovieBrowser.Modules.MovieBrowserManagerModule;
using HeBianGu.MovieBrowserModules.MovieBrowserDeleteModule;
using Microsoft.Practices.Prism.MefExtensions;
using Microsoft.Practices.Prism.Modularity;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MovieBrowserToolApp
{
    public class QuickStartBootstrapper : MefBootstrapper
    {
        private const string ModuleCatalogUri = "/ViewSwitchingNavigation;component/ModulesCatalog.xaml";

        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();

            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(QuickStartBootstrapper).Assembly));
            

            #region - 左侧按钮 -

            // Todo ：注册组件 
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(MovieBrowerFavoriteModule).Assembly));

            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(MovieBrowserDeleteModule).Assembly));

            #endregion


            #region - 注册绑定数据 -

            HeBianGu.MovieBrowser.Modules.MovieBrowserfavoriteModule.ViewModel.MovieBrowerViewModel normal = new HeBianGu.MovieBrowser.Modules.MovieBrowserfavoriteModule.ViewModel.MovieBrowerViewModel();

            MovieBrowserDataManager.Instance.Register(normal);

            HeBianGu.MovieBrowser.Modules.MovieBrowserManagerModule.ViewModel.MovieBrowerViewModel favorate = new HeBianGu.MovieBrowser.Modules.MovieBrowserManagerModule.ViewModel.MovieBrowerViewModel();

            MovieBrowserDataManager.Instance.Register(favorate);

            HeBianGu.MovieBrowserModules.MovieBrowserDeleteModule.ViewModel.MovieBrowerViewModel delete = new HeBianGu.MovieBrowserModules.MovieBrowserDeleteModule.ViewModel.MovieBrowerViewModel();

            MovieBrowserDataManager.Instance.Register(delete);

            #endregion

        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            //ModuleCatalog modules = this.ModuleCatalog as ModuleCatalog;

            //modules.AddModule();


            return new ConfigurationModuleCatalog();
        }

        protected override DependencyObject CreateShell()
        {
            return this.Container.GetExportedValue<ShellWindow>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            Application.Current.MainWindow = (Window)this.Shell;
            Application.Current.MainWindow.Show();


            // Todo ：默认要显示的在此注册 
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(MovieBrowerPrototypeModule).Assembly));


        }
    }
}
