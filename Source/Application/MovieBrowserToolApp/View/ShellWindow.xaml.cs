using HeBianGu.Base.Util;
using HeBianGu.General.ModuleManager.ModuleManager;
using HeBianGu.General.ModuleManager.Service;
using HeBianGu.MovieBrowser.Modules.MovieBrowserManagerModule;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using MovieBrowserToolApp.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
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
using System.Windows.Shapes;

namespace MovieBrowserToolApp
{
    /// <summary>
    /// ShellWindow.xaml 的交互逻辑
    /// </summary>
    [Export]
    public partial class ShellWindow : IPartImportsSatisfiedNotification
    {

        ShellViewModel _vm = new ShellViewModel();
        public ShellWindow()
        {
            InitializeComponent();

            this.DataContext = _vm;

            this.Closing += ShellWindow_Closing;
        }

        private void ShellWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Todo ：保存案例
            _vm.RelayCommand.Execute("SaveCase");
        }

        private const string MovieBrowerPrototypeModule = "MovieBrowerPrototypeModule";

        private static Uri InboxViewUri = new Uri("/FunctionView", UriKind.Relative);

        [Import(AllowRecomposition = false)]
        public IModuleManager ModuleManager;

        [Import(AllowRecomposition = false)]
        public IRegionManager RegionManager;

        public void OnImportsSatisfied()
        {
            this.ModuleManager.LoadModuleCompleted += (s, e) =>
            {
                // todo: 01 - Navigation on when modules are loaded.
                // When using region navigation, be sure to use it consistently
                // to ensure you get proper journal behavior.  If we mixed
                // usage of adding views directly to regions, such as through
                // RegionManager.AddToRegion, and then use RegionManager.RequestNavigate,
                // we may not be able to navigate back correctly.
                // 
                // Here, we wait until the module we want to start with is
                // loaded and then navigate to the view we want to display
                // initially.
                //     
                if (e.ModuleInfo.ModuleName == MovieBrowerPrototypeModule)
                {
                    this.RegionManager.RequestNavigate(RegionNames.MainContentRegion, InboxViewUri);
                }
            };
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            // Todo ：注册监视剪贴板 
            ClipBoardRegisterService.Instance.Register(this);
        }
    }
}
