using HeBianGu.General.ModuleManager.ModuleManager;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HeBianGu.MovieBrowserModules.MovieBrowserDeleteModule.View
{
    /// <summary>
    /// MenuToolButton.xaml 的交互逻辑
    /// </summary>
    [Export]
    [ViewSortHint("03")]
    public partial class MenuToolButton : UserControl, IPartImportsSatisfiedNotification
    {
        public MenuToolButton()
        {
            InitializeComponent();

            //this.DataContext = new MovieBrowerViewModel();

            this.Loaded += MenuToolButton_Loaded;
        }

        private void MenuToolButton_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private static Uri emailsViewUri = new Uri("/DeleteView", UriKind.Relative);

        [Import]
        public IRegionManager regionManager;

        void IPartImportsSatisfiedNotification.OnImportsSatisfied()
        {
            IRegion mainContentRegion = this.regionManager.Regions[RegionNames.MainContentRegion];

            if (mainContentRegion != null && mainContentRegion.NavigationService != null)
            {
                mainContentRegion.NavigationService.Navigated += this.MainContentRegion_Navigated;
            }
        }

        public void MainContentRegion_Navigated(object sender, RegionNavigationEventArgs e)
        {
            this.UpdateNavigationButtonState(e.Uri);
        }

        private void UpdateNavigationButtonState(Uri uri)
        {
            this.btn_bar.IsChecked = (uri == emailsViewUri);
        }

        private void NavigateToEmailRadioButton_Click(object sender, RoutedEventArgs e)
        {
            this.regionManager.RequestNavigate(RegionNames.MainContentRegion, emailsViewUri);
        }
    }
}
