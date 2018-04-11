using HeBianGu.General.WpfDocument;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace WpfControlDemo.View
{
    /// <summary>
    /// DocumentPage.xaml 的交互逻辑
    /// </summary>
    public partial class DocumentPage : Page
    {
        public DocumentPage()
        {
            InitializeComponent();

            this.Loaded += DocumentPage_Loaded;
        }

        private void DocumentPage_Loaded(object sender, RoutedEventArgs e)
        {

            BindDocumentEngine _vm = new BindDocumentEngine();

            string path = this.GetPath("ProtocolDocument.ini");

            string title = "四川省家庭医生签约服务协议";

            _vm.SetData(this.document, DocumentModel.CreateTestDemo(), path, title);

            this.document.DataContext = _vm;


            FileDocumentEngine _fvm = new FileDocumentEngine();

            _fvm.SetdData(this.document, path);

            this.filedocument.DataContext = _fvm;



        }

        string GetPath(string fileName)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;

            path = System.IO.Path.GetDirectoryName(path);

            path = System.IO.Path.GetDirectoryName(path);

            path = System.IO.Path.Combine(path, "Lib", fileName);

            if (!File.Exists(path))
            {
                throw new Exception("不存在模板配置文件,请检查：" + path);
            }

            return path;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog open = new Microsoft.Win32.OpenFileDialog();
            var r = open.ShowDialog();

            if (r.HasValue && r.Value)
            {
                FileDocumentEngine _fvm = new FileDocumentEngine();

                _fvm.SetdData(this.document, open.FileName);

                this.filedocument.DataContext = _fvm;
            }

        }
    }
}
