using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Manager<EventManager>
{
    // Tip: 已经完成的事件
    public List<int> completedEvents = new List<int>();
    public List<BaseGameEvent> eventDatas = new List<BaseGameEvent>();


    /// <summary>
    /// Trigger:
    /// 触发普通事件
    /// </summary>
    /// <param name="id">事件ID</param>
    public void TriggerEvent(string eventName)
    {
        DebugEX.Log("执行事件", eventName);
        var curEvent = InstanceGameEvent(eventName);
        if (curEvent != null)
        {
            var eventData = new EventData();
            eventData.args = null;
            eventData.id = 0;
            curEvent.TriggerEvent(eventData);
        }
    }

    /// <summary>
    /// Trigger:
    /// 触发普通事件
    /// </summary>
    /// <param name="id">事件ID</param>
    public void TriggerEvent<T>(int id, string[] args)
    {
        var curEvent = InstanceGameEvent(typeof(T).Name);
        if (curEvent != null)
        {
            var eventData = new EventData();
            eventData.args = args;
            eventData.id = id;
            curEvent.TriggerEvent(eventData);
            eventDatas.Add(curEvent);
        }
    }

 


    /// <summary>
    /// Trigger:
    /// 触发普通事件
    /// </summary>
    /// <param name="id">事件ID</param>
    /// <param name="eventName">事件名称</param>
    /// <param name="args">事件参数</param>
    public bool TriggerEvent(int id, string eventName, string[] args, bool isLoad)
    {
        if (GetCompleteEvent(id))
        {
            DebugEX.LogError("事件执行失效  事件已经被触发过一次", id);
            return false;
        }

        var curEvent = InstanceGameEvent(eventName);
        if (curEvent != null)
        {
            var eventData = new EventData();
            eventData.args = args;
            eventData.id = id;
            curEvent.TriggerEvent(eventData);
            eventDatas.Add(curEvent);
        }
        completedEvents.Add(id);
        return true;
    }
    
   
    public void Update()
    {
        foreach (var item in eventDatas)
        {
            item.Update();
        }
    }

    /// <summary>
    /// Get:
    /// 事件是否已经完成
    /// </summary>
    /// <param name="id">事件id</param>
    /// <returns>完成状态</returns>
    public bool GetCompleteEvent(int id)
    {
        foreach (var item in completedEvents)
        {
            if (item == id)
                return true;
        }
        return false;
    }

    /// <summary>
    /// Instance:
    /// 通过反射实例化一个事件类
    /// </summary>
    /// <param name="eventName">事件名称</param>
    /// <returns>事件类</returns>
    private BaseGameEvent InstanceGameEvent(string eventName)
    {
        var typeName = eventName;
        Type type = Type.GetType(typeName);
        if (type != null)
        {
            var ev = Activator.CreateInstance(type) as BaseGameEvent;
            return ev;
        }
        return null;
    }

    public void Recycle()
    { 
        completedEvents.Clear();
    }

}
