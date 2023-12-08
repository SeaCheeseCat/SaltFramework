using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*using RpcData;*/

/// <summary>
/// 对象池管理
/// </summary>
public class PoolManager : Manager<PoolManager>
{
    private bool alinit = false;
    public const string InitPoolAB_NAME = "System/PoolRoot";
    public GameObjectPool ChatItemPool;  //聊天对象池
    public GameObjectPool EffectItemPool;  //特效 对象池
    public  Dictionary<int, GameObjectPool> objpool = new Dictionary<int, GameObjectPool>();   //对象链表
    private InitPool pooldata;

    public override IEnumerator Init(MonoBehaviour obj)
    {
        yield return base.Init(obj);

        if (!alinit)
        {
            GameObject poolRoot = ResourceManager.AllocObjectSync(InitPoolAB_NAME);
            poolRoot.transform.SetParent(obj.transform);
            pooldata = poolRoot.GetComponent<InitPool>();         
            ChatItemPool = new GameObjectPool(4, "ChatItemPool", poolRoot.transform, 30);
            EffectItemPool = new GameObjectPool(10, "EffectItemPool", poolRoot.transform, 5);
            InitData();
            alinit = true;
        }
    }

   
    public void InitData() 
    {
        if (GameBase.Instance != null)
        {
            if (GameBase.Instance.InitPoolMode == InitPoolEnum.OnByPoolObj)
            {
                InitByPoolObj();
            }
            else if (GameBase.Instance.InitPoolMode == InitPoolEnum.OnByScript)
            {
                InitByScript();
            }
        }
    }


    /// <summary>
    /// 通过配置好的System/PoolRoot  进行对象池的初始化
    /// </summary>
    private void InitByPoolObj() {
        PoolItem[] items = pooldata.PoolItems;
        for (int i = 0; i < items.Length; i++)
        {
            var item = items[i];
            var pool = objpool[item.PoolIndex];
            if (item.Obj == null && item.path == "")
                continue;
            else if (item.path != null)
                pool.Spawn(item.path, item.num);
            else if (item.Obj != null)
                pool.Spawn(item.Obj, item.num);
        }
    }

    /// <summary>
    /// 通过手写进行初始化
    /// </summary>
    private void InitByScript() {
       /* Lines.Spawn("Item/gameobject", 2);*/
    }
}   

    /// <summary>
    /// 类对象池
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ClassPool<T> where T : class, IPoolable, new()
{
    protected Stack<T> pool = new Stack<T>();   //对象链表
    protected int maxCount; //缓存个数,在某些时候清理缓存

    public ClassPool(int max = 0)
    {
        maxCount = max;
    }

    /// <summary>
    /// 从池里面取类对象
    /// </summary>
    public T Spawn()
    {
        T rtn;
        if (pool.Count > 0)
        {
            rtn = pool.Pop();
        }
        else
        {
            rtn = new T();
        }

        rtn.OnSpawn();
        return rtn;
    }

    /// <summary>
    /// 回收类对象
    /// </summary>
    public bool Recycle(T obj)
    {
        if (obj == null)
            return false;
        if (pool.Count >= maxCount)
        {
            obj = null;
            return false;
        }
        obj.OnRecycle();
        pool.Push(obj);
        return true;
    }

    /// <summary>
    /// 释放所有对象
    /// </summary>
    public void Release()
    {
        pool.Clear();
    }
}

/// <summary>
/// Gameobject对象池
/// </summary>
public class GameObjectPool
{
    protected Dictionary<string,ObjPoolData> pool = new Dictionary<string, ObjPoolData>();   //对象链表
    protected Transform node;   //缓存节点
    protected int limit;
    protected int index;
    public GameObjectPool(int index,string name,Transform root,int max = 0)
    {
        limit = max;
        this.index = index;
        var obj = new GameObject(name);
        obj.transform.ResetParent(root);
        this.node = obj.transform;
        PoolManager.Instance.objpool.Add(index, this);
    }
    
