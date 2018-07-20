using System;
using System.Collections.Generic;
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

namespace WpfControlDemo
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl1 : HeBianGu.General.WpfControlLib.BaseUserControl
    {
        public UserControl1()
        {
            InitializeComponent();

            this.Style = this.FindResource("OpacityVisibleUserControl") as Style;

            

            
        }

    }
}
