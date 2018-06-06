using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// DataBindingPage.xaml 的交互逻辑
    /// </summary>
    public partial class DataBindingPage : Page
    {
        public DataBindingPage()
        {
            InitializeComponent();
        }
    }


    /// <summary> 说明 </summary>
   public class Person
    {
        private string _personName;
        /// <summary> 说明 </summary>
        public string PersonName
        {
            get { return _personName; }
            set { _personName = value; }
        }

        public Person()
        {
            
        }

        public Person(string personName)
        {
            _personName = personName;
        }

    }

    public class AgeRangeRule : ValidationRule
    {
        private int _min;
        private int _max;

        public AgeRangeRule()
        {
        }

        public int Min
        {
            get { return _min; }
            set { _min = value; }
        }

        public int Max
        {
            get { return _max; }
            set { _max = value; }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int age = 0;

            try
            {
                if (((string)value).Length > 0)
                    age = Int32.Parse((String)value);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, "Illegal characters or " + e.Message);
            }

            if ((age < Min) || (age > Max))
            {
                return new ValidationResult(false,
                  "Please enter an age in the range: " + Min + " - " + Max + ".");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }
}
