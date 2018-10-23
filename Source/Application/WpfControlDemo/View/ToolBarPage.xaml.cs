using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace WpfControlDemo.View
{
    /// <summary>
    /// ToolBarPage.xaml 的交互逻辑
    /// </summary>
    public partial class ToolBarPage : Page
    {

        ToolBarPageNotifyClass _vm = new ToolBarPageNotifyClass();

        public ToolBarPage()
        {
            InitializeComponent();

            this.DataContext = _vm;
        }
    }



    partial class ToolBarPageNotifyClass
    {


        private List<FButton> _buttons;
        /// <summary> 说明  </summary>
        public List<FButton> Buttons
        {
            get { return _buttons; }
            set
            {
                _buttons = value;
                RaisePropertyChanged("Buttons");
            }
        }


        public void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Init")
            {
                List<FButton> collection = new List<FButton>();

                FButton button = new FButton();

                collection.Add(button);

                this.Buttons = collection;

            }
            //  Do：取消
            else if (command == "Cancel")
            {


            }
        }
    }

    partial class ToolBarPageNotifyClass : INotifyPropertyChanged
    {
        public RelayCommand RelayCommand { get; set; }

        public ToolBarPageNotifyClass()
        {
            RelayCommand = new RelayCommand(RelayMethod);

            RelayMethod("Init");

        }
        #region - MVVM -

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

}
