using System.Threading;
using BindingTest.ViewModel;
using BindingTest.ViewWinForms;
using BindingTest.ViewWpf;

using WinFormsApp = System.Windows.Forms.Application;
using WpfApp = BindingTest.ViewWpf.App;

namespace BindingTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            LaunchStaThread(StartWpf);
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
