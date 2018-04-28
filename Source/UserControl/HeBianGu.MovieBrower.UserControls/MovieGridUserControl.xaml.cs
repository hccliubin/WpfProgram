using HeBianGu.Base.WpfBase;
using HeBianGu.MovieBrower.UserControls.ImageViewControl;
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
using System.Windows.Threading;

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
            control.lb_list1.ItemsSource = e.NewValue as IEnumerable;
        }




        public bool IsShowImageList
        {
            get { return (bool)GetValue(IsShowImageListProperty); }
            set { SetValue(IsShowImageListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsShowImageList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsShowImageListProperty =
            DependencyProperty.Register("IsShowImageList", typeof(bool), typeof(MovieGridUserControl), new PropertyMetadata(true, IsShowImageListPropertyChanged));

        public static void IsShowImageListPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MovieGridUserControl control = d as MovieGridUserControl;

            if (control == null) return;
            control.lb_list.Visibility = (bool)e.NewValue ? Visibility.Visible : Visibility.Collapsed;
            control.lb_list1.Visibility = (bool)e.NewValue ? Visibility.Collapsed : Visibility.Visible;

        }

    }



}
