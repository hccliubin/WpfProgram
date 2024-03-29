﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Discount.RandomPrice.Client.UC
{
    /// <summary>
    /// NumberItem.xaml 的交互逻辑
    /// </summary>
    public partial class NumberItem : UserControl
    {
        private string _NumberValue;
        /// <summary>
        /// 数字值
        /// </summary>
        public string NumberValue 
        {
            get
            {
                return _NumberValue;
            }
            set
            {
                textBlockNumberText.Text = value.ToString();
                _NumberValue = value;
            }
        }

        public NumberItem()
        {
            InitializeComponent();
        }
    }
}
