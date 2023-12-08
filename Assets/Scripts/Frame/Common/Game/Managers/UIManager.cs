using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum UIType
{
    TypeNormal,
    TypeFullScreen,
    TypeModel,
    TypeModelStay,
    TypeGuide,
}

public enum UIStackIndex
{
    StackBottom = 0,
    StackNormal = 1,
    StackTop = 2,
    StackTopTop = 3,
    StackSystem = 4
}

internal class UIStack
{
    public int m_baseOrder = 0;
    public float m_baseZDeep = 0;
    public List<uint> m_UIList = new List<uint> ();
    public Transform m_parentTrans;
}

/// <summary>
/// UI界面管理器
/// </summary>
public class UIManager : Manager<UIManager>
{
    public const string FONT_CN_PATH = "Fonts/迷你简艺黑";
    public const string FONT_EN_PATH = "Fonts/AlibabaPuHuiTi-2-115-Black";

    public const string NumFontRes = "Fonts/PlayCombo";
    public const string NumFontCommon = "Fonts/PlayGold";

    public const string UI_AB_NAME = "System/UI/";
    public const string UI_ROOT = "System/UIRoot";
    public const string MODEL_BACKGROUND = "System/UI/UIModelBG";
    public const string GUIDE_BACKGROUND = "System/UI/GuideBG";

    public const string TIP_UI = "System/UI/TipMessageUI";

    public const int MAX_CANVAS_SORTING_ORDER = 20;

    private static uint nextUIID = 1;

    private GameObject m_uiRoot;
    private Canvas m_canvas;
    private Camera m_camera;
    private Transform m_canvasTrans;

    private int m_tipOrder;


    public Camera UICamera { get { return m_camera; } }

    /// <summary>
    /// 不同显示顺序的的窗口列表
    /// </summary>
    private UIStack[] m_listUIStack = new UIStack[((int) UIStackIndex.StackSystem + 1)];

    /// <summary>
    /// 类型到实例的索引
    /// </summary>
    public Dictionary<string, UIBase> m_typeToInst = new Dictionary<string, UIBase> ();

    /// <summary>
    /// ID到实例的索引
    /// </summary>
    private Dictionary<uint, UIBase> m_idToInst = new Dictionary<uint, UIBase> ();

    /// <summary>
    /// 预制对象
    /// </summary>
    private Dictionary<string, GameObject> UIList = new Dictionary<string, GameObject>();

    /// <summary>
    /// UI缓存
    /// </summary>
    private List<UIBase> m_uiCacheList = new List<UIBase> ();

    /// <summary>
    /// 是否有被缓存的UI
    /// </summary>
    public bool HasCachedUI { get; private set; }

    private static TMP_FontAsset s_cnFont;
    private static TMP_FontAsset s_enFont;
    private static TMP_FontAsset s_numResFont;
    private static TMP_FontAsset s_numComFont;

    bool matchHeight;
    public bool IsMatchHeight
    {
        get
        {
            return matchHeight;
        }
    }

    float _matchHeightScale;
    public float matchHeightScale
    {
        get
        {
            return _matchHeightScale;
        }
    }

