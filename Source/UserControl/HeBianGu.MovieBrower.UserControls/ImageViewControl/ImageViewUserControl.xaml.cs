﻿using HeBianGu.Base.WpfBase;
using System;
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

namespace HeBianGu.MovieBrower.UserControls.ImageViewControl
{
    /// <summary>
    /// ImageViewUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class ImageViewUserControl : UserControl
    {
        public ImageViewUserControl()
        {
            InitializeComponent();

            this.tpageControl.SelectChanged += l =>
            {
                if (l < 0) return;

                if (this.ImagePaths == null) return;

                if (this.ImagePaths.Count <= l - 1) return;

                if (l - 1 < 0) return;

                this.SelectValue = this.ImagePaths[l- 1];

                //this.SelectValue = this.ImagePaths[l-1];
            };
        }

        public ObservableCollection<string> ImagePaths
        {
            get { return (ObservableCollection<string>)GetValue(ImagePathsProperty); }
            set { SetValue(ImagePathsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImagePaths.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImagePathsProperty =
            DependencyProperty.Register("ImagePaths", typeof(ObservableCollection<string>), typeof(ImageViewUserControl), new PropertyMetadata(null, ImagePathsPropertyChangedCallback));

        public static void ImagePathsPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ImageViewUserControl;

            if (control == null) return;

            List<UserControl> userControls = new List<UserControl>();

            if (e.NewValue == null)
            {
                control.tpageControl.BindControls = userControls;
                return;
            }

            ObservableCollection<string> collection = e.NewValue as ObservableCollection<string>;

            if (collection.Count > 0)
            {
                control.SelectValue = collection[0];
            }

            foreach (var item in collection)
            {
                ImageItemUserControl c = new ImageItemUserControl();

                FileInfo file = new FileInfo(item);

                MovieFileViewModel m = new MovieFileViewModel(file);

                c.DataContext = m;

                userControls.Add(c);
            }
            control.tpageControl.ClearPage();

            control.tpageControl.BindControls = userControls;
        }



        public string SelectValue
        {
            get { return (string)GetValue(SelectValueProperty); }
            set { SetValue(SelectValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectValueProperty =
            DependencyProperty.Register("SelectValue", typeof(string), typeof(ImageViewUserControl), new PropertyMetadata(null));




        public bool IsShowLeftRight
        {
            get { return (bool)GetValue(IsShowLeftRightProperty); }
            set { SetValue(IsShowLeftRightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsShowLeftRight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsShowLeftRightProperty =
            DependencyProperty.Register("IsShowLeftRight", typeof(bool), typeof(ImageViewUserControl), new PropertyMetadata(true, IsShowLeftRightPropertyChangedCallback));

        public static void IsShowLeftRightPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ImageViewUserControl;

            if (control == null) return;

            if ((bool)e.NewValue) return;

            control.tpageControl.LeftWidth = 0;
            control.tpageControl.RightWidth = 0;
        }



        public bool IsAutoMove
        {
            get { return (bool)GetValue(IsAutoMoveProperty); }
            set { SetValue(IsAutoMoveProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAutoMove.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAutoMoveProperty =
            DependencyProperty.Register("IsAutoMove", typeof(bool), typeof(ImageViewUserControl), new PropertyMetadata(false, IsAutoMovePropertyChangedCallback));

        public static void IsAutoMovePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ImageViewUserControl;

            if (control == null) return;

            control.tpageControl.IsAutoMove = (bool)e.NewValue;
        }


    }

    partial class ImageViewUserControl : INotifyPropertyChanged
    {
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
