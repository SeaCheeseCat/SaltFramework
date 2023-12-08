using System;

/// <summary>
/// Load内容
/// </summary>
public class SwitchLetterCfg : BaseResData
{
    public string[] Contents;

    public override void FillData(string data)
    {
        var array = data.Split('|');

        ResourceInitUtil.InitField(array[0],out this.ID);
        ResourceInitUtil.InitShortArray(array[1],out this.Contents);

    }

public new static bool HasKey {get{return true;}}
}
