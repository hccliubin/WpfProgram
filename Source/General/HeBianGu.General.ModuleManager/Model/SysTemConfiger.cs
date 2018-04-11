#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/11/28 9:57:50 
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

namespace HeBianGu.General.ModuleManager
{
   public class SysTemConfiger
    {
        public static List<string> ExceptShowFile = new List<string> { ".ini" };

        public const string ConfigerFolder = "Configer";

        public const string ConfigerMyFiles = "myDoc.configer";

        public const string ConfigerNotePadFile = "notepad.dat";

        public const string ConfigerClipBoardFile = "clipboard.txt";

        public const string ConfigerSaveboardFile = "saveboard.txt";

        public const string ConfigerTempFile = "tempFile.txt";

        public const string ConfigerExtend = "Extend";

        public const string ConfigerSolutions = "Solutions";

        public const string ConfigerMulBat = "MulBat";

        public const string ConfigerBakFiles = "BakFiles";

        public const string ConfigerCut = "最小的截图软件.exe";

    }
}
