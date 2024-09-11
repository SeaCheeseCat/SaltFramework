using UnityEngine;
using System.Collections;
using System;
using echobeast.resource.loader;

public class ResourceLoader
{
    /// <summary>
    /// 资源默认路径
    /// </summary>
    public static readonly string RESOURCE_BASE_PATH = "file://" + Application.streamingAssetsPath + "/resData/";
    
    /// <summary>
    /// 异步读取资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="action"></param>
    /// <returns></returns>
    public static IEnumerator LoadResource<T>(Action<T[]> action) where T : BaseResData , new ()
    {
        string fileName = typeof(T).Name;
        string path = RESOURCE_BASE_PATH + fileName + ".csv";

        WWW www = new WWW(path);
        if (string.IsNullOrEmpty(www.error))
        {
            yield return www;
            string str = System.Text.Encoding.Default.GetString(www.bytes);

            string[] array = str.Split('\n');
            yield return null;
            if (array.Length > 0)
            {
                T[] res = new T[array.Length];
                for (int k = 0; k < array.Length; k++)
                {
                    if (array[k] != "" && array[0].Trim()!="")
                    {
                        res[k] = new T();
                        res[k].FillData(array[k]);
                    }
                }
                action(res);
            }
            yield return null;
        }
    }

}
