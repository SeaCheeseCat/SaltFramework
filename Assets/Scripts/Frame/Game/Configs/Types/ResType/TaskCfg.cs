using System;

/// <summary>
/// 物体任务
/// </summary>
public class TaskCfg : BaseResData
{
    public int CuleID;
    public string[] Task;
    public int Event;
    public int Page;
    public string Arg;

    public override void FillData(string data)
    {
        var array = data.Split('|');

        ResourceInitUtil.InitField(array[0],out this.ID);
        ResourceInitUtil.InitField(array[1],out this.CuleID);
        ResourceInitUtil.InitShortArray(array[2],out this.Task);
        ResourceInitUtil.InitField(array[3],out this.Event);
        ResourceInitUtil.InitField(array[4],out this.Page);
        ResourceInitUtil.InitField(array[5],out this.Arg);

    }

public new static bool HasKey {get{return true;}}
}
