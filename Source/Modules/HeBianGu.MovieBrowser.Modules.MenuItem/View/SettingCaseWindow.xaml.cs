using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.MovieBrowser.Modules.MenuItem.Controls;
using HeBianGu.MovieBrowser.Modules.MenuItem.ViewModel;
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
using System.Windows.Shapes;

namespace HeBianGu.MovieBrowser.Modules.MenuItem.View
{
    /// <summary>
    /// SettingCaseWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SettingCaseWindow
    {
        SettingCaseViewModel _vm = new SettingCaseViewModel();

        ItemType _type;
        public SettingCaseWindow(ItemType type)
        {
            InitializeComponent();

            this.DataContext = _vm;

            _type = type;

            this.Loaded += SettingCaseWindow_Loaded;

        }

        private void SettingCaseWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _vm.ItemTypeValue = _type.ToString();

            this.uc_fileSetting.ShowItem = _type;

        }
    }

    class SettingCaseViewModel : NotifyPropertyChanged
    {
        private Visibility _baseVisible = Visibility.Collapsed;
        /// <summary> 说明 </summary>
        public Visibility BaseVisible
        {
            get { return _baseVisible; }
            set
            {
                _baseVisible = value;
                RaisePropertyChanged();
            }
        }

        private Visibility _caseVisible = Visibility.Visible;
        /// <summary> 说明 </summary>
        public Visibility CaseVisible
        {
            get { return _caseVisible; }
            set
            {
                _caseVisible = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand RelayCommand { get; set; }

        public SettingCaseViewModel()
        {
            RelayCommand = new RelayCommand(new Action<object>(ButtonClickFunc));
        }

        private string _itemTypeValue;
        /// <summary> 说明 </summary>
        public string ItemTypeValue
        {
            get { return _itemTypeValue; }
            set
            {
                _itemTypeValue = value;
                RaisePropertyChanged();
            }
        }


        private void ButtonClickFunc(object obj)
        {
            string buttonName = obj as string;

            switch (buttonName)
            {
                case "BaseSetClick":
                    {
                        this.BaseVisible = Visibility.Visible;
                        this.CaseVisible = Visibility.Collapsed;
                    }
                    break;

                case "CaseSetClick":
                    {
                        this.BaseVisible = Visibility.Collapsed;
                        this.CaseVisible = Visibility.Visible;
                    }
                    break;
                case "Btn_Sumit":
                    {



                    }
                    break;
                case "Case2":
                    {

                    }
                    break;
                case "Case3":
                    {

                    }
                    break;
                case "Case4":
                    {

                    }
                    break;
                case "Case5":
                    {

                    }
                    break;
                case "Case6":
                    {

                    }
                    break;
                case "Case7":
                    {

                    }
                    break;
                case "Case8":
                    {

                    }
                    break;
                case "Case9":
                    {

                    }
                    break;
                case "Case10":
                    {

                    }
                    break;
                case "Case11":
                    {

                    }
                    break;
                case "Case12":
                    {

                    }
                    break;
                default:
                    break;
            }

            if(obj is Window)
            {
                WindowBase window = obj as WindowBase;
                window.BegionStoryClose();
            }
        }
    }
}
