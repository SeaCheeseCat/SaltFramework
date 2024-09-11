using System;

/// <summary>
/// 星级升级配置
/// </summary>
public class StarCostCfg : BaseResData
{
    public int Card;
    public int Gold;

    public override void FillData(string data)
    {
        var array = data.Split('|');

        ResourceInitUtil.InitField(array[0],out this.ID);
        ResourceInitUtil.InitField(array[1],out this.Card);
        ResourceInitUtil.InitField(array[2],out this.Gold);

    }

public new static bool HasKey {get{return true;}}
}
