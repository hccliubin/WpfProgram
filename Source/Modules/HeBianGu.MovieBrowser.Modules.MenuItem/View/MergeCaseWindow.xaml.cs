using HeBianGu.General.ModuleManager.Model;
using HeBianGu.MovieBrowser.Modules.MenuItem.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// MergeCaseWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MergeCaseWindow
    {
        MergeCaseViewModel _vm = new MergeCaseViewModel();
        public MergeCaseWindow(List<CaseModel> models)
        {
            InitializeComponent();

            this.DataContext = _vm;

            foreach (var item in models)
            {
                SelectCaseViewModel m = new SelectCaseViewModel(item);
                _vm.Collection.Add(m);
            }
        }
        

        private void FButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        public List<CaseModel> SelectItems()
        {
            return this._vm.Collection.ToList().FindAll(l => l.IsChecked).Select(l => l.Model).ToList();
        }
    }

    /// <summary> 说明 </summary>
    public partial class MergeCaseViewModel
    {


        private ObservableCollection<SelectCaseViewModel> _collection = new ObservableCollection<SelectCaseViewModel>();
        /// <summary> 说明 </summary>
        public ObservableCollection<SelectCaseViewModel> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged();
            }
        }

    }

    partial class MergeCaseViewModel : INotifyPropertyChanged
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
