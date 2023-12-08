using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ConfigManager : Manager<ConfigManager>
{
    public const string RESOURCE_BASE_PATH = "Config/";

    private static Dictionary<string, BaseResData[]> s_cfgArrayDict = new Dictionary<string, BaseResData[]>();
    private static Dictionary<string, Dictionary<int, BaseResData>> s_cfgDict = new Dictionary<string, Dictionary<int, BaseResData>>();

    /// <summary>
    /// 获取配置表
    /// </summary>
    /// <returns>The config list.</returns>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public static T[] GetConfigList<T>() where T : BaseResData, new()
    {
        string fileName = typeof(T).Name;
        CheckOrInitConfig<T>();
        return s_cfgArrayDict[fileName] as T[];
    }

    /// <summary>
    /// 获取制定ID的配置
    /// </summary>
    /// <returns>The config.</returns>
    /// <param name="id">Identifier.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public static T GetConfigByID<T>(int id) where T : BaseResData, new()
    {
        string fileName = typeof(T).Name;
        CheckOrInitConfig<T>();

        if (s_cfgDict.ContainsKey(fileName))
        {
            var dict = s_cfgDict[fileName];
            dict.TryGetValue(id, out BaseResData data);
            return data as T;
        }
        else
        {
            Debug.LogError("未找到配置:"+fileName + ":id");
            return null;
        }
    }

    /// <summary>
    /// 是否存在制定ID的配置
    /// </summary>
    /// <returns><c>true</c>, if config was hased, <c>false</c> otherwise.</returns>
    /// <param name="id">Identifier.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public static bool CheckConfigByID<T>(int id) where T : BaseResData, new()
    {
        string fileName = typeof(T).Name;
        CheckOrInitConfig<T>();

        if (s_cfgDict.ContainsKey(fileName))
        {
            var dict = s_cfgDict[fileName];
            return dict.ContainsKey(id);
        }
        else
        {
            return false;
        }
    }

    private static void CheckOrInitConfig<T>() where T : BaseResData, new()
    {
        string fileName = typeof(T).Name;
        if (!s_cfgArrayDict.ContainsKey(fileName))
        {
            Type type = typeof(T);
            var property = type.GetProperty("HasKey");
            bool hasKey = (bool)property.GetValue(type);

            if (hasKey && !s_cfgDict.ContainsKey(fileName))
            {
                s_cfgDict.Add(fileName, new Dictionary<int, BaseResData>());
            }

            UnityEngine.Object res = ResourceManager.LoadPrefabSync(RESOURCE_BASE_PATH + fileName);
            TextAsset textAsset = res as TextAsset;

            string str = textAsset.text;
            string[] array = str.Split('\n');

            if (array.Length > 0)
            {
                List<T> dataList = new List<T>();
                for (int k = 0; k < array.Length; k++)
                {
                    if (array[k] != "" && array[0].Trim() != "")
                    {
                        T data = new T();
                        data.FillData(array[k]);
                        if(hasKey)
                        {
                            s_cfgDict[fileName].Add(data.ID, data);
                            dataList.Add(data);
                        }
                    }
                }
                s_cfgArrayDict.Add(fileName, dataList.ToArray());
            }
            //ResourceManager.UnloadAssetBundle(RESOURCE_BASE_PATH + fileName, true);
        }
    }
}
