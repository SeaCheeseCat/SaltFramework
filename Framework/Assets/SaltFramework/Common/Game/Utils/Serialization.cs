using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializedDict<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField]
    List<TKey> keys;
    [SerializeField]
    List<TValue> values;

    public void OnBeforeSerialize ()
    {
        keys = new List<TKey> (Keys);
        values = new List<TValue> (Values);
    }

    public void OnAfterDeserialize ()
    {
        var count = Math.Min (keys.Count, values.Count);
        for (var i = 0; i < count; ++i)
        {
            Add (keys[i], values[i]);
        }
    }
}

[Serializable]
public class SerializedString2Int : SerializedDict<string, int>
{

}

[Serializable]
public class SerializedInt2string : SerializedDict<int, string>
{

}

[Serializable]
public class SerializedInt2Int:SerializedDict<int,int>
{

}