    public void InitPerfab(string path, int  num)
    {
        Spawn(path, num);
    }

    /// <summary>
    /// 设置对象池数量
    /// </summary>
    /// <param name="count"></param>
    public void SetPoolLimit(string name, int count)
    {
        if (!pool.ContainsKey(name))
        {
            var data = new ObjPoolData();
            pool.Add(name, data);
        }
        pool[name].max = count;
    }

    /// <summary>
    /// 从池里面取类对象
    /// </summary>
    public GameObject Spawn(string path)
    {
        if (!pool.ContainsKey(path))
        {
            var data = new ObjPoolData();
            data.max = limit;
            pool.Add(path, data);
            DebugEX.Log("对象池创建新的");
        }
        GameObject rtn;
        if (pool[path].Objs.Count > 0)
        {
           
            rtn = pool[path].Objs.Pop();
            if (rtn == null)
            {
                rtn = ResourceManager.AllocObjectSync(path);
                rtn.AddComponent<PoolData>().key = path;
            }
        }
        else
        {
            rtn = ResourceManager.AllocObjectSync(path);
            rtn.AddComponent<PoolData>().key = path;
        }
        
        var p = rtn.GetComponent<IPoolable>();
        if (p != null)
        {
            p.OnSpawn();
        }
        rtn.transform.SetParent(null);
        rtn.SetActive(true);
        return rtn;
    }

    public void Spawn(string path, int num)
    {
        for (int i = 0; i < num; i++)
        {
            var item = Spawn(path);
            item.transform.ResetParent(node);
        }
    }


    /// <summary>
    /// 从池里面取类对象  
    /// </summary>
    public GameObject Spawn(GameObject obj)
    {
        var path = obj.GetPrefabAssetPath();
        var npath = path.Replace("Assets/Resources/","");
        var nnpath = npath.Replace(".prefab","");
        return Spawn(nnpath);
    }


    /// <summary>
    /// 从池里面取类对象   用于初始化对象  
    /// </summary>
    public void Spawn(GameObject obj,  int num)
    {
        for (int i = 0; i < num; i++)
        {
            var item = Spawn(obj);
            item.transform.ResetParent(node);
        }
    }

    /// <summary>
    /// 回收类对象,在回收之前需要清理干净
    /// </summary>
    public bool Recycle(GameObject obj)
    {
        if (obj == null)
            return false;
        var path = obj.GetComponent<PoolData>().key;
        if (!pool.ContainsKey(path))
        {
            Object.Destroy(obj);
            return false;
        }
        if (pool[path].Objs.Count >= pool[path].max)
        {
            //Debug.Log("超出限制，直接删除");
            Object.Destroy(obj);
            return false;
        }
        obj.transform.ResetParent(node);
        obj.SetActive(false);
        var p = obj.GetComponent<IPoolable>();
        if (p != null)
        {
            p.OnRecycle();
        }
        pool[path].Objs.Push(obj);
        //Debug.Log("回收对象" + path);
        return true;
    }



    /// <summary>
    /// 释放所有对象
    /// </summary>
    public void Release()
    {
        foreach(var v in pool)
        {
            while (v.Value.Objs.Count > 0)
            {
                var obj = v.Value.Objs.Pop();
                Object.Destroy(obj);
            }
        }

        pool.Clear();
    }

    /// <summary>
    /// 对象池数据
    /// </summary>
    public class ObjPoolData
    {
        public Stack<GameObject> Objs = new Stack<GameObject>();
        public int max;
    }
}

/// <summary>
/// 可用对象池
/// </summary>
public interface IPoolable
{
    public abstract void OnSpawn();

    public abstract void OnRecycle();
}


//是否打开  初始化对象池的方法
public enum InitPoolEnum { 
    Off,   
    OnByPoolObj,//通过配置PoolObj初始化
    OnByScript   //通过在脚本里写初始化
}