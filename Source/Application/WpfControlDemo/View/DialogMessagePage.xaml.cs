using HeBianGu.Base.WpfBase;
using HeBianGu.Controls.MaterialControl;
using MaterialDesignColors.WpfExample.Domain;
using System;
using System.Collections.Generic;
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
    /// DialogMessagePage.xaml 的交互逻辑
    /// </summary>
    public partial class DialogMessagePage : Page
    {

        DialogMessageNotifyClass _vm = new DialogMessageNotifyClass();
        public DialogMessagePage()
        {
            InitializeComponent();

            this.DataContext = _vm;
        }

        private void Sample1_DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            Console.WriteLine("SAMPLE 1: Closing dialog with parameter: " + (eventArgs.Parameter ?? ""));


            var messageQueue = MainSnackbar.MessageQueue;
            var message = $"SAMPLE 1: Closing dialog with parameter: {eventArgs.Parameter}";

            //the message queue can be called from any thread
            Task.Factory.StartNew(() => messageQueue.Enqueue(message));


            if (!Equals(eventArgs.Parameter, true)) return;

            if (!string.IsNullOrWhiteSpace(FruitTextBox.Text))
                FruitListBox.Items.Add(FruitTextBox.Text.Trim());
        }

        private void Sample2_DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            Console.WriteLine("SAMPLE 2: Closing dialog with parameter: " + (eventArgs.Parameter ?? ""));

            var messageQueue = MainSnackbar.MessageQueue;
            var message = $"SAMPLE 2: Closing dialog with parameter: {eventArgs.Parameter}";

            //the message queue can be called from any thread
            Task.Factory.StartNew(() => messageQueue.Enqueue(message));
        }

        private void Sample5_DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            Console.WriteLine("SAMPLE 5: Closing dialog with parameter: " + (eventArgs.Parameter ?? ""));
            var messageQueue = MainSnackbar.MessageQueue;
            var message = $"SAMPLE 5: Closing dialog with parameter: {eventArgs.Parameter}";

            //the message queue can be called from any thread
            Task.Factory.StartNew(() => messageQueue.Enqueue(message));

            //you can cancel the dialog close:
            //eventArgs.Cancel();

            if (!Equals(eventArgs.Parameter, true)) return;

            //if (!string.IsNullOrWhiteSpace(AnimalTextBox.Text))
            //    AnimalListBox.Items.Add(AnimalTextBox.Text.Trim());
        }



    }



    partial class DialogMessageNotifyClass
    {

        public void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "runcode")
            {
                RunCode();
            }
            //  Do：取消
            else if (command == "Cancel")
            {


            }
        }

        public async void RunCode()
        {
            var view = new SampleMessageDialog();

            view.MessageStr = "测试";

            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog");
        }
    }

    partial class DialogMessageNotifyClass : INotifyPropertyChanged
    {
        public RelayCommand RelayCommand { get; set; }

        public DialogMessageNotifyClass()
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
