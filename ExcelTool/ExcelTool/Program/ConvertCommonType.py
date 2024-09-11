# encoding: utf-8
############################################
#转换并记录公有类型，
#将会生成公有类型的cs文件，以及py描述文件
############################################

from py.TypeDefine import TypeList 

#代码模板文件名
TEMPLATE_FILE_NAME = "templates/UserTypeTemplate.cs"
#用户类型描述文件名
USER_TYPE_FILE_NAME = "py/userType.py"

def ConvertCSharpFile(typeItem):
    dec = typeItem['Desc']
    name = typeItem['Name']
    attr = ""
    method = ""
    index = 0
    for field in typeItem['Fields']:
        #处理数组
        if 'count' in field:
            if field['count'] < 0 :
                attr += "    public {0}[] {1};\n".format(field['type'],field['name'])
                
                method +="        ResourceInitUtil.InitShortArray(data[start + {0}], out this.{1});\n".format(index, field['name'])
                index+=1
            else:
                attr += "    public {0}[] {1};\n".format(field['type'],field['name'])
                
                method +="        ResourceInitUtil.InitDefaultArray(data,start + {0},start + {1}, out this.{2});\n".format(index,index+field['count']-1,field['name'])
                index+=field['count']
        #处理非数组
        elif field['type'].startswith("e_"):
            field['type'] = field['type'].split("_")[1]
            attr += "    public {0} {1};\n".format(field['type'],field['name'])
            method += "        {0} = ({1})int.Parse(data[start + {2}]);\n".format(field['name'],field['type'],index)
            index+=1
        else:
            attr += "    public {0} {1};\n".format(field['type'],field['name'])
            if field['type'] != "string":
                method +="        ResourceInitUtil.InitField(data[start + {0}],out this.{1});\n".format(index,field['name'])
            else:
                method +="        ResourceInitUtil.InitField(data[start + {0}],out this.{1},LanguageValue);\n".format(index,field['name'])
            index+=1
            
    writeCSharpFile(dec,name,attr,method);


def ConvertPYFile(typeList):
    typestr = 'USERTYPE = [';
    for item in TypeList:
        typestr+='"{0}",'.format(item['Name'])
    typestr +=']'
    csTarget = open(USER_TYPE_FILE_NAME, 'w')
    csTarget.write(typestr)
    csTarget.close()

def writeCSharpFile(dec,name,attr,method):
    csTemplate = open(TEMPLATE_FILE_NAME, 'r')
    fileString = ""
    try:
        fileString = csTemplate.read()
    finally:
        csTemplate.close()
        
    fileString = fileString.replace('{0}', dec)
    fileString = fileString.replace('{1}', name)
    fileString = fileString.replace('{2}', attr)  
    fileString = fileString.replace('{3}', method)
    
    csTarget = open('out/code/UserType/'+name+".cs", 'w')
    csTarget.write(fileString)
    csTarget.close()

def main():
    for item in TypeList:
        ConvertCSharpFile(item)
    ConvertPYFile(TypeList)
    
if __name__=="__main__":
    main()
