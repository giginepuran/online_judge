#import time
def PickiSti(StiSet, Side, Sti_No, totSti, l):
    if l == Side:
        return [True, []]
    if Sti_No == totSti or l > Side:
        return [False, []]
    for i in range(Sti_No, totSti):
        [suc, StiTake] = PickiSti(StiSet, Side, i + 1, totSti, l + StiSet[Sti_No])
        if suc:
            StiTake.append(StiSet[Sti_No])
            return [True, StiTake]
    return [False, []]

def DistriSti(StiSet, totSti, Side):
    L = totSti - 1
    for i in range(L):
        fix = [StiSet.pop(i)]
        for j in range(i, L):
            fix.append(StiSet.pop(j))
            [suc, Take] = PickiSti(StiSet, Side, 0, totSti - 2, 0)
            if suc:
                Temp_Set = StiSet.copy()
                for taken in Take:
                    Temp_Set.remove(taken)
                [suc, Take] = PickiSti(Temp_Set, Side, 0, len(Temp_Set), 0)
                if suc:
                    for taken in Take:
                        Temp_Set.remove(taken)
                    Temp_Set += fix
                    [suc, Take] = PickiSti(Temp_Set, Side, 0, len(Temp_Set), 0)
                    if suc:
                        return True
            StiSet.insert(j, fix.pop(-1))
        StiSet.insert(i, fix.pop(-1))
    return False

#13 33 47 9 1 14 14 29 11 8 4 29 3 6
n = int(input())
#t = time.time()
while(n):
    n -= 1
    StiSet = input()
    StiSet = [*map(int, StiSet.split())]
    totSti = StiSet[0]
    StiSet.remove(totSti)
    Side = sum(StiSet)
    if (Side % 4 != 0):
        print("no")
        continue
    StiSet.sort()
    Side /= 4
    if (StiSet[-1] > Side):
        print("no")
        continue
    StiSet.sort()
    StiSet.reverse()
    if DistriSti(StiSet, totSti, Side):
        print("yes")
    else:
        print("no")
#elapsed = time.time() - t
#print(elapsed)
