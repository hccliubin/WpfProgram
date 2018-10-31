using HeBianGu.Control.DockPanelControl;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace WpfControlDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void property_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {

            // if (!this.IsActive) return;

            // //if (this.selectProperty.State == DockableContentState.AutoHide) return;

            // if (this.selectProperty.State == DockableContentState.Hidden) return;

            // if (this.selectProperty.State != DockableContentState.Docked) return;

            //this.property.SelectedObject = e.NewFocus;

            if (this.AutoProperty.IsChecked.Value)

                this.property.SelectedObject = e.NewFocus;

            Debug.WriteLine(e.NewFocus);

        }


    }
}
