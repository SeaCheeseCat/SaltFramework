using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public abstract class UIBase : MonoBehaviour
{
    //Tip: UI的id
    public string id;
    //Tip: UI类型
    [EnumToggleButtons]
    public UiType type;
    //Tip: UI动画
    [EnumPaging]
    public UiAnimation uiAnimation;
    //Tip: canvasGroup
    public CanvasGroup canvasGroup;

    public int SortingOrder { get; set; }

    /// <summary>
    /// Init: internal
    /// 界面初始化(禁止用来注册事件)
    /// </summary>
    /// <param name="dialogArgs"></param>
    internal virtual void Init(params object[] dialogArgs)
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    internal virtual void OnOpen()
    {
    
    }

    /// <summary>
    /// Base: awake
    /// </summary>
    public virtual void Awake()
    {
       
    }

    /// <summary>
    /// Base: start
    /// </summary>
    public virtual void Start()
    {
        if (type == UiType.INITEXIST)
        {
            RegisterUI();
        }
    }

    /// <summary>
    /// Add: internal
    /// 注册事件
    /// </summary>
    internal virtual void AddEvent()
    {

    }

    /// <summary>
    /// Callback: virtual
    /// 销毁时执行
    /// </summary>
    internal virtual void OnDeath()
    {

    }

    /// <summary>
    /// Close: virtual
    /// 关闭UI
    /// </summary>
    internal virtual void Close()
    {
        
        OnClose();
        CloseWithAnimation();
    }

    /// <summary>
    /// Close:
    /// 关闭自己这个UI
    /// </summary>
    internal virtual void CloseMySelf()
    {
        UIManager.Instance.CloseUI(GetType().Name);
    }


    /// <summary>
    /// Callback:
    /// 关闭时触发
    /// </summary>
    public virtual void OnClose() 
    { 
    
    }

    /// <summary>
    /// Open:
    /// 通过动画打开 如果你需要重写就重写
    /// </summary>
    public virtual void OpenWithAnimation() 
    {
        switch (uiAnimation)
        {
            case UiAnimation.OPACITY:

                if (canvasGroup != null)
                {
                    canvasGroup.DOFade(1, 0.3f);
                }

            break;

            case UiAnimation.SCALE:

                gameObject.transform.DOScale(new Vector3(1, 1, 1), 0.3f);

            break;

            case UiAnimation.TRANSLATE:

                break;
        }
    }

    /// <summary>
    /// Close:
    /// 如果你需要重写动画就重写这个方法就可以
    /// </summary>
    public virtual void CloseWithAnimation() 
    {
        switch (uiAnimation)
        {
            case UiAnimation.NULL:
                DestroyUiWithAnimatoin();
            break;

            case UiAnimation.OPACITY:
                if (canvasGroup != null)
                {
                    canvasGroup.DOFade(0, 0.3f).OnComplete(
                        () =>
                        {
                            DestroyUiWithAnimatoin();
                        });
                }
            break;

            case UiAnimation.SCALE:
                gameObject.transform.DOScale(new Vector3(0, 0, 0), 0.3f).OnComplete(
                    () =>
                    {
                        DestroyUiWithAnimatoin();
                    });
            break;

            case UiAnimation.TRANSLATE:
                DestroyUiWithAnimatoin();
            break;
        }
    }

    /// <summary>
    /// Destroy: 
    /// 销毁UI通过动画
    /// </summary>
    public void DestroyUiWithAnimatoin()
    {
        Recycle();
        if (type == UiType.INITEXIST)
        {
            gameObject.SetActive(false);
        }
        else if (type == UiType.COMMON)
        {
            PoolManager.Instance.UiUnitPool.Recycle(gameObject);
        }
        else if (type == UiType.Tip)
        {
            PoolManager.Instance.UiUnitPool.Recycle(gameObject);
        }
    }


    /// <summary>
    /// Destroy: virtual
    /// 销毁UI
    /// </summary>
    internal virtual void DestroyUI()
    {
        UIManager.Instance.DestroyUI(name);
    }

    /// <summary>
    /// Register: virtual
    /// 注册UI（不需要每个都注册）
    /// 注册仅限使用在场景中自己构建的UI
    /// </summary>
    public virtual void RegisterUI() 
    {
        UIManager.Instance.RegisterUI(this);
    }

    /// <summary>
    /// Recycle:
    /// 回收数据
    /// </summary>
    public virtual void Recycle()
    { 

    }

}

/// <summary>
/// Enum : 
/// UI类型
/// *INITEXIST (初始存在型）该UI类型不会被销毁,只会进行显隐，初始在游戏场景中存在的界面可以用此类型
/// *COMMON (通用型）该UI类型从对象池中取出放回，可以同时弹出多个
/// </summary>
public enum UiType 
{
    COMMON,
    INITEXIST,
    Tip,
}

/// <summary>
/// Enum:
/// UI动画 统一预制的动画，打开和关闭都遵循动画 （参数不可调） 如果想自己重写动画，请重写方法
/// *NULL 无动画
/// *OPACITY 淡入淡出
/// *SCALE 缩放动画
/// *TRANSLATE 平移动画
/// </summary>
public enum UiAnimation
{
    NULL,
    OPACITY,
    SCALE,
    TRANSLATE
}