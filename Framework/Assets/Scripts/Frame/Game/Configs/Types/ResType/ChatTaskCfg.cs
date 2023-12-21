using System;

/// <summary>
/// 第一章主线对话配置
/// </summary>
public class ChatTaskCfg : BaseResData
{
    public int Task;
    public int[] Chat;
    public int ClueID;

    public override void FillData(string data)
    {
        var array = data.Split('|');

        ResourceInitUtil.InitField(array[0],out this.ID);
        ResourceInitUtil.InitField(array[1],out this.Task);
        ResourceInitUtil.InitShortArray(array[2],out this.Chat);
        ResourceInitUtil.InitField(array[3],out this.ClueID);

    }

public new static bool HasKey {get{return true;}}
}