    public override IEnumerator Init(MonoBehaviour obj)
    {
        m_uiRoot = ResourceManager.AllocObjectSync(UI_ROOT);    //打开UI根节点
        m_uiRoot.transform.SetParent(obj.transform);
        m_camera = m_uiRoot.GetComponentInChildren<Camera>();

        Canvas canvas = m_uiRoot.GetComponentInChildren<Canvas>();
        if (canvas != null)
        {
            m_canvas = canvas;
            m_canvasTrans = canvas.transform;
        }

        CanvasScaler canvasScaler = m_uiRoot.GetComponentInChildren<CanvasScaler>();
        Resolution resolution = Screen.currentResolution;
        Debug.Log("当前分辨率" + Screen.width + "/" + Screen.height);
        //16:9的为1.7777,4:3为1.3333故取1.6作为阈值
        if ((float)Screen.height / (float)Screen.width <= 1.6f)
        {
            Debug.Log("调整为1");
            canvasScaler.matchWidthOrHeight = 1;

            // pad设备时 计算高宽比的缩放比例，以便给UI粒子特效使用
            matchHeight = true;
            var hw = canvasScaler.referenceResolution.y / canvasScaler.referenceResolution.x;
            var curHw = Screen.height * 1.0f / Screen.width;
            if (curHw > hw)
            {
                _matchHeightScale = hw / curHw;
            }
            else
            {
                _matchHeightScale = 1;
            }
        }
        else
        {
            Debug.Log("调整为0");
            canvasScaler.matchWidthOrHeight = 0;
            matchHeight = false;
            _matchHeightScale = 1;
        }

        //Debug.Log("调整为0");
        //canvasScaler.matchWidthOrHeight = 0;
        /*matchHeight = false;
        _matchHeightScale = 1;*/

        int baseOrder = 1000;
        for (int i = 0; i <= (int)UIStackIndex.StackSystem; i++)
        {
            m_listUIStack[i] = new UIStack();
            m_listUIStack[i].m_baseOrder = baseOrder;
            m_listUIStack[i].m_baseZDeep = -(i - 1) * 5000;
            m_listUIStack[i].m_parentTrans = m_canvasTrans;
            baseOrder += 1000;
        }

        m_tipOrder = baseOrder;

        return base.Init(obj);
    }

    //提示信息
    public static TipMessageUI ShowTip (string tip)
    {
        return Instance.ShowTipInner (tip);
    }

    private TipMessageUI ShowTipInner (string tip)
    {
        TipMessageUI tipUI = Object.Instantiate (ResourceManager.LoadPrefabSync (TIP_UI) as GameObject, m_canvasTrans).GetComponent<TipMessageUI> ();
        tipUI.msgTxt.text = tip;
        tipUI.GetComponent<Canvas> ().sortingOrder = m_tipOrder;
        tipUI.transform.localPosition = new Vector3 (0, 0, -3000);
        return tipUI;
    }

    /*public static void ShowWaitting (float lifeTime, System.Action timeoutAction)
    {
       OpenUI<WaittingUI> ().Setup (lifeTime, timeoutAction);
    }

    public static void HideWaitting ()
    {
       CloseUI<WaittingUI> ();
    }*/

    /// <summary>
    /// 打开界面
    /// </summary>
    /// <returns>The user interface.</returns>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public static T OpenUI<T> () where T : UIBase
    {
        /*var rt = Instance.OpenUIInner<T>();
        GameBase.Instance.ShaderRecoverUI(rt.gameObject);
        Debug.Log("打开恢复");*/
        return Instance.OpenUIInner<T>();
    }


 

    /// <summary>
    /// 预加载界面
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static void LoadUI<T>(System.Action action=null) where T : UnityEngine.Object
    {
        var UIName = typeof(T).Name;
        string abPath = UI_AB_NAME + UIName;
        ResourceManager.LoadPrefabAsync(abPath,msg=>
        {
            var obj = msg as GameObject;
            if (!Instance.UIList.ContainsKey(UIName))
            {
                Instance.UIList.Add(UIName, obj);
            }
            Debug.LogWarning(UIName + "预加载完毕");
            action?.Invoke();
        });
    }

    /// <summary>
    /// 预加载界面
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IEnumerator LoadUICoro<T>() where T : UnityEngine.Object
    {
        bool check = true;
        LoadUI<T>(delegate
        {
            check = false;
        });
        while (check)
        {
            yield return null;
        }
    }


    /// <summary>
    ///  对游戏UI的顺序进行排序   
    ///  设置当前UI的顺序 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="index"> index越靠前  显示越在上面 </param>
    /// <returns></returns>
    private T UISort<T>(int index) where T : UIBase
    {
        return (T)SortUI(typeof(T).Name, index);
    }

