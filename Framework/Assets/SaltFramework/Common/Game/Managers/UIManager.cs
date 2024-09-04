using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIManager : Singleton<UIManager>
{
    //Tip: 父类的Canvas
    private Transform parentCanvas;
    //Tip: UI数据
    public Dictionary<string, UIBase> uis = new Dictionary<string, UIBase>();
    //Tip: Canvas
    public Transform Canvas;

    /// <summary>
    /// Init:
    /// 初始化UI框架:Auto自动搜索
    /// </summary>
    public void Init() 
    {
        this.parentCanvas = GameObject.Find("Canvas").transform;
    }
    
    /// <summary>
    /// Init:
    /// 初始化
    /// </summary>
    /// <param name="Canvas"></param>
    /// <param name="parentCanvas"></param>
    public void Init(Transform Canvas, Transform parentCanvas)
    {
        this.Canvas = Canvas;
        this.parentCanvas = parentCanvas;
    }
    
    /// <summary>
    /// Open:
    /// 打开UI
    /// </summary>
    /// <param name="UiName">UI名称</param>
    /// <param name="dialogArgs">参数</param>
    public UIBase OpenUI(string UiName, params object[] dialogArgs)
    {
        if (!uis.ContainsKey(UiName))
        {  
            GameObject go = LoadUiWithRes(UiName);
            UIBase uibase = go.GetComponent<UIBase>();
            go.transform.SetParent(parentCanvas);
            uis.Add(UiName, uibase);
            uibase.Init(dialogArgs);
            uibase.OnOpen();
            uibase.OpenWithAnimation();
            go.transform.localPosition = new Vector3(0, 0, 0);
            return uibase;
        }
        else 
        {
            var ui = uis[UiName];
            if (ui.type == UiType.INITEXIST)
            {
                GameObject go = LoadUiWithUis(UiName);
                UIBase uibase = go.GetComponent<UIBase>();
                //go.transform.SetParent(parentCanvas);
                //uis.Add(UiName, uibase);
                uibase.Init(dialogArgs);
                uibase.OnOpen();
                uibase.OpenWithAnimation();
                //go.transform.localPosition = new Vector3(0, 0, 0);
                return uibase;
            }
            else if (ui.type == UiType.Tip)
            {
                GameObject go = LoadUiWithRes(UiName);
                UIBase uibase = go.GetComponent<UIBase>();
                go.transform.SetParent(parentCanvas);
                uibase.Init(dialogArgs);
                uibase.OnOpen();
                uibase.OpenWithAnimation();
                go.transform.localPosition = new Vector3(0, 0, 0);
                return uibase;
            }
        }

        return null;
    }
    
    /// <summary>
    /// Open:
    /// 打开UI
    /// </summary>
    /// <typeparam name="T">UI类型</typeparam>
    /// <param name="dialogArgs">UI的参数</param>
    public T OpenUI<T>(params object[] dialogArgs) where T : UIBase
    {
        string typename = typeof(T).Name;
        var uiBase = OpenUI(typename, dialogArgs);
        if (uiBase != null)
            return uiBase as T;
        return default(T);
    }

    /// <summary>
    /// Register:
    /// 注册UI（使用于本身就在场景上存在的UI)
    /// </summary>
    public void RegisterUI(UIBase Uibase, params object[] dialogArgs)
    {
        if (!uis.ContainsKey(Uibase.name))
        {
            uis.Add(Uibase.name, Uibase);
            Uibase.Init(dialogArgs);
        }
    }

    /// <summary>
    /// Close:
    /// 关闭UI
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void CloseUI<T>()
    {
        string typename = typeof(T).Name;
        CloseUI(typename);
    }

    /// <summary>
    /// Get:
    /// 获取一个UI
    /// </summary>
    /// <param name="UiName">ui名称</param>
    /// <returns>UI</returns>
    public UIBase GetUI(string UiName)
    {
        if (!uis.ContainsKey(UiName))
            return null;
        return uis[UiName];
    }

    /// <summary>
    /// Get:
    /// 获取一个UI
    /// </summary>
    /// <typeparam name="T">UI类型</typeparam>
    /// <returns>UI</returns>
    public T GetUI<T>() where T: UIBase
    {
        string typename = typeof(T).Name;
        if (!uis.ContainsKey(typename))
            return null;
        return uis[typename] as T;
    }

    /// <summary>
    /// Close:
    /// 关闭一个UI
    /// </summary>
    /// <param name="UiName"></param>
    public void CloseUI(string UiName)
    {
        if (uis.ContainsKey(UiName))
        {
            var ui = uis[UiName];
            ui.OnDeath();
            ui.Close();
            if (ui.type == UiType.COMMON)
                uis.Remove(UiName);
        }
    }

    /// <summary>
    /// Close:
    /// 关闭所有UI
    /// </summary>
    public void CloseAllUI()
    {
        foreach (var ui in uis.Values)
        {
            ui.Recycle();
            ui.OnDeath();
            PoolManager.Instance.UiUnitPool.Recycle(ui.gameObject);
        }
        uis.Clear();
    }

    /// <summary>
    /// Destroy:
    /// 销毁全部UI
    /// </summary>
    public void DestroyAllUI()
    {
        foreach (var ui in uis.Keys)
        {
            DestroyUI(ui);
        }
        uis.Clear();
    }

    /// <summary>
    /// Destroy:
    /// 销毁一个UI
    /// </summary>
    /// <param name="UiName"></param>
    public void DestroyUI(string UiName)
    {
        if (uis.ContainsKey(UiName))
        {
            var ui = uis[UiName];
            ui.OnDeath();
            ui.gameObject.SetActive(false);
            uis.Remove(UiName);
        }
    }

    /// <summary>
    /// Load:
    /// 载入一个UI资源
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    private GameObject LoadUiWithRes(string name)
    {
        GameObject obj = PoolManager.Instance.UiUnitPool.Spawn("UI/" + name);
        if (obj == null)
        {
            obj = ResourceManager.LoadPrefabSync("UI/" + name) as GameObject;
            obj.name = name;
        }
        return obj;
    }
    
    /// <summary>
    /// Load:
    /// 载入一个UI资源
    /// </summary>
    /// <returns></returns>
    private GameObject LoadUiWithUis(string name)
    {
        uis[name].gameObject.SetActive(true);
        return uis[name].gameObject;
    }

    /// <summary>
    /// Open:
    /// 打开一个通用游戏弹出框
    /// </summary>
    /// <param name="title">标题</param>
    /// <param name="content">内容</param>
    /// <param name="yesAction">确定事件</param>
    /// <param name="cancelAction">取消事件</param>
    public void OpenTipUI(string title, string content, Action yesAction, Action cancelAction = null, bool hasCloseBtn = false) 
    {
        var ui = OpenUI<TipBoxUI>();
        ui.Init(title, content, yesAction, cancelAction, hasCloseBtn);
    }

    /// <summary>
    /// Open：
    /// 打开游戏通用弹出框
    /// </summary>
    /// <param name="title">标题</param>
    /// <param name="content">内容</param>
    /// <param name="yesContent">按钮1文本</param>
    /// <param name="cancelContent">按钮2文本</param>
    /// <param name="yesAction">按钮1事件</param>
    /// <param name="cancelAction">按钮2事件</param>
    public void OpenTipUI(string title, string content, string yesContent, string cancelContent, Action yesAction, Action cancelAction = null, bool hasCloseBtn = false)
    {
        var ui = OpenUI<TipBoxUI>();
        ui.Init(title, content, yesContent, cancelContent, yesAction, cancelAction, hasCloseBtn);
    }

    /// <summary>
    /// Open:
    /// 打开游戏通用弹出框
    /// </summary>
    /// <returns></returns>
    public TipBase OpenTipUI() 
    {
        var ui = OpenUI<TipBoxUI>();
        return ui;
    }

}


