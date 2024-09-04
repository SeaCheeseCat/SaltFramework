using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.IO;
using Excel;
using Newtonsoft.Json.Linq;
using System;
using UnityEditor;
using Newtonsoft.Json;


public class ExcelEditor2 : EditorWindow
{
    /// <summary>
    /// 表格的存放位置
    /// </summary>
    static string configPath = "../Doc/Excel";

    /// <summary>
    /// 模板存放位置
    /// </summary>
    static string scriptsPath
    {
        get
        {
            return PathManager.GetPath + PathManager.JsonScriptsPath;
        }
    }

    /// <summary>
    /// json文件存放位置
    /// </summary>
    static string jsonPath
    {
        get
        {
            return PathManager.GetPath + PathManager.JsonPath;
        }
    }

    /// <summary>
    /// 表格数据列表
    /// </summary>
    static List<TableData> dataList = new List<TableData>();
    private static bool isModify;

    static Project project = null;

    public class Project
    {
        public int excelVes;
        public string BuildTime;
    }

    string toppath;
    string cfgpath;
    private void OnEnable()
    {

        DirectoryInfo topDir = Directory.GetParent(Application.dataPath);
        toppath = topDir.FullName;

        string foldpath = toppath + "/ProjectConfig";


        if (!File.Exists(foldpath))
        {
            Directory.CreateDirectory(foldpath);
        }
        cfgpath = foldpath + "/config.txt";

        if (!File.Exists(cfgpath))
        {

            project = new Project();
            project.excelVes = 1;
            File.WriteAllText(cfgpath, JsonConvert.SerializeObject(project));

        }
        else
        {
            project = JsonConvert.DeserializeObject<Project>(File.ReadAllText(cfgpath));
        }

        //if (PlayerPrefs.HasKey(ExcelVersionkey))
        //{
        //    ExcelVersion = PlayerPrefs.GetInt(ExcelVersionkey, 1);

        //}
        if (PlayerPrefs.HasKey("EditorExcel"))
        {
            configPath = PlayerPrefs.GetString("EditorExcel");
        }
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical(GUILayout.Width(position.width), GUILayout.Height(position.height));
        {
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("资源路径", GUILayout.Width(50f));
                configPath = EditorGUILayout.TextField(configPath);
                if (GUILayout.Button("Browse...", GUILayout.Width(80f)))
                {
                    string directory = EditorUtility.OpenFolderPanel("选择输出路径", configPath, string.Empty);
                    if (!string.IsNullOrEmpty(directory))
                    {
                        configPath = directory;
                    }
                    PlayerPrefs.SetString("EditorExcel", configPath);
                }
            }
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("生成配置", GUILayout.Height(40f), GUILayout.Width(200f)))
            {
                Create();
            }
            if (GUILayout.Button("打开路径", GUILayout.Height(40f), GUILayout.Width(200f)))
            {
                DirectoryInfo direction = new DirectoryInfo(configPath);
                System.Diagnostics.Process.Start(direction.FullName);
            }
            
        }
        EditorGUILayout.EndVertical();
    }


    private static bool Create()
    {
        if (configPath == "" || configPath == null)
        {
            return false;
        }
        if (!PlayerPrefs.HasKey("EditorExcel") && (configPath != "" && configPath != null))
        {
            PlayerPrefs.SetString("EditorExcel", configPath);
        }
        dataList = new List<TableData>();

        if (Directory.Exists(configPath))
        {
            RemoveUnnecessary();
            //获取指定目录下所有的文件
            DirectoryInfo direction = new DirectoryInfo(configPath);
            FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);
            Debug.Log("fileCount:" + files.Length);
            List<string> exs = new List<string>();
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Name.EndsWith(".meta") || !files[i].Name.EndsWith(".xlsx"))
                {
                    continue;
                }
                Debug.Log("FullName:" + files[i].FullName);
                LoadData(files[i].FullName, files[i].Name);
                var nam = files[i].Name.Replace(".xlsx", "");
                exs.Add(nam);
            }

            var excevsPath = PathManager.GetPath + "/StreamingAssets/ExcelVs.txt";

            ExcelList ex = new ExcelList();
            ex.Vs = project.excelVes;
            ex.excels = exs;
            File.WriteAllText(excevsPath, JsonConvert.SerializeObject(ex));

            //刷新项目
            AssetDatabase.Refresh();
            //AssetBundleBuilder.SetAbNameInPath(jsonPath);
            AssetDatabase.Refresh();
            return true;
        }
        else
        {
            Debug.LogError("ReadExcel configPath not Exists!");
            return false;
        }
    }


    /// <summary>
    /// 删除被启用的json与模板
    /// </summary>
    private static void RemoveUnnecessary()
    {
        DebugEX.Log(scriptsPath, jsonPath);
        Directory.Delete(scriptsPath, true);
        Directory.Delete(jsonPath, true);
    }



    /// <summary>
    /// 读取表格并保存脚本及json
    /// </summary>
    static void LoadData(string filePath, string fileName)
    {
        DebugEX.Log("读取文件", filePath, fileName);
        //获取文件流
        FileStream fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read);
        //生成表格的读取
        IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fileStream);
        // 表格数据全部读取到result里(引入：DataSet (using System.Data;)
        DataSet result = excelDataReader.AsDataSet();
        DebugEX.Log(filePath);
        CreateTemplate(result, fileName);
        CreateJson(result, fileName);
    }

    /// <summary>
    /// 生成json文件
    /// </summary>
    static void CreateJson(DataSet result, string fileName)
    {
        int columns = result.Tables[0].Columns.Count;
        int rows = result.Tables[0].Rows.Count;
        DebugEX.Log("读取Excel表格", columns, rows, fileName);

        TableData tempData;
        string value;
        JArray array = new JArray();

        //第一行为表头，第二行为类型 ，第三行为字段名 不读取
        for (int i = 4; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                // 获取表格中指定行指定列的数据 
                value = result.Tables[0].Rows[i][j].ToString();

                if (string.IsNullOrEmpty(result.Tables[0].Rows[0][j].ToString())
                    && string.IsNullOrEmpty(result.Tables[0].Rows[1][j].ToString())
                    && string.IsNullOrEmpty(result.Tables[0].Rows[3][j].ToString()))
                {
                    continue;
                }
                //DebugEX.LogError("biao", result.Tables[0].Rows[j][0].ToString());
                //if (/*string.IsNullOrEmpty(result.Tables[0].Rows[0][j].ToString())*/)
                //{
                //    continue;
                //}
                if (result.Tables[0].Rows[3][j].ToString() == "ID" && string.IsNullOrEmpty(value) )
                {
                    break;
                }
                tempData = new TableData();
                tempData.type = result.Tables[0].Rows[1][j].ToString();
                tempData.fieldName = result.Tables[0].Rows[3][j].ToString();
                tempData.value = value;

                dataList.Add(tempData);
            }

            if (dataList != null && dataList.Count > 0)
            {
                JObject tempo = new JObject();
                foreach (var item in dataList)
                {
                    string name = item.type.ToLower();
                    switch (name)
                    {
                        case "string":
                            tempo[item.fieldName] = item.value == "" ? "" : GetValue<string>(item.value, fileName);
                            break;
                        case "int":
                            tempo[item.fieldName] = item.value == "" ? 0 : GetValue<int>(item.value, fileName);
                            break;
                        case "float":
                            tempo[item.fieldName] = item.value == "" ? 0.0f : GetValue<float>(item.value, fileName);
                            break;
                        case "bool":
                            tempo[item.fieldName] = item.value == "" ? false : GetValue<bool>(item.value, fileName);
                            break;
                        case "string[]":
                            tempo[item.fieldName] = new JArray(GetList<string>(item.value, '&', fileName));
                            break;
                        case "int[]":
                            tempo[item.fieldName] = new JArray(GetList<int>(item.value, ',', fileName));
                            break;
                        case "float[]":
                            tempo[item.fieldName] = new JArray(GetList<float>(item.value, ',', fileName));
                            break;
                        case "bool[]":
                            tempo[item.fieldName] = new JArray(GetList<bool>(item.value, ',', fileName));
                            break;
                    }
                }

                if (tempo != null)
                    array.Add(tempo);
                dataList.Clear();
            }
        }

        JObject o = new JObject();
        o["datas"] = array;
        fileName = fileName.Replace(".xlsx", "");
        var path = jsonPath;
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        File.WriteAllText(path + fileName + ".json", o.ToString());
    }


    /// <summary>
    /// 字符串拆分列表
    /// </summary>
    static List<T> GetList<T>(string str, char spliteChar, string fileName)
    {
        List<T> arry;
        if (str != "")
        {
            string[] ss = str.Split(spliteChar);
            int length = ss.Length;
            arry = new List<T>(ss.Length);
            for (int i = 0; i < length; i++)
            {
                arry.Add(GetValue<T>(ss[i], fileName));
            }
        }
        else
        {
            arry = new List<T>(0);
        }
        return arry;
    }

    static T GetValue<T>(object value, string fileName)
    {
        DebugEX.Log("val ", value, fileName);
        try
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }
        catch
        {
            DebugEX.LogError(fileName, "Excel错误属性", value, typeof(T).Name);
            return (T)Convert.ChangeType(value, typeof(T));
        }
    }



    /// <summary>
    /// 生成实体类模板
    /// </summary>
    static void CreateTemplate(DataSet result, string fileName)
    {
        if (!Directory.Exists(scriptsPath))
        {
            Directory.CreateDirectory(scriptsPath);
        }

        field = "";
        for (int i = 0; i < result.Tables[0].Columns.Count; i++)
        {
            string typeStr = result.Tables[0].Rows[1][i].ToString();
            if (typeStr == "des")
                continue;
            typeStr = typeStr.ToLower();
            if (typeStr.Contains("[]"))
            {
                typeStr = typeStr.Replace("[]", "");
                typeStr = string.Format("{0}[]", typeStr);
            }

            string nameStr = result.Tables[0].Rows[3][i].ToString();
            if (string.IsNullOrEmpty(typeStr) || string.IsNullOrEmpty(nameStr)) continue;
            field += "public " + typeStr + " " + nameStr + " { get; set; }\r\t";
        }

        fileName = fileName.Replace(".xlsx", "");
        string tempStr = Eg_str;
        tempStr = tempStr.Replace("@Name", fileName);
        tempStr = tempStr.Replace("@File1", field);
        File.WriteAllText(scriptsPath + fileName + ".cs", tempStr);

    }

    /// <summary>
    /// 字段
    /// </summary>
    static string field;

    /// <summary>
    /// 实体类模板
    /// </summary>
    static string Eg_str =

        "using System.Collections.Generic;\r" +
        "using UnityEngine;\r" +
        "using Newtonsoft.Json;\r\r" +
        "public class @Name  {\r\t" +
        "@File1 \r\t" +
        "public static string configName = \"@Name\";\r\t" +
        "public static @Name config { get; set; }\r\t" +
        "public string version { get; set; }\r\t" +
        "public List<@Name> datas { get; set; }\r\r\t" +
        "public static @Name Get(int id)\r\t" +
        "{\r\t\tif (config == null)\r\t\t" +
        "{\r\t\t\t" +
        "config = ConfigManager.Instance.Readjson<@Name>(configName);\r\t\t" +
        "}\r\r\t\t" +
        "foreach (var item in config.datas)\r\t\t{\r\t\t\t" +
        "if (item.ID == id)\r\t\t\t" +
        "{ \r\t\t\t\t\t" +
        "return item;" +
        "\r\t\t\t}" +
        "\r\t\t}\r\t\t" +
        "return null;\r\t" +
        "}\r\t"
            + "public static List<@Name> " +
        "GetList()\r\t" +
        "{\r\t\t" +
        "if (config == null)\r\t\t" +
        "{\r\t\t\t" +
        "config= ConfigManager.Instance.Readjson<@Name>(configName);" +
        "\r\t\t}\r\t\treturn config.datas;" +
        "\r\t}\r"
        + "\r}"
        ;
}
