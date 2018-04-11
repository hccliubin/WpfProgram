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

namespace NotePadModule.View
{
    /// <summary> 导航按钮 </summary>
    [Export]
    [ViewSortHint("03")]
    public partial class NavigationBar : UserControl, IPartImportsSatisfiedNotification
    {
        public NavigationBar()
        {
            InitializeComponent();
            this.DataContext = new NotePadViewModel();

            this.Loaded += NavigationBar_Loaded;
        }

        private void NavigationBar_Loaded(object sender, RoutedEventArgs e)
        {
            Action action = () =>
            {
                var m = NotePadPrivider.Instance.Current;

                this.Dispatcher.Invoke(() =>
                {
                    this.DataContext = m;
                    m.IsBusyFlag = false;
                });
            };

            action.DoTask();
        }

        private static Uri emailsViewUri = new Uri("/NotePadContent", UriKind.Relative);

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
