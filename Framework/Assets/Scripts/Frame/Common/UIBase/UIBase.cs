using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public abstract class UIBase : MonoBehaviour
{
    /// <summary>
    /// 界面唯一标识，用于标识界面对象
    /// </summary>
    [HideInInspector]
    public uint ID;

    public UIStackIndex UIStack = UIStackIndex.StackNormal;

    public Action CloseAction;

    public UIType UIType;
    protected Transform frame;

    public CanvasGroup frameCanvasGroup;

    public Transform Frame { get {
            if (frame == null)
            {
                frame = transform.Find("Frame");
            }
            return frame; 
        } }

    public float zDeep;
    public bool noSound = false;

    public string abPath;
   
    private Canvas m_canvas;
    private List<Canvas> m_childCanvas;
    private bool isAwaked = false;

    protected Coroutine Coro;

    public Button CloseBtn;

    protected object openParams;
    public object Params
    {
        set
        {
            openParams = value;
        }
    }

    protected virtual void Awake()
    {   
        frame = transform.Find("Frame");
        UIManager.SetTextFont(gameObject);
        if (CloseBtn != null)
            CloseBtn.onClick.AddListener(delegate { Close(); });
       
    }

    /// <summary>
    /// 界面排序
    /// </summary>
    public int SortingOrder
    {
        get
        {
            if (m_canvas != null)
            {
                return m_canvas.sortingOrder;
            }

            return 0;
        }

        set
        {
            AwakeUI();
            if (m_canvas != null)
            {

                int oldOrder = m_canvas.sortingOrder;
                for (int i = 0; i < m_childCanvas.Count; i++)
                {
                    m_childCanvas[i].sortingOrder = value + (m_childCanvas[i].sortingOrder - oldOrder);
                }
                m_canvas.sortingOrder = value;
            }
        }
    }

    /// <summary>
    /// 初始化界面，用于获取一些必要的组件
    /// </summary>
    private void AwakeUI()
    {
        if (!isAwaked)
        {
            m_canvas = GetComponent<Canvas>();
            if (m_canvas == null)
            {
                m_canvas = gameObject.AddComponent<Canvas>();
            }

            m_childCanvas = new List<Canvas>(gameObject.GetComponentsInChildren<Canvas>());

            if (gameObject.GetComponent<GraphicRaycaster>() == null)
            {
                gameObject.AddComponent<GraphicRaycaster>();
            }
            isAwaked = true;
        }
    }

    /// <summary>
    /// 关闭界面  附带渐变动画        对话结束   参与游戏评选
    /// 如果需要重写关闭方法   可以先调用 CloseCoroutine 再重写
    /// </summary>
    public virtual void Close()
    {

        CloseCoroutine();
        //UIManager.Instance.CloseUI(GetType().Name);
        
        if (frameCanvasGroup == null)
        {
            frameCanvasGroup = Frame.gameObject.AddComponent<CanvasGroup>();
        }
        frameCanvasGroup.alpha = 1;
        frameCanvasGroup.interactable = false;

        frameCanvasGroup.DOFade(0, 0.2f).OnComplete(delegate {
            OnClose();
            gameObject.SetActive(false);

        });

        

    }

  

    public virtual void Destroy() {
        UIManager.Instance.DestroyUI(GetType().Name);
    }

    /// <summary>
    /// 关闭界面   附带动画
    /// </summary>
    public void CloseCoroutine()
    {
        if (Coro != null)
        {
            StopCoroutine(Coro);
        }
    }



    public virtual void OnOpen()
    {
        frameCanvasGroup = Frame.GetComponent<CanvasGroup>();
        if (frameCanvasGroup == null)
        {
            frameCanvasGroup = Frame.gameObject.AddComponent<CanvasGroup>();
        }
        frameCanvasGroup.alpha = 0;
        frameCanvasGroup.interactable = true;
        frameCanvasGroup.DOFade(1, 0.2f);

        

    }

    public virtual void OnOpen(object arg)
    {
    
    }

    public virtual void OnCreat(){ }

    public virtual void OnRecover(){ }

    public virtual void OnClose()
    {
        CloseAction?.Invoke();
        CloseAction = null;
    }

   
    public virtual void OnUIDestroy()
    {

    }


    //滚动显示数字
    protected void TextToValue(TextMeshProUGUI ui,int last,int target)
    {
        StartCoroutine(TextToValueCoro(ui, last, target));
    }

    IEnumerator TextToValueCoro(TextMeshProUGUI text,int start,int target)
    {
        int step = Math.Abs(Mathf.CeilToInt((target - start) / 1f));
        float cache = start;
        while (start != target)
        {
            cache = Mathf.MoveTowards(cache, target, step * Time.deltaTime);
            start = (int)cache;
            text.text = cache.ToString("f1");
            yield return null;
        }
        text.text = target.ToString();
    }

}

public enum eUIVoice
{
    None,
    CommonOpen,
    CommonClose
}