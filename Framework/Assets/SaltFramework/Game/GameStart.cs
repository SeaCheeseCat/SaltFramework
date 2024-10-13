using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 关卡的启动器
/// 从这里启动
/// </summary>
public class GameStart : MonoBehaviour
{
    public float distancePerKeyPress = 1f; // 每次按键移动的距离
    public float scaleIncreaseAmount = 0.01f; // 每次移动时比例增加的量

    private void Awake()
    {
        UIManager.Instance.Init();
    }

    private void Start()
    {
        if (GameBaseData.levelType == LevelType.COMMON)
        {

        }
        else if (GameBaseData.levelType == LevelType.HAVESUBTITLE)
        {
        }

#if UNITY_EDITOR
     /*   var debugui = UIManager.Instance.OpenUI<DebugUI>();
        debugui.GetComponent<RectTransform>().anchoredPosition = new Vector2(-172, 459f);*/
#endif

    }

    public void Update()
    {
       
    }

}
