using linhang.Controls;
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

namespace HeBianGu.Control.UserControls._3DGalleryControl.Demo
{
    /// <summary>
    /// ViewPort3DUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class ViewPort3DUserControl : UserControl
    {
        public ViewPort3DUserControl()
        {
            InitializeComponent();
        }
        Viewport3DControl view=null;

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            viewport3DControl1.MoveToRight();
            if (view != null)
                view.MoveToRight();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            viewport3DControl1.MoveToLeft();
            if (view != null)
                view.MoveToLeft();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            viewport3DControl1.MoveToCenter();
        }

    }
}
