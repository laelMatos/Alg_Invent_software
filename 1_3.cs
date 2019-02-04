using System;

namespace Atividade1_3
{
    class 1_3
    {
        static void Main(string[] args)
        {
            while (true){
                try{
                    Console.Write("Digite um valor inteiro valido: ");
                    int a = int.Parse(Console.ReadLine());
                    Console.WriteLine(a);
                    break;
                }
                catch (OverflowException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            Console.ReadKey(true);
        }
    }
}
