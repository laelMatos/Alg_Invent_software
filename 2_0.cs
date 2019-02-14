using System;
using System.Collections.Generic;

namespace atividade_2_0
{
    /// <summary> 
    /// Atividade avaliação invent software.
    /// implementação do padrão Interpreter.
    /// <remarks>
    /// Neste exemplo é utilizado a validação e seleção de notas para um caixa eletronico.
    /// fazendo com que saia a menor quantidade possivel de notas para o valor desejado.
    /// </remarks>
    /// </ summary> 
    class MainApp

    {
        static void Main()
        {
            int valor;

            try
            {
                do
                {
                    Console.Write("Digite o valor a ser sacado: ");
                    valor = int.Parse(Console.ReadLine());
                    //Validação para impedir que seja solicitado notas de 1 e valores acima de 2000
                    if ((valor % 10) != 3 && (valor % 10) != 1 && (valor <= 2000))
                    {
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine(
                                        "---------------------------------------------\n" +
                                        "O valor limete é de R$2.000,00\n" +
                                        "\n" +
                                        "valores terminados em 1 ou 3 são invalidos\n" +
                                        "Não existe nota de R$1,00\n" +
                                        "---------------------------------------------\n"
                                        );
                    }
                } while (true);
                Console.Clear();              
                //Cria um objeto da classe Dados com input recebendo o valor 
                Dados dados = new Dados(valor);        
                //Lista do tipo Expression
                List<Expression> lista = new List<Expression>();
                lista.Add(new Unidade());
                lista.Add(new Dezena());
                lista.Add(new Centena());
                //loop do tamanho da lista, para cada ciclo exp recebe um elemento Expression da lista.
                foreach (Expression exp in lista)
                {
                    exp.Interpret(dados);//Cada elemento da list recebe o objeto dados dentro da função Interpret
                }
                //----------Saida do resultado no console-------------
                string[] nomeCedulas = { "dois", "cinco", "dez", "vinte", "cinquenta", "cem" };
                Console.WriteLine("\n");
                for (int i = 0; i < dados.Result.Length; i++)
                {
                    if (dados.Result[i] > 0)
                    {
                        if (dados.Result[i] == 1)
                        {
                            Console.WriteLine("{0} nota de {1} reais", dados.Result[i], nomeCedulas[i]);
                        }
                        else
                        {
                            Console.WriteLine("{0} notas de {1} reais", dados.Result[i], nomeCedulas[i]);
                        }
                    }
                }
            }
            // Tratamento de erro
            catch (Exception ex)
            {
                Console.WriteLine("Erro: Valor invalido");
            }

            // Espera o usuário para fechar a janela
            Console.WriteLine("\n\n------------------------------------------\n" +
                             "Aperte qualquer botão para sair");
            Console.ReadKey();
        }
    }

    /// <summary> 
    /// A classe 'Dados'
    /// <remarks>
    /// utilizada para manipular a entrada e saida de dados sendo que 
    /// para esta aplicação a entrada é string e a saida é int.
    /// </remarks>
    /// </ summary>
    class Dados

    {
        private int _input;
        private int _Output;
        private int[] cedulas = new int[6]; //[0]"dois", [1]"cinco", [2]"dez", [3]"vinte", [4]"cinquenta", [5]"cem"

        // Construtor obrigando uma entrada ao criar um objeto da classe Context
        public Dados(int input)
        {
            this._input = input;
        }

        public int[] Result { get { return cedulas; } }

        // Obtém ou define a entrada
        public int Input
        {
            get { return _input; }
            set { _input = value; }
        }
        public int Output
        {
            get { return _Output; }
            set { _Output = value; }
        }

        //Fuções de entrada e saida da nota de 2
        public int Dois
        {
            get { return cedulas[0]; }
            set { cedulas[0] = value; }
        }

        //Fuções de entrada e saida da nota de 5
        public int Cinco
        {
            get { return cedulas[1]; }
            set { cedulas[1] = value; }
        }

        //Fuções de entrada e saida da nota de 10
        public int Dez
        {
            get { return cedulas[2]; }
            set { cedulas[2] = value; }
        }

        //Fuções de entrada e saida da nota de 20
        public int Vinte
        {
            get { return cedulas[3]; }
            set { cedulas[3] = value; }
        }

        //Fuções de entrada e saida da nota de 50
        public int Cinquenta
        {
            get { return cedulas[4]; }
            set { cedulas[4] = value; }
        }

