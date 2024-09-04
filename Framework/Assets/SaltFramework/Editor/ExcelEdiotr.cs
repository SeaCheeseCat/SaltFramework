using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.IO;
using Excel;
using Newtonsoft.Json.Linq;
using System;
using UnityEditor;
using Newtonsoft.Json;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector;
using OfficeOpenXml;

public class ExcelEditor : OdinEditorWindow
{

    [Sirenix.OdinInspector.FolderPath(ParentFolder = "../Doc/Excel", AbsolutePath = true)]
    public string configPath = "../Doc/Excel";
    public static ExcelEditor window;

    [MenuItem("▷ SaltFramework/Excel配置表工具")]
    private static void OpenWindow()
    {
        window = GetWindow<ExcelEditor>();
        window.Init();
        window.Show();
        window.UpdateList();
    }

    public ExcelEditor()
    {
        // 设置窗口标题
        this.titleContent = new GUIContent("表格解析器");
        
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        PlayerPrefs.SetString("EditorExcel", configPath);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
      
    }

    public void Init()
    {
        if (PlayerPrefs.HasKey("EditorExcel"))
        {
            configPath = PlayerPrefs.GetString("EditorExcel");
        }
        UpdateList();
    }



    [TabGroup("配置")]
    [Button("生成配置", ButtonSizes.Large)]
    [MenuItem("Window/ShortcutKey/生成Excel配置  _F3")]
    public static void CreateConfig()
    {
        if (window == null)
        { 
            window = GetWindow<ExcelEditor>();
            window.Init();
        }
        window.Create();
    }

    [TabGroup("配置")]
    [Button("打开路径", ButtonSizes.Large)]
    private void OpenPath()
    {
        DirectoryInfo direction = new DirectoryInfo(configPath);
        System.Diagnostics.Process.Start(direction.FullName);
    }



    [TabGroup("配置")]
    [Button("刷新列表", ButtonSizes.Large)]
    private void UpdateList()
    {
        if (!Directory.Exists(configPath))
        {
            Debug.LogWarning("Folder does not exist: " + configPath);
            return;
        }

        string[] files = Directory.GetFiles(configPath, "*.xlsx");

        ExcelList = new ExcelItem[files.Length];
        for (int i = 0; i < files.Length; i++)
        {
            ExcelList[i] = new ExcelItem(Path.GetFileNameWithoutExtension(files[i]), files[i]);
        }
    }

    [TabGroup("配置")]
    [ListDrawerSettings(ShowItemCount = true)]
    public ExcelItem[] ExcelList = new ExcelItem[]
    {
        
    };

    [Serializable]
    public class ExcelItem
    {
        [DisplayAsString]
        [OnInspectorGUI("OnStringClicked", append: true)]
        [LabelText("Excel:")]
        public string name;

        private string path;

        private double lastClickTime = 0;
        private const double DoubleClickThreshold = 0.3;

        private void OnStringClicked()
        {
            if (Event.current.type == EventType.MouseDown && GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition))
            {
                double clickTime = EditorApplication.timeSinceStartup;
                if (clickTime - lastClickTime < DoubleClickThreshold)
                {
                    // 双击事件发生
                    OpenFile(path);
                }
                lastClickTime = clickTime;
            }
         }

        public ExcelItem(string name, string path)
        {
            this.name = name;
            this.path = path;
        }

