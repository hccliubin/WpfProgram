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
using SystemFolderModule.Provider;
using SystemFolderModule.ViewModel;

namespace SystemFolderModule.View
{
    /// <summary>
    /// SystemFolderContent.xaml 的交互逻辑
    /// </summary>
    [Export("SystemFolderContent")]
    public partial class SystemFolderContent : UserControl
    {
        public SystemFolderContent()
        {
            InitializeComponent();
            this.Loaded += ProgramContent_Loaded;
        }

        private void ProgramContent_Loaded(object sender, RoutedEventArgs e)
        {
            Action action = () =>
              {
                  var m = SystemFolderProvider.Instance.Create();

                  this.Dispatcher.Invoke(()=>
                  {
                      this.ViewModel = m;
                      this.ViewModel.IsBusyFlag = false;
                  });
              };

            action.DoTask();

        }

        [Import]
        public SystemFolderViewModel ViewModel
        {
            get
            {
                return this.DataContext as SystemFolderViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }
    }
}
