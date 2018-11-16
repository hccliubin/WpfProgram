using HeBianGu.Base.WpfBase;
using HeBianGu.General.ModuleManager.Service;
using HeBianGu.MovieBrowser.Modules.MenuItem.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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

                if(targetPosition.Y-point.Y<50)
                {
                    ScrollToControlAction.flag = true;
                    item.Item2.IsChecked = true;
                    ScrollToControlAction.flag = false;
                }
            }
        }

        public ItemType ShowItem
        {
            get { return (ItemType)GetValue(ShowItemProperty); }
            set { SetValue(ShowItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowItem.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty ShowItemProperty =
            DependencyProperty.Register("ShowItem", typeof(ItemType), typeof(FileSettingControl), new PropertyMetadata(ItemType.MatchFile, PropertyChangedCallback));


        static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FileSettingControl control = d as FileSettingControl;

            ItemType type = (ItemType)e.NewValue;

            switch (type)
            {
                case ItemType.FileType:
                    control.rb_fileType.IsChecked = true;
                    control.act_fileType.OnInvoke(null);
                    break;
                case ItemType.MatchFile:
                    control.rb_matchType.IsChecked = true;
                    control.act_matchType.OnInvoke(null);
                    break;
                default:
                    break;
            }
        }
    }
    
    public enum ItemType
    {
        MatchFile=0,FileType
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


            RelayCommand = new RelayCommand(new Action<object>(ButtonClickFunc));

            if (CaseNotifyService.MatchTypes == null) return;

            foreach (var item in CaseNotifyService.MatchTypes)
            {
                SettingFileItemViewModel model = new SettingFileItemViewModel(item);

                model.DeleteAction += () =>
                  {
                      this.MatchFileCollection.Remove(model);
                  };

                this.MatchFileCollection.Add(model);
            }

            if (CaseNotifyService.MovieTypes == null) return;

            foreach (var item in CaseNotifyService.MovieTypes)
            {
                SettingFileItemViewModel model = new SettingFileItemViewModel(item);

                model.DeleteAction += () =>
                {
                    this.FileTypeCollection.Remove(model);
                };
                this.FileTypeCollection.Add(model);

            }
        }

        private string _addMatchString;
        /// <summary> 添加子项 </summary>
        public string AddMatchString
        {
            get { return _addMatchString; }
            set
            {
                _addMatchString = value;
                RaisePropertyChanged();
            }
        }

        private string _addFileString;
        /// <summary> 添加子项 </summary>
        public string AddFileString
        {
            get { return _addFileString; }
            set
            {
                _addFileString = value;
                RaisePropertyChanged();
            }
        }

        private bool _isMatchChecked=true;
        /// <summary> 说明 </summary>
        public bool IsMatchChecked
        {
            get { return _isMatchChecked; }
            set
            {
                _isMatchChecked = value;
                RaisePropertyChanged();
            }
        }

        private bool _isFileChecked;
        /// <summary> 说明 </summary>
        public bool IsFileChecked
        {
            get { return _isFileChecked; }
            set
            {
                _isFileChecked = value;
                RaisePropertyChanged();
            }
        }


        /// <summary>
        /// 按钮点击事件
        /// </summary>
        /// <param name="obj"></param>
        private void ButtonClickFunc(object obj)
        {
            string buttonName = obj.ToString();

            if (buttonName=="Btn_Sumit")
            {
                // Todo ：保存 
                List<string> matchs = new List<string>();

                foreach (var item in this.MatchFileCollection)
                {
                    matchs.Add(item.FileName);
                }

                List<string> files = new List<string>();

                foreach (var item in this.FileTypeCollection)
                {
                    files.Add(item.FileName);
                }

                CaseNotifyService.MatchTypes = matchs;

                CaseNotifyService.MovieTypes = files;

            }
            else if (buttonName == "btn_AddMatch")
            {
                if (string.IsNullOrEmpty(this.AddMatchString)) return;

                SettingFileItemViewModel model = new SettingFileItemViewModel(this.AddMatchString);

                model.DeleteAction += () =>
                  {
                      this.MatchFileCollection.Remove(model);
                  };

                this.MatchFileCollection.Add(model);

            }
            else if (buttonName == "btn_AddTypes")
            {
                if (string.IsNullOrEmpty(this.AddFileString)) return;

                SettingFileItemViewModel model = new SettingFileItemViewModel(this.AddFileString);

                model.DeleteAction += () =>
                {
                    this.FileTypeCollection.Remove(model);
                };

                this.FileTypeCollection.Add(model);

            }

            else if (buttonName == "DeleteItem")
            {
                if (string.IsNullOrEmpty(this.AddFileString)) return;
                

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

        public static bool flag;

        protected override void Invoke(object parameter)
        {
            if (flag) return;

            if (TargetControl == null || ScrollViewer == null)
            {
                throw new ArgumentNullException($"{ScrollViewer} or {TargetControl} cannot be null");
            }

            // 获取要定位之前 ScrollViewer 目前的滚动位置
            var currentScrollPosition = ScrollViewer.VerticalOffset;
            var point = new Point(0, currentScrollPosition);

            // 计算出目标位置并滚动
            var targetPosition = TargetControl.TransformToVisual(ScrollViewer).Transform(point);
            ScrollViewer.ScrollToVerticalOffset(targetPosition.Y-10);
        }

        public void OnInvoke(object obj)
        {
            this.Invoke(obj);
        }
    }
}
