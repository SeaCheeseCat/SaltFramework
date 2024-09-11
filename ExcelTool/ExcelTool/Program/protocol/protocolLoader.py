# encoding: utf-8
############################################
#协议生成工具,本工具可以将协议文件转化为c#代码,
#协议定义格式尽量靠近protoBuff,但由于工程中使用的http特殊性,只能有不少特殊处理
############################################
import sys
import os
from stack import Stack

CONFIG_FILE_PATH = "protocol" #配置文件路径
TMPL_SEND_FILE_PATH = "Tmpls/SendTmpl.cs"
TMPL_RECEIVE_FILE_PATH = "Tmpls/ReceiveTmpl.cs"
PROTOCOLS_FILE_PATH = "Tmpls/Protocols.cs"

BASETYPE = ["string","int","float","bool"]

sendTypes = []
receiveTypes = []
protocols = []
protocolNameDefines = []
protocolCreators = []

############################################
#读取配置
############################################
def loadConfig():
    configFile = open(CONFIG_FILE_PATH, 'r')
    config = ""
    try:
        config = configFile.readlines()
    finally:
        configFile.close()

    index = 0
    while index < len(config):
        line = config[index]
        annotationIndex = line.find("#")
        if annotationIndex >= 0:
            config[index] = line[0:annotationIndex]
        index += 1
    
    index = 0
    while index < len(config):
        line = config[index].strip()
        if line != "":
            if line.startswith("send"):
                index = LoadSend(config, index)
            elif line.startswith("receive"):
                index = LoadReceive(config, index)
            elif line.startswith("protocol"):
                index = loadProtocol(config, index)
            else:
                print(line)
        index += 1
    writeProtocols()

############################################
#读取send部分,send部分被用于发送请求,
#生成的类型会带有一个序列化成http form的方法,并且类型不可嵌套
############################################
def LoadSend(config, index):
    curLine = config[index].strip()
    curLine = ' '.join(curLine.split()) #去除多余空格
    
    curLineArray = curLine.split(" ")
    typeName = curLineArray[1].strip("{")

    sendTypes.append(typeName)
    block, index = loadBlock(config, index)

    fieldsStr = ""
    funcStr = ""
    logStr = ""
    signStr = ""
    
    index = 0
    for fieldLine in block:
        fieldLineArr = fieldLine.split(" ")
        fieldType = fieldLineArr[0]
        fieldName = fieldLineArr[1]
        if isBaseType(fieldType) :
            #基本类型
            if fieldName.find("[]") > 0:
                #数组
                fieldName = fieldName[0:fieldName.index("[]")]
                fieldsStr += "    public List<" + fieldType + "> " + fieldName + ";\n"
                funcStr += "        string "+ fieldName +"Str = GetListData(\"" + fieldName  + "\"," + fieldName + ");\n"
                logStr += "        builder.Append (space + \"["+fieldName +":\" + LogList ("+fieldName+", offset) + \"]\\n\");\n"
            elif fieldType == "string":
                #特殊处理字符串
                fieldsStr += "    public " + fieldType + " " + fieldName + ";\n"
                funcStr += "        string " + fieldName + "Str = " + fieldName + "==null?\"\":"+ fieldName +".ToString();\n"
                logStr += "        builder.Append (space + \"["+fieldName +":\" + " + fieldName + " + \"]\\n\");\n"
            elif fieldType == "bool":
                #特殊处理布尔型,用字符串传值
                fieldsStr += "    public " + fieldType + " " + fieldName + ";\n"
                funcStr += "        string " + fieldName + "Str = " + fieldName + "?\"1\":\"0\";\n"
                logStr += "        builder.Append (space + \"["+fieldName +":\" + " + fieldName + " + \"]\\n\");\n"
            else:
                fieldsStr += "    public " + fieldType + " " + fieldName + ";\n"
                funcStr += "        string " + fieldName + "Str = " + fieldName + ".ToString();\n"
                logStr += "        builder.Append (space + \"["+fieldName +":\" + " + fieldName + " + \"]\\n\");\n"

        funcStr += "        _resData.Add(\"" + fieldName + "\"," + fieldName + "Str);\n"
        if index != 0:
            signStr += ","
        signStr += fieldName + "Str"
        index += 1
    funcStr += "        _resData.Add (\"sign\", ServerTools.GetSign (channelID, uid, " + signStr + ", timestamp));\n"
                    
    print("正在生成发送实体[" + typeName + "]")
    print("fields:")
    print(fieldsStr)

    csTemplate = open(TMPL_SEND_FILE_PATH, 'r')
    fileString = ""
    try:
        fileString = csTemplate.read()
    finally:
        csTemplate.close()

    fileString = fileString.replace('{0}', typeName)
    fileString = fileString.replace('{1}', fieldsStr)
    fileString = fileString.replace('{2}', funcStr)
    fileString = fileString.replace('{3}', logStr)

    csTarget = open('out/'+typeName+".cs", 'w')
    csTarget.write(fileString)
    csTarget.close()

    return index - 1