    /// <summary>
    /// 开启界面，泛型版本   
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    private T OpenUIInner<T> () where T : UIBase
    {
        return (T) OpenUI (typeof (T).Name);
    }




    
    /// <summary>
    /// 开启界面，字符串版本
    /// </summary>
    /// <param name="UIName"></param>
    /// <returns></returns>
    public UIBase OpenUI (string UIName)
    {
        
        UIBase UI = GetUI (UIName);
        if (UI == null)
        {
            UI = CreateUIGO (UIName);
            UI.OnCreat();
        }
        UI.OnOpen();
        UIStack stack = GetUIStack (UI);

        List<uint> UIList = stack.m_UIList;

        int findIdx = -1;
        int destIndex = 0;
        for (int i = 0; i < UIList.Count; i++, destIndex++)
        {
            UIList[destIndex] = UIList[i];
            if (UIList[i] == UI.ID)
            {
                destIndex--;
                findIdx = i;
            }
        }

        if (findIdx != -1)
        {
            UIList[UIList.Count - 1] = UI.ID;
        }
        else
        {
            UIList.Add (UI.ID);
            m_idToInst[UI.ID] = UI;
        }

        ReorderUIGO (stack, findIdx);

        return UI;
    }



    /// <summary>
    /// 开启界面， 不创建界面版本      配合游戏调整
    /// </summary>
    /// <param name="UIName"></param>
    /// <returns></returns>
    public void OpenUINoInstance(string UIName, UIBase uIBase)
    {
        UIBase UI = uIBase;
        m_typeToInst.Add(UIName, uIBase);
        UI.OnOpen();
        
    }



    /// <summary>
    /// 对UI进行排序   srot越小越靠前
    /// </summary>
    /// <param name="UIName"></param>
    /// <param name="sort"></param>
    /// <returns></returns>
    public UIBase SortUI(string UIName, int sort)
    {

        UIBase UI = GetUI(UIName);
        if (UI == null)
        {

            UI = CreateUIGO(UIName);
            UI.OnCreat();
        }
        UI.OnOpen();
        UIStack stack = GetUIStack(UI);

        List<uint> UIList = stack.m_UIList;

        int findIdx = -1;
        int destIndex = 0;
        for (int i = 0; i < UIList.Count; i++, destIndex++)
        {
            UIList[destIndex] = UIList[i];
            if (UIList[i] == UI.ID)
            {
                destIndex--;
                findIdx = i;
            }
        }

        if (findIdx != -1)
        {
            UIList[UIList.Count - 1] = UI.ID;
        }
        else
        {
            UIList.Add(UI.ID);
            m_idToInst[UI.ID] = UI;
        }

        ReorderUIGO(stack, findIdx);

        return UI;
    }

    /// <summary>
    /// 关闭所有UI    关闭所有UI不再进行销毁 而是  False 
    /// </summary>
    public void CloseAllUI ()
    {
        foreach (var ui in m_typeToInst)
        {
            ui.Value.OnClose ();
            Object.Destroy (ui.Value.gameObject);
        }
        m_typeToInst.Clear ();
        m_idToInst.Clear ();

        foreach (var st in Instance.m_listUIStack)
        {
            st.m_UIList.Clear ();
        }


    }

    /// <summary>
    /// 缓存所有UI，一般用于切换场景的情况
    /// </summary>
    public void CacheAllUI ()
    {
        for (UIStackIndex index = UIStackIndex.StackBottom; index < UIStackIndex.StackSystem; index++)
        {
            UIStack stack = m_listUIStack[(int) index];
            for (int i = 0; i < stack.m_UIList.Count; i++)
            {
                uint curID = stack.m_UIList[i];
                if (m_idToInst.TryGetValue (curID, out UIBase UI))
                {
                    UI.gameObject.SetActive (false);
                    m_uiCacheList.Add (UI);

                    m_typeToInst.Remove (UI.GetType ().Name);
                    m_idToInst.Remove (curID);
                }
            }
            stack.m_UIList.Clear ();
        }

        HasCachedUI = true;
    }

