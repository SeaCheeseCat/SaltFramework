using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class EditTool 
{
    public static System.Type GetScriptType(string scriptName)
    {
        // 获取已加载的程序集
        Assembly[] assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
        foreach (var assembly in assemblies)
        {
            // 在每个程序集中查找指定名称的类型
            System.Type type = assembly.GetType(scriptName);
            if (type != null)
            {
                // 如果找到类型，则返回
                return type;
            }
        }
        return null;
    }


    public static GameObject CreatePrefabToScene(string path, string name)
    {
        GameObject obj = Resources.Load<GameObject>(path);
        obj = GameObject.Instantiate(obj);
        obj.name = name;
        return obj;
    }


}
