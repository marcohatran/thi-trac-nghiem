using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThiTracNghiem.PL;

namespace ThiTracNghiem
{
    static class Program
    {
        public static FrmMain frmChinh;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            frmChinh = new FrmMain();
            Application.Run(frmChinh);
        }
    }
}
