
#\/\/\/\/\/\/--retorna o elemento que fecha a prioridade.
def priorOpCl(a):
    swit = {
        "(": ")",
        "{": "}",
        "[": "]"}
    return swit.get(a,'')#retorna o valor do dicionário ou valor vazio.

#\/\/\/\/\/\/--limpa todos os elementos que não defina prioridade.
def cleanChar (ch):
    sw = {'(':'(',
          ')':')',
          '{':'{',
          '}':'}',
          '[':'[',
          ']':']'}
    return sw.get(ch,'')#retorna o valor do dicionário ou valor vazio.

#\/\/\/\/\/\/--Faz a varredura na string sem números e executa as funções necessarias para validação.
def sweepCharCle (x):
    if len(x)/2 > 0.5: #Verifica se a quantidade de elementos é par.
        indice = ['false']*len(x)#Cria vetor do tamanho da string com valor boolean false.
        for i in range(int((len(x)))):
            if indice[i] == 'false':
                for j in range(i+1,len(x),2):
                    if priorOpCl(x[i])== x[j]:
                        indice[i] = 'true'
                        indice[j] = 'true'
        for k in range(len(indice)):#Verifica se tem falha registrada no indice.
            if indice[k] != "true":
                return "Fechamento invalido!"
        return "Ok"
    else:
        return "Fechamento invalido!"

#\/\/\/\/\/\/--Faz a varredura na string com números e executa as funções necessarias para validação.
def sweepChar (c):
    if c.count("()") > 0 or \
            c.count(")(") > 0 or \
            c.count("[]") > 0 or \
            c.count("][") > 0 or \
            c.count("{}") > 0 or \
            c.count("}{") > 0:
        return "Fechamento invalido!"
    x=''
    for j in range(len(c)):
        x += cleanChar(c[j])
    return sweepCharCle(x)


string ="(2+{[2+5]*2})/3+(57/2)" # input("Escreva sua operação para validar se as prioridades estão corretas:")

print("1º Teste:",sweepChar(string)) #***Verificação com numeros e operadores
print("2º Teste:",sweepCharCle('([)]')) #***Verificação sem numeros
