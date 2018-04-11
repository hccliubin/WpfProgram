using HeBianGu.General.WpfControlLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using HeBianGu.General.WpfControlLib;

namespace WpfControlDemo.View
{
    /// <summary>
    /// DataPagePage.xaml 的交互逻辑
    /// </summary>
    public partial class DataPagePage : Page
    {
        public DataPagePage()
        {
            Result = new StuResult();

            this.DataContext = Result;

            InitializeComponent();
        }


        private void dataPager_PageChanged(object sender, PageChangedEventArgs args)
        {
            Query(args.PageSize, args.PageIndex);
        }

        private void PagingDataGrid_PagingChanged(object sender, PagingChangedEventArgs args)
        {
            Query(args.PageSize, args.PageIndex);
        }

        public StuResult Result { get; set; }

        public void Query(int size, int pageIndex)
        {
            Result.Total = Student.Students.Count;
            Result.Students = Student.Students.Skip((pageIndex - 1) * size).Take(size).ToList();

        }
    }


    public class StuResult : INotifyPropertyChanged
    {

        public int _total;
        public int Total
        {
            get
            {
                return _total;
            }
            set
            {
                if (_total != value)
                {
                    _total = value;
                    RaisePropertyChanged("Total");
                }
            }
        }

        private List<Student> _students;
        public List<Student> Students
        {
            get
            {
                return _students;
            }
            set
            {
                if (_students != value)
                {
                    _students = value;
                    RaisePropertyChanged("Students");
                }
            }
        }

        public StuResult()
        {
            Students = new List<Student>();
        }

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public bool Gender { get; set; }
        public string Desc { get; set; }

        public static List<Student> Students { get; set; }

        static Student()
        {
            Students = new List<Student>();

            Random random = new Random();
            int count = random.Next(20, 200);
            for (int i = 1; i <= count; i++)
            {
                Student stu = new Student
                {
                    Name = "Name" + i,
                    Age = random.Next(20, 50),
                    Gender = (i % 3 != 0),
                    Desc = "Desc " + i,
                };
                Students.Add(stu);
            }
        }
    }
}
