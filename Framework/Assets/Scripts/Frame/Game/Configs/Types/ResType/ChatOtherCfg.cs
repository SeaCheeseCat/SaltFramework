using System;

/// <summary>
/// 第一章主线对话配置
/// </summary>
public class ChatOtherCfg : BaseResData
{
    public string[] Content;
    public int Close;

    public override void FillData(string data)
    {
        var array = data.Split('|');

        ResourceInitUtil.InitField(array[0],out this.ID);
        ResourceInitUtil.InitShortArray(array[1],out this.Content);
        ResourceInitUtil.InitField(array[2],out this.Close);

    }

public new static bool HasKey {get{return true;}}
}
