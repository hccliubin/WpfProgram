using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HeBianGu.Base.Util;
using System.Diagnostics;
using System.Threading;

namespace HeBianGu.Base.UtilUnitTester
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            ShortCutHookService.Instance.CreateMoniter();

            Action action = () =>
             {
                 Debug.WriteLine("D");
             };

            ShortCutHookService.Instance.RegisterCommand("D", action);


            Thread.Sleep(10000);
        }
    }
}
