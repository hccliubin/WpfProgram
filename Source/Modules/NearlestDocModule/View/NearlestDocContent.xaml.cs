using NearlestDocModule.Provider;
using NearlestDocModule.ViewModel;
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
using System.Windows.Threading;

namespace NearlestDocModule.View
{
    /// <summary>
    /// NearlestDocContent.xaml 的交互逻辑
    /// </summary>
    /// 
    [Export("NearlestDocContent")]
    public partial class NearlestDocContent : UserControl
    {
        public NearlestDocContent()
        {
            InitializeComponent();

            this.Loaded += CommonContent_Loaded;

        }

        private void CommonContent_Loaded(object sender, RoutedEventArgs e)
        {
            Action action = () =>
              {
                  var v = NearlestDocProvider.Instance.Create();

              this.Dispatcher.Invoke(() =>
              {
                  v.IsBusyFlag = false;
                  this.ViewModel = v;
              });
              };

            Task t = new Task(action);
            t.Start();
        }

        [Import]
        public NearlestDocViewModel ViewModel
        {
            get { return this.DataContext as NearlestDocViewModel; }
            set { this.DataContext = value; }
        }
    }
}