        //Fuções de entrada e saida da nota de 100
        public int Cem
        {
            get { return cedulas[5]; }
            set { cedulas[5] = value; }
        }
    }


    /// <summary>
    /// A classe abstrata 'Expression'
    /// <remarks>
    /// Classe onde é definida todos os parametros e condições para manipular os valores da classe 'context'
    /// a mesma deve ser herdada por outra classe por ser uma classe abstract.
    /// </remarks>
    /// </summary>
    abstract class Expression
    {
        public void Interpret(Dados dados)
        {
            dados.Output = dados.Input % fator();
            dados.Input -= dados.Output;

            while (dados.Output > 0)
            {
                //verifica 5, 50, 100 - (Se o valor for 6 ele passa para o proximo e contabiliza em notas de 2)
                if (fator_1() > 0 && dados.Output >= fator_1() && dados.Output - (fator_1()) != 1)
                {
                    dados.Output += -(fator_1());
                    //contagem 5, 50 e 100
                    dados.Cinco += cedula_5();
                    dados.Cinquenta += cedula_50();
                    dados.Cem += cedula_100();
                }
                //verifica 2, 20 , 0
                else if (fator_2() > 0 && dados.Output >= fator_2() && dados.Output - (fator_2()) != 1)
                {
                    dados.Output += -(fator_2());
                    //contagem 2 e 20
                    dados.Dois += cedula_2();
                    dados.Vinte += cedula_20();
                }
                //verifica 0, 10, 0
                else if (fator_3() > 0 && dados.Output >= fator_3())
                {
                    dados.Output += -(fator_3());
                    //contagem 10
                    dados.Dez += 1;
                }
            }
            return;
        }

        /// <summary>
        /// Funções abstratas da classe Expression
        /// <remarks>
        /// Estas funções deveram ser sobreescrita (override) em outra classe, que herda a classe Expression
        /// </remarks>
        /// </summary>
        public abstract int cedula_2();
        public abstract int cedula_5();
        public abstract int cedula_10();
        public abstract int cedula_20();
        public abstract int cedula_50();
        public abstract int cedula_100();
        public abstract int fator();//Fator para selecionar casas decimais
        public abstract int fator_1();//Fator para verificação 5, 50, 100
        public abstract int fator_2();//Fator para verificação 2, 20, 0
        public abstract int fator_3();//Fator para verificação 0, 10, 0

    }
    /// <summary>
    /// Uma classe 'Unidade'
    /// <remarks>
    /// verificações para notas: 5 e 2
    /// Recebe a classe Expression com herança e sobrescreve as funções com override
    /// </remarks>
    /// </summary>
    class Unidade : Expression

    {
        public override int cedula_2() { return 1; }
        public override int cedula_5() { return 1; }
        public override int cedula_10() { return 0; }
        public override int cedula_20() { return 0; }
        public override int cedula_50() { return 0; }
        public override int cedula_100() { return 0; }
        public override int fator() { return 10; }
        public override int fator_1() { return 5; }
        public override int fator_2() { return 2; }
        public override int fator_3() { return 0; }
    }

    /// <summary>
    /// Uma classe 'Dezena'
    /// <remarks>
    /// verificações para notas: 50, 20 e 10
    /// Recebe a classe Expression com herança e sobrescreve as funções com override
    /// </remarks>
    /// </summary>
    class Dezena : Expression

    {
        public override int cedula_2() { return 0; }
        public override int cedula_5() { return 0; }
        public override int cedula_10() { return 1; }
        public override int cedula_20() { return 1; }
        public override int cedula_50() { return 1; }
        public override int cedula_100() { return 0; }
        public override int fator() { return 100; }
        public override int fator_1() { return 50; }
        public override int fator_2() { return 20; }
        public override int fator_3() { return 10; }
    }

    /// <summary>
    /// Uma classe 'Centena'
    /// <remarks>
    /// verificações para notas de 100
    /// Recebe a classe Expression com herança e sobrescreve as funções com override
    /// </remarks>
    /// </summary>
    class Centena : Expression

    {
        public override int cedula_2() { return 0; }
        public override int cedula_5() { return 0; }
        public override int cedula_10() { return 0; }
        public override int cedula_20() { return 0; }
        public override int cedula_50() { return 0; }
        public override int cedula_100() { return 1; }
        public override int fator() { return 10000; }
        public override int fator_1() { return 100; }
        public override int fator_2() { return 0; }
        public override int fator_3() { return 0; }
    }


}
