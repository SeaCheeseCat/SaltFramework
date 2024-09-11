using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ViewEvent : BaseGameEvent
{
    private EventData eventData;
    public override void TriggerEvent(EventData data)
    {
        base.TriggerEvent(data);
        this.eventData = data;
        var cardid = int.Parse(eventData.args[0]);
        var viewID = int.Parse(eventData.args[1]);
    }

    public override void Update()
    {
       
    }


    public void OnEventEnd()
    {
        
    }
}
