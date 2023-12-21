using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单例mono
/// </summary>
/*public abstract class SingleMono<T> : MonoBehaviour where T :SingleMono<T>
{
    *//*protected static T instance = null;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();

                if (FindObjectsOfType<T>().Length > 1)
                {
                    Debug.Log("找到对象");
                    return instance;
                }
                else
                {
                    Debug.Log("未找到对象");
                    return null;
                }

                //不需要自动实例化
                *//*if (instance == null)
                {
                    string instanceName = typeof(T).Name;
                    GameObject instanceGO = GameObject.Find(instanceName);

                    if (instanceGO == null)
                    {
                        throw new System.Exception("缺少实例对象");
                    }
                    instance = instanceGO.AddComponent<T>();
                    DontDestroyOnLoad(instanceGO);  //保证实例不会被释放
                }*//*
            }
            return instance;
        }
    }*//*

    public static T Instance { get; protected set; }
}*/

public class SingleMono<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance = null;
    public static T Instance
    {
        get
        {
            return instance;
        }
    }
    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = GetComponent<T>();
        }
    }
}


public class Singleton<T> where T : new()
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new T();
            }
            return instance;
        }
    }
}