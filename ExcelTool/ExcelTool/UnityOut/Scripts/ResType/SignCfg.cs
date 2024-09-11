using System;

/// <summary>
/// 签到配置
/// </summary>
public class SignCfg : BaseResData
{
    public int DropType;
    public int[] Values;
    public string Icon;
    public int IconDesc;

    public override void FillData(string data)
    {
        var array = data.Split('|');

        ResourceInitUtil.InitField(array[0],out this.ID);
        ResourceInitUtil.InitField(array[1],out this.DropType);
        ResourceInitUtil.InitShortArray(array[2],out this.Values);
        ResourceInitUtil.InitField(array[3],out this.Icon);
        ResourceInitUtil.InitField(array[4],out this.IconDesc);

    }

public new static bool HasKey {get{return true;}}
}