    /// <summary>
    /// 恢复所有UI，与CacheAllUI成对使用
    /// </summary>
    public void RecoverAllUI ()
    {
        for (int i = 0; i < m_uiCacheList.Count; i++)
        {
            UIBase curUI = m_uiCacheList[i];

            string typeName = curUI.GetType ().Name;
            if (!m_typeToInst.ContainsKey (typeName) && !m_idToInst.ContainsKey (curUI.ID))
            {
                m_typeToInst.Add (typeName, curUI);
                m_idToInst.Add (curUI.ID, curUI);

                m_listUIStack[(int) curUI.UIStack].m_UIList.Add (curUI.ID);

                curUI.OnRecover();
                curUI.gameObject.SetActive (true);
            }
            else
            {
                curUI.OnClose ();
                Object.Destroy (curUI.gameObject);
            }
        }
        m_uiCacheList.Clear ();
        HasCachedUI = false;
    }

    /// <summary>
    /// 关闭界面
    /// </summary>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public static void CloseUI<T> () where T : UIBase
    {
        Instance.CloseUIInner<T> ();
    }

    /// <summary>
    /// 关闭界面，泛型版本
    /// </summary>
    /// <typeparam name="T"></typeparam>
    private void CloseUIInner<T> () where T : UIBase
    {
        CloseUI (typeof (T).Name);
    }

    /// <summary>
    /// 关闭界面，字符串版本   
    /// </summary>
    /// <param name="UIName"></param>
    public void CloseUI(string UIName)
    {
        UIBase UI = GetUI (UIName);
        
        if (UI == null)
        {
            return;
        }

        //UI.gameObject.SetActive(false);
        UI.Close();
        UI.OnClose();
    }

    /// <summary>
    /// 销毁界面
    /// </summary>
    /// <param name="UIName"></param>
    public void DestroyUI(string UIName)
    {
        UIBase UI = GetUI(UIName);
        if (UI == null)
        {
            //Debug.Log("未找到ui" + UIName);
            return;
        }

        DestroyUIGO(UI, UIName);

        UIStack stack = GetUIStack(UI);
        List<uint> UIList = stack.m_UIList;

        int findIdx = -1;
        for (int i = 0; i < UIList.Count; i++)
        {
            if (UIList[i] == UI.ID)
            {
                findIdx = i;
                break;
            }
        }
        ReorderUIGO(stack, findIdx);
        UI.OnUIDestroy();
    }

    /// <summary>
    /// 检查界面是否存在
    /// </summary>
    /// <returns><c>true</c>, if user interface was checked, <c>false</c> otherwise.</returns>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public static bool CheckUI<T> ()
    {
        return Instance.CheckUIInner<T> ();
    }

    /// <summary>
    /// 检测界面是否存在，泛型版本
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    private bool CheckUIInner<T> ()
    {
        return CheckUI (typeof (T).Name);
    }

    /// <summary>
    /// 检测界面是否存在，字符串版本
    /// </summary>
    /// <param name="UIName"></param>
    /// <returns></returns>
    public bool CheckUI (string UIName)
    {
        return m_typeToInst.ContainsKey (UIName);
    }

    /// <summary>
    /// 获取界面，泛型版本
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T GetUI<T> () where T : UIBase
    {
        return Instance.GetUIInner<T> ();
    }

    /// <summary>
    /// 获取界面，泛型版本
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    private T GetUIInner<T> () where T : UIBase
    {
        return GetUI (typeof (T).Name) as T;
    }

    /// <summary>
    /// 获取界面，字符串版本
    /// </summary>
    /// <param name="UIName"></param>
    /// <returns></returns>
    public UIBase GetUI (string UIName)
    {
        UIBase UI;
        if (m_typeToInst.TryGetValue(UIName, out UI))
        {
            return UI;
        }
        DebugEX.LogError("当前界面不存在");
        /*foreach(var v in m_typeToInst)
        {
            Debug.LogWarning("当前界面存在:" + v.Key);
        }*/
        return null;
    }

