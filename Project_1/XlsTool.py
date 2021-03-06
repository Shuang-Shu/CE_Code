import xlrd as rd

class XLSTool:
    #域
    workBook=None
    filePath=None
    #方法
    #XLSTool用于打开.xls文件，并读取其中数据
    def __init__(self, filePath):
        self.workBook=rd.open_workbook(filePath)
        self.filePath=filePath
    
    def openXls(self, filePath):
        #打开.xls（表格）文件
        self.workBook=rd.open_workbook(filePath)
        self.filePath=filePath
    
    def readLines(self, sheetNum=0, start=0, end=-1):
        #读取xls文件中sheetNum的[start, end]行，返回一个列表，列表中元素为属性元组
        #若end=-1，则自动读取到sheet最后一行
        if self.workBook==None:
            print('先使用openXls打开一个.xls文件')
            return
        sheet=self.workBook.sheets()[sheetNum]
        if end==-1:
            end=sheet.nrows
        result=[]
        for i in range(start, end):
            result.append(tuple(sheet.row_values(i)))
        return result
