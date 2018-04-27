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

namespace HeBianGu.MovieBrower.UserControls.TypeCheckControl
{
    /// <summary>
    /// TypeFilterUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class TypeFilterUserControl : UserControl
    {
        public TypeFilterUserControl()
        {
            InitializeComponent();

            this.lb_list.SelectionChanged += Lb_list_SelectionChanged;
        }


        public List<string> DataSource
        {
            get { return (List<string>)GetValue(DataSourceProperty); }
            set { SetValue(DataSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataSourceProperty =
            DependencyProperty.Register("DataSource", typeof(List<string>), typeof(TypeFilterUserControl), new PropertyMetadata(null, DataSourcePropertyChanged));


        public static void DataSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TypeFilterUserControl control = d as TypeFilterUserControl;

            if (control == null) return;

            List<string> source = e.NewValue as List<string>;

            control.lb_list.ItemsSource = source;
        }



        public event EventHandler CheckChanged
        {
            add { AddHandler(CheckChangedEvent, value); }
            remove { RemoveHandler(CheckChangedEvent, value); }
        }

        public static readonly RoutedEvent CheckChangedEvent = EventManager.RegisterRoutedEvent(
        "CheckChanged", RoutingStrategy.Bubble, typeof(EventHandler), typeof(MovieGridUserControl));

       


    private void Lb_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Todo ：触发路由事件 
        var args = new RoutedEventArgs(CheckChangedEvent);
        RaiseEvent(args);
    }

}
}
