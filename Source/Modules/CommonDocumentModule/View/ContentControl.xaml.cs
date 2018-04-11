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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel.Composition;
using CommonDocumentModule.ViewModel;

namespace CommonDocumentModule.View
{
    [Export("ContentControl")]
    public partial class ContentControl : UserControl
    {
        public ContentControl()
        {
            InitializeComponent();
        }

        [Import]
        public Test ViewModel
        {
            get { return this.DataContext as Test; }
            set { this.DataContext = value; }
        }
    }
}
