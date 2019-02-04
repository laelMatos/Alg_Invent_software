
def delChar (a):
    a = a.replace(' ','')
    a = a.replace('+','')
    num = {'0':'0','1': '1', '2': '2', '3': '3', '4': '4', '5': '5', '6': '6', '7': '7', '8': '8', '9': '9', '.': '.',}
    new_a = ''
    for i in range(len(a)):
        new_a += str(num.get(a[i],''))
    return new_a


def clearDot (a):
   x = a.count('.')
   return a.replace('.', '',x-1)

def literInt (a):
    if a[0]=='-':
        x='-'
    else:
        x=''
    a = clearDot(a)
    a = x+delChar(a)
    a = float(a)
    return int(a)


string = input("Escreva um valor: ")
print(literInt(string))




