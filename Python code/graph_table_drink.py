import matplotlib.pyplot as plt
import pandas as pd
import csv


cumulativeRewardDrinkingPPO = 0.0
cumulativeRewardDrinkingSAC = 0.0

maxRewardDrinkPPO = 0.0
minRewardDrinkPPO = 10000000.0

maxRewardDrinkSAC = 0.0
minRewardDrinkSAC = 10000000.0

idx = 0
with open('Drink-PPO-1-9-21.csv', newline='\n') as csvfile:
        spamreader = csv.reader(csvfile, delimiter=',', quotechar='|')
        
        for row in spamreader:
                if idx > 0:
                        cumulativeRewardDrinkingPPO = float(row[2])
                        maxRewardDrinkPPO = float(row[2]) / float(row[1])
                        minRewardDrinkPPO = float(row[2]) / float(row[1])
                if idx > 1:
                        _temp1 = float(row[2]) - float(prevRow[2])
                        _temp2 = float(row[1]) - float(prevRow[1])
                        _temp = _temp1 / _temp2
                        if _temp > maxRewardDrinkPPO:
                                maxRewardDrinkPPO = _temp
                        if _temp < minRewardDrinkPPO:
                                minRewardDrinkPPO = _temp        

                prevRow = row
                idx = idx + 1
idx = 0
with open('Drink-SAC-1-9-21.csv', newline='\n') as csvfile:
        spamreader = csv.reader(csvfile, delimiter=',', quotechar='|')
        
        for row in spamreader:
                if idx > 0:
                        cumulativeRewardDrinkingSAC = float(row[2])
                        maxRewardDrinkSAC = float(row[2]) / float(row[1])
                        minRewardDrinkSAC = float(row[2]) / float(row[1])
                if idx > 1:
                        _temp1 = float(row[2]) - float(prevRow[2])
                        _temp2 = float(row[1]) - float(prevRow[1])
                        _temp = _temp1 / _temp2
                        if _temp > maxRewardDrinkSAC:
                                maxRewardDrinkSAC = _temp
                        if _temp < minRewardDrinkSAC:
                                minRewardDrinkSAC = _temp        

                prevRow = row
                idx = idx + 1
                               

val1 = ['Mean', 'STD', 'Max Reward', 'MinReward'] 
val2 = ['PPO', 'SAC'] 
val3 = [[(maxRewardDrinkPPO+minRewardDrinkPPO)/2,0,maxRewardDrinkPPO,minRewardDrinkPPO], [(maxRewardDrinkSAC+minRewardDrinkSAC)/2,0,maxRewardDrinkSAC,minRewardDrinkSAC] ] 
   
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
ax.set_title('Drinking Task', fontsize = 30,
             fontweight ="bold") 
   
plt.show() 
