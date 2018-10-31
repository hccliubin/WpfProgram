using System;
using System.Collections;
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

namespace WpfControlDemo.View
{
    /// <summary>
    /// ObjectDataProviderPage.xaml 的交互逻辑
    /// </summary>
    public partial class ObjectDataProviderPage : Page
    {
        public ObjectDataProviderPage()
        {
            InitializeComponent();
        }


     
    }

    public class MathTest
    {
        public string MethodAdd(string a, string b)
        {
            if (string.IsNullOrEmpty(a) || string.IsNullOrEmpty(b)) return null;

            return (int.Parse(a) + int.Parse(b)).ToString();
        }

    }

    public class ObjectDataSourceService
    {

        public IEnumerable GetStaticPropertys(Type type, Type propertyType)
        {
            var collection = type.GetProperties();

            foreach (var item in collection)
            {
                if (item.PropertyType == propertyType)
                {
                    yield return item.GetValue(null, null);
                }
            }
        }

        public IEnumerable GetPropertys(Type type, Type propertyType = null)
        {
            var collection = type.GetProperties();

            foreach (var item in collection)
            {
                if (item.PropertyType == propertyType)
                {
                    yield return item.GetValue(type, null);
                }
            }
        }

        public IEnumerable GetEnumValues(Type type)
        {
            return Enum.GetValues(type);
        }
    }
}
