using HeBianGu.MovieBrower.UserControls;
using HeBianGu.MovieBrowser.Modules.MovieBrowserManagerModule.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HeBianGu.MovieBrowser.Modules.MovieBrowserManagerModule.View
{
    /// <summary>
    /// FunctionView.xaml 的交互逻辑
    /// </summary>
    [Export("FunctionView")]
    public partial class FunctionView : UserControl
    {
        
        public FunctionView()
        {
            InitializeComponent();

            this.DataContext = MovieBrowserDataManager.Instance.ViewModelItem.Find(l=>l.Type==General.ModuleManager.Model.FileType.Normal);

            this.Loaded += CommonContent_Loaded;
        
        }

        private void CommonContent_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
