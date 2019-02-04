using System;
using System.Reflection;
using System.CodeDom.Compiler;
using Microsoft.CSharp;

namespace ConsoleApp2
{
    class Program
    {
        /*Este programa é para verificar a validade da operação e passar o resultado
         * como Foi solicitado para verificar caso receba apenas os operadoes de prioridade:
         * (){}[], então serve apenas para ser usado como comparação, a versão .Py executa a
         * verificação dos operadores.
         */
        static void Main(string[] args)
        {
            /******************O mesmo Programa em Python*********
             
            a = "36 + 2 * {25 + [ 18 - (5 - 2) * 3]}"
            a = a.replace("{","(").replace("}",")").replace("[","(").replace("]",")")
            try:
                print("{0} = {1}".format(string,eval(a)))
            except Exception:
                print("A operação {} é invalida!".format(string))              

             ****************************************************/

            String expressao = "36 + 2 * {25 + [ 18 - (5 - 2) * 3]}";
            //Redefinição de prioridade
            expressao = expressao.Replace('{', '(').Replace('}', ')').Replace('[', '(').Replace(']', ')');
            //String com o codigo a ser executado
            String classeMetodo = String.Format("public static class Func{{ public static int func(){{ return {0};}}}}", expressao);
            Console.WriteLine(classeMetodo);
            //Parametros do compilador
            CompilerParameters param = new CompilerParameters();
            param.GenerateExecutable = false;
            param.GenerateInMemory = true;
            param.IncludeDebugInformation = false;
            //Provedor C#
            CodeDomProvider provider = CSharpCodeProvider.CreateProvider("CSharp");
            //Compilar código
            CompilerResults preAssembly = provider.CompileAssemblyFromSource(param, classeMetodo);
            //Verificação
            if (preAssembly.Errors.HasErrors)
            {
                Console.WriteLine("Erro na expressão!");
            }
            else
            {
                Assembly assembly = preAssembly.CompiledAssembly;
                var resultado = assembly.GetType("Func").GetMethod("func").Invoke(null, null);

                Console.WriteLine(String.Format("{0} = {1}", expressao, resultado));
            }
            Console.ReadLine();
        }
    }
}
