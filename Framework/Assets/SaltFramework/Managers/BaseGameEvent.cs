using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGameEvent 
{
    /// <summary>
    /// 触发一个事件
    /// </summary>
    /// <param name="data">事件数据</param>
    public virtual void TriggerEvent(EventData data)
    {  
    }

    /// <summary>
    /// 通过参数直接使用事件
    /// </summary>
    /// <param name="args">参数的具体数值</param>
    public virtual void DirectTriggerEvent(int[] args) 
    {
    }


    public virtual void Update()
    { 
    
    }
}

//Tip: 基础的事件数据结构类
public class EventData 
{
    public int id;
    public string[] args;
}
