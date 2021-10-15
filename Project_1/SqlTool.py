import pymysql as sql

class SQLTool:
    #域
    con=None
    cursor=None
    attributeName=None
    tableName=None
    #attributesName为表中属性名
    #方法
    def __init__(self):
        return

    def connectDB(self, aHost='localhost', aPort=3306, aUser='root', aPassword='', aDatabase='', aCharset='utf8'):
        #连接MySQL数据库
        self.con=sql.connect(
        host=aHost,
        port=aPort,
        user=aUser,
        password=aPassword,
        database=aDatabase,
        charset=aCharset
        )
        self.cursor=self.con.cursor()

    def check(self):
        if self.con==None:
            print("未连接数据库")
            return False
        if self.tableName==None:
            print("未设置操作表名")
            return False
        return True

    def setTable(self, tableName):
        #设置表，同时获取表的属性名
        self.tableName=tableName
        if self.con==None:
            print('未连接数据库')
            return
        self.cursor.execute('desc testBird')
        DbDesc=self.cursor.fetchall()
        temp=[]
        for ele in DbDesc:
            temp.append(ele[0])
        self.attributeName=tuple(temp)

    def getAttributes(self, birdName):
        #根据birdName从数据库中查询鸟类属性组成的元组
        if(not self.check()):
            return
        instruction='SELECT * FROM '+self.tableName+' WHERE 鸟名=\''+birdName+'\';'
        self.cursor.execute(instruction)
        result=self.cursor.fetchall()
        return result

    def update(self, attributes):
        #更新birdName的关系，attributes为tuple对象
        if(not self.check()):
            return
        for i in range(1, len(self.attributeName)):
            instruction="UPDATE "+self.tableName+' SET '+self.attributeName[i]+'=\''+attributes[i]+'\' WHERE '+self.attributeName[0]+'=\''+attributes[0]+'\';'
            print(instruction)
            self.cursor.execute(instruction)
            self.con.commit()
        return

    def insert(self, attributes):
        #将attributes中的属性插入tableName对应的table
        self.check()
        instruction='INSERT INTO '+self.tableName+' VALUES'+str(attributes)+';'
        self.cursor.execute(instruction)
        self.con.commit()
        return
    
    def delete(self, birdName):
        #删除数据库中birdName的关系
        if(not self.check()):
            return
        instruction="DELETE FROM "+self.tableName+' WHERE 鸟名='+'\''+birdName+'\';'
        print(instruction)
        self.cursor.execute(instruction)
        self.con.commit()
        return