############################################
#从字符串中直接生成发送对象
############################################
def LoadSendFromString(name, data):
    array = data.split('\n')
    array[0] = "send " + name + "\n" + array[0]
    LoadSend(array, 0)

############################################
#读取receive部分,receive部分被用于接受请求
#生成的类型会带有一个反序列化json方法
############################################
def LoadReceive(config, index):
    curLine = config[index].strip()
    curLine = ' '.join(curLine.split()) #去除多余空格
    
    curLineArray = curLine.split(" ")
    typeName = curLineArray[1].strip("{")
    
    sendTypes.append(typeName)
    block, index = loadBlock(config, index)
    
    fieldsStr = ""
    funcStr = ""
    logStr = ""

    for fieldLine in block:
        fieldLineArr = fieldLine.split(" ")
        fieldType = fieldLineArr[0]
        fieldName = fieldLineArr[1]
        if isBaseType(fieldType) :
            #基本类型
            if fieldName.find("[]") > 0:
                #数组
                fieldName = fieldName[0:fieldName.index("[]")]
                fieldsStr += "    public List<" + fieldType + "> " + fieldName + ";\n"
                funcStr += "        ConvertList(_rawdata, \"" + fieldName  + "\", ref " + fieldName + ");\n"
                logStr += "        builder.Append (space + \"["+fieldName +":\" + LogList ("+fieldName+", offset) + \"]\\n\");\n"
            else:
                fieldsStr += "    public " + fieldType + " " + fieldName + ";\n"
                funcStr += "        ConvertType(_rawdata, \"" + fieldName  + "\", ref " + fieldName + ");\n"
                logStr += "        builder.Append (space + \"["+fieldName +":\" + " + fieldName + " + \"]\\n\");\n"

        else:
            #自定义类型
            if fieldName.find("[]") > 0:
                #数组
                fieldName = fieldName[0:fieldName.index("[]")]
                fieldsStr += "    public List<" + fieldType + "> " + fieldName + ";\n"
                funcStr += "        ConvertList<" + fieldType + ">(_rawdata, \"" + fieldName  + "\", ref " + fieldName + ");\n"
                logStr += "        builder.Append (space + \"["+fieldName +":\" + LogList<"+ fieldType +"> (" + fieldName + ", offset) + space  + \"]\\n\");\n"

            else:
                fieldsStr += "    public " + fieldType + " " + fieldName + ";\n"
                funcStr += "        ConvertType<" + fieldType + ">(_rawdata, \"" + fieldName  + "\", ref " + fieldName + ");\n"
                logStr += "        builder.Append (space + \"["+fieldName +":\" + LogType (" + fieldName + ", offset) + space  + \"]\\n\");\n"


    print("正在生成接收实体:[" + typeName + "]")
    print("fields:")
    print(fieldsStr)

    csTemplate = open(TMPL_RECEIVE_FILE_PATH, 'r')
    fileString = ""
    try:
        fileString = csTemplate.read()
    finally:
        csTemplate.close()
    
    fileString = fileString.replace('{0}', typeName)
    fileString = fileString.replace('{1}', fieldsStr)
    fileString = fileString.replace('{2}', funcStr)
    fileString = fileString.replace('{3}', logStr)

    csTarget = open('out/'+typeName+".cs", 'w')
    csTarget.write(fileString)
    csTarget.close()
    
    return index - 1

############################################
#从字符串中直接生成接受对象
############################################
def LoadReceiveFromString(name, data):
    array = data.split('\n')
    array[0] = "receive " + name + "\n" + array[0]
    LoadReceive(array, 0)