    /// <summary>
    /// 创建一个界面对象
    /// </summary>
    /// <param name="UIName"></param>
    /// <returns></returns>
    private UIBase CreateUIGO(string UIName)
    {
        UIBase UI;
        string abPath = UI_AB_NAME + UIName;
        GameObject p;
        GameObject go;
        if (UIList.ContainsKey(UIName))
        {
            p = UIList[UIName];
            go = Object.Instantiate(p);
        }
        else
        {
            go = ResourceManager.AllocObjectSync(abPath);
        }
        if (go == null)
        {
            Debug.LogError("开启ui失败" + abPath);
            return null;
        }

        UI = go.GetComponent<UIBase>();
        if (UI == null)
        {
            return null;
        }
        UI.abPath = abPath;

        if (UI.UIType == UIType.TypeModel || UI.UIType == UIType.TypeModelStay)
        {
            GameObject modelBG = ResourceManager.AllocObjectSync(MODEL_BACKGROUND);
            modelBG.name = "modelBG";
            modelBG.transform.SetParent(go.transform);
            modelBG.transform.SetSiblingIndex(0);
            UIModelBG bg = modelBG.GetComponent<UIModelBG>();
            bg.InitBG(UI, UI.UIType == UIType.TypeModel);

            modelBG.transform.localPosition = Vector3.zero;
            modelBG.transform.localRotation = Quaternion.identity;
            modelBG.transform.localScale = Vector3.one;

            CanvasGroup frameCanvasGroup = UI.Frame.GetComponent<CanvasGroup>();
            if (frameCanvasGroup == null)
            {                                                
                frameCanvasGroup = UI.Frame.gameObject.AddComponent<CanvasGroup>();
            }
            frameCanvasGroup.alpha = 0;
            frameCanvasGroup.DOFade(1, 0.1f);
        }
        else if (UI.UIType == UIType.TypeGuide)
        {
            GameObject guideBG = ResourceManager.AllocObjectSync(GUIDE_BACKGROUND);
            guideBG.name = "guideBG";
            guideBG.transform.SetParent(go.transform);
            guideBG.transform.SetSiblingIndex(0);

            guideBG.transform.localPosition = Vector3.zero;
            guideBG.transform.localRotation = Quaternion.identity;
            guideBG.transform.localScale = Vector3.one;
        }

        UIStack UIStack = GetUIStack(UI);
        RectTransform rectTrans = UI.transform as RectTransform;
        rectTrans.SetParent(UIStack.m_parentTrans);
        rectTrans.localPosition = Vector2.zero;
        rectTrans.sizeDelta = Vector2.zero;
        rectTrans.localRotation = Quaternion.identity;
        rectTrans.localScale = Vector3.one;

        m_typeToInst[UIName] = UI;

        UI.ID = nextUIID++;
        return UI;
    }

    /// <summary>
    /// 销毁一个界面对象
    /// </summary>
    /// <param name="UI"></param>
    private void DestroyUIGO (UIBase UI, string UIName)
    {
        ///移除类型的隐射
        // string typeName = UI.GetType ().Name;
        UIBase typeWindow = null;
        if (m_typeToInst.TryGetValue (UIName, out typeWindow) && typeWindow == UI)
        {
            m_typeToInst.Remove (UIName);
        }

        uint UIID = UI.ID;
        m_idToInst.Remove (UIID);
        UIStack stack = GetUIStack (UI);
        stack.m_UIList.Remove (UIID);

        if (UI.UIType == UIType.TypeModel || UI.UIType == UIType.TypeModelStay)
        {

            GameObject modelBG = UI.gameObject.transform.Find ("modelBG").gameObject;
            UIModelBG bg = modelBG.GetComponent<UIModelBG> ();

            CanvasGroup frameCanvasGroup = UI.Frame.GetComponent<CanvasGroup> ();
            if (frameCanvasGroup == null)
            {
                frameCanvasGroup = UI.Frame.gameObject.AddComponent<CanvasGroup> ();
            }
            frameCanvasGroup.alpha = 1;
            frameCanvasGroup.interactable = false;
            frameCanvasGroup.DOFade (0, 0.2f).onComplete = () =>
            {
                Object.Destroy (UI.gameObject);
                // m_Cache.Add(UIName, UI);
                // UI.gameObject.SetActive(false);
            };

            bg.image.DOFade (0, 0.1f);
        }
        else
        {
            Object.Destroy (UI.gameObject);
            // m_Cache.Add(UIName, UI);
            // UI.gameObject.SetActive(false);
        }
    }



