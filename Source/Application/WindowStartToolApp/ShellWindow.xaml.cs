using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
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
using HeBianGu.General.ModuleManager.ModuleManager;
using HeBianGu.Base.Util;
using System.Windows.Forms;
using System.Diagnostics;
using HeBianGu.General.WpfControlLib;
using System.Windows.Threading;
using System.Windows.Interop;
using System.Runtime.InteropServices;

namespace WindowStartToolApp
{
    /// <summary>
    /// ShellWindow.xaml 的交互逻辑
    /// </summary>
    [Export]
    public partial class ShellWindow:IPartImportsSatisfiedNotification
    {
        public ShellWindow()
        {
            InitializeComponent();

            this.RegisterNotify();

            this.Loaded += ShellWindow_Loaded;
        }

        private void ShellWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(new Action(RegisterApi), DispatcherPriority.SystemIdle, null);
        }

        private const string CommonDocumentModule = "CommonDocumentModule";

        private static Uri InboxViewUri = new Uri("/CommonContent", UriKind.Relative);

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
                   if (e.ModuleInfo.ModuleName == CommonDocumentModule)
                   {
                       this.RegionManager.RequestNavigate(RegionNames.MainContentRegion, InboxViewUri);
                   }
               };
        }

        /// <summary> 显示窗体 </summary>
        private void RefreshVisible()
        {
            if (this.Visibility == System.Windows.Visibility.Visible 
                && this.WindowState != WindowState.Minimized)
            {
                this.Visibility = System.Windows.Visibility.Hidden;
                this.WindowState = WindowState.Normal;
            }
            else
            {

                this.Visibility = System.Windows.Visibility.Visible;
                this.ShowInTaskbar = true;
                this.Topmost = true;
                this.WindowState = WindowState.Normal;
                this.Activate();
            }
        }

        /// <summary> 此方法的说明 </summary>
        public void RegisterApi()
        {

            // Todo ：双击大小写切换 
            ShortCutEntitys s = new ShortCutEntitys();

            KeyEntity k = new KeyEntity();
            k.Key = Keys.Capital;
            s.Add(k);

            KeyEntity c = new KeyEntity();
            c.Key = Keys.Capital;
            s.Add(c);

            ShortCutHookService.Instance.RegisterCommand(s, RefreshVisible);


            // Todo ：双击Ctrl键 
            ShortCutEntitys d = new ShortCutEntitys();

            KeyEntity c1 = new KeyEntity();
            c1.Key = Keys.LControlKey;
            d.Add(c1);

            KeyEntity c2 = new KeyEntity();
            c2.Key = Keys.LControlKey;
            d.Add(c2);

            Action action = () =>
              {
                  // HTodo  ：双击ctrl 模拟delete按键 
                  KeyHelper.OnKeyPress((byte)Keys.Delete);
              };

            ShortCutHookService.Instance.RegisterCommand(d, action);


            // Todo ：复制当前时间 
            ShortCutEntitys tt = new ShortCutEntitys();

            KeyEntity t1 = new KeyEntity();
            t1.Key = Keys.E;
            tt.Add(t1);

            KeyEntity t2 = new KeyEntity();
            t2.Key = Keys.D;
            tt.Add(t2);

            Action actiont = () =>
            {
             // Todo ：复制当前时间格式 
                System.Windows.Clipboard.SetText(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            };

            ShortCutHookService.Instance.RegisterCommand(tt, actiont);

            ShortCutEntitys ed = new ShortCutEntitys();

            KeyEntity e = new KeyEntity();
            e.Key = Keys.E;
            ed.Add(e);

            KeyEntity dd = new KeyEntity();
            dd.Key = Keys.D;
            ed.Add(dd);

            ShortCutHookService.Instance.RegisterCommand(ed, actiont);
        }

        NotifyIcon notifyIcon;
        /// <summary> 注册托盘 </summary>
        void RegisterNotify()
        {
            this.notifyIcon = new NotifyIcon();
            this.notifyIcon.BalloonTipText = "光速启动";
            this.notifyIcon.ShowBalloonTip(2000);
            this.notifyIcon.Text = "光速启动";
            
            this.notifyIcon.Icon = WindowStartToolApp.Properties.Resources.Feather;
            this.notifyIcon.Visible = true;

            this.notifyIcon.MouseDoubleClick += (object sender, System.Windows.Forms.MouseEventArgs e) =>
            {
                this.RefreshVisible();
            };

            ContextMenuStrip m = new ContextMenuStrip();
            ToolStripItem t = new ToolStripMenuItem();
            t.Text = "退出";
            t.Click += (object ss, EventArgs ee) =>
            {
                this.BegionStoryClose();
            };
            this.notifyIcon.ContextMenuStrip = m;
            this.notifyIcon.ContextMenuStrip.Items.Add(t);
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            // Todo ：注册监视剪贴板 
            ClipBoardRegisterService.Instance.Register(this);
        }

    }

}
