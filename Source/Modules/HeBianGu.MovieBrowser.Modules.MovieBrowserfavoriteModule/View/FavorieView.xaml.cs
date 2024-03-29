﻿using HeBianGu.MovieBrower.UserControls;
using HeBianGu.MovieBrower.UserControls.DataManager;
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

        MovieBroswerViewModelBase _viewModel;


        public FavorieView()
        {
            InitializeComponent();

            _viewModel = MovieBrowserDataManager.Instance.ViewModelItem.Find(l => l.Type == General.ModuleManager.Model.FileType.Favorate);

            this.DataContext = _viewModel;

            this.Loaded += CommonContent_Loaded;

        }


        private void CommonContent_Loaded(object sender, RoutedEventArgs e)
        {
            MovieBrowserDataManager.Instance.SetActived(this._viewModel);
        }
    }
}
