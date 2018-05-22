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
    /// OtherControlPage.xaml 的交互逻辑
    /// </summary>
    public partial class OtherControlPage : Page
    {

        MyClass _vm = new MyClass();

        public OtherControlPage()
        {
            InitializeComponent();

            this.Loaded +=(s, e) =>
                                {
                                    this.DataContext = _vm;
                                };



           
        }
    }

    /// <summary> 说明 </summary>
    partial class MyClass
    {
        private double _left = 0.1;
        /// <summary> 说明 </summary>
        public double Left
        {
            get { return _left; }
            set
            {
                _left = value;
                RaisePropertyChanged();
            }
        }

        private double _right = 0.7;
        /// <summary> 说明 </summary>
        public double Right
        {
            get { return _right; }
            set
            {
                _right = value;
                RaisePropertyChanged();
            }
        }


    }

    partial class MyClass : INotifyPropertyChanged
    {
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