############################################
#读取send部分,send部分被用于发送请求,
#生成的类型会带有一个序列化成http form的方法,并且类型不可嵌套
############################################
def loadProtocol(config, index):
    curLine = config[index].strip()
    curLine = ' '.join(curLine.split()) #去除多余空格
    
    curLineArray = curLine.split(" ")
    protocolName = curLineArray[1]
    
    block, index = loadBlock(config, index)
    
    url = ""
    request = "null"
    response = "null"
    blockTime = "0f"
    
    for fieldLine in block:
        tmpImdex = fieldLine.index(" ")
        fieldType = fieldLine[0:tmpImdex]
        fieldName = fieldLine[tmpImdex + 1:len(fieldLine)]
    
        if fieldType == "url":
            url = fieldName
        elif fieldType == "request":
            if fieldName.find('{') >= 0:
                fileName = getProtocolName(protocolName) + "SendData"
                request = "\"" + fileName + "\""
                LoadSendFromString(fileName, fieldName)
            else:
                request = "\"" + fieldName + "\""
        elif fieldType == "response":
            if fieldName.find('{') >= 0:
                fileName = getProtocolName(protocolName) + "ReceiveData"
                response = "\"" + fileName + "\""
                LoadReceiveFromString(fileName, fieldName)
            else:
                response = "\"" + fieldName + "\""

        elif fieldType == "blockTime":
            blockTime = fieldName + "f"

    protocolNameDefines.append("    public const string " + protocolName + "= \"" + getProtocolName(protocolName) + "\";")
    protocolCreators.append("        s_dict.Add (" + protocolName + ", new ProtocolCfg (" + protocolName + ",\"" + url + "\"," + request + "," + response + "," + blockTime +"));")
    
    print("正在生成协议:[" + protocolName + "]")
    print("url:[" + url + "]")
    print("request:[" + request.strip("\"") + "]")
    print("response:[" + response.strip("\"") + "]")
    print("")

    return index - 1

############################################
#写协议文件
############################################
def writeProtocols():
    csTemplate = open(PROTOCOLS_FILE_PATH, 'r')
    fileString = ""
    try:
        fileString = csTemplate.read()
    finally:
        csTemplate.close()

    fileString = fileString.replace('{0}', '\n'.join(protocolNameDefines) + "\n")
    fileString = fileString.replace('{1}', '\n'.join(protocolCreators))

    csTarget = open('out/Protocols.cs', 'w')
    csTarget.write(fileString)
    csTarget.close()


############################################
#读取函数块
############################################
def loadBlock(config, index):
    curLine = config[index].strip()
    curLine = ' '.join(curLine.split()) #去除多余空格

    firstStart = True
    res = []
    stack = Stack()
    
    while (firstStart or not stack.is_empty()):
        curLine = config[index].strip()
        if curLine != "":
            lineData = ""
            
            leftBraceIndex = curLine.find("{")
            rightBraceIndex = curLine.find("}")

            #处理有两个括号的情况
            if leftBraceIndex >=0 and rightBraceIndex >= 0 :
                firstStart = False
                res = []
                res.append(resetLine(curLine[leftBraceIndex + 1:rightBraceIndex]))
                if not stack.is_empty() :
                    stack.top()[-1] += " {" + '\n'.join(res) + "}"

            
            #处理左括号
            elif leftBraceIndex >= 0 :
                if stack.is_empty() :
                    firstStart = False
                stack.push([])
                
                if leftBraceIndex != len(curLine) - 1:
                    stack.top().append(resetLine(curLine[leftBraceIndex + 1:len(curLine)]))
            
            #处理右括号
            elif rightBraceIndex >= 0 :
                if rightBraceIndex != 0:
                    stack.top().append(resetLine(curLine[0:rightBraceIndex]))
                        
                res = stack.pop()

#                print("block= " + '|'.join(res))
#                print(res)

                if not stack.is_empty() :
                    stack.top()[-1] += " {" + '\n'.join(res) + "}"
            
            elif not stack.is_empty():
                stack.top().append(resetLine(curLine))
        index += 1
    
    return res, index

def resetLine(line):
    line = line.strip().strip(';')
    line = ' '.join(line.split())
    return line

def isBaseType(typeName):
    return typeName in BASETYPE

def getProtocolName(rawName):
    if(rawName.find('_') > 0):
        array = rawName.split('_')
        for i in range(0, len(array)):
            array[i] = array[i].capitalize()
        return ''.join(array)
    else:
        return rawName

if __name__=="__main__":
    if sys.argv[0] != "protocolLoader.py" :
        os.chdir(sys.argv[0].replace("protocolLoader.py",""))
        loadConfig()

