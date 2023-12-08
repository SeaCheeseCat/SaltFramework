using echo.common.structures;
using System.Collections;
using UnityEngine;

public abstract class Manager<T> : Singleton<T> where T : new ()
{
    public virtual void InitializeManager (MonoBehaviour obj) { }

    //初始化协程
    public virtual IEnumerator Init(MonoBehaviour obj)
    {
        yield return null;
    }
}