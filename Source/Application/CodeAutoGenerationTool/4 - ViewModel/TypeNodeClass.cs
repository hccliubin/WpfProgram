using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CodeAutoGenerationTool.ViewModel
{


    partial class TypeNodeClass
    {
        private bool _isChecked;
        /// <summary> 说明  </summary>
        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;


                foreach (var item in this.Children)
                {
                    item.IsChecked = value;
                }

                RaisePropertyChanged("IsChecked");
            }
        }


        private object _value;
        /// <summary> 说明  </summary>
        public object Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged("Value");
            }
        }


        private Type _type;
        /// <summary> 说明  </summary>
        public Type Type
        {
            get { return this.Value.GetType(); }
        }

        private string _name;
        /// <summary> 说明  </summary>
        public string Name
        {
            get
            {
                if (this.Type.IsPrimitive || this.Type == typeof(string))
                {
                    return this.Type.Name;
                }

                else if (this.Value is PropertyInfo)
                {
                    return (this.Value as PropertyInfo).Name;
                }
                else if (this.Value is Type)
                {
                    return (this.Value as Type).Name;
                }
                else
                {
                    return this.Type.Name;
                }

            }
        }

        private string _fullPath;
        /// <summary> 说明  </summary>
        public string FullPath
        {
            get { return _fullPath; }
            set
            {
                _fullPath = value;
                RaisePropertyChanged("FullPath");
            }
        }


        private ObservableCollection<TypeNodeClass> _children;
        /// <summary> 说明  </summary>
        public ObservableCollection<TypeNodeClass> Children
        {
            get
            {
                if (_children != null) return _children;

                _children = new ObservableCollection<TypeNodeClass>();

                if (this.Value is Type)
                {
                    var properties = (this.Value as Type).GetProperties();

                    _children.Clear();

                    foreach (var item in properties)
                    {
                        TypeNodeClass t = new TypeNodeClass();
                        t.IsChecked = false;
                        t.Value = item;
                        t.FullPath = this.FullPath + "." + item.Name;

                        this._children.Add(t);
                    }
                }
                else if (this.Value is PropertyInfo)
                {
                    PropertyInfo p = this.Value as PropertyInfo;

                    if (p.PropertyType.IsPrimitive || p.PropertyType == typeof(string))
                    {
                        return _children;
                    }
                    else
                    {
                        var properties = p.PropertyType.GetProperties();

                        foreach (var item in properties)
                        {
                            TypeNodeClass t = new TypeNodeClass();
                            t.IsChecked = false;
                            t.FullPath = this.FullPath + "." + item.Name;
                            t.Value = item;

                            this._children.Add(t);
                        }
                    }

                }
                return _children;
            }
            set
            {
                _children = value;
                RaisePropertyChanged("Children");
            }


        }


        private Visibility _comboboxVisible;
        /// <summary> 说明  </summary>
        public Visibility ComboboxVisible
        {
            get
            {
                return this.Children == null || this.Children.Count == 0 ? Visibility.Visible : Visibility.Hidden;
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


        private TypeNodeClass _mapNode;
        /// <summary> 说明  </summary>
        public TypeNodeClass MapNode
        {
            get { return _mapNode; }
            set
            {
                _mapNode = value;
                RaisePropertyChanged("MapNode");
            }
        }

    }

    partial class TypeNodeClass : INotifyPropertyChanged
    {
        public RelayCommand RelayCommand { get; set; }

        public TypeNodeClass()
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
