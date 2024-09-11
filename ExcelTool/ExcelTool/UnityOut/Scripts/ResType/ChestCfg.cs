using System;

/// <summary>
/// 宝箱配置
/// </summary>
public class ChestCfg : BaseResData
{
    public int Name;
    public int Desc;
    public string Icon;
    public string Frame;
    public string ChestClose;
    public string ChestOpen;
    public int[] Blue;
    public int[] Purple;
    public int[] Orange;
    public int Coin;
    public int Diamond;

    public override void FillData(string data)
    {
        var array = data.Split('|');

        ResourceInitUtil.InitField(array[0],out this.ID);
        ResourceInitUtil.InitField(array[1],out this.Name);
        ResourceInitUtil.InitField(array[2],out this.Desc);
        ResourceInitUtil.InitField(array[3],out this.Icon);
        ResourceInitUtil.InitField(array[4],out this.Frame);
        ResourceInitUtil.InitField(array[5],out this.ChestClose);
        ResourceInitUtil.InitField(array[6],out this.ChestOpen);
        ResourceInitUtil.InitShortArray(array[7],out this.Blue);
        ResourceInitUtil.InitShortArray(array[8],out this.Purple);
        ResourceInitUtil.InitShortArray(array[9],out this.Orange);
        ResourceInitUtil.InitField(array[10],out this.Coin);
        ResourceInitUtil.InitField(array[11],out this.Diamond);

    }

public new static bool HasKey {get{return true;}}
}
