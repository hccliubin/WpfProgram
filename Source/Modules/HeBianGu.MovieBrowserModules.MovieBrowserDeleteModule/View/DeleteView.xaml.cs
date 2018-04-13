using HeBianGu.MovieBrower.UserControls;
using HeBianGu.MovieBrower.UserControls.DataManager;
using HeBianGu.MovieBrowserModules.MovieBrowserDeleteModule.ViewModel;
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

namespace HeBianGu.MovieBrowserModules.MovieBrowserDeleteModule.View
{
    /// <summary>
    /// DeleteView.xaml 的交互逻辑
    /// </summary>
    [Export("DeleteView")]
    public partial class DeleteView : UserControl
    {
        MovieBroswerViewModelBase _viewModel;


        public DeleteView()
        {
            InitializeComponent();

            _viewModel = MovieBrowserDataManager.Instance.ViewModelItem.Find(l => l.Type == General.ModuleManager.Model.FileType.Delete);

            this.DataContext = _viewModel;

            this.Loaded += CommonContent_Loaded;

            this.mgu_list.LeftClilk += (object sender, RoutedEventArgs e) =>
            {
                _viewModel.DoCommandWith("ShowImage");
            };

            this.mgu_list.DoubleClilk += (object sender, RoutedEventArgs e) =>
            {
                _viewModel.DoCommandWith("Open");
            };

        }


        private void CommonContent_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
