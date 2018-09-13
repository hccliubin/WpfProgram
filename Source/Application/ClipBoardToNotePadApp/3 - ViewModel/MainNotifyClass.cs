using HeBianGu.Base.Util;
using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClipBoardToNotePadApp
{
    partial class MainNotifyClass
    {


        private ObservableCollection<NotePadItemNotifyClass> _collection = new ObservableCollection<NotePadItemNotifyClass>();


        /// <summary> 说明  </summary>
        public ObservableCollection<NotePadItemNotifyClass> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged("Collection");
            }
        }

        private bool _isSaving;
        /// <summary> 说明  </summary>
        public bool IsSaving
        {
            get { return _isSaving; }
            set
            {
                _isSaving = value;
                RaisePropertyChanged("IsSaving");
            }
        }


        public void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Init")
            {
                ClipBoardRegisterService.Instance.ClipBoardChanged += () =>
                {
                    if (!System.Windows.Clipboard.ContainsText()) return;

                    string text = string.Empty;

                    try
                    {
                        // HTodo  ：复制的文件路径 
                        text = System.Windows.Clipboard.GetText();
                    }
                    catch
                    {

                    }


                    if (string.IsNullOrEmpty(text)) return;

                    if (this.Collection.Count > 0)
                    {
                        NotePadItemNotifyClass last = this.Collection.First();

                        if (last.Content != text)
                        {
                            NotePadItemNotifyClass f = new NotePadItemNotifyClass();
                            f.Content = text;
                            f.Date = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                            this.Collection.Insert(0, f);
                        }
                    }
                    else
                    {
                        NotePadItemNotifyClass f = new NotePadItemNotifyClass();
                        f.Content = text;
                        f.Date = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        this.Collection.Insert(0, f);
                    }
                };

                this.Load();

                //  ToDo：定时保存
                System.Timers.Timer time = new System.Timers.Timer();

                time.Interval = 10000;

                time.Elapsed += (l, k) =>
                {
                    this.Save();
                };
                time.Start();




            }

            else if (command == "ButtonCommand_Save")
            {
                this.Save();
            }
        }


        void Save()
        {
            this.IsSaving = true;

            Task.Run(() =>
            {
                string filePath = ConfigService.Instance.LocalDataPath;

                List<NotePadItem> models = new List<NotePadItem>();

                foreach (var item in this.Collection)
                {
                    models.Add(item.SaveTo());
                }

                string c = models.SerializeJson<List<NotePadItem>>();

                Thread.Sleep(2000);

                File.WriteAllText(filePath, c);

                this.IsSaving = false;
            }
);




        }

        void Load()
        {
            string filePath = ConfigService.Instance.LocalDataPath;

            if (!System.IO.Path.GetDirectoryName(filePath).IsExistDirectory())
            {
                System.IO.Path.GetDirectoryName(filePath).CreateDir();
            }

            if (!File.Exists(filePath)) return;

            string result = File.ReadAllText(filePath);

            var loadResult = result.SerializeDeJson<List<NotePadItem>>();

            if (loadResult == null) return;

            foreach (var item in loadResult)
            {
                this.Collection.Add(NotePadItemNotifyClass.CreateFrom(item));
            }

        }

    }

    partial class MainNotifyClass : INotifyPropertyChanged
    {
        public RelayCommand RelayCommand { get; set; }

        public MainNotifyClass()
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
