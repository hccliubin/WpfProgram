using HeBianGu.MovieBrowser.Modules.MenuItem.ViewModel;
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
using System.Windows.Shapes;

namespace HeBianGu.MovieBrowser.Modules.MenuItem.View
{
    /// <summary>
    /// SettingCaseWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SettingCaseWindow
    {
        SettingCaseViewModel _vm = new SettingCaseViewModel();

        public SettingCaseWindow()
        {
            InitializeComponent();

            this.DataContext = _vm;
        }
    }
}
