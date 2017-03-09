using SQLiter.TestEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLiter
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());


            NorthwindContext context = new NorthwindContext();
            var empList = context.Employees.OrderBy(c => c.FirstName).ToList();

            Console.WriteLine(empList.Count);
            Console.ReadLine();
        }
    }
}
