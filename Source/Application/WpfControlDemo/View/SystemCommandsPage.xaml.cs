using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
    /// SystemCommandsPage.xaml 的交互逻辑
    /// </summary>
    public partial class SystemCommandsPage : Page
    {
        public SystemCommandsPage()
        {
            InitializeComponent();

            this.Loaded += SystemCommandsPage_Loaded;
        }

        private void SystemCommandsPage_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }

    public static class CommandsClass
    {


        public static List<RoutedUICommand> GetCommands(Type type)
        {
            List<RoutedUICommand> result = new List<RoutedUICommand>();

            var collection = type.GetProperties();

            foreach (PropertyInfo item in collection)
            {
                 RoutedUICommand command = item.GetValue(null, null) as RoutedUICommand;
                result.Add(command);

            }
            
            return result;
        }
    }

    //public class RoutedUICommandClass
    //{
    //    public string Content { get; set; }

    //    public RoutedUICommand Command { get; set; }
    //}
}
