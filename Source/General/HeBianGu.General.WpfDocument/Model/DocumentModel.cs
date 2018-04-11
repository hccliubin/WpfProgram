#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2018/1/30 10:54:39 
 * 文件名：DocumentModel 
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

namespace HeBianGu.General.WpfDocument
{
    public class DocumentModel
    {
        private string _country;
        /// <summary> 市 </summary>
        public string Country
        {
            get { return _country; }
            set
            {
                _country = value;
            }
        }

        private string _community;
        /// <summary> 社区 </summary>
        public string Community
        {
            get { return _community; }
            set { _community = value; }
        }

        private string _complain;
        /// <summary> 投诉电话 </summary>
        public string Complain
        {
            get { return _complain; }
            set { _complain = value; }
        }

        private string _leaderB;
        /// <summary> 乙方负责人 </summary>
        public string LeaderB
        {
            get { return _leaderB; }
            set { _leaderB = value; }
        }

        private string _operator;
        /// <summary> 乙方经办人 </summary>
        public string Operator
        {
            get { return _operator; }
            set { _operator = value; }
        }

        private List<Tuple<string, string>> _members = new List<Tuple<string, string>>();
        /// <summary> 成员姓名和联系方式 </summary>
        public List<Tuple<string, string>> Members
        {
            get { return _members; }
            set { _members = value; }
        }

        private List<Tuple<string, string, string, string>> _famliys = new List<Tuple<string, string, string, string>>();
        /// <summary> 甲方成员 </summary>
        public List<Tuple<string, string, string, string>> Famliys
        {
            get { return _famliys; }
            set { _famliys = value; }
        }

        private string _contract;
        /// <summary> 甲方签约代表 </summary>
        public string Contract
        {
            get { return _contract; }
            set { _contract = value; }
        }



        public static DocumentModel CreateTestDemo()
        {
            DocumentModel d = new DocumentModel();

            d.Country = "成都";
            d.Community = "高新";
            d.Complain = "135167388766";
            d.LeaderB = "会挥发";
            d.Operator = "发黑";
            d.Contract = "备份";

            for (int i = 0; i < 2; i++)
            {
                Tuple<string, string> t = new Tuple<string, string>("实力派" + i, "18783764563");
                d.Members.Add(t);

            }

            for (int i = 0; i < 3; i++)
            {
                Tuple<string, string, string, string> t = new Tuple<string, string, string, string>("顺旺基" + i, "女", "22", "18783764563");
                d.Famliys.Add(t);

            }

            d.Result.Add("703");
            d.Result.Add("704");
            d.Result.Add("707");
            return d;
        }

        private List<string> _result = new List<string>();
        /// <summary> 示例： 701 702 705 </summary>
        public List<string> Result
        {
            get { return _result; }
            set { _result = value; }
        }

        private string _signYear;
        /// <summary> 年限 </summary>
        public string SignYear
        {
            get { return _signYear; }
            set { _signYear = value; }
        }

        private string _areaName;
        /// <summary> 说明 </summary>
        public string AreaName
        {
            get { return _areaName; }
            set { _areaName = value; }
        }
    }

    public static class DocumentExtention
    {
        public static string ToDL(this string str)
        {
            string format = "     {0}     ";
            return string.Format(format, str);
        }
    }

}
