using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main
{
    static class Program
    {
        // Contest with the current flow of the main window.
        internal static ApplicationContext s_context { get; set; }

        /// <summary>
        /// Entry point.
        /// </summary>
        [STAThreadAttribute]
        public static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 newForm = new(true);
            MyForms.Add(newForm);
            s_context = new ApplicationContext(newForm);
            Application.Run(s_context);
        }
    }
}
