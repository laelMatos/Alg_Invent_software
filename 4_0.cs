using System;

namespace Ativid_4
{
    class 4_0
    {
        public static double Average(int a, int b)
        {
            return (a + b) / 2;//Tem que colocar entre parentese a soma antes de dividir para se obter a média.
        }
        static void Main(string[] args)
        {
            Console.WriteLine(Average(2, 1));

            Console.ReadKey(true);
        }
    }
}
