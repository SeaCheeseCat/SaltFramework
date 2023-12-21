using System;

/// <summary>
/// Load内容
/// </summary>
public class LoadCfg : BaseResData
{
    public string Content;

    public override void FillData(string data)
    {
        var array = data.Split('|');

        ResourceInitUtil.InitField(array[0],out this.ID);
        ResourceInitUtil.InitField(array[1],out this.Content);

    }

public new static bool HasKey {get{return true;}}
}
