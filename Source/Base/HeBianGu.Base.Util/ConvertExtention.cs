#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2018/3/30 16:49:12 
 * 文件名：Class1 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Base.Util
{
    /// <summary> 字符串转换 </summary>
    public static class ConvertExtention
    {
        /// <summary> 转换为Double类型 </summary>
        public static double ToDouble(this string s)
        {
            return Convert.ToDouble(s);
        }

        /// <summary> 转换为Int类型 </summary>
        public static int ToInt(this string s)
        {
            return Convert.ToInt32(s);
        }

        /// <summary> 转换为Double类型 </summary>
        public static double? ToDoubleNull(this string s)
        {
            if (string.IsNullOrEmpty(s)) return null;

            return Convert.ToDouble(s);
        }

        /// <summary> 转换为Int类型 </summary>
        public static int? ToIntNull(this string s)
        {
            if (string.IsNullOrEmpty(s)) return null;

            return Convert.ToInt32(s);
        }

        /// <summary> 转换为string 空则返回空 </summary>
        public static string ToStringNull(this double? s)
        {
            return s == null ? null : s.Value.ToString();
        }

        /// <summary> 转换为Double类型 </summary>
        public static bool IsDouble(this string s)
        {
            double d;
            return double.TryParse(s, out d);
        }

        /// <summary> 转换为Int类型 </summary>
        public static bool IsInt(this string s)
        {
            int i;
            return int.TryParse(s, out i);
        }

        /// <summary> 转换为Int类型 </summary>
        public static bool IsLong(this string s)
        {
            long i;
            return long.TryParse(s, out i);
        }

        /// <summary> 转换为Int类型 </summary>
        public static long ToLong(this string s)
        {
            long i;
            long.TryParse(s, out i);

            return i;
        }

        /// <summary> 字符串转换成日期 string format = "yyyy-MM-dd hh:mi:ss"; </summary>
        public static DateTime ToDateTime(this string str)
        {
            DateTime ss = DateTime.ParseExact(str, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

            return ss;
        }

        /// <summary> 字符串转换成日期 string format = "yyyy-MM-dd hh:mi:ss"; </summary>
        public static DateTime ToDateTime(this string str, string format)
        {
            DateTime ss = DateTime.ParseExact(str, format, CultureInfo.InvariantCulture);

            return ss;
        }

        /// <summary> 时间转换成字符串 string format = "yyyy-MM-dd hh:mi:ss"; </summary>
        public static string ToDateTimeString(this DateTime time)
        {
            return time.ToString("yyyy-MM-dd HH:mm:ss");
        }


        /// <summary> 截取小数点 </summary>
        public static string Round(this string str, int len)
        {
            return Math.Round(str.ToDouble(), len).ToString();
        }

        /// <summary> 格式化字符串 </summary>
        public static string PadLeftExtion(this string str, string format = "{0,10}")
        {
            return String.Format(format, str);
        }
    }
}
