using ProgramModule.Provider;
using ProgramModule.ViewModel;
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

namespace ProgramModule.View
{
    /// <summary>
    /// ProgramContent.xaml 的交互逻辑
    /// </summary>
    [Export("ProgramContent")]
    public partial class ProgramContent : UserControl
    {
        public ProgramContent()
        {
            InitializeComponent();

            this.Loaded += ProgramContent_Loaded;
        }

        private void ProgramContent_Loaded(object sender, RoutedEventArgs e)
        {

            Action action = () =>
            {
                var m = ProgramProvider.Instance.Create();

                this.Dispatcher.Invoke(() =>
                {
                    this.ViewModel = m;
                    this.ViewModel.IsBusyFlag = false;
                });
            };

            action.DoTask();
        }

        [Import]
        public ProgramViewModel ViewModel
        {
            get
            {
                return this.DataContext as ProgramViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }
    }
}
