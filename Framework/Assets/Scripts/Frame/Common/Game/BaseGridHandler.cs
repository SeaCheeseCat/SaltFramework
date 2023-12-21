using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// 格子操作处理逻辑,战场上和UI中的格子都由本类派生
/// </summary>
public class BaseGridHandler : MonoBehaviour
{

    Dictionary<EventTriggerType, EventTrigger.Entry> Entry = new Dictionary<EventTriggerType, EventTrigger.Entry>();


    public void AddTriggersListener(EventTriggerType eventID, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = GetComponent<EventTrigger>();
      
        if (trigger == null)
        {
            trigger = gameObject.AddComponent<EventTrigger>();
        }

        if (trigger.triggers.Count == 0)
        {
            trigger.triggers = new List<EventTrigger.Entry>();
        }

        UnityAction<BaseEventData> callback = new UnityAction<BaseEventData>(action);
        EventTrigger.Entry entry;
        if (Entry.ContainsKey(eventID))
        {
            entry = Entry[eventID];
        }
        else
        {
            entry = new EventTrigger.Entry();
            entry.eventID = eventID;
            trigger.triggers.Add(entry);
        }
        entry.callback.AddListener(callback);
    }


    //把事件透下去
    public void PassEvent<T>(PointerEventData data, ExecuteEvents.EventFunction<T> function)where T : IEventSystemHandler
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(data, results);
        GameObject current = data.pointerCurrentRaycast.gameObject;
        for (int i = 0; i < results.Count; i++)
        {
            if (current != results[i].gameObject)
            {
                ExecuteEvents.Execute(results[i].gameObject, data, function);
            }
        }
    }




    public void RemoveTriggersListener(EventTriggerType eventID)
    {
        if (Entry.ContainsKey(eventID))
        {
            EventTrigger.Entry entry = Entry[eventID];
            entry.callback.RemoveAllListeners();
        }
    }

    public virtual void Clear()
    {
        Entry.Clear();
        EventTrigger trigger = GetComponent<EventTrigger>();
        if (trigger != null)
        {
            trigger.triggers.Clear();
        }
    }
}