    /// <summary>
    /// 对界面对象重新排序
    /// </summary>
    private void ReorderUIGO (UIStack stack, int startIdx)
    {
        startIdx = startIdx == -1 ? (stack.m_UIList.Count - 1) : startIdx;

        float zDeep = 0;
        if (stack.m_UIList.Count > 0)
        {
            for (int i = 0; i < stack.m_UIList.Count; i++)
            {
                uint UIID = stack.m_UIList[i];
                UIBase UI;
                if (m_idToInst.TryGetValue (UIID, out UI))
                {
                    if (i >= startIdx)
                    {
                        UI.SortingOrder = stack.m_baseOrder + i * MAX_CANVAS_SORTING_ORDER;
                    }
                    UI.transform.localPosition = new Vector3 (UI.transform.localPosition.x, UI.transform.localPosition.y, stack.m_baseZDeep - zDeep);
                    zDeep += UI.zDeep;
                }
            }

            bool hasTop = false;
            for (int i = stack.m_UIList.Count - 1; i >= 0; i--)
            {
                uint UIID = stack.m_UIList[i];
                UIBase UI;
                if (m_idToInst.TryGetValue (UIID, out UI))
                {
                    if (!hasTop)
                    {
                        if (UI.UIType != UIType.TypeModel && UI.UIType == UIType.TypeFullScreen)
                        {
                            hasTop = true;
                        }
                        UI.gameObject.SetActive (true);
                    }
                    else
                    {
                        UI.gameObject.SetActive (false);
                    }
                }
            }
        }
    }

    /// <summary>
    /// 获取界面对应的栈
    /// </summary>
    /// <param name="UI"></param>
    /// <returns></returns>
    private UIStack GetUIStack (UIBase UI)
    {
        if (UI == null)
        {
            return m_listUIStack[(int) UIStackIndex.StackBottom];
        }
        return m_listUIStack[(int) UI.UIStack];
    }

    /// <summary>
    /// 设置文本字体
    /// </summary>
    /// <param name="obj"></param>
    public static void SetTextFont (GameObject obj)
    {
        //Debug.Log("设置字体材质");
        /*TMP_FontAsset font;
        switch (TextManager.Instance.GetLanguageType())
        {
            case RpcData.LanguageType.Cn:
                if (s_cnFont == null)
                {
                    s_cnFont = ResourceManager.LoadPrefabSync (FONT_CN_PATH) as TMP_FontAsset;
                }
                font = s_cnFont;
                break;
            default:
                if (s_enFont == null)
                {
                    s_enFont = ResourceManager.LoadPrefabSync (FONT_EN_PATH) as TMP_FontAsset;
                }
                font = s_enFont;
                break;
        }

        var texts = obj.GetComponentsInChildren<TextMeshProUGUI> (true);
        for (int i = 0; i < texts.Length; i++)
        {
            if (texts[i].tag != "CustomFont")
            {
                texts[i].font = font;
                if(TextManager.Instance.GetLanguageType() == RpcData.LanguageType.Cn)
                {
                    //Debug.Log("调整间距");
                    texts[i].characterSpacing = -10;
                }
            }
        }*/
    }




    public static void SetTextFont(GameObject obj,int index)
    {
       /* TMP_FontAsset font=null;
        switch (index)
        {
            case 0:
                if (s_numResFont == null)
                {
                    s_numResFont = ResourceManager.LoadPrefabSync(NumFontRes) as TMP_FontAsset;
                }
                font = s_numResFont;
                break;
            case 1:
                if (s_numComFont == null)
                {
                    s_numComFont = ResourceManager.LoadPrefabSync(NumFontCommon) as TMP_FontAsset;
                }
                font = s_numComFont;
                break;
        }

        var texts = obj.GetComponentsInChildren<TextMeshProUGUI>(true);
        for (int i = 0; i < texts.Length; i++)
        {
            if (texts[i].tag != "CustomFont")
            {
                texts[i].font = font;
            }
        }*/
    }
}