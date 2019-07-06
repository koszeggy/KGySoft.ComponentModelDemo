using System;
using System.Threading;
using System.Windows.Forms;
using KGySoft.ComponentModelDemo.ViewModel;
using KGySoft.ComponentModelDemo.ViewWinForms;
using KGySoft.ComponentModelDemo.ViewWpf;

using WinFormsApp = System.Windows.Forms.Application;
using WpfApp = KGySoft.ComponentModelDemo.ViewWpf.App;

namespace KGySoft.ComponentModelDemo
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            //LaunchStaThread(StartWpf);
            LaunchStaThread(StartWinForms);
        }

        private static void LaunchStaThread(ThreadStart start)
        {
            var thread = new Thread(start);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private static void StartWpf()
        {
            var application = new WpfApp();
            application.InitializeComponent();
            application.Run(new BindingTestWindow(new BindingViewModel()));
        }

        private static void StartWinForms()
        {
            WinFormsApp.EnableVisualStyles();
            WinFormsApp.SetCompatibleTextRenderingDefault(false);
            WinFormsApp.Run(new BindingTestForm(new BindingViewModel()));
        }
    }
}
