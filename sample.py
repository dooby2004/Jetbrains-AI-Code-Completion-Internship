import os
import random
import pickle as pkl

directory = 'Data'

for filename in os.listdir(directory):
    f = open(directory + '/' + filename)
    lines = f.readlines()
    n = len(lines)
    for i in range(5):
        g = open('Samples/' + filename + '-' + str(i) + '.pkl', 'wb')
        flag = True
        while flag:
            l = random.randint(0,n-1)
            line = lines[l]
            p = random.randint(1, len(line))
            if ('//' not in line) and ('#' not in line) and (not line[:p].isspace()) and (len(line)-p+1 >= 10):
                pre = ''.join(lines[:l]) + line[:p]
                mid = line[p:-1]
                suf = '\n' + ''.join(lines[l+1:])
                pkl.dump([pre,mid,suf], g)
                flag = False
        g.close()
    f.close()
