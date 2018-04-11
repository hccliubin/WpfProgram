#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/11/27 16:01:56 
 * 文件名：Class1 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using Microsoft.Practices.Prism.MefExtensions;
using Microsoft.Practices.Prism.Modularity;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommonDocumentModule;
using ProcessingBatchModule;
using WindowToolModule.Modules;

namespace WindowStartToolApp
{
    public class QuickStartBootstrapper : MefBootstrapper
    {
        private const string ModuleCatalogUri = "/ViewSwitchingNavigation;component/ModulesCatalog.xaml";

        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();

            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(QuickStartBootstrapper).Assembly));


            #region - 左侧按钮 -

            //Todo ：注册组件
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(CasePrototypeModule.CasePrototypeModule).Assembly));

            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(NotePadModule.NotePadModule).Assembly));

            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ClipBoardModule.ClipBoardModule).Assembly));

            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ProgramModule.ProgramModule).Assembly));


            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(NearlestDocModule.NearlestDocModule).Assembly));

            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(IntergrationToolModule.IntergrationToolModule).Assembly));

            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ProcessingBatchMudule).Assembly));

            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(SystemFolderModule.SystemFolderModule).Assembly));

            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(FavoriteModule.FavoriteModule).Assembly));

            #endregion

            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ComputeModule).Assembly));
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
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(CommonDocumentModule.CommonDocumentModule).Assembly));




        }
    }
}
