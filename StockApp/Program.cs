
using StockApp.DataAccess; 

namespace StockApp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        { 
            // D�marrer l'application WinForms
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());


        }
    }
}