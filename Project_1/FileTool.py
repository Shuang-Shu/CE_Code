import glob

class FileTool:
    #FileTool用于视频/音频文件的读取
    #域
    pathPrefix=None
    #方法
    def __init__(self, aPath='Resource'):
        self.pathPrefix=aPath
    
    def setPathPrefix(self, aPath):
        self.pathPrefix=aPath
        return

    def getPath(self, birdName, type='Video'):
        #根据birdName从文件系统读取视频/音频，返回视频/音频文件的路径列表
        #鸟类视频的命名格式为:Name_[编号].mp4
        #例如：麻雀_1.mp4
        #鸟类音频的命名格式为:Name_[编号].mp3
        #例如：麻雀_1.mp3
        if self.pathPrefix==None:
            print('先使用self.setPathPrefix设置视频/视频文件路径前缀')
            return
        path=self.pathPrefix+'\\'+type+'\\'
        print(path)
        fileNameList=glob.glob(path+birdName+'_*')
        print(fileNameList)
        return fileNameList

    def getFileByPath(self, filePathList):
        #根据filePathList读取视频/音频，返回视频/音频文件的字节流列表
        fileList=[]
        for filePath in filePathList:
            f=open(filePath, 'wr')
            fileList.append(f.read())
            f.close()
        return fileList

    def getFile(self, birdName, type='Audio'):
        #根据birdName从文件系统读取视频/音频，返回视频/音频文件的字节流列表
        filePathList=self.getPath(birdName, type)
        return self.getFileByPath(filePathList)