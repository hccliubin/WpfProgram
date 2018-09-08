using HeBianGu.Base.Util;
using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClipBoardToNotePadApp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {

        MainNotifyClass _vm = new MainNotifyClass();

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = _vm;


            // Todo ：双击大小写切换 
            ShortCutEntitys s = new ShortCutEntitys();

            KeyEntity k = new KeyEntity();
            k.Key = Keys.LShiftKey;
            s.Add(k);

            KeyEntity c = new KeyEntity();
            c.Key = Keys.LShiftKey;
            s.Add(c);

            ShortCutHookService.Instance.RegisterCommand(s, RefreshVisible);
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            // Todo ：注册监视剪贴板 
            ClipBoardRegisterService.Instance.Register(this);
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

    }

}
