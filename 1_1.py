piramide = [4],[3,4],[6,5,7],[4,1,8,3]

def sumMinPiram (a):
    add = 0
    for i in range(len(a)): #Repetição do tamanho da lista
        add += min(a[i]) #Soma o menor valor de cada posição da piramide.
    return add

print(sumMinPiram(piramide))
