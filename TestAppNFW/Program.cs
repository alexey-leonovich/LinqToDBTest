using ClassLibrary;
using System;

namespace TestAppNFW
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var val = Class1.TestDatabaseEdit();
            Console.WriteLine($"TestDatabaseEdit - OK! Val = {val}");
            Class1.TestConnectionFTS5Init();
            Console.WriteLine("TestConnectionFTS5Init - OK!");
            Console.ReadKey();
        }
    }
}
