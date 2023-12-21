using System;

/// <summary>
/// CG内容
/// </summary>
public class CgCfg : BaseResData
{
    public string[] Image;
    public string[] Content;

    public override void FillData(string data)
    {
        var array = data.Split('|');

        ResourceInitUtil.InitField(array[0],out this.ID);
        ResourceInitUtil.InitShortArray(array[1],out this.Image);
        ResourceInitUtil.InitShortArray(array[2],out this.Content);

    }

public new static bool HasKey {get{return true;}}
}
