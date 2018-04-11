using HeBianGu.General.WpfControlLib;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WindowToolModule.View
{
    /// <summary>
    /// RegeditTool.xaml 的交互逻辑
    /// </summary>
    [Export]
    [ViewSortHint("08")]
    public partial class RegeditTool : UserControl
    {
        public RegeditTool()
        {
            InitializeComponent();
        }

        private void btn_bar_Click(object sender, RoutedEventArgs e)
        {
            //Process.Start("regedit");


            Tuple<string, Action> t = new Tuple<string, Action>("确定", () => { });

            Tuple<string, Action> t1 = new Tuple<string, Action>("确定", () => { });

            Tuple<string, Action> t2 = new Tuple<string, Action>("确定", () => { });

            MessageWindow.ShowDialog("测试按钮", "提示！", 10, t, t1, t2);
        }
    }
}
