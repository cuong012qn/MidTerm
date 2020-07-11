using System;

namespace MidTerm
{
    class Program
    {
        static void Main(string[] args)
        {
            HonSo honso1 = new HonSo(20, 90, 50);
            HonSo honso2 = new HonSo(12, 23, 30);
            Console.WriteLine(honso1.ConvertToString());
            Console.WriteLine(honso2.ConvertToString());
            Console.WriteLine("{0} + {1} = {2}", honso1.ConvertToString(), honso2.ConvertToString(),
                honso1.Cong(honso2).ConvertToString());
            Console.WriteLine("{0} - {1} = {2}", honso1.ConvertToString(), honso2.ConvertToString(),
                honso1.Tru(honso2).ConvertToString());
            Console.WriteLine("Gia tri so thuc " + honso1.GiaTri());
            Console.WriteLine("Nghich dao " + honso1.NghichDao());
            Console.ReadKey();
        }

    }
}
