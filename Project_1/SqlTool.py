import pymysql as sql

class SQLTool:
    #域
    con=None
    cursor=None
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

    def getAttributes(self, birdName):
        #根据birdName从数据库中查询鸟类属性组成的元组
        if self.con==None:
            print("未连接数据库")
            return
        self.cursor.execute('SELECT * FROM birds WHERE 鸟名=\''+self.birdName+'\';')
        result=self.cursor.fetchall()
        return result

    def create(self, attributeNames):
        #根据attributeNames新建一个关系列表，attributeNames为tuple对象
        if self.con==None:
            print("未连接数据库")
            return
        
        return

    def update(self, birdName, attributes):
        #更新birdName的关系，attributes为tuple对象
        if self.con==None:
            print("未连接数据库")
            return
        
        return

    def insert(self, birdName, attributes):
        #插入birdName的关系，attributes为tuple对象
        if self.con==None:
            print("未连接数据库")
            return
        
        return
    
    def delete(self, birdName):
        #删除数据库中birdName的关系
        if self.con==None:
            print("未连接数据库")
            return
        
        return