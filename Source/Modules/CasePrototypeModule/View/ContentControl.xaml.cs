using System;
using System.Collections.Generic;
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
using System.ComponentModel.Composition;
using CasePrototypeModule.ViewModel;
using CasePrototypeModule.Provider;

namespace CasePrototypeModule.View
{
    /// <summary>
    /// ContentControl.xaml 的交互逻辑
    /// </summary>
    [Export("ContentControl")]
    public partial class ContentControl : UserControl
    {
        public ContentControl()
        {
            InitializeComponent();

            this.Loaded += CommonContent_Loaded;

        }

        private void CommonContent_Loaded(object sender, RoutedEventArgs e)
        {
            Action action = () =>
            {
                var m = CaseProvider.Instance.Current;

                this.Dispatcher.Invoke(() =>
                {
                    this.ViewModel = m;
                    this.ViewModel.IsBusyFlag = false;
                });
            };

            action.DoTask();
        }

        [Import]
        public CaseContentViewModel ViewModel
        {
            get { return this.DataContext as CaseContentViewModel; }
            set { this.DataContext = value; }
        }
    }
}
