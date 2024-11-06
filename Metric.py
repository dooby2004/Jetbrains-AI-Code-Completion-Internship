import pickle as pkl
import os
from torchmetrics.text import CHRFScore
chrf = CHRFScore()

f = open('predictions.pkl', 'rb')
data = pkl.load(f)
f.close()
pred = data[0]
ref = data[1]
refLists = []
for r in ref:
    refLists.append([r])

metSum = 0
for i in range(len(pred)):
    if pred[i-1] == ref[i-1]:
        metSum += 1
print("Exact Match: " + str(metSum/25))

print("CHRF: " + str(chrf(pred, refLists).item()))


metSum = 0
for i in range(len(pred)):
    if len(pred[i-1]) != 0: # If 0, then it does not match at all, assuming we are looking for something to be filled.
        metSum += (len(os.path.commonprefix([pred[i-1], ref[i-1]]))/min(len(pred[i-1]), len(ref[i-1])))
print("Prefix Min. Match: " + str(metSum/25))


metSum = 0
for i in range(len(pred)):
    metSum += (len(os.path.commonprefix([pred[i-1], ref[i-1]]))/len(ref[i-1]))
print("Prefix Actual Match: " + str(metSum/25))


metSum = 0
for i in range(len(pred)):
    j=0
    found = False
    while j<len(ref[i-1]) and found == False:
        l = len(ref[i-1])-j
        k=0
        while k<=j and found == False:
            if ref[i-1][k:k+l] in pred[i-1]:
                metSum += l/len(ref[i-1])
                found = True
            k += 1
        j += 1
print("Substring Search: " + str(metSum/25))


metSum = 0
for i in range(len(pred)):
    j=0
    found = False
    while j<len(ref[i-1]) and found == False:
        l = len(ref[i-1])-j
        k=0
        while k<=j and found == False and l>=5: # Assuming the filled in text should be 5 or more characters (as is in the data provided)
            if ref[i-1][k:k+l] in pred[i-1]:
                metSum += l/len(ref[i-1])
                found = True
            k += 1
        j += 1
print("Min. 5 Substring Search: " + str(metSum/25))
