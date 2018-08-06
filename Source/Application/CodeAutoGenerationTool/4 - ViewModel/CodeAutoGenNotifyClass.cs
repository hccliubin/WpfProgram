using CodeAutoGenerationTool.Domain;
using CodeAutoGenerationTool.Provider;
using HeBianGu.Base.Util;
using HeBianGu.Base.WpfBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CodeAutoGenerationTool.ViewModel
{


    partial class CodeAutoGenNotifyClass
    {


        private string _dllPath;
        /// <summary> 说明  </summary>
        public string DllPath
        {
            get { return _dllPath; }
            set
            {
                _dllPath = value;
                RaisePropertyChanged("DllPath");
            }
        }


        private string _pdbPath;
        /// <summary> 说明  </summary>
        public string PdbPath
        {
            get { return _pdbPath; }
            set
            {
                _pdbPath = value;
                RaisePropertyChanged("PdbPath");
            }
        }



        private ObservableCollection<TupleExtend<bool, Type>> _classCollection = new ObservableCollection<TupleExtend<bool, Type>>();
        /// <summary> 说明  </summary>
        public ObservableCollection<TupleExtend<bool, Type>> ClassCollection
        {
            get { return _classCollection; }
            set
            {
                _classCollection = value;
                RaisePropertyChanged("ClassCollection");
            }
        }



        private ObservableCollection<TupleExtend<bool, PropertyInfo>> _propertycollection = new ObservableCollection<TupleExtend<bool, PropertyInfo>>();
        /// <summary> 说明  </summary>
        public ObservableCollection<TupleExtend<bool, PropertyInfo>> PropertyCollection
        {
            get { return _propertycollection; }
            set
            {
                _propertycollection = value;
                RaisePropertyChanged("PropertyCollection");
            }
        }



        public void RelayMethod(object obj)
        {
            string command = obj.ToString();

            Debug.WriteLine(command);


            //  Do：应用
            if (command == "TextChanged")
            {
                this.RefreshValue();
            }
            //  Do：取消
            else if (command == "ClassSelectionChanged")
            {

                this.RefreshProperty();
            }
            //  Do：取消
            else if (command == "Generation")
            {

                this.Generation();
            }
            else if (command == "Init")
            {
                ITemplateCommandCollection = CodeAutoGenerationDomain.Instance.GetAllTemplateCommand();
            }
            else if (command == "TemplateSelectionChanged")
            {
                if (this.SelectITemplateCommand == null) return;

                this.TemplateText = this.SelectITemplateCommand.Template("propertyName", "说明");
            }

        }



        private ITemplateCommand _selectItemplateCommand;
        /// <summary> 说明  </summary>
        public ITemplateCommand SelectITemplateCommand
        {
            get { return _selectItemplateCommand; }
            set
            {
                _selectItemplateCommand = value;
                RaisePropertyChanged("SelectITemplateCommand");
            }
        }


        private ObservableCollection<ITemplateCommand> _itemplateCommandcollection = new ObservableCollection<ITemplateCommand>();
        /// <summary> 说明  </summary>
        public ObservableCollection<ITemplateCommand> ITemplateCommandCollection
        {
            get { return _itemplateCommandcollection; }
            set
            {
                _itemplateCommandcollection = value;
                RaisePropertyChanged("ITemplateCommandCollection");
            }
        }



        public void Generation()
        {
            var s = System.Convert.ToString(18, 2).PadLeft(6, '0');


            StringBuilder sb = new StringBuilder();


            Func<string, string, string> fun = (l, k) =>
            {

                string ss = @"    string _" + l.ToLower() + @";
            /// <summary> " + k + @" </summary>
            public string " + l + @"
            {
                get
                {
                    return _" + l.ToLower() + @";
                }
                set
                {
                    _" + l.ToLower() + @" = value;

                    RaisePropertyChanged();
                }
            }";

                return ss;
            };

            XmlTools.Load(PdbPath);


            var v = XmlTools.GetNodes("member");

            foreach (var item in v)
            {
                Debug.WriteLine(item);
            }

            string format = "P:{0}.{1}";

            var ps = this.PropertyCollection.ToList().FindAll(l => l.Item1);

            foreach (var item in ps)
            {

                string pn = item.Item2.Name;

                if (item.Item2.ReflectedType.IsEnumerableType()) continue;

                string d = "说明";

                string pName = string.Format(format, item.Item2.DeclaringType.FullName, pn);

                var f = XmlTools.FindNode("member", l => l.Attributes.Find(k => k.Name == "name" && k.InnerText == pName) != null);

                if (f != null)
                    d = f.InnerText.Replace("\r\n", "").Trim();

                Debug.WriteLine(fun.Invoke(pn.Substring(0, 1).ToUpper() + pn.Substring(1), d));

                sb.AppendLine(fun.Invoke(pn.Substring(0, 1).ToUpper() + pn.Substring(1), d));
            }

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GenerationText.txt");



            File.WriteAllText(path, sb.ToString());

            Process.Start(path);

        }

        void RefreshValue()
        {
            if (this.DllPath == null) return;

            this.ClassCollection.Clear();
            this.PropertyCollection.Clear();

            //  Message：刷新pdb路径
            string pdb = Path.Combine(Path.GetDirectoryName(this.DllPath), Path.GetFileNameWithoutExtension(this.DllPath) + ".XML");

            if (File.Exists(pdb))
            {
                this.PdbPath = pdb;
            }
            else
            {
                this.PdbPath = string.Empty;
            }

            //  Message：获取所有类型

            var ass = Assembly.LoadFile(this.DllPath);

            this.ClassCollection.Clear();

            foreach (var item in ass.GetTypes())
            {
                TupleExtend<bool, Type> t = new TupleExtend<bool, Type>();
                t.Item1 = false;
                t.Item2 = item;
                this.ClassCollection.Add(t);
            }

        }

        void RefreshProperty()
        {
            var selection = this.ClassCollection.ToList().FindAll(l => l.Item1);

            this.PropertyCollection.Clear();

            foreach (var item in selection)
            {

                ObservableCollection<PropertyInfo> collection = new ObservableCollection<PropertyInfo>();

                

                foreach (var c in item.Item2.GetProperties())
                {
                    TupleExtend<bool, PropertyInfo> t = new TupleExtend<bool, PropertyInfo>();

                    t.Item1 = true;
                    t.Item2 = c;

                    this.PropertyCollection.Add(t);
                }

            }
        }


        private string _templateText;
        /// <summary> 说明  </summary>
        public string TemplateText
        {
            get { return _templateText; }
            set
            {
                _templateText = value;
                RaisePropertyChanged("TemplateText");
            }
        }

    }

    partial class CodeAutoGenNotifyClass : INotifyPropertyChanged
    {
        public RelayCommand RelayCommand { get; set; }

        public CodeAutoGenNotifyClass()
        {
            RelayCommand = new RelayCommand(RelayMethod);


            RelayMethod("Init");

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
