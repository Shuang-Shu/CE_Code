import SqlTool as ST
import XlsTool as XT

def insert(args, sqlTool):
    fileNames=str(args).split(',')
    for i in range(len(fileNames)):
        xl=XT.XLSTool(fileNames[i])
        temp=xl.readLines(start=1)
        for element in temp:
            sqlTool.insert(element)


def update(args, sqlTool):
    fileNames=str(args).split(',')
    for i in range(len(fileNames)):
        xl=XT.XLSTool(fileNames[i])
        temp=xl.readLines(start=1)
        for element in temp:
            sqlTool.update(element)

def delete(args, sqlTool):
    birdNames=str(args).split(',')
    for name in birdNames:
        sqlTool.delete(name)

f=open('config.cfg', encoding='utf-8')
configList=f.readlines()
for i in range(len(configList)):
    configList[i]=configList[i][0:-1]
f.close()

database=configList[0]
table=configList[1]
password=configList[2]

db=ST.SQLTool()
db.connectDB(aDatabase=database, aPassword=password)
db.setTable(table)

#print(configList)
i=3

while(i<len(configList)):
    if configList[i]=='':
        i+=1
        continue
    if configList[i]=='Insert':
        insert(configList[i+1], db)
        i+=2
    elif configList[i]=='Update':
        update(configList[i+1], db)
        i+=2
    elif configList[i]=='Delete':
        delete(configList[i+1], db)
        i+=2
    else:
        i+=2