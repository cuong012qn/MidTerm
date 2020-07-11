using System;

namespace MidTerm
{
    class Program
    {
        static void Main(string[] args)
        {
            var honso1 = new HonSo(20, 90, 50);
            var honso2 = new HonSo(12, 23, 30);
            Console.WriteLine(honso1.Tru(honso2).ConvertToString());
            Console.ReadKey();
        }

    }
}
