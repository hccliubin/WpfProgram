﻿using HeBianGu.Base.WpfBase;
using HeBianGu.Base.WpfBase.Color;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
    /// <summary> 视觉规范 </summary>
    public partial class VisuaNormsPagePage : Page
    {

        VisuaNormsNotifyClass _vm = new VisuaNormsNotifyClass();
        public VisuaNormsPagePage()
        {
            InitializeComponent();

            this.DataContext = _vm;


        }


    }



    partial class VisuaNormsNotifyClass
    {

        ResourceDictionary GetThemeDictionary()
        {
            return (from dict in Application.Current.Resources.MergedDictionaries
                    where dict.Contains("S_AccentBrush")
                    select dict).FirstOrDefault();
        }


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

        private ObservableCollection<ItemNotifyClass> _fontcollection = new ObservableCollection<ItemNotifyClass>();
        /// <summary> 说明  </summary>
        public ObservableCollection<ItemNotifyClass> FontCollection
        {
            get { return _fontcollection; }
            set
            {
                _fontcollection = value;
                RaisePropertyChanged("FontCollection");
            }
        }


        public void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Loaded")
            {

                ResourceDictionary resource = this.GetThemeDictionary();

                foreach (var item in resource.Keys)
                {

                    object current = resource[item.ToString()];

                    if (current is SolidColorBrush)
                    {
                        //  Message：查找到了想要的资源
                        ItemNotifyClass itemClass = new ItemNotifyClass();
                        SolidColorBrush brush = current as SolidColorBrush;
                        itemClass.Color = brush;
                        itemClass.Name = item.ToString();
                        itemClass.Value = current.ToString();

                        if( ThemeService.Current.KeyToMarkDictionary.ContainsKey(itemClass.Name))
                        {
                            itemClass.Mark = ThemeService.Current.KeyToMarkDictionary[itemClass.Name];
                        }

                        this.Collection.Add(itemClass);

                    }
                }

            }
            //  Do：取消
            else if (command == "Cancel")
            {


            }
        }



    }

    partial class VisuaNormsNotifyClass : INotifyPropertyChanged
    {
        public RelayCommand RelayCommand { get; set; }

        public VisuaNormsNotifyClass()
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

    partial class ItemNotifyClass
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


        private object _color;
        /// <summary> 说明  </summary>
        public object Color
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

    partial class ItemNotifyClass : INotifyPropertyChanged
    {
        public RelayCommand RelayCommand { get; set; }

        public ItemNotifyClass()
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
