using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 消息定义
/// </summary>
public static class MSGInfo
{
    public static string REFRESHLAYOUT = "REFRESHLAYOUT";     //刷新 文本布局   对话

    public static string BOXLOSE = "BOXLOSE";   //盒子的状态 - 失落
    public static string BOXANGER = "BOXANGER";   //盒子的状态 - 愤怒
    public static string BOXRELAX = "BOXRELAX";   //盒子的状态 - 放松
    public static string BOXRLERT = "BOXRLERT";   //盒子的状态 - 警惕
    public static string BOXSTEADY = "BOXSTEADY";   //盒子的状态 - 稳定

    public static string BOXSTATE = "BOXSTATE";   //盒子状态
    public static string FINSHLINE = "FINSHLINE";  //章节结束
    public static string NEWLINE = "NEWLINE";  //新的章节开始
    
    public static string TASKCOMPLETE = "TASKCOMPLETE";  //任务完成检测
    
    public static string FRESHITEMLAYOUT = "FRESHITEMLAYOUT";  //刷新物体布局  
    //public static string FRESHMAILLAYOUT = "FRESHMAILLAYOUT";  //刷新物体布局

    ///  ----------------  这里是一些 POp 弹出 ---------------
    public static string NEWITEM = "NEWITEM";  //新的线索/主体 创建了
    public static string REPEATCONTENT = "REPEATCONTENT";  //重复连接
    public static string NEWEVENT = "NEWEVENT";  //新的事件增加
    public static string REFRESHLABEL = "REFRESHLABEL";   //刷新标签了
    public static string REFRESHEVENT = "REFRESHEVENT"; //刷新事件了


    // -------------------  这里是一些特殊事件 --------------------
    public static string REFRESHSIDE = "REFRESHSIDE"; //刷新侧边栏
    //public static string NEWEVENT = "NEWEVENT";  //新的事件增加
    //public static string DELETLABEL = "DELETLABEL";  //标签被删除了
    public static string EVENTPROGRESS = "EVENTPROGRESS";    //事件进度条

}