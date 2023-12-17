import matplotlib.pyplot as plt
import pandas as pd
import csv


cumulativeRewardDrinkingPPO = 0.0
cumulativeRewardDrinkingSAC = 0.0


cumulativeRewardFeedPPO = 0.0
cumulativeRewardFeedSAC = 0.0


maxRewardDrinkPPO = 0.0
minRewardDrinkPPO = 10000000.0

maxRewardDrinkSAC = 0.0
minRewardDrinkSAC = 10000000.0

maxRewardFeedPPO = 0.0
minRewardFeedPPO = 10000000.0

maxRewardFeedSAC = 0.0
minRewardFeedSAC = 10000000.0

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


datareward = {'PPO': [cumulativeRewardDrinkingPPO,cumulativeRewardFeedPPO,], 
        'SAC': [cumulativeRewardDrinkingSAC,cumulativeRewardFeedSAC,] 
        }

dfreward = pd.DataFrame(datareward,columns=['PPO','SAC'], index = ['Drink Reward','Feed Reward'])

plt.style.use('ggplot')

dfreward.plot.barh()

plt.title('Compare Reward PPO vs SAC')
plt.ylabel('')
plt.xlabel('')
plt.show()

