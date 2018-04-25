using HeBianGu.Base.WpfBase;
using HeBianGu.General.ModuleManager.Service;
using HeBianGu.MovieBrowser.Modules.MenuItem.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HeBianGu.MovieBrowser.Modules.MenuItem.Controls
{
    /// <summary>
    /// FileSettingControl.xaml 的交互逻辑
    /// </summary>
    public partial class FileSettingControl : UserControl
    {
        FileSetingViewModel _vm = new FileSetingViewModel();

        public FileSettingControl()
        {
            InitializeComponent();

            this.DataContext = _vm;

            collection.Add(new Tuple<TextBlock, RadioButton>(this.txt_matchType, this.rb_matchType));
            collection.Add(new Tuple<TextBlock, RadioButton>(this.txt_fileType, this.rb_fileType));
            collection.Add(new Tuple<TextBlock, RadioButton>(this.txt_fileType1, this.rb_filetyp1));
            collection.Add(new Tuple<TextBlock, RadioButton>(this.txt_fileType2, this.rb_filetype2));
        }

        List<Tuple<TextBlock, RadioButton>> collection = new List<Tuple<TextBlock, RadioButton>>();

        private void scrollView_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {

            foreach (var item in collection)
            {
                // 获取要定位之前 ScrollViewer 目前的滚动位置
                var currentScrollPosition = this.scrollView.VerticalOffset;
                var point = new Point(0, currentScrollPosition);

                // 计算出目标位置并滚动
                var targetPosition = item.Item1.TransformToVisual(this.scrollView).Transform(point);

                if(targetPosition.Y-point.Y<5)
                {
                    item.Item2.IsChecked = true;
                }
            }
        }
    }


    /// <summary> 说明 </summary>
    partial class FileSetingViewModel : NotifyPropertyChanged
    {
        private ObservableCollection<SettingFileItemViewModel> _matchFileCollection = new ObservableCollection<SettingFileItemViewModel>();
        /// <summary> 说明 </summary>
        public ObservableCollection<SettingFileItemViewModel> MatchFileCollection
        {
            get { return _matchFileCollection; }
            set
            {
                _matchFileCollection = value;
                RaisePropertyChanged();
            }
        }
        private ObservableCollection<SettingFileItemViewModel> _fileTypeCollection = new ObservableCollection<SettingFileItemViewModel>();
        /// <summary> 说明 </summary>
        public ObservableCollection<SettingFileItemViewModel> FileTypeCollection
        {
            get { return _fileTypeCollection; }
            set
            {
                _fileTypeCollection = value;
                RaisePropertyChanged();
            }
        }


        public FileSetingViewModel()
        {
            foreach (var item in CaseNotifyService.MatchTypes)
            {
                SettingFileItemViewModel model = new SettingFileItemViewModel();
                model.IsChecked = true;
                model.FileName = item;
                this.MatchFileCollection.Add(model);
            }

            foreach (var item in CaseNotifyService.MovieTypes)
            {
                SettingFileItemViewModel model = new SettingFileItemViewModel();
                model.IsChecked = true;
                model.FileName = item;
                this.FileTypeCollection.Add(model);
            }

            RelayCommand = new RelayCommand(new Action<object>(ButtonClickFunc));
        }

        /// <summary>
        /// 按钮点击事件
        /// </summary>
        /// <param name="obj"></param>
        private void ButtonClickFunc(object obj)
        {
            if (obj is TextBlock)
            {
                TextBlock tb = obj as TextBlock;
            }
        }

        public RelayCommand RelayCommand { get; set; }
    }


    /// <summary>
    /// 在 ScrollViewer 中定位到指定的控件
    /// 说明：目前支持的是垂直滚动
    /// </summary>
    public class ScrollToControlAction : TriggerAction<FrameworkElement>
    {
        public static readonly DependencyProperty ScrollViewerProperty =
            DependencyProperty.Register("ScrollViewer", typeof(ScrollViewer), typeof(ScrollToControlAction), new PropertyMetadata(null));

        public static readonly DependencyProperty TargetControlProperty =
            DependencyProperty.Register("TargetControl", typeof(FrameworkElement), typeof(ScrollToControlAction), new PropertyMetadata(null));

        /// <summary>
        /// 目标 ScrollViewer
        /// </summary>
        public ScrollViewer ScrollViewer
        {
            get { return (ScrollViewer)GetValue(ScrollViewerProperty); }
            set { SetValue(ScrollViewerProperty, value); }
        }

        /// <summary>
        /// 要定位的到的控件
        /// </summary>
        public FrameworkElement TargetControl
        {
            get { return (FrameworkElement)GetValue(TargetControlProperty); }
            set { SetValue(TargetControlProperty, value); }
        }

        protected override void Invoke(object parameter)
        {
            if (TargetControl == null || ScrollViewer == null)
            {
                throw new ArgumentNullException($"{ScrollViewer} or {TargetControl} cannot be null");
            }

            // 获取要定位之前 ScrollViewer 目前的滚动位置
            var currentScrollPosition = ScrollViewer.VerticalOffset;
            var point = new Point(0, currentScrollPosition);

            // 计算出目标位置并滚动
            var targetPosition = TargetControl.TransformToVisual(ScrollViewer).Transform(point);
            ScrollViewer.ScrollToVerticalOffset(targetPosition.Y);
        }
    }
}
