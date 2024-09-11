using System;

/// <summary>
/// 内购配置
/// </summary>
public class EndlessCfg : BaseResData
{
    public int Name;
    public string Num;
    public int[] EndlessUnits;
    public int[] Units;

    public override void FillData(string data)
    {
        var array = data.Split('|');

        ResourceInitUtil.InitField(array[0],out this.ID);
        ResourceInitUtil.InitField(array[1],out this.Name);
        ResourceInitUtil.InitField(array[2],out this.Num);
        ResourceInitUtil.InitShortArray(array[3],out this.EndlessUnits);
        ResourceInitUtil.InitShortArray(array[4],out this.Units);

    }

public new static bool HasKey {get{return true;}}
}
