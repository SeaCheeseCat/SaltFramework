using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 可交互的物体
/// 游戏里代指可移动到别的地方的
/// </summary>
public class InteractableObject : Item
{
   
    //Tip：完成状态
    [HideInInspector]
    public bool complete;
   

    public virtual void Awake()
    {
        OnCreate();
    }

    /// <summary>
    /// Callback:
    /// 物体创建回调
    /// </summary>
    public virtual void OnCreate() 
    { 
    }



    /// <summary>
    /// Callback:
    /// 拖动时执行
    /// </summary>
    public virtual void OnDray()
    {

    }

    /// <summary>
    /// Callback:
    /// 任务完成
    /// </summary>
    /// <param name="id"></param>
    public virtual void OnTaskComplete(int id) 
    {
        complete = true;
    }
}
