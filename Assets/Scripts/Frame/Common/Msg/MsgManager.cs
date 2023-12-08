using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 消息管理器
/// </summary>
public class MsgManager : Manager<MsgManager>
{
    /// <summary>
    /// 消息映射表
    /// </summary>
    /// <returns></returns>
    private Dictionary<string, List<MsgHandler>> m_table = new Dictionary<string, List<MsgHandler>>();

    /// <summary>
    /// 正在处理中的消息
    /// </summary>
    /// <returns></returns>
    private HashSet<List<MsgHandler>> m_invoking = new HashSet<List<MsgHandler>>();

    public override IEnumerator Init(MonoBehaviour obj)
    {
        m_table.Clear();
        return base.Init(obj);
       
    }

    /// <summary>
    /// 发送消息,无参数版本
    /// </summary>
    /// <param name="msgType"></param>
    public void SendMessage (string msgType)
    {
        MsgData msg = MsgData.Obtain ();
        SendMessage (msgType, msg);
    }

    /// <summary>
    /// 发送消息,无参数版本
    /// </summary>
    /// <param name="msgType"></param>
    public void SendMessage(string msgType,bool boolean,int arg1)
    {
        MsgData msg = MsgData.Obtain();
        msg.boolArg = boolean;
        msg.arg1 = arg1;
        SendMessage(msgType, msg);
    }

    /// <summary>
    /// 发送消息,单个整形参数版本
    /// </summary>
    /// <param name="msgType"></param>
    /// <param name="arg"></param>
    public void SendMessage (string msgType, int arg,eInfoType type = eInfoType.normal)
    {
        MsgData msg = MsgData.Obtain ();
        msg.arg1 = arg;
        msg.type = type;
        SendMessage (msgType, msg);
    }

    public void SendMessage(string msgType,int arg,object obj, eInfoType type = eInfoType.normal)
    {
        MsgData msg = MsgData.Obtain();
        msg.arg1 = arg;
        msg.data = obj;
        msg.type = type;
        SendMessage(msgType, msg);
    }

    public void SendMessage(string msgType,int arg,int arg2,eInfoType  type = eInfoType.normal)
    {
        MsgData msg = MsgData.Obtain();
        msg.arg1 = arg;
        msg.arg2 = arg2;
        msg.type = type;
        SendMessage(msgType, msg);
    }

    public void SendMessage(string msgType, int arg, int arg2, string arg3, eInfoType type = eInfoType.normal)
    {
        MsgData msg = MsgData.Obtain();
        msg.arg1 = arg;
        msg.arg2 = arg2;
        msg.msg1 = arg3;
        SendMessage(msgType, msg);
    }


