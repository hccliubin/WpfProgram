using HeBianGu.General.ModuleManager.ModuleManager;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
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
    /// ComputeTool.xaml 的交互逻辑
    /// </summary>
    [Export]
    [ViewSortHint("01")]
    public partial class ComputeTool : UserControl
    {
        public ComputeTool()
        {
            InitializeComponent();
        }

        private void btn_bar_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("::{20D04FE0-3AEA-1069-A2D8-08002B30309D}");
        }

    }
}
