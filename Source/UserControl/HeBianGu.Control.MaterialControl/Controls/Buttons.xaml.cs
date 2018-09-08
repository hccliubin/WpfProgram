using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using HeBianGu.Controls.MaterialControl;

namespace HeBianGu.Controls.MaterialControl
{
    /// <summary>
    /// Interaction logic for Buttons.xaml
    /// </summary>
    public partial class Buttons : UserControl
    {
        public Buttons()
        {
            InitializeComponent();
            FloatingActionDemoCommand = new AnotherCommandImplementation(Execute);
        }

        public ICommand FloatingActionDemoCommand { get; }

        public void Execute(object o)
        {
            Console.WriteLine("Floating action button command. - " + (o ?? "NULL").ToString());
        }

        public void ButtonBase_OnClick(object sender, RoutedEventArgs e)
	{
            Console.WriteLine("Just checking we haven't suppressed the button.");
	}

        public void PopupBox_OnOpened(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Just making sure the popup has opened.");
        }

        public void PopupBox_OnClosed(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Just making sure the popup has closed.");
        }

        public void CountingButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (CountingBadge.Badge == null || Equals(CountingBadge.Badge, ""))
                CountingBadge.Badge = 0;
            
            var next = int.Parse(CountingBadge.Badge.ToString()) + 1;
            
            CountingBadge.Badge = next < 21 ? (object)next : null;
        }

        public void BasicRatingBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            Debug.WriteLine($"BasicRatingBar value changed from {e.OldValue} to {e.NewValue}.");
        }

        private void PopupBox_Opened(object sender, RoutedEventArgs e)
        {

        }

        private void PopupBox_Closed(object sender, RoutedEventArgs e)
        {

        }
    }
}
