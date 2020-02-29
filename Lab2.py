from bitstring import BitArray
from decimal import Decimal
import math

def multiply_bins(m, r, x, y): #Алгоритм Бута
    print(m," x ",r)
    length = x + y + 1
    m_arr = BitArray(int = m, length = length)
    A = m_arr<<(y+1) #Заповнити найбільш значимі (з ліва) біти значенням m. Біти (y + 1), які залишилися, заповнити нулями.
    print("A = ", A.bin)
    S = BitArray(int = -m, length = length) << (y+1) #Заповнити найбільш значимі біти значенням (−m) в додатковому коді. Біти (y + 1), які залишилися, заповнити нулями.
    print("S = ", S.bin)
    P = BitArray(int = r, length = y) #заповнити біти значенням r.
    P.prepend(BitArray(int = 0, length = x)) #Заповнити найбільш значимі x біт нулями.
    P = P << 1 #Записати 0 в крайній найменший значимий (правий) біт.
    print("P = ", P.bin)

    for _ in range(1, y + 1):
        if P[-2:] == '0b01': #if last two bits are 01, find P+A
            P = BitArray(int = P.int + A.int, length = length)
            print("P + A = ", P.bin)
        elif P[-2:] == '0b10': #if last two bits are 10, find P+S
            P = BitArray(int = P.int + S.int, length = length)
            print("P + S = ", P.bin)
        P = ar_shift(P, 1) #Виконати операцію арифметичного зсуву над значенням, яке було отримане на другому кроці, на один біт вправо. Присвоїти P це нове значення.
        print("P >> 1 = ", P.bin)
    P = ar_shift(P, 1) #Відкинути крайній найменш значимий (правий) біт P. Це і є добуток m і r.
    print("P >> 1 = ", P.bin)
    return P.int
def ar_shift(barr, shift_by): #arithmetic shift
    return BitArray(int = (barr.int >> shift_by), length = barr.len)

#-------------------------Division-----------------------------------
def divide(dividend, divisor):
    # dividend ÷ divisor = quotient
    #HI = Remainder
    #LO = Quoitent
    print(dividend," / ", divisor)
    LO = BitArray(int = dividend, length = 32)#LO = Dividend
    print("quoitent = ", LO.bin)
    HI = BitArray(int = 0, length = 32)#HI = 0
    print("remainder = ", HI.bin)
    for _ in range(32):
        req = HI.copy()
        req.append(LO)
        req <<= 1
        LO = req[32:]
        print("quoitent << 1 ",LO.bin)
        HI = req[:32]
        print("remainder << 1 ",HI.bin)
        diff = HI.int - divisor
        if diff >= 0:
            print("diff > 0")
            HI = BitArray(int = diff, length = 32)
            print("remainder = ",HI.bin)
            LO.set(True, -1)#set lsb of LO as 1
            print("quoitent = ", LO.bin)
    print("remainder = ", HI.int)
    print("quoitent = ", LO.int)
    print("res = ",LO.int,"+",HI.int,"/", divisor, " = ", LO.int+HI.int/divisor)

print("Booth's algorithm")
print(multiply_bins(7,2,5,4),'\n\r')
print("Sequental division")
divide(120,42)

