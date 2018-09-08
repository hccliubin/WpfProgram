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

namespace HeBianGu.Controls.MaterialControl
{
    /// <summary>
    /// Interaction logic for Slide6_Origins.xaml
    /// </summary>
    public partial class Slide6_Origins : UserControl
    {
        public Slide6_Origins()
        {
            InitializeComponent();
        }

        private void FirstSlideButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.Transitioner.SelectedIndex = 0;
        }

        private void SecondSlideButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.Transitioner.SelectedIndex = 1;
        }
    }
}
