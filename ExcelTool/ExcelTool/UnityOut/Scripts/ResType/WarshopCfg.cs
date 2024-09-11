using System;

/// <summary>
/// 战斗内商店配置
/// </summary>
public class WarshopCfg : BaseResData
{
    public int[] Gold ;
    public int[] Energy;

    public override void FillData(string data)
    {
        var array = data.Split('|');

        ResourceInitUtil.InitField(array[0],out this.ID);
        ResourceInitUtil.InitShortArray(array[1],out this.Gold );
        ResourceInitUtil.InitShortArray(array[2],out this.Energy);

    }

public new static bool HasKey {get{return true;}}
}
