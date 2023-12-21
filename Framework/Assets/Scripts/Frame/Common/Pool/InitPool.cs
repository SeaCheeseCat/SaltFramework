using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  InitPool : MonoBehaviour
{
    public PoolItem[] PoolItems;
}

[System.Serializable]
public class PoolItem {
    public  GameObject Obj;
    public string path;
    public int num;
    public int PoolIndex;
}
