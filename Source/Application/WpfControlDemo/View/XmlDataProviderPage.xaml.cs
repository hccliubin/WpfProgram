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
using System.Xml;
using System.Xml.Linq;

namespace WpfControlDemo.View
{
    /// <summary>
    /// XmlDataProviderPage.xaml 的交互逻辑
    /// </summary>
    public partial class XmlDataProviderPage : Page
    {
        public XmlDataProviderPage()
        {
            InitializeComponent();
        }

        private void cmdExpandAll_Click(object sender, RoutedEventArgs e)
        {
            this.treeXml.Style = (Style)this.FindResource("treeView_AllExpanded");
        }

        private void cmdCollapseAll_Click(object sender, RoutedEventArgs e)
        {
            this.treeXml.Style = (Style)this.FindResource("treeView_AllCollapsed");
        }

        private void cmdLoadXml_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.OpenFileDialog openFD = new Microsoft.Win32.OpenFileDialog();
                openFD.Filter = "XML Documents (*.xml)|*.xml|All Files (*.*)|*.*";
                Nullable<bool> isUserPickFile = openFD.ShowDialog();

                if (isUserPickFile == true)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(openFD.FileName);
                    XmlDataProvider xmlDP = (XmlDataProvider)this.FindResource("xmlDataProvider");
                    xmlDP.Document = xmlDoc;
                    xmlDP.XPath = "*";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.OpenFileDialog openFD = new Microsoft.Win32.OpenFileDialog();
                openFD.Filter = "XML Documents (*.xml)|*.xml|All Files (*.*)|*.*";
                Nullable<bool> isUserPickFile = openFD.ShowDialog();

                if (isUserPickFile == true)
                {
                    XDocument planetsDoc = XDocument.Load(openFD.FileName);
                    stacky.DataContext = planetsDoc.Elements();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
