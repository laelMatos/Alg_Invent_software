using System;


namespace atividade5_0
{
    //<sumary>
    //5.1 - Código abaixo.
    //5.2 - 10927,27
    //5.3 - 9,2727 %
    //5.4 - Código abaixo.
    //</sumary>
    
    class FuncJur
    {
        //-5.1----Função para calcular o valor final a ser pago com o juros composto.
        public decimal obtenhaMontante(decimal capInicial, decimal txJuros, decimal prazo)
        {
            double pv, i, n;
            //<sumary>
            //A função Pow da Classe Math trabalho com double, por isso temo que converter as entradas em double.
            //</sumary>
            pv = double.Parse(capInicial.ToString());
            i = double.Parse(txJuros.ToString());
            n = double.Parse(prazo.ToString());
            //<sumary>
            //O retorno da função é definida como decimal por este motivo antes de retornar o valor o mesmo é convertido em decimal.
            //</sumary>
            return decimal.Parse((pv * Math.Pow(1 + (i / 100), n)).ToString());//Retrona o valor de (FV=Future Value)
        }

        //-5.3----Função para calcular juros final.
        public decimal ObtenhaJuroTotal(decimal montante, decimal capitalInicial)
        {
            return (montante * 100 / capitalInicial) - 100;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            FuncJur x = new FuncJur();

            decimal pv, i, n, fv;          
            pv = 10000; //Capital inicial
            i = 3; //Percentagem
            n = 3; //Meses

            try
            {
                fv = x.obtenhaMontante(pv, i, n);
                Console.WriteLine("Valor final a ser pago é {0} com juros total de {1}%",fv, (x.ObtenhaJuroTotal(fv, pv)));
                //"Valor final a ser pago é 10927,27 com juros total de 9,2727%"
            }
            catch (Exception a)
            {
                Console.WriteLine("Erro: {0}",a.Message);
            }            
            Console.ReadKey(true);
        }
    }
}
