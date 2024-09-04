using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameBaseData
{
    //Tip: 章节
    public static int Chapter
    {
        get;set;
    }

    //Tip: 关卡
    public static int Level {
        get;set;
    }

    //Tip: 关卡类型
    public static LevelType levelType
    {
        get;set;
    }   

    // Tip: 需要传递的事件
    public static Action Event
    {
        get;set;
    }

    // Tip: 需要传递的泛型
    public static string EventName
    {
        get; set;
    }

    /// <summary>
    /// Tip:当前的语言
    /// </summary>
    public static Language language
    {
        get;set;
    }
}

/// <summary>
/// 关卡类型
/// </summary>
public enum LevelType
{ 
    //Tip: 常用的
    COMMON,
    //Tip: 包含字幕的
    HAVESUBTITLE
}