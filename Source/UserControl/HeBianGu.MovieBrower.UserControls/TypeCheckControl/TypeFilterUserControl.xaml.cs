using HeBianGu.Base.WpfBase;
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
        "CheckChanged", RoutingStrategy.Bubble, typeof(EventHandler), typeof(TypeFilterUserControl));


        private void Lb_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            StringBuilder sb = new StringBuilder();

            string str = string.Empty;

            foreach (var item in this.lb_list.SelectedItems)
            {
                sb.Append(item.ToString()).Append(this.SplitChar);

                str += item.ToString();

            }
            this.Text = sb.ToString().Trim(this.SplitChar.ToCharArray()[0]);

            flag = true;

            this.SelectText = this.Text;

            flag = false;

            // Todo ：触发路由事件 
            var args = new RoutedEventArgs(CheckChangedEvent);
            RaiseEvent(args);
           
        }

        bool flag;



        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(TypeFilterUserControl), new PropertyMetadata(null));




        public string SelectText
        {
            get { return (string)GetValue(SelectTextProperty); }
            set { SetValue(SelectTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectTextProperty =
            DependencyProperty.Register("SelectText", typeof(string), typeof(TypeFilterUserControl), new PropertyMetadata(null, SelectTextPropertyChanged));

        public static void SelectTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            var control = d as TypeFilterUserControl;

            if (control.flag) return;

            control.Text = e.NewValue == null ? string.Empty : e.NewValue.ToString();

            control.RefreshList();

        }

        void RefreshList()
        {
            var collection = this.Text.Split(new char[] { this.SplitChar.ToCharArray()[0] }, StringSplitOptions.RemoveEmptyEntries);

            if (this.lb_list == null) return;

            this.lb_list.SelectedItems.Clear();

            foreach (var item in collection)
            {
                this.lb_list.SelectedItems.Add(item);
            }
        }


        public string SplitChar
        {
            get { return (string)GetValue(SplitCharProperty); }
            set { SetValue(SplitCharProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SplitChar.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SplitCharProperty =
            DependencyProperty.Register("SplitChar", typeof(string), typeof(TypeFilterUserControl), new PropertyMetadata("\\"));



    }
}
