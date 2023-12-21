using System;

/// <summary>
/// 第一章主线对话配置
/// </summary>
public class ChatCfg : BaseResData
{
    public string[] Content;
    public string[] Choose;
    public int[] Next;
    public int Page;
    public int Event;
    public string Born;

    public override void FillData(string data)
    {
        var array = data.Split('|');

        ResourceInitUtil.InitField(array[0],out this.ID);
        ResourceInitUtil.InitShortArray(array[1],out this.Content);
        ResourceInitUtil.InitShortArray(array[2],out this.Choose);
        ResourceInitUtil.InitShortArray(array[3],out this.Next);
        ResourceInitUtil.InitField(array[4],out this.Page);
        ResourceInitUtil.InitField(array[5],out this.Event);
        ResourceInitUtil.InitField(array[6],out this.Born);

    }

public new static bool HasKey {get{return true;}}
}
