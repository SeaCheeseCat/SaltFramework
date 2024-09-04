using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DebugUI : UIBase, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //Tip: UI和指针的位置偏移量
    Vector3 offset;
    //Tip: 当前的位置
    Vector3 pos;
    //Tip: 自身的RectTransform
    RectTransform rt;
    //Tip: 水平最小拖拽范围
    float minWidth;
    //Tip: 水平最大拖拽范围
    float maxWidth;
    //Tip: 垂直最小拖拽范围  
    float minHeight;
    //Tip: 垂直最大拖拽范围
    float maxHeight;
    //Tip: 拖拽范围
    float rangeX;
    //Tip: 拖拽范围
    float rangeY;
    //Tip: 关闭按钮
    public Button closeBtn;
    //Tip: 摄像机的X文本
    public Text cameraforText;
    //Tip: 当前关卡的文本
    public Text levelText;

    /// <summary>
    /// Base:update
    /// </summary>
    void Update()
    {
        DragRangeLimit();
        UpdataData();
    }

    /// <summary>
    /// Update:
    /// 更新数据
    /// </summary>
    public void UpdataData()
    {
       
    }

    /// <summary>
    /// Base:start
    /// </summary>
    public override void Start()
    {
        rt = GetComponent<RectTransform>();
        pos = rt.position;
        minWidth = rt.rect.width / 2;
        maxWidth = Screen.width - (rt.rect.width / 2);
        minHeight = rt.rect.height / 2;
        maxHeight = Screen.height - (rt.rect.height / 2);
        closeBtn.onClick.AddListener(() =>
        {
            Close();
        });
    }

    /// <summary>
    /// Drag:
    /// 拖拽范围限制
    /// </summary>
    void DragRangeLimit()
    {
        rangeX = Mathf.Clamp(rt.position.x, minWidth, maxWidth);
        rangeY = Mathf.Clamp(rt.position.y, minHeight, maxHeight);
        rt.position = new Vector3(rangeX, rangeY, 0);
    }

    /// <summary>
    /// Drag:
    /// 开始拖拽
    /// </summary>
    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, null, out globalMousePos))
        {
            offset = rt.position - globalMousePos;
        }
    }

    /// <summary>
    /// Drag:
    /// 拖拽中
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    {
        SetDraggedPosition(eventData);
    }

    /// <summary>
    /// Drag:
    /// 结束拖拽
    /// </summary>
    public void OnEndDrag(PointerEventData eventData)
    {

    }

    /// <summary>
    /// Drag:
    /// 更新UI的位置
    /// </summary>
    private void SetDraggedPosition(PointerEventData eventData)
    {
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, null, out globalMousePos))
        {
            rt.position = offset + globalMousePos;
        }
    }
}
