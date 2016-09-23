using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Itcastcater
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //FrmLogin f1 = new FrmLogin();
            //if (f1.ShowDialog() == DialogResult.OK)
            //{ 
            //    Application.Run(new FrmMain());
            //}
            Application.Run(new FrmMain());
        }
    }
}
