using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;


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
        static int ValidaEntrada()
        {
            int x;
            do
            {
                Console.Write("Digite o valor a ser sacado: ");
                x = int.Parse(Console.ReadLine());
                //Validação para impedir que seja solicitado notas de 1 e valores acima de 2000
                if (x > 2000)
                {
                    Console.Clear();
                    Console.WriteLine(
                                    "---------------------------------------------\n" +
                                    "O valor limete é de R$2.000,00\n" +
                                    "---------------------------------------------\n");
                }
                else if ((x % 10) == 3 || (x % 10) == 1)
                {
                    Console.Clear();
                    Console.WriteLine(
                                    "---------------------------------------------\n" +
                                    "valores terminados em 1 ou 3 são invalidos\n" +
                                    "Não existe nota de R$1,00\n" +
                                    "---------------------------------------------\n");
                }
                else
                {
                    break;
                }
            } while (true);
            Console.Clear();
            return x;
        }

        static void ResultConsole(int[] Result,int valor)
        {
            string[] nomeCedulas = { "dois", "cinco", "dez", "vinte", "cinquenta", "cem" };
            int[] cedVal = { 2, 5, 10, 20, 50, 100 };

            Console.WriteLine("\n");
            int verifVal=0;
            //Verifica se valor solicitado é o mesmo de saida.
            for (int i = 0; i < 6; i++) { verifVal += Result[i] * cedVal[i]; }
            if (verifVal == valor)
            {

                for (int i = 0; i < Result.Length; i++)
                {
                    if (Result[i] > 0)
                    {
                        if (Result[i] == 1)
                        {
                            Console.WriteLine("{0} nota de {1} reais", Result[i], nomeCedulas[i]);
                        }
                        else
                        {
                            Console.WriteLine("{0} notas de {1} reais", Result[i], nomeCedulas[i]);
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Valor de saida divergente com o de entrada!");
            }
            // Espera o usuário para fechar a janela
            Console.WriteLine("\n\n------------------------------------------\n" +
                             "Aperte qualquer botão para sair");
            Console.ReadKey();
        }
        //------------------------Inicia o programa-----------------------
        static void Main()
        {
            try
            {
                int valor = ValidaEntrada();
                //Cria um objeto da classe Dados com input recebendo valor 
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
                //Teste do bloqueio de set, evitando inserir um valor diferente no Result
                dados.Result[0] = 500;//---R$2,00----
                dados.Result[1] = 500;//---R$5,00----
                dados.Result[2] = 500;//--R$10,00----
                dados.Result[3] = 500;//--R$20,00----
                dados.Result[4] = 500;//--R$50,00----
                dados.Result[5] = 500;//-R$100,00----
                //Console.WriteLine(dados.Result[5]);
                //----------Saida do resultado no console-------------
                ResultConsole(dados.Result(), valor);
                //----------------------------------------------------
            }
            //=================Tratamento de erro=====================
            catch (Exception ex)
            {
                Console.WriteLine("Erro: {0}", ex.Message);
                Console.ReadKey();
            }
        }
    }

    /// <summary> 
    /// A classe 'Dados'
    /// <remarks>
    /// utilizada para manipular a entrada e saida de dados.
    /// </remarks>
    /// </ summary>
    class Dados

    {
        private int _input;
        private int _Output;
        private readonly int[] cedulas = new int[6]; //[0]"dois", [1]"cinco", [2]"dez", [3]"vinte", [4]"cinquenta", [5]"cem"
        
        // Construtor obrigando uma entrada ao criar um objeto da classe Context
        public Dados(int input)
        {
            this._input = input;
        }
        
        //Metodo privada para transferencia de valor
        private int[] Troca() { int[] Retorno = new int[6];
            for (int i = 0; i < 6; i++) { Retorno[i] = cedulas[i]; }
            return Retorno;
        }

        //Função para retono apenas leitura.
        public int[] Result { protected set { }
            get { return this.Troca(); } }
        
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
        public bool Dois { set { if (value) { this.cedulas[0] += 1; } } }

        //Fuções de entrada e saida da nota de 5
        public bool Cinco { set { if (value) { this.cedulas[1] += 1; } } }

        //Fuções de entrada e saida da nota de 10
        public bool Dez { set { if (value) { this.cedulas[2] += 1; } } }

        //Fuções de entrada e saida da nota de 20
        public bool Vinte { set { if (value) { this.cedulas[3] += 1; } } }

        //Fuções de entrada e saida da nota de 50
        public bool Cinquenta { set { if (value) { this.cedulas[4] += 1; } } }

        //Fuções de entrada e saida da nota de 100
        public bool Cem { set { if (value) { this.cedulas[5] += 1; } } }
    }


    /// <summary>
    /// A classe abstrata 'Expression'
    /// <remarks>
    /// Classe onde é definida todos os parametros e condições para manipular os valores da classe 'Dados'
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
                if (fator_1() > 0 && dados.Output >= fator_1() && dados.Output - (fator_1()) != 1
                    && dados.Output - (fator_1()) != 3)
                {
                    dados.Output += -(fator_1());
                    //contagem 5, 50 e 100
                    dados.Cinco = cedula_5();
                    dados.Cinquenta = cedula_50();
                    dados.Cem = cedula_100();
                }
                //verifica 2, 20 , 0
                else if (fator_2() > 0 && dados.Output >= fator_2() && dados.Output - (fator_2()) != 1)
                {
                    dados.Output += -(fator_2());
                    //contagem 2 e 20
                    dados.Dois = cedula_2();
                    dados.Vinte = cedula_20();
                }
                //verifica 0, 10, 0
                else if (fator_3() > 0 && dados.Output >= fator_3())
                {
                    dados.Output += -(fator_3());
                    //contagem 10
                    dados.Dez = cedula_10();
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
        public abstract bool cedula_2();
        public abstract bool cedula_5();
        public abstract bool cedula_10();
        public abstract bool cedula_20();
        public abstract bool cedula_50();
        public abstract bool cedula_100();
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
        public override bool cedula_2() { return true; }
        public override bool cedula_5() { return true; }
        public override bool cedula_10() { return false; }
        public override bool cedula_20() { return false; }
        public override bool cedula_50() { return false; }
        public override bool cedula_100() { return false; }
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
        public override bool cedula_2() { return false; }
        public override bool cedula_5() { return false; }
        public override bool cedula_10() { return true; }
        public override bool cedula_20() { return true; }
        public override bool cedula_50() { return true; }
        public override bool cedula_100() { return false; }
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
        public override bool cedula_2() { return false; }
        public override bool cedula_5() { return false; }
        public override bool cedula_10() { return false; }
        public override bool cedula_20() { return false; }
        public override bool cedula_50() { return false; }
        public override bool cedula_100() { return true; }
        public override int fator() { return 10000; }
        public override int fator_1() { return 100; }
        public override int fator_2() { return 0; }
        public override int fator_3() { return 0; }
    }


}
