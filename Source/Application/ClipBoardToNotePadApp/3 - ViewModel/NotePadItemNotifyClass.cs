using HeBianGu.Base.Util;
using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClipBoardToNotePadApp
{


    partial class NotePadItemNotifyClass
    {


        private string _title = "标题说明:";
        /// <summary> 说明  </summary>
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged("Title");
            }
        }


        private string _content;
        /// <summary> 说明  </summary>
        public string Content
        {
            get { return _content; }
            set
            {
                _content = value;
                RaisePropertyChanged("Content");
            }
        }

        private string _date;
        /// <summary> 说明  </summary>
        public string Date
        {
            get { return _date; }

            set
            {
                _date = value;
                RaisePropertyChanged("Date");
            }
        }

        private string _type;
        /// <summary> 说明  </summary>
        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
                RaisePropertyChanged("Type");
            }
        }

        public static NotePadItemNotifyClass CreateFrom(NotePadItem model)
        {
            NotePadItemNotifyClass vm = new NotePadItemNotifyClass();

            vm.CopyFromObj(model);

            return vm;
        }

        public NotePadItem SaveTo()
        {
            NotePadItem model = new NotePadItem();

            model.CopyFromObj(this);

            return model;
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
            //  Do：取消
            else if (command == "MenuItemCommand_ToCSharp")
            {
                var ss = ItemEnum.CSharp.GetAttribute<DescriptionAttribute>();
                this.Type = ItemEnum.CSharp.GetAttribute<DescriptionAttribute>().Description;

            }
            //  Do：取消
            else if (command == "MenuItemCommand_ToPython")
            {

                this.Type = ItemEnum.Python.GetAttribute<DescriptionAttribute>().Description;
            }
            //  Do：取消
            else if (command == "MenuItemCommand_ToJava")
            {

                this.Type = ItemEnum.Java.GetAttribute<DescriptionAttribute>().Description;
            }
            //  Do：取消
            else if (command == "MenuItemCommand_ToDelete")
            {
                this.Type = string.Empty;
            }
            //  Do：取消
            else if (command == "MenuItemCommand_ToCopy")
            {
                System.Windows.Clipboard.SetDataObject(this.Content);
            }
            //  Do：取消
            else if (command == "MenuItemCommand_ToSave")
            {
                this.Type = ItemEnum.Save.GetAttribute<DescriptionAttribute>().Description;
            }
            //  Do：取消
            else if (command == "MenuItemCommand_ToTemp")
            {
                this.Type = ItemEnum.Temp.GetAttribute<DescriptionAttribute>().Description;
            }
            //  Do：取消
            else if (command == "MenuItemCommand_ToMessage")
            {
                this.Type = ItemEnum.Message.GetAttribute<DescriptionAttribute>().Description;
            }
            //  Do：取消
            else if (command == "MenuItemCommand_ToWork")
            {
                this.Type = ItemEnum.Work.GetAttribute<DescriptionAttribute>().Description;
            }
            //  Do：取消
            else if (command == "MenuItemCommand_ToSecret")
            {
                this.Type = ItemEnum.Secret.GetAttribute<DescriptionAttribute>().Description;
            }

            //  Do：取消
            else if (command == "MenuItemCommand_ToFile")
            {
                this.Type = ItemEnum.File.GetAttribute<DescriptionAttribute>().Description;
            }
            //  Do：取消
            else if (command == "MenuItemCommand_ToUrl")
            {
                this.Type = ItemEnum.Url.GetAttribute<DescriptionAttribute>().Description;

            }


        }
    }

    partial class NotePadItemNotifyClass : INotifyPropertyChanged
    {
        public RelayCommand RelayCommand { get; set; }

        public NotePadItemNotifyClass()
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