    public void SendMessage(string msgType, int arg, int arg2,int arg3, eInfoType type = eInfoType.normal)
    {
        MsgData msg = MsgData.Obtain();
        msg.arg1 = arg;
        msg.arg2 = arg2;
        msg.arg3 = arg3;
        msg.type = type;
        SendMessage(msgType, msg);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="msgType"></param>
    /// <param name="boolean"></param>
    /// <param name="vec3"></param>
    public void SendMessage(string msgType,bool boolean,Vector3 vec3, eInfoType type = eInfoType.normal)
    {
        MsgData msg = MsgData.Obtain();
        msg.boolArg = boolean;
        msg.vec3 = vec3;
        msg.type = type;
        SendMessage(msgType, msg);
    }

    /// <summary>
    /// 发送消息,布尔型版本
    /// </summary>
    /// <param name="msgType"></param>
    /// <param name="arg"></param>
    public void SendMessage (string msgType, bool arg, eInfoType type = eInfoType.normal)
    {
        MsgData msg = MsgData.Obtain ();
        msg.boolArg = arg;
        msg.type = type;
        SendMessage (msgType, msg);
    }

    /// <summary>
    /// 发送消息,字符串版本
    /// </summary>
    /// <param name="msgType"></param>
    /// <param name="arg"></param>
    public void SendMessage (string msgType, string arg, eInfoType type = eInfoType.normal)
    {
        MsgData msg = MsgData.Obtain ();
        msg.msg1 = arg;
        msg.type = type;
        SendMessage (msgType, msg);
    }

    public void SendMessage(string msgType,string msg1,object data,eInfoType type = eInfoType.normal)
    {
        MsgData msg = MsgData.Obtain();
        msg.msg1 = msg1;
        msg.data = data;
        msg.type = type;
        SendMessage(msgType, msg);
    }

    /// <summary>
    /// 发送消息,通用类型版本
    /// </summary>
    /// <param name="msgType"></param>
    /// <param name="data"></param>
    public void SendMessage (string msgType, object data,eInfoType type = eInfoType.normal)
    {
        MsgData msg = MsgData.Obtain ();
        msg.data = data;
        msg.type = type;
        SendMessage (msgType, msg);
    }

    public void SendMessage(string msgType, float float1, eInfoType type = eInfoType.normal)
    {
        MsgData msg = MsgData.Obtain();
        msg.float1 = float1;
        msg.type = type;
        SendMessage(msgType, msg);
    }

    public void SendMessage(string msgtype,int arg1,float float1, eInfoType type = eInfoType.normal)
    {
        MsgData msg = MsgData.Obtain();
        msg.arg1 = arg1;
        msg.float1 = float1;
        msg.type = type;
        SendMessage(msgtype,msg);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="msgType">消息类型</param>
    /// <param name="msg">消息内容</param>
    public void SendMessage (string msgType, MsgData msg)
    {
        if (string.IsNullOrEmpty (msgType))
        {
            Debug.LogError ("A notification name is required");
            return;
        }

        if (!m_table.ContainsKey (msgType))
        {
            return;
        }

        List<MsgHandler> handlers = m_table[msgType];
        m_invoking.Add (handlers);
        for (int i = 0; i < handlers.Count; ++i)
        {
            handlers[i](msg);
        }

        m_invoking.Remove (handlers);
        if(msg!= null)
         msg.Release();
    }

    /// <summary>
    /// 注册消息监听
    /// </summary>
    /// <param name="msgType">消息类型</param>
    /// <param name="handler">消息委托</param>
    public void AddObserver (string msgType, MsgHandler handler)
    {
        if (!m_table.ContainsKey (msgType))
        {
            m_table.Add (msgType, new List<MsgHandler> ());
        }

        List<MsgHandler> list = m_table[msgType];
        if (!list.Contains (handler))
        {
            if (m_invoking.Contains (list))
                m_table[msgType] = list = new List<MsgHandler> (list);
            list.Add (handler);
        }
    }

    /// <summary>
    /// 注销消息监听
    /// </summary>
    /// <param name="msgType">消息类型</param>
    /// <param name="handler">消息委托</param>
    public void RemoveObserver (string msgType, MsgHandler handler)
    {
        if (!m_table.ContainsKey (msgType))
        {
            return;
        }

        List<MsgHandler> list = m_table[msgType];
        int index = list.IndexOf (handler);
        if (index != -1)
        {
            if (m_invoking.Contains (list))
            {
                m_table[msgType] = list = new List<MsgHandler> (list);
            }
            list[index] = list[list.Count - 1];
            list.RemoveAt (list.Count - 1);
        }
    }
}

/// <summary>
/// 消息处理委托
/// </summary>
/// <param name="msg"></param>
public delegate void MsgHandler (MsgData msg);

/// <summary>
/// 消息委托
/// </summary>
/// <param name="msg"></param>
public delegate void OldMsgHandler (params object[] msg);

/// <summary>
/// 消息内容,
/// 本类型对象将使用对象池循环利用,减少实例化产生的碎片
/// 同时也提供一系列基础类型参数避免装箱拆箱
/// 本类型对象禁止在外部持有
/// </summary>
public class MsgData
{
    public eInfoType type;

    public int arg1;
    public int arg2;
    public int arg3;

    public bool boolArg;
    public bool boolArg2;

    public string msg1;
    public string msg2;

    public float float1;
    public float float2;

    public object data;

    public Vector3 vec3;
    


    //消息池
    private static Queue<MsgData> m_msgQueue = new Queue<MsgData> ();

    /// <summary>
    /// 获取消息对象
    /// </summary>
    /// <returns></returns>
    public static MsgData Obtain ()
    {
        if (m_msgQueue.Count > 0)
        {
            return m_msgQueue.Dequeue ();
        }
        else
        {
            return new MsgData ();
        }
    }

    /// <summary>
    /// 释放消息对象
    /// </summary>
    public void Release ()
    {
        arg1 = 0;
        arg2 = 0;
        arg3 = 0;
        boolArg = false;
        boolArg2 = false;
        msg1 = null;
        data = null;
        m_msgQueue.Enqueue (this);
    }

}

public enum eInfoType
{
    normal,
    error,
    good,
    bigError
}