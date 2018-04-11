using HeBianGu.MovieBrower.UserControls;
using HeBianGu.MovieBrowser.Modules.MovieBrowserfavoriteModule.ViewModel;
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

namespace HeBianGu.MovieBrowser.Modules.MovieBrowserfavoriteModule.View
{
    /// <summary>
    /// FavorieView.xaml 的交互逻辑
    /// </summary>
    [Export("FavorieView")]
    public partial class FavorieView : UserControl
    {

        public FavorieView()
        {
            InitializeComponent();

            this.DataContext = MovieBrowserDataManager.Instance.ViewModelItem.Find(l => l.Type == General.ModuleManager.Model.FileType.Favorate);

            this.Loaded += FavorieView_Loaded;
        }

        private void FavorieView_Loaded(object sender, RoutedEventArgs e)
        {
            //_vm.RelayCommand.Execute("Load");
        }
    }
}
