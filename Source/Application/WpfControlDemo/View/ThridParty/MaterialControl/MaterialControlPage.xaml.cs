
using HeBianGu.Controls.MaterialControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// DrawHostPage.xaml 的交互逻辑
    /// </summary>
    public partial class MaterialControlPage : Page
    {
        public static Snackbar Snackbar;
        public MaterialControlPage()
        {
            InitializeComponent();

            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(2500);
            }).ContinueWith(t =>
            {
                //note you can use the message queue from any thread, but just for the demo here we 
                //need to get the message queue from the snackbar, so need to be on the dispatcher
                MainSnackbar.MessageQueue.Enqueue("Welcome to Material Design In XAML Tookit");
            }, TaskScheduler.FromCurrentSynchronizationContext());

            DataContext = new MainWindowViewModel(MainSnackbar.MessageQueue);

            Snackbar = this.MainSnackbar;
        }


        private void UIElement_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var dependencyObject = Mouse.Captured as DependencyObject;

            while (dependencyObject != null)
            {
                if (dependencyObject is ScrollBar) return;

                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);

            }

            MenuToggleButton.IsChecked = false;
        }

        private async void MenuPopupButton_OnClick(object sender, RoutedEventArgs e)
        {
            var sampleMessageDialog = new SampleMessageDialog();

            sampleMessageDialog.MessageStr= ((ButtonBase)sender).Content.ToString();

            await DialogHost.Show(sampleMessageDialog, "RootDialog");
        }
    }

    //public class MainWindowViewModel
    //{
    //    public MainWindowViewModel(ISnackbarMessageQueue snackbarMessageQueue)
    //    {
    //        if (snackbarMessageQueue == null) throw new ArgumentNullException(nameof(snackbarMessageQueue));

    //        DemoItems = new[]
    //        {
    //            new DemoItem("Home", null,
    //                null
    //            ),
    //              new DemoItem("Button", null,
    //               null
    //            )
    //        };
    //    }

    //    public DemoItem[] DemoItems { get; }
    //}

    //public class DemoItem : INotifyPropertyChanged
    //{
    //    private string _name;
    //    private object _content;
    //    private ScrollBarVisibility _horizontalScrollBarVisibilityRequirement;
    //    private ScrollBarVisibility _verticalScrollBarVisibilityRequirement;
    //    private Thickness _marginRequirement = new Thickness(16);

    //    public DemoItem(string name, object content, IEnumerable<DocumentationLink> documentation)
    //    {
    //        _name = name;
    //        Content = content;
    //        Documentation = documentation;
    //    }

    //    public string Name
    //    {
    //        get { return _name; }
    //        set { this.MutateVerbose(ref _name, value, RaisePropertyChanged()); }
    //    }

    //    public object Content
    //    {
    //        get { return _content; }
    //        set { this.MutateVerbose(ref _content, value, RaisePropertyChanged()); }
    //    }

    //    public ScrollBarVisibility HorizontalScrollBarVisibilityRequirement
    //    {
    //        get { return _horizontalScrollBarVisibilityRequirement; }
    //        set { this.MutateVerbose(ref _horizontalScrollBarVisibilityRequirement, value, RaisePropertyChanged()); }
    //    }

    //    public ScrollBarVisibility VerticalScrollBarVisibilityRequirement
    //    {
    //        get { return _verticalScrollBarVisibilityRequirement; }
    //        set { this.MutateVerbose(ref _verticalScrollBarVisibilityRequirement, value, RaisePropertyChanged()); }
    //    }

    //    public Thickness MarginRequirement
    //    {
    //        get { return _marginRequirement; }
    //        set { this.MutateVerbose(ref _marginRequirement, value, RaisePropertyChanged()); }
    //    }

    //    public IEnumerable<DocumentationLink> Documentation { get; }

    //    public event PropertyChangedEventHandler PropertyChanged;

    //    private Action<PropertyChangedEventArgs> RaisePropertyChanged()
    //    {
    //        return args => PropertyChanged?.Invoke(this, args);
    //    }
    //}

    //public class DocumentationLink
    //{

    //    //public static DocumentationLink DemoPageLink<TDemoPage>()
    //    //{
    //    //    return DemoPageLink<TDemoPage>(null);
    //    //}

    //    //public static DocumentationLink DemoPageLink<TDemoPage>(string label)
    //    //{
    //    //    return DemoPageLink<TDemoPage>(label, null);
    //    //}

    //    public string Label { get; }

    //    public DocumentationLinkType Type { get; }

    //    public ICommand Open { get; }

    //}

    //public enum DocumentationLinkType
    //{
    //    Wiki,
    //    DemoPageSource,
    //    ControlSource,
    //    StyleSource,
    //    Video
    //}

    //public static class NotifyPropertyChangedExtension
    //{
    //    public static void MutateVerbose<TField>(this INotifyPropertyChanged instance, ref TField field, TField newValue, Action<PropertyChangedEventArgs> raise, [CallerMemberName] string propertyName = null)
    //    {
    //        if (EqualityComparer<TField>.Default.Equals(field, newValue)) return;
    //        field = newValue;
    //        raise?.Invoke(new PropertyChangedEventArgs(propertyName));
    //    }
    //}
}
