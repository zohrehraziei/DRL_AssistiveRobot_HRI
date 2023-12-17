import matplotlib.pyplot as plt
import pandas as pd
import csv

cumulativeRewardFeedPPO = 0.0
cumulativeRewardFeedSAC = 0.0

maxRewardFeedPPO = 0.0
minRewardFeedPPO = 10000000.0

maxRewardFeedSAC = 0.0
minRewardFeedSAC = 10000000.0

idx = 0
                   
idx = 0
with open('Feed-PPO-1-9-21.csv', newline='\n') as csvfile:
        spamreader = csv.reader(csvfile, delimiter=',', quotechar='|')
        
        for row in spamreader:
                if idx > 0:
                        cumulativeRewardFeedPPO = float(row[2])
                        maxRewardFeedPPO = float(row[2]) / float(row[1])
                        minRewardFeedPPO = float(row[2]) / float(row[1])
                if idx > 1:
                        _temp1 = float(row[2]) - float(prevRow[2])
                        _temp2 = float(row[1]) - float(prevRow[1])
                        _temp = _temp1 / _temp2
                        if _temp > maxRewardFeedPPO:
                                maxRewardFeedPPO = _temp
                        if _temp < minRewardFeedPPO:
                                minRewardFeedPPO = _temp        

                prevRow = row
                idx = idx + 1
idx = 0
with open('Feed-SAC-1-9-21.csv', newline='\n') as csvfile:
        spamreader = csv.reader(csvfile, delimiter=',', quotechar='|')
        
        for row in spamreader:
                if idx > 0:
                        cumulativeRewardFeedSAC = float(row[2])
                        maxRewardFeedSAC = float(row[2]) / float(row[1])
                        minRewardFeedSAC = float(row[2]) / float(row[1])
                if idx > 1:
                        _temp1 = float(row[2]) - float(prevRow[2])
                        _temp2 = float(row[1]) - float(prevRow[1])
                        _temp = _temp1 / _temp2
                        if _temp > maxRewardFeedSAC:
                                maxRewardFeedSAC = _temp
                        if _temp < minRewardFeedSAC:
                                minRewardFeedSAC = _temp        

                prevRow = row
                idx = idx + 1


   
val1 = ['Mean', 'STD', 'Max Reward', 'MinReward'] 
val2 = ['SAC', 'PPO'] 
val3 = [[(maxRewardFeedPPO+minRewardFeedPPO)/2,0,maxRewardFeedPPO,minRewardFeedPPO], [(maxRewardFeedSAC+minRewardFeedSAC)/2,0,maxRewardFeedSAC,minRewardFeedSAC] ] 
   
fig, ax = plt.subplots() 
ax.set_axis_off() 
table = ax.table( 
    cellText = val3,  
    rowLabels = val2,  
    colLabels = val1, 
    rowColours =["palegreen"] * 10,  
    colColours =["palegreen"] * 10, 
    cellLoc ='center',  
    loc ='upper center')    
table.auto_set_font_size(False)     
table.set_fontsize(12)
ax.set_title('Feeding Task', fontsize = 30, fontweight ="bold") 
   
plt.show() 