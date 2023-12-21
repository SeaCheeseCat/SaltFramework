using System;

/// <summary>
/// 第一章主线对话配置
/// </summary>
public class ClueItemCfg : BaseResData
{
    public string Title;
    public int Type;
    public string[] Args;
    public int[] Event;

    public override void FillData(string data)
    {
        var array = data.Split('|');

        ResourceInitUtil.InitField(array[0],out this.ID);
        ResourceInitUtil.InitField(array[1],out this.Title);
        ResourceInitUtil.InitField(array[2],out this.Type);
        ResourceInitUtil.InitShortArray(array[3],out this.Args);
        ResourceInitUtil.InitShortArray(array[4],out this.Event);

    }

public new static bool HasKey {get{return true;}}
}
