using System;
using System.Collections;
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

namespace HeBianGu.MovieBrower.UserControls
{
    /// <summary>
    /// MovieGridUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class MovieGridUserControl : UserControl
    {
        public MovieGridUserControl()
        {
            InitializeComponent();

            //this.fivePointStarGroup1.SelectCountChangeEvent += new RoutedEventHandler(fivePointStarGroup1_SelectCountChangeEvent);
        }

        void fivePointStarGroup1_SelectCountChangeEvent(object sender, RoutedEventArgs e)
        {
            //InitialData();
        }




        public IEnumerable DataSource
        {
            get { return (IEnumerable)GetValue(DataSourceProperty); }
            set { SetValue(DataSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataSourceProperty =
            DependencyProperty.Register("DataSource", typeof(IEnumerable), typeof(MovieGridUserControl), new PropertyMetadata(null, DataSourcePropertyChanged));


        public static void DataSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MovieGridUserControl control = d as MovieGridUserControl;

            if (control == null) return;

            control.lb_list.ItemsSource = e.NewValue as IEnumerable;
        }


    }
}