        private void OpenFile(string path)
        {
            if (File.Exists(path))
            {
               System.Diagnostics.Process.Start(path);
            }
            else
            {
                UnityEngine.Debug.LogWarning("File does not exist: " + path);
            }
        }
    }

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

    [TabGroup("模板")]
    [LabelText("文件名称")]
    public string excelName;

    [TabGroup("模板")]
    [Button("创建Excel", ButtonSizes.Medium)]
    private void CreateExcel()
    {
        var orgfileName = configPath.Replace("ExcelConfig", "") + "Template/TemplateCfg.xlsx";
        var newfilePath = configPath + "/" + excelName + ".xlsx";
        CreateTemplateExcelFile(orgfileName, newfilePath);
        UpdateList();
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


    private bool Create()
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
            List<string> exs = new List<string>();
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Name.EndsWith(".meta") || !files[i].Name.EndsWith(".xlsx"))
                {
                    continue;
                }
                LoadData(files[i].FullName, files[i].Name);
                var nam = files[i].Name.Replace(".xlsx", "");
                exs.Add(nam);
            }

            var excevsPath = PathManager.GetPath + "/StreamingAssets/ExcelVs.txt";

            ExcelList ex = new ExcelList();
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
        Directory.Delete(scriptsPath, true);
        Directory.Delete(jsonPath, true);
    }



    /// <summary>
    /// 读取表格并保存脚本及json
    /// </summary>
    static void LoadData(string filePath, string fileName)
    {
        FileStream fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read);
        //生成表格的读取
        IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fileStream);
        // 表格数据全部读取到result里(引入：DataSet (using System.Data;)
        DataSet result = excelDataReader.AsDataSet();
        CreateTemplate(result, fileName);
        CreateJson(result, fileName);
    }

    /// <summary>
    /// 生成json文件
    /// </summary>
    static void CreateJson(DataSet result, string fileName)
    {
        // 获取表格有多少列
        int columns = result.Tables[0].Columns.Count;
        // 获取表格有多少行 
        int rows = result.Tables[0].Rows.Count;
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
                if (result.Tables[0].Rows[3][j].ToString() == "ID" && string.IsNullOrEmpty(value))
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

        var  languageData = CreateLanguageJson(result, fileName);
        JObject o = new JObject();
        o["datas"] = array;
        o["language"] = languageData;
        fileName = fileName.Replace(".xlsx", "");
        var path = jsonPath;
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        File.WriteAllText(path + fileName + ".json", o.ToString());
    }


    /// <summary>
    /// 生成json文件
    /// </summary>
    static JArray CreateLanguageJson(DataSet result, string fileName)
    {
        var table = result.Tables[1];
        int columns = table.Columns.Count;
        int rows = table.Rows.Count;

        TableData tempData;
        string value;
        JArray array = new JArray();
        List<TableData> dataList = new List<TableData>();

        for (int i = 4; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                value = table.Rows[i][j].ToString();

                if (string.IsNullOrEmpty(table.Rows[0][j].ToString())
                    && string.IsNullOrEmpty(table.Rows[1][j].ToString())
                    && string.IsNullOrEmpty(table.Rows[3][j].ToString()))
                {
                    continue;
                }
                if (table.Rows[3][j].ToString() == "ID" && string.IsNullOrEmpty(value))
                {
                    break;
                }
                tempData = new TableData();
                tempData.type = table.Rows[1][j].ToString();
                tempData.fieldName = table.Rows[3][j].ToString();
                tempData.value = value;

                dataList.Add(tempData);
            }

            if (dataList != null && dataList.Count > 0)
            {
                JObject tempo = new JObject();
                foreach (var item in dataList)
                {
                    if(item.fieldName == "ID")
                        tempo[item.fieldName] = item.value == "" ? "" : GetValue<int>(item.value, fileName);
                    else
                        tempo[item.fieldName] = item.value == "" ? "" : GetValue<string>(item.value, fileName);
                }

                if (tempo != null)
                    array.Add(tempo);
                dataList.Clear();
            }
        }
        return array;
    }

    public void CreateTemplateExcelFile(string originalFilePath, string newFilePath)
    {
        // 检查原始文件是否存在
        if (!File.Exists(originalFilePath))
        {
            Debug.LogError("Original Excel file does not exist at path: " + originalFilePath);
            return;
        }
        // 加载原始Excel文件
        using (ExcelPackage originalPackage = new ExcelPackage(new FileInfo(originalFilePath)))
        {
            // 获取工作簿中的所有工作表
            ExcelWorksheets worksheets = originalPackage.Workbook.Worksheets;

            // 检查工作表数量是否大于0
            if (worksheets.Count > 0)
            {
                // 获取第一个工作表
                ExcelWorksheet worksheet = worksheets[1];
                 
                // 创建新文件并保存
                FileInfo newFile = new FileInfo(newFilePath);
                originalPackage.SaveAs(newFile);
                DebugEX.LogFrameworkMsg("配置表:" + newFile.FullName+ "创建成功");
            }
            else
            {
                Debug.LogError("No worksheets found in the Excel file.");
            }
        }
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
        DebugEX.LogFrameworkMsg("配置表生成成功", fileName);
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
        "public static @Name config { get; set; }\r\r\t" +
        "public static LanguageConfigData languageConfigData;\r\t" +
        "public string version { get; set; }\r\t" +
        "public List<@Name> datas { get; set; }\r\r\t" +
        "public static @Name Get(int id)\r\t" +
        "{\r\t\tif (config == null)\r\t\t" +
        "{\r\t\t\t" +
        "config = ConfigManager.Instance.Readjson<@Name>(configName);\r\t\t\t" +
        "languageConfigData = ConfigManager.Instance.ReadLanguageJson(configName);;\r\t\t" +
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
        "\r\t}\r\t" +
        "public static string GetLangugeText(int id, Language language)\r\t" +
        "{\r\t\t" +
        "if (config == null)\r\t\t" +
        "{\r\t\t\t" +
        "config = ConfigManager.Instance.Readjson<@Name>(configName);\r\t\t\t" +
        "languageConfigData = ConfigManager.Instance.ReadLanguageJson(configName);;\r\t\t" +
        "}\r\t\t" +
        "if (languageConfigData.languageItemDatas.ContainsKey(id))\r\t\t" +
        "{\r\t\t\t" +
        "foreach (var data in languageConfigData.languageItemDatas[id])\r\t\t\t{\r\t\t\t\t" +
        "if (data.language == language)\r\t\t\t\t" +
        "return data.value;\r\t\t\t"+
        "}\r\t\t" +
        "}\r\t\t" +
        "return \"\";\r\t" +
        "}\r\t" +
        "\r}"
        ;


/*    public string GetLangugeText(int id, Language language)
    {
        if (languageConfigData.languageItemDatas.ContainsKey(id))
        {
            foreach (var data in languageConfigData.languageItemDatas[id])
            {
                if (data.language == language)
                    return data.value;
            }
        }
        return "";
    }*/
}



public struct TableData
{
    public string fieldName;
    public string type;
    public string value;

    public override string ToString()
    {
        return string.Format("fieldName:{0} type:{1} value:{2}", fieldName, type, value);
    }
}

public class ExcelList
{
    public int Vs;
    public List<string> excels;
    public int Strvs;
}

