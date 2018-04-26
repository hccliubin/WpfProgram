using HeBianGu.General.ModuleManager.Service;
using HeBianGu.MovieBrowser.Modules.MenuItem.ViewModel;
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
using System.Windows.Shapes;

namespace HeBianGu.MovieBrowser.Modules.MenuItem.View
{
    /// <summary>
    /// AddCaseWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AddCaseWindow
    {
        public AddCaseWindow()
        {
            InitializeComponent();

            this.ViewModel = new AddCaseViewModel();
        }


        /// <summary> 说明 </summary>
        public AddCaseViewModel ViewModel
        {
            get { return this.DataContext as AddCaseViewModel; }
            set { this.DataContext = value; }
        }

        private void FButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }


    /// <summary> 说明 </summary>
    public partial class AddCaseViewModel
    {
        private string _caseName;
        /// <summary> 说明 </summary>
        public string CaseName
        {
            get { return _caseName; }
            set
            {
                _caseName = value;
                RaisePropertyChanged();
            }
        }

        private string _casePath;
        /// <summary> 说明 </summary>
        public string CasePath
        {
            get { return _casePath; }
            set
            {
                _casePath = value;
                RaisePropertyChanged();
            }
        }

    }

    partial class AddCaseViewModel : INotifyPropertyChanged
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
