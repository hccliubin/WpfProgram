using HeBianGu.Controls.ArthasControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace WpfControlDemo.View.ThridParty.ArthasControl
{
    /// <summary>
    /// ArthasControlPage.xaml 的交互逻辑
    /// </summary>
    public partial class ArthasControlPage : Page
    {
        public ArthasControlPage()
        {
            InitializeComponent();

            ts.IsChecked = true;

            treeView.SizeChanged += delegate { waterfallFlow.Refresh(); };

            rt1.Clear();

            rt1.AddLine("阅读模式");
            rt1.AddLine();
            rt1.AddLine("添加正常内容");
            rt1.AddLine("添加正常内容可点击", delegate { MessageBox.Show("你点击了我！"); });
            rt1.AddLine("添加自定义颜色内容", new RgbaColor(255, 0, 0, 255));
            rt1.AddLine("添加自定义颜色内容可点击", new RgbaColor(255, 0, 0, 255), delegate { MessageBox.Show("你点击了我！"); });

            rt3.Clear();

            rt3.AddLine("内容追加测试（不换行添加）");
            rt3.AddLine("http://www.baidu.com", "http://www.baidu.com");
            rt3.AddLine("中间的间距是Add(\"  \");方法添加的两个空格");
            rt3.AddLine();

            rt3.AddLine("追加正常内容");
            rt3.AddLine();
            rt3.Add("正常1");
            rt3.Add("   ");
            rt3.Add("正常2");
            rt3.Add("   ");
            rt3.Add("正常3");
            rt3.AddLine();

            rt3.AddLine("追加正常内容可点击");
            rt3.AddLine();
            rt3.Add("正常1", delegate { MessageBox.Show("你点击了我！"); });
            rt3.Add("   ");
            rt3.Add("正常2", delegate { MessageBox.Show("你点击了我！"); });
            rt3.Add("   ");
            rt3.Add("正常3", delegate { MessageBox.Show("你点击了我！"); });
            rt3.AddLine();

            rt3.AddLine("追加自定义颜色内容");
            rt3.AddLine();
            rt3.Add("颜色1", new RgbaColor(255, 0, 0, 255));
            rt3.Add("   ");
            rt3.Add("颜色2", new RgbaColor(0, 255, 0, 255));
            rt3.Add("   ");
            rt3.Add("颜色3", new RgbaColor(0, 0, 255, 255));
            rt3.AddLine();

            rt3.AddLine("追加自定义颜色内容可点击");
            rt3.AddLine();
            rt3.Add("颜色1", new RgbaColor(255, 0, 0, 255), delegate { MessageBox.Show("你点击了我！"); });
            rt3.Add("   ");
            rt3.Add("颜色2", new RgbaColor(0, 255, 0, 255), delegate { MessageBox.Show("你点击了我！"); });
            rt3.Add("   ");
            rt3.Add("颜色3", new RgbaColor(0, 0, 255, 255), delegate { MessageBox.Show("你点击了我！"); });
            rt3.AddLine();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += delegate
            {
                pb1.Value = pb1.Value + 1 > pb1.Maximum ? 0 : pb1.Value + 1;
                pb2.Value = pb2.Value + 1 > pb2.Maximum ? 0 : pb2.Value + 1;
                pb2.Title = pb2.Hint;
                pb2.Hint = null;
            };
            timer.Interval = new TimeSpan(0, 0, 0, 0, 20);
            timer.Start();

            //ContentRendered += delegate
            //{
            //    // 手动加载指定HTML
            //    web1.Document = ResObj.GetString(Assembly.GetExecutingAssembly(), "Resources.about.html");

            //    // 导航到指定网页
            //    web2.Source = new Uri("http://ie.icoa.cn/");
            //};

            foreach (FrameworkElement fe in lists.Children)
            {
                if (fe is MetroExpander)
                {
                    (fe as MetroExpander).Click += delegate (object sender, EventArgs e)
                    {
                        if ((fe as MetroExpander).CanHide)
                        {
                            foreach (FrameworkElement fe1 in lists.Children)
                            {
                                if (fe1 is MetroExpander && fe1 != sender)
                                {
                                    (fe1 as MetroExpander).IsExpanded = false;
                                }
                            }
                        }
                    };
                }
            }

            /*
            // Chrome 浏览器封装
            ChromeBrowser chrome = new ChromeBrowser();
            chromeGrid.Children.Add(chrome);
            chromeText.Text = chrome.Address;
            chromeText.ButtonClick += delegate { chrome.Load(chromeText.Text); };
            chromeText.KeyUp += delegate (object sender, KeyEventArgs e) { if (e.Key == Key.Enter) { chrome.Load(chromeText.Text); } };
            chrome.Load("http://ie.icoa.cn/");
            */
        }
    }

    public class MainWindowVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _title = "Arthas.Demo";
        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); }
        }

        private bool _btnEnabled = true;
        public bool BtnEnabled
        {
            get { return _btnEnabled; }
            set { _btnEnabled = value; OnPropertyChanged(); }
        }

        private ICommand _cmdSample;
        public ICommand CmdSample => _cmdSample ?? (_cmdSample = new AsyncCommand(async () =>
        {
            Title = "Busy...";
            BtnEnabled = false;
            //do something
            await Task.Delay(2000);
            Title = "Arthas.Demo";
            BtnEnabled = true;
        }));

        private ICommand _cmdSampleWithParam;
        public ICommand CmdSampleWithParam => _cmdSampleWithParam ?? (_cmdSampleWithParam = new AsyncCommand<string>(async str =>
        {
            Title = $"Hello I'm {str} currently";
            BtnEnabled = false;
            //do something
            await Task.Delay(2000);
            Title = "Arthas.Demo";
            BtnEnabled = true;
        }));
    }

    #region Command

    public class AsyncCommand : ICommand
    {
        protected readonly Predicate<object> _canExecute;
        protected Func<Task> _asyncExecute;

        public AsyncCommand(Func<Task> asyncExecute, Predicate<object> canExecute = null)
        {
            _asyncExecute = asyncExecute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public async void Execute(object parameter)
        {
            await _asyncExecute();
        }
    }

    public class AsyncCommand<T> : ICommand
    {
        protected readonly Predicate<T> _canExecute;
        protected Func<T, Task> _asyncExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public AsyncCommand(Func<T, Task> asyncExecute, Predicate<T> canExecute = null)
        {
            _asyncExecute = asyncExecute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute((T)parameter);
        }

        public async void Execute(object parameter)
        {
            await _asyncExecute((T)parameter);
        }
    }
    #endregion
}
