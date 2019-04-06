using System;
using System.IO;

namespace ZnymkyHub.DAL
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).ToString(), "ZnymkyHub.DAL", "SQLScripts", "Inserting_data_script.sql");
            Console.WriteLine(path);
        }
    }
}
