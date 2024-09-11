using System;

/// <summary>
/// 挑战配置
/// </summary>
public class ChallengeCfg : BaseResData
{
    public int Name;
    public int Desc;
    public string Icon;
    public int Unit;
    public int Num;
    public int StartCool;
    public int CoolDown;
    public int Rare;
    public int Cost;
    public int[] Drop;
    public int DropPerLv;

    public override void FillData(string data)
    {
        var array = data.Split('|');

        ResourceInitUtil.InitField(array[0],out this.ID);
        ResourceInitUtil.InitField(array[1],out this.Name);
        ResourceInitUtil.InitField(array[2],out this.Desc);
        ResourceInitUtil.InitField(array[3],out this.Icon);
        ResourceInitUtil.InitField(array[4],out this.Unit);
        ResourceInitUtil.InitField(array[5],out this.Num);
        ResourceInitUtil.InitField(array[6],out this.StartCool);
        ResourceInitUtil.InitField(array[7],out this.CoolDown);
        ResourceInitUtil.InitField(array[8],out this.Rare);
        ResourceInitUtil.InitField(array[9],out this.Cost);
        ResourceInitUtil.InitShortArray(array[10],out this.Drop);
        ResourceInitUtil.InitField(array[11],out this.DropPerLv);

    }

public new static bool HasKey {get{return true;}}
}
