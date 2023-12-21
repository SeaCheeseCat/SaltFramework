using System;

/// <summary>
/// 物体任务
/// </summary>
public class ItemTaskCfg : BaseResData
{
    public string[] Task;
    public String Take;
    public int Event;
    public int Page;

    public override void FillData(string data)
    {
        var array = data.Split('|');

        ResourceInitUtil.InitField(array[0],out this.ID);
        ResourceInitUtil.InitShortArray(array[1],out this.Task);
        ResourceInitUtil.InitField(array[2],out this.Take);
        ResourceInitUtil.InitField(array[3],out this.Event);
        ResourceInitUtil.InitField(array[4],out this.Page);

    }

public new static bool HasKey {get{return true;}}
}
