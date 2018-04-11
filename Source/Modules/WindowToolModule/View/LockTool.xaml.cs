﻿using HeBianGu.Base.Util;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
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

namespace WindowToolModule.View
{
    /// <summary>
    /// LockTool.xaml 的交互逻辑
    /// </summary>
    [Export]
    [ViewSortHint("04")]
    public partial class LockTool : UserControl
    {
        public LockTool()
        {
            InitializeComponent();
        }

        private void btn_bar_Click(object sender, RoutedEventArgs e)
        {
            WinAPIServer.Instance.Lock();
        }
    }
}
