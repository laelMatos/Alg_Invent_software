'''''''''
No python não existe limitação para o tamanho das variaveis, 
não é necessario tratar um possivel estouro como seria necessario em: 
C#, Java, C++, dentre outras.
'''''''''
while 'true':
    try:
        a = int(input("Escreva um numero inteiro qualquer: "))
        print(a)
        break
    except ValueError:
        print("Digite apenas valores validos!")
