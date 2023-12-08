using System.Collections;
using System.Collections.Generic;
//using TapTap.TapDB;
using UnityEngine;

public class DBMgr : Singleton<DBMgr>
{
    public void TrackEvent(string name, object data)
    {
        string properties = string.Format("{0}{1}", "#", name);
        DebugEX.Log("Event", properties);
        //TapDB.TrackEvent(properties, JsonUtility.ToJson(data));
    }
    public void TrackEvent(string name, string data = null)
    {
       // TapDB.TrackEvent(name, data);
    }
    public void AddEvent(string name)
    {
        string properties = string.Format("{{\"{0}{1}\":1}}","#", name);
        DebugEX.Log("UserAdd", properties);
       // TapDB.DeviceAdd(properties);
       
    }
}
