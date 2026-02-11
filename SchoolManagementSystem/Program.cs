using System.Diagnostics;
using System.Drawing.Imaging;
using System.Xml.Linq;

namespace SchoolManagementSystem
{
    internal static partial class Program
    {
        

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        /// 
        [STAThread]
        
        static void Main()
        {

            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new mainForm());


        }




    }
}