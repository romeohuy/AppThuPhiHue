using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;

namespace AppThuPhiHue
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var serviceProvider = new ServiceCollection()
                .AddTransient<QueryFactoryCustom>()
                .BuildServiceProvider();
            serviceProvider.GetService<QueryFactoryCustom>();
            Application.Run(new FormMain());
        }
    }
}
