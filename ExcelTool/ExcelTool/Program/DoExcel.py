import pandas as pd
import json
import os

TEMPLATE_FILE_NAME="templates/Template.cs"
DIRECTORY_PATH = "../ExcelConfig"
GAMECODE_PATH = "../../../../All-Doe-s-Life/Code/Assets/"
JSON_OUT_PATH="Resources/Config/Table"
CODE_OUT_PATH="Scripts/Game/Configs/Types/ResType/"
JSONTEMP_OUT_PATH="../Out/Config"
CODETEMP_OUT_PATH="../Out/Scripts/ResType/"


def process_excel_file(excel_file, output_folder):
    # 读取 Excel 文件
    excel_data = pd.read_excel(excel_file, header=None)  # 禁用默认的列名，因为第四行将用作键

    # 读取第二行数据，用于确定数据类型
    data_types = excel_data.iloc[1]

    # 读取第四行数据，作为键
    keys = excel_data.iloc[3]

    # 将第四行数据作为键，之后的数据作为值
    data = excel_data.T.set_index(3).T.iloc[3:].reset_index(drop=True)

    # 转换数据类型
    for i, dtype in enumerate(data_types):
        column_name = keys[i]
        if dtype == "int":
            data[column_name] = data[column_name].astype(int)
        elif dtype == "bool":
            data[column_name] = data[column_name].astype(bool)
        elif dtype == "string":
            data[column_name] = data[column_name].astype(str)
        elif dtype == "array":
            data[column_name] = data[column_name].apply(lambda x: eval(x) if isinstance(x, str) else x)
        elif dtype == "int[]":
            # 处理 "int[]" 数据类型，将字符串转换为整数列表
            data[column_name] = data[column_name].apply(lambda x: [int(num) for num in x.strip("int[]").split(",")] if isinstance(x, str) else x)
        elif dtype == "float[]":
            # 处理 "float[]" 数据类型，将字符串转换为浮点数列表
            data[column_name] = data[column_name].apply(lambda x: [float(num) for num in x.strip("float[]").split(",")] if isinstance(x, str) else x)
        elif dtype == "string[]":
            # 处理 "string[]" 数据类型，将字符串转换为字符串列表
            data[column_name] = data[column_name].apply(lambda x: x.strip("string[]").split("&") if isinstance(x, str) else x)

    # 将数据放入名为 "data" 的键中
    json_data = {"data": data.to_dict(orient='records')}

    # 输出的 JSON 文件名与 Excel 文件名相同，但扩展名为 .json
    json_file_name = os.path.splitext(os.path.basename(excel_file))[0] + ".json"
    output_path = os.path.join(output_folder, json_file_name)

    # 转换为 JSON 格式
    json_data_str = json.dumps(json_data, indent=4)

    # 将 JSON 数据写入文件
    with open(output_path, "w") as json_file:
        json_file.write(json_data_str)

    print("JSON 数据已保存到文件:", output_path)

def process_excel_files_in_directory(directory):
    # 输出文件夹路径
    output_folder = JSONTEMP_OUT_PATH

    # 创建输出文件夹
    os.makedirs(output_folder, exist_ok=True)

    # 遍历目录下的所有文件
    for filename in os.listdir(directory):
        if filename.endswith(".xlsx") or filename.endswith(".xls"):
            # 拼接文件路径
            excel_file = os.path.join(directory, filename)
            # 处理 Excel 文件
            process_excel_file(excel_file, output_folder)
            process_excel_file(excel_file, GAMECODE_PATH+JSON_OUT_PATH)
            process_code_file(excel_file, CODETEMP_OUT_PATH)
            process_code_file(excel_file, GAMECODE_PATH+CODE_OUT_PATH)

def process_code_file(excel_file, output_folder):
    # 读取 Excel 文件
    excel_data = pd.read_excel(excel_file, header=None)  # 禁用默认的列名，因为第四行将用作键

    # 读取第二行数据，用于确定数据类型
    data_types = excel_data.iloc[1]

    # 读取第四行数据，作为键
    keys = excel_data.iloc[3]
    attr = ""
    # 转换数据类型
    for i, dtype in enumerate(data_types):
        if dtype == "des":
            continue
        column_name = keys[i]
        attr += "    public {0} {1};\n".format(dtype,column_name)
    print(attr)
    classname = os.path.splitext(os.path.basename(excel_file))[0]
    writeCSharpFile("",classname, attr,classname,classname,output_folder)

def writeCSharpFile(dec,name,attr,configName,classname, path):
    csTemplate = open(TEMPLATE_FILE_NAME, 'r')
    fileString = ""
    try:
        fileString = csTemplate.read()
    finally:
        csTemplate.close()
        
    fileString = fileString.replace('{0}', dec)
    fileString = fileString.replace('{1}', name)
    fileString = fileString.replace('{2}', attr)  
    fileString = fileString.replace('{3}', "\""+configName+"\"")
    fileString = fileString.replace('{4}', classname)

    csTarget = open(path+name+".cs", 'w', encoding='utf-8')
    csTarget.write(fileString)
    csTarget.close()
   

if __name__ == "__main__":
    # 指定目录路径
    directory_path = DIRECTORY_PATH
    # 处理目录下的所有 Excel 文件
    process_excel_files_in_directory(directory_path)
