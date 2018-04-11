using ProcessingBatchModule.Provider;
using ProcessingBatchModule.ViewModel;
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

namespace ProcessingBatchModule.View
{
    /// <summary>
    /// ProcessingBatchContent.xaml 的交互逻辑
    /// </summary>
    [Export("ProcessingBatchContent")]
    public partial class ProcessingBatchContent : UserControl
    {
        public ProcessingBatchContent()
        {
            InitializeComponent();
            this.Loaded += CommonContent_Loaded;

        }

        private void CommonContent_Loaded(object sender, RoutedEventArgs e)
        {
            Action action = () =>
            {
                var m = ProcessingBatchProvider.Instance.Create();

                this.Dispatcher.Invoke(() =>
                {
                    this.ViewModel = m;
                    this.ViewModel.IsBusyFlag = false;
                });
            };

            action.DoTask();
        }

        [Import]
        public ProcessingBatchViewModel ViewModel
        {
            get { return this.DataContext as ProcessingBatchViewModel; }
            set { this.DataContext = value; }
        }
    }
}
