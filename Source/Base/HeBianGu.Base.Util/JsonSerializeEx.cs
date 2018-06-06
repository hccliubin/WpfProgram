#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/11/28 10:55:08 
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.IO;

namespace HeBianGu.Base.Util
{
    public static class JsonSerializeEx
    {
        /// <summary>
        /// 序列化Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string SerializeJson<T>(this object target)
        {
            T result = (T)target;

            DataContractJsonSerializer json = new DataContractJsonSerializer(result.GetType());

            using (MemoryStream stream = new MemoryStream())
            {
                json.WriteObject(stream, result);
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }

        /// <summary>
        /// 返序列化Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <returns></returns>
        public static T SerializeDeJson<T>(this string target)
        {
            if (string.IsNullOrEmpty(target)) return default(T);

            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(target)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(ms);
            }
        }

    }
}
