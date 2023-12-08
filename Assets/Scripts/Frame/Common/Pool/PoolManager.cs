using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*using RpcData;*/

/// <summary>
/// 对象池管理
/// </summary>
public class PoolManager : Manager<PoolManager>
{
    /*  public ClassPool<AttackAction> AttackAction;    //攻击数据
      public ClassPool<DamageAction> DamageAction;     //技能数据
      public ClassPool<HealAction> HealAction;    //治疗数据
    */
    private bool alinit = false;
    //public ClassPool<MapLineData> MapLineDataAction;    //攻击数据
    //public ClassPool<PassKeyData> PassKeyDataAction;    //密钥数据
    /*public ClassPool<UnitData> UnitDataAction;    //实体连接数据
    public ClassPool<UnitData> LineUnitDataAction;    //实体连接数据*/

    public const string InitPoolAB_NAME = "System/PoolRoot";

    public GameObjectPool TextChatUnitPool;   //聊天对象池
    public GameObjectPool LetterItemPool;  //信件条对象池
    public GameObjectPool ChatItemPool;  //聊天对象池

    public GameObjectPool MapLinePool;   //地图线的对象池

    public GameObjectPool SockNodePool;   //地图线的对象池

    public GameObjectPool ItemPool;   //物体对象池
    
    public GameObjectPool UISideLabelPool;   //打开UI后侧边栏的Label对象池
    public GameObjectPool VideoItemPool;   //Video物体
    public GameObjectPool RoleSumSignItemPool;   //人物解锁总结  小物体 对象池
    public GameObjectPool EffectItemPool;  //特效 对象池

   

    /*public GameObjectPool Lines;    //线条管理
    public GameObjectPool HeadTipSlot;  //单位头顶提示
    public GameObjectPool TakeSlot;  //已读消息*/

    /*   public ClassPool<ItemConnect> ItemConnects;
       public ClassPool<TakeNote> TakeNotes;
       public ClassPool<TipNote> TipNotes;
       public ClassPool<RoleNoteData> RoleNoteDatas;
       public ClassPool<NpcConnect> NpcConnects;*/

    public  Dictionary<int, GameObjectPool> objpool = new Dictionary<int, GameObjectPool>();   //对象链表
    private InitPool pooldata;

    public override IEnumerator Init(MonoBehaviour obj)
    {
        yield return base.Init(obj);

        /*  AttackAction = new ClassPool<AttackAction>(5);s
          DamageAction = new ClassPool<DamageAction>(5);
          HealAction = new ClassPool<HealAction>(5);*/
        if (!alinit)
        {
            /*MapLineDataAction = new ClassPool<MapLineData>(10);
            PassKeyDataAction = new ClassPool<PassKeyData>(10);*/
            /* UnitDataAction = new ClassPool<UnitData>(10);
               LineUnitDataAction = new ClassPool<UnitData>(10);*/


            GameObject poolRoot = ResourceManager.AllocObjectSync(InitPoolAB_NAME);
            poolRoot.transform.SetParent(obj.transform);
            pooldata = poolRoot.GetComponent<InitPool>();


            TextChatUnitPool = new GameObjectPool(1, "TextChatUnitPool", poolRoot.transform, 5);
            LetterItemPool = new GameObjectPool(2, "LetterLayoutPool", poolRoot.transform, 7);
            MapLinePool = new GameObjectPool(3, "MapLinePool", poolRoot.transform, 8);
            ChatItemPool = new GameObjectPool(4, "ChatItemPool", poolRoot.transform, 30);
            SockNodePool = new GameObjectPool(5, "SockNodePool", poolRoot.transform, 8);
            ItemPool = new GameObjectPool(6, "ItePool", poolRoot.transform, 8);
            UISideLabelPool = new GameObjectPool(7, "UISideLabelPool", poolRoot.transform, 3);
            VideoItemPool = new GameObjectPool(8, "VideoItemPool", poolRoot.transform, 5);
            RoleSumSignItemPool = new GameObjectPool(9, "RoleSumSignItemPool", poolRoot.transform, 5);
            EffectItemPool = new GameObjectPool(10, "EffectItemPool", poolRoot.transform, 5);
            InitData();

        alinit = true;
        }

        //TextChatUnitPool = new ClassPool<TextChatUnitPool>(5); 
        /* ItemConnects = new ClassPool<ItemConnect>(5);
         TakeNotes = new ClassPool<TakeNote>(5);
         TipNotes = new ClassPool<TipNote>(5);
         RoleNoteDatas = new ClassPool<RoleNoteData>(5);
         NpcConnects = new ClassPool<NpcConnect>(5);*/
        //ChatBoxPool = new GameObjectPool(1,"ChatBox", )
        //NpcConnects = new ClassPool<NpcConnect>(5);

        //Lines = new GameObjectPool(1,"LineUnit", poolRoot.transform, 3);
        //HeadTipSlot = new GameObjectPool(3,"HeadTipSlot", poolRoot.transform, 3);
        //TakeSlot = new GameObjectPool(4, "TakeSlot", poolRoot.transform, 5);
        //使用Objcet  初始化对象池数量   也可以通过 调用对象池的InitPerfab方法进行初始化 

    }

   
    public void InitData() {
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
/// 回收对象扩展方法
/// </summary>
public static class PoolRecycleExtend
{
    /// <summary>
    /// 回收攻击数据
    /// </summary>
    /// <param name="action"></param>
    /*public static void Recycle(this MapLineData action)
    {
        PoolManager.Instance.MapLineDataAction.Recycle(action);
    }

    public static void Recycle(this PassKeyData action)
    {
        PoolManager.Instance.PassKeyDataAction.Recycle(action);
    }*/

    /*    public static void Recycle(this UnitData action)
        {
            PoolManager.Instance.UnitDataAction.Recycle(action);
        }*/

    /* /// <summary>
     /// 回收攻击数据
     /// </summary>
     /// <param name="action"></param>
     public static void Recycle(this ItemConnect action)
     {
         PoolManager.Instance.ItemConnects.Recycle(action);
     }

     /// <summary>
     /// 回收 对话记录数据
     /// </summary>
     /// <param name="action"></param>
     public static void Reccyle(this TakeNote action)
     {
         PoolManager.Instance.TakeNotes.Recycle(action);
     }

     public static void Recyle(this TipNote tipNote)
     {
         PoolManager.Instance.TipNotes.Recycle(tipNote);
     }

     public static void Recyle(this RoleNoteData roleNoteData)
     {
         PoolManager.Instance.RoleNoteDatas.Recycle(roleNoteData);
     }

     public static void Recyle(this NpcConnect data)
     {
         PoolManager.Instance.NpcConnects.Recycle(data);
     }*/

}

    /// <summary>
    /// 回收对象扩展方法
    /// </summary>
    /*public static class PoolRecycleExtend
    {
        /// <summary>
        /// 回收攻击数据
        /// </summary>
        /// <param name="action"></param>
        public static void Recycle(this AttackAction action)
        {
            PoolManager.Instance.AttackAction.Recycle(action);
        }

        /// <summary>
        /// 回收伤害数据
        /// </summary>
        /// <param name="action"></param>
        public static void Recycle(this DamageAction action)
        {
            PoolManager.Instance.DamageAction.Recycle(action);
        }

        /// <summary>
        /// 回收治疗数据
        /// </summary>
        /// <param name="action"></param>
        public static void Recycle(this HealAction action)
        {
            PoolManager.Instance.HealAction.Recycle(action);
        }
    }
    */
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