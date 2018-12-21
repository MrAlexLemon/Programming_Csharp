using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Lab5
{
    class Program
    {
        public static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + System.IO.Directory.GetCurrentDirectory() + "\\Aircompany.mdf;Integrated Security=True;Connect Timeout=30";
        static void Main(string[] args)
        {
            //Insertdata insd = new Insertdata();
            //insd.insdat("Airline");
            AdapterSet ads = new AdapterSet();
            ads.selAdap();
            Updated upd = new Updated();
            upd.Upd(1,"UpdatedName");
            Insertdata ins = new Insertdata();
            ins.insdat(6, "Berlin", "West Europe");
            //upd.test();
            Deleted del = new Deleted();
            del.deld(1);
            Console.Read();
        } 
    }
}
