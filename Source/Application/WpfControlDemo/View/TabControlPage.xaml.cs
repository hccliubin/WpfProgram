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

namespace WpfControlDemo.View
{
    /// <summary>
    /// TabControlPage.xaml 的交互逻辑
    /// </summary>
    public partial class TabControlPage : Page
    {
        public TabControlPage()
        {
            InitializeComponent();

            this.Loaded += TabControlPage_Loaded;
        }

        private void TabControlPage_Loaded(object sender, RoutedEventArgs e)
        {
            //Button btn = new Button();

            //btn.Content = "444444455555555555555555555555555";

            this.tb_all.Items.Clear();

            Frame f = new Frame();
            f.Content = new ButtonPage();

            this.tb_all.Items.Add(f);

            Frame f1 = new Frame();
            f1.Content = new TextBoxPage();

            this.tb_all.Items.Add(f1);
        }
    }
}
