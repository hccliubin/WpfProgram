﻿using HeBianGu.Base.WpfBase;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfControlDemo.View
{
    /// <summary>
    /// WindowSystemPage.xaml 的交互逻辑
    /// </summary>
    public partial class WindowSystemPage : Page
    {

        WindowSystemNotifyClass _vm = new WindowSystemNotifyClass();
        public WindowSystemPage()
        {
            InitializeComponent();

            this.DataContext = _vm;
        }
    }


    partial class WindowSystemNotifyClass
    {

        private ObservableCollection<ItemNotifyClass> _collection = new ObservableCollection<ItemNotifyClass>();
        /// <summary> 说明  </summary>
        public ObservableCollection<ItemNotifyClass> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged("Collection");
            }
        }


        public void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Loaded")
            {

                //var value= System.Windows.SystemColors

                foreach (var item in typeof(SystemColors).GetProperties())
                {
                    var value = item.GetValue(null, null);

                    if (item.PropertyType==typeof(SolidColorBrush))
                    {
                        ItemNotifyClass itemClass = new ItemNotifyClass();
                        SolidColorBrush brush = value as SolidColorBrush;
                        itemClass.Color = brush;
                        itemClass.Name = item.Name.ToString();
                        itemClass.Value = value.ToString();

                        this.Collection.Add(itemClass);

                    }
                    //else if(item.PropertyType==typeof(Color))
                    //{
                    //    ItemNotifyClass itemClass = new ItemNotifyClass();
                    //    Color brush = (Color)value;
                    //    itemClass.Color = new SolidColorBrush(brush);
                    //    itemClass.Name = item.Name.ToString();
                    //    itemClass.Value = value.ToString();

                    //    this.Collection.Add(itemClass);
                    //}
                    
                 

                   

                }



                //foreach (var item in resource.Keys)
                //{

                //    object current = resource[item.ToString()];

                //    if (current is SolidColorBrush)
                //    {
                //        //  Message：查找到了想要的资源
                //        ItemNotifyClass itemClass = new ItemNotifyClass();
                //        SolidColorBrush brush = current as SolidColorBrush;
                //        itemClass.Color = brush;
                //        itemClass.Name = item.ToString();
                //        itemClass.Value = current.ToString();

                //        this.Collection.Add(itemClass);

                //    }
                //}

            }
            //  Do：取消
            else if (command == "Cancel")
            {


            }
        }
    }

    partial class WindowSystemNotifyClass : INotifyPropertyChanged
    {
        public RelayCommand RelayCommand { get; set; }

        public WindowSystemNotifyClass()
        {
            RelayCommand = new RelayCommand(RelayMethod);

        }
        #region - MVVM -

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

    partial class WindowSystemItemNotifyClass
    {

        private string _name;
        /// <summary> 说明  </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged("Name");
            }
        }


        private Brush _color;
        /// <summary> 说明  </summary>
        public Brush Color
        {
            get { return _color; }
            set
            {
                _color = value;
                RaisePropertyChanged("Color");
            }
        }


        private string _mark;
        /// <summary> 说明  </summary>
        public string Mark
        {
            get { return _mark; }
            set
            {
                _mark = value;
                RaisePropertyChanged("Mark");
            }
        }


        private string _value;
        /// <summary> 说明  </summary>
        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged("Value");
            }
        }



        public void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Sumit")
            {


            }
            //  Do：取消
            else if (command == "Cancel")
            {


            }
        }
    }

    partial class WindowSystemItemNotifyClass : INotifyPropertyChanged
    {
        public RelayCommand RelayCommand { get; set; }

        public WindowSystemItemNotifyClass()
        {
            RelayCommand = new RelayCommand(RelayMethod);

        }
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
