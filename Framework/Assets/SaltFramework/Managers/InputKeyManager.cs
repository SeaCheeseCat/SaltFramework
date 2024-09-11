using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKeyManager : SingleMono<InputKeyManager>
{
    //Tip: Esc关闭按键的优先级缓存 （缓存的作用是是否打开SetUI)
    public List<int> escCache = new List<int>();
    //Tip: 浏览模式
    public bool browseMode = false;
    //Tip: 待操作集
    public List<KeyCode> tobeKeyRun = new List<KeyCode>();

    /// <summary>
    /// Add:
    /// 添加一个待执行操作
    /// </summary>
    /// <param name="code"></param>
    public void AddTobeKey(KeyCode code)
    {
        tobeKeyRun.Add(code);
    }

    /// <summary>
    /// Remove:
    /// 移除一个待执行操作
    /// </summary>
    /// <param name="code"></param>
    public void RemoveTobeKey(KeyCode code)
    { 
        tobeKeyRun.Remove(code);
    }

    /// <summary>
    /// Add:
    /// 添加一个Esc缓存
    /// </summary>
    /// <param name="id"></param>
    public void AddEscCache(int id)
    {
        escCache.Add(id);
    }
    
    /// <summary>
    /// Remove:
    /// 移除一个Esc缓存
    /// </summary>
    /// <param name="id"></param>
    public void RemoveEscCache(int id)
    {
        escCache.Remove(id);
    }

    public bool IsEscCache(int id)
    {
        return escCache.Contains(id);
    }

    public int GetEscCacheCount()
    {
        return escCache.Count;
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        OnAngKeyDown();
        OnConditionsKeyDown();
    }

    public void OnAngKeyDown()
    { 
        
    }
    
    /// <summary>
    /// Callback:
    /// 当存在条件执行的操作
    /// </summary>
    public void OnConditionsKeyDown()
    {
        
    }

    /// <summary>
    /// Callback:
    /// 关卡中的按键按下
    /// </summary>
    public void OnGameInitKeyDown()
    {
        if (IsCanDoRun(KeyCode.M))
        {
            
        }

       
    }

   

    /// <summary>
    /// Callback:
    /// 关卡中的按键按下
    /// </summary>
    public void OnGameLevelKeyDown()
    {
       
        if (Input.GetKeyDown(KeyCode.Escape) && escCache.Count <= 0)
        {
           
        }
        else if (browseMode)
        {
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioManager.Instance.PlayEffic(1004);
        }

        OnGameLevelLongKeyDown();
    }

    /// <summary>
    /// Callback:
    /// 当在游戏中长按操作
    /// </summary>
    public void OnGameLevelLongKeyDown()
    {   

    }


    /// <summary>
    /// Is:
    /// 从操作集中查看是否有待执行操作
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public bool IsCanDoRun(KeyCode code)
    {
        foreach (var item in tobeKeyRun)
        {
            if (item == code)
                return true;
        }
        return false;
    }
}
