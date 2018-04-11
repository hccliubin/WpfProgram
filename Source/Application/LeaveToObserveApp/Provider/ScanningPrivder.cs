#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2018/4/2 17:34:39 
 * 文件名：ScanningPrivder 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HeBianGu.Base.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeaveToObserveApp.Provider
{
    class ScanningPrivder
    {
        public static ScanningPrivder Instance = new ScanningPrivder();

        public void StartEngine()
        {
            HookKeyboardEngine.KeyDown += HookKeyboardEngine_KeyDown;
        }

        public void StopEngine()
        {
            HookKeyboardEngine.KeyDown -= HookKeyboardEngine_KeyDown;
        }
        private StringBuilder inputKey = new StringBuilder();

        DateTime previewTime;

        private void HookKeyboardEngine_KeyDown(object sender, KeyEventArgs e)
        {
            string temp = string.Empty;

            DateTime nowTime = DateTime.Now;

            //判断是不是数字的键盘值
            if ((e.KeyData >= Keys.D0 && e.KeyData <= Keys.D9) || (e.KeyData >= Keys.NumPad0 && e.KeyData <= Keys.NumPad9))
            {
                temp = (e.KeyData.ToString()).Last().ToString();
            }
            else
            {
                temp = e.KeyData.ToString();
            }
            //通过判断键盘输入的间隔来确定是扫描枪还是通过键盘输入的
            if ((nowTime - previewTime).Milliseconds < 50)
            {
                if (this.IsMatch(e.KeyValue, inputKey))
                {
                    //this.tb_Key.Text = inputKey.ToString();
                    if (CallBackScanning != null)
                    {
                        CallBackScanning(inputKey.ToString());
                    }
                    inputKey = new StringBuilder();
                    previewTime = DateTime.Now;
                    return;
                }

                inputKey.Append(temp);

            }
            else
            {
                inputKey = new StringBuilder(temp);
            }
            previewTime = DateTime.Now;
        }

        public event Action<string> CallBackScanning;


        public bool IsMatch(int lastKeyValue, StringBuilder inputKey)
        {
            return lastKeyValue == (int)Keys.Return && inputKey.Length > 14;
        }
    }



}
