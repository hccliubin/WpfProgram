#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/11/30 17:18:27 
 * 文件名：Converter 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HeBianGu.General.ModuleManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace HeBianGu.General.ModuleManager
{
    /// <summary> 枚举转换图片 </summary>
    [ValueConversion(typeof(string), typeof(ClipBoardType))]
    public class TypeToIconFontConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ClipBoardType t = (ClipBoardType)value;

            switch (t)
            {
                case ClipBoardType.FileSystem:
                    return null;
                case ClipBoardType.Image:
                    return "\xe61e";
                case ClipBoardType.Text:
                    return "\xe649";
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new Exception();
        }
    }

    [ValueConversion(typeof(Visibility), typeof(ClipBoardType))]
    public class TypeToVisibleFontConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ClipBoardType t = (ClipBoardType)value;

            switch (t)
            {
                case ClipBoardType.FileSystem:
                    return Visibility.Hidden;
                case ClipBoardType.Image:
                    return Visibility.Visible;
                case ClipBoardType.Text:
                    return Visibility.Visible;
                default:
                    return Visibility.Hidden;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new Exception();
        }
    }
}
