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
    /// <summary>
    /// NotePadContent.xaml 的交互逻辑
    /// </summary>
    [Export("NotePadContent")]
    public partial class NotePadContent : UserControl
    {
        public NotePadContent()
        {
            InitializeComponent();

            this.Loaded += CommonContent_Loaded;
        }


        private void CommonContent_Loaded(object sender, RoutedEventArgs e)
        {
            Action action = () =>
            {
                var m = NotePadPrivider.Instance.Create();

                this.Dispatcher.Invoke(() =>
                {
                    this.ViewModel = m;
                    this.ViewModel.IsBusyFlag = false;
                });
            };

            action.DoTask();
        }

        [Import]
        public NotePadViewModel ViewModel
        {
            get { return this.DataContext as NotePadViewModel; }
            set { this.DataContext = value; }
        }
    }

}
