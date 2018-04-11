#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/11/30 15:34:30 
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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Base.Util
{
    /// <summary> 说明 </summary>
    partial class WinAPIServer
    {
        private bool DoExitWin(int DoFlag)
        {
            bool ok;
            WindowsAPI.TokPriv1Luid tp;
            IntPtr hproc = WindowsAPI.GetCurrentProcess();
            IntPtr htok = IntPtr.Zero;
            ok = WindowsAPI.OpenProcessToken(hproc, WindowsAPI.TOKEN_ADJUST_PRIVILEGES | WindowsAPI.TOKEN_QUERY, ref htok);
            tp.Count = 1;
            tp.Luid = 0;
            tp.Attr = WindowsAPI.SE_PRIVILEGE_ENABLED;
            ok = WindowsAPI.LookupPrivilegeValue(null, WindowsAPI.SE_SHUTDOWN_NAME, ref tp.Luid);
            ok = WindowsAPI.AdjustTokenPrivileges(htok, false, ref tp, 0, IntPtr.Zero, IntPtr.Zero);
            ok = WindowsAPI.ExitWindowsEx(DoFlag, 0);
            return ok;
        }

        /// <summary>  锁定计算机 </summary>
        public void Lock()
        {
            WindowsAPI.LockWorkStation();
        }

        /// <summary>  重新启动  </summary>
        public bool Reboot()
        {
            return DoExitWin(WindowsAPI.EWX_FORCE | WindowsAPI.EWX_REBOOT);
        }

        /// <summary> 关机 </summary>
        public bool PowerOff()
        {
            return DoExitWin(WindowsAPI.EWX_FORCE | WindowsAPI.EWX_POWEROFF);
        }
        /// <summary>  注销  </summary>
        public bool LogOff()
        {
            return DoExitWin(WindowsAPI.EWX_FORCE | WindowsAPI.EWX_LOGOFF);
        }
    }


    /// <summary> 此类的说明 </summary>
    public partial class WinAPIServer
    {
        #region - Start 单例模式 -

        /// <summary> 单例模式 </summary>
        private static WinAPIServer t = null;

        /// <summary> 多线程锁 </summary>
        private static object localLock = new object();

        /// <summary> 创建指定对象的单例实例 </summary>
        public static WinAPIServer Instance
        {
            get
            {
                if (t == null)
                {
                    lock (localLock)
                    {
                        if (t == null)
                            return t = new WinAPIServer();
                    }
                }
                return t;
            }
        }
        /// <summary> 禁止外部实例 </summary>
        private WinAPIServer()
        {

        }
        #endregion - 单例模式 End -

    }

    /// <summary> 计算机控制 </summary>
    internal class WindowsAPI
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct TokPriv1Luid
        {
            public int Count;
            public long Luid;
            public int Attr;
        }
        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GetCurrentProcess();

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool OpenProcessToken(IntPtr h, int acc, ref IntPtr phtok);

        [DllImport("advapi32.dll", SetLastError = true)]
        internal static extern bool LookupPrivilegeValue(string host, string name, ref long pluid);

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall,
            ref TokPriv1Luid newst, int len, IntPtr prev, IntPtr relen);

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool ExitWindowsEx(int DoFlag, int rea);
        [DllImport("user32.dll")]
        public static extern void LockWorkStation();

        internal const int SE_PRIVILEGE_ENABLED = 0x00000002;
        internal const int TOKEN_QUERY = 0x00000008;
        internal const int TOKEN_ADJUST_PRIVILEGES = 0x00000020;
        internal const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";
        internal const int EWX_LOGOFF = 0x00000000;
        internal const int EWX_SHUTDOWN = 0x00000001;
        internal const int EWX_REBOOT = 0x00000002;
        internal const int EWX_FORCE = 0x00000004;
        internal const int EWX_POWEROFF = 0x00000008;
        internal const int EWX_FORCEIFHUNG = 0x00000010;

    }
}
