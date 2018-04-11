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
    /// TPageControlPage.xaml 的交互逻辑
    /// </summary>
    public partial class TPageControlPage : Page
    {
        public TPageControlPage()
        {
            InitializeComponent();

            this.DataContext = this;

            this.Loaded += TPageControlPage_Loaded;

        }

        private void TPageControlPage_Loaded(object sender, RoutedEventArgs e)
        {

            List<UserControl> cs = new List<UserControl>();

            for (int i = 0; i < 10; i++)
            {
                UserControl uc = new UserControl();

                Button btn = new Button();

                btn.Content = i;

                uc.Content = btn;

                cs.Add(uc);
            }

            this.Controls = cs;
           
        }

        private List<UserControl> _controls;
        /// <summary> 说明 </summary>
        public List<UserControl> Controls
        {
            get { return _controls; }
            set
            {
                _controls = value;
                RaisePropertyChanged();
            }
        }

    }

    partial class TPageControlPage : INotifyPropertyChanged
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
