using System;

/// <summary>
/// 卡牌配置
/// </summary>
public class CardCfg : BaseResData
{
    public int Unit;
    public int Show;
    public int Desc;
    public int UnockType;
    public int Rare;
    public int Food;
    public int Gold;
    public int InitLevel;
    public int MaxLv;
    public float StarPlug;
    public int[] Cost;
    public int Limit;
    public int[] Power;

    public override void FillData(string data)
    {
        var array = data.Split('|');

        ResourceInitUtil.InitField(array[0],out this.ID);
        ResourceInitUtil.InitField(array[1],out this.Unit);
        ResourceInitUtil.InitField(array[2],out this.Show);
        ResourceInitUtil.InitField(array[3],out this.Desc);
        ResourceInitUtil.InitField(array[4],out this.UnockType);
        ResourceInitUtil.InitField(array[5],out this.Rare);
        ResourceInitUtil.InitField(array[6],out this.Food);
        ResourceInitUtil.InitField(array[7],out this.Gold);
        ResourceInitUtil.InitField(array[8],out this.InitLevel);
        ResourceInitUtil.InitField(array[9],out this.MaxLv);
        ResourceInitUtil.InitField(array[10],out this.StarPlug);
        ResourceInitUtil.InitShortArray(array[11],out this.Cost);
        ResourceInitUtil.InitField(array[12],out this.Limit);
        ResourceInitUtil.InitShortArray(array[13],out this.Power);

    }

public new static bool HasKey {get{return true;}}
}
