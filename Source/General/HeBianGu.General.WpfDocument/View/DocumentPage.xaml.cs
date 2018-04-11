using Controls.PrintWorkService.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace HeBianGu.General.WpfDocument
{
    /// <summary>
    /// DocumentPage.xaml 的交互逻辑
    /// </summary>
    public partial class DocumentPage : UserControl
    {
        public DocumentPage()
        {
            InitializeComponent();
        }

        public ObservableCollection<ItemModel> ItemSource
        {
            get { return (ObservableCollection<ItemModel>)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemSourceProperty =
            DependencyProperty.Register("ItemSource", typeof(ObservableCollection<ItemModel>), typeof(DocumentPage), new PropertyMetadata(new ObservableCollection<ItemModel>()));

        public void Print()
        {
            //PrintProvider.Instance.PrintGrid(this.grid_all);
        }
             


    }
}
