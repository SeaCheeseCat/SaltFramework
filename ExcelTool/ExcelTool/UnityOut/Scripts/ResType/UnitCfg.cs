using System;

/// <summary>
/// 战场单位配置
/// </summary>
public class UnitCfg : BaseResData
{
    public string Icon;
    public string IconSimple;
    public int Card;
    public int Type;
    public int Name;
    public int Desc;
    public int Model;
    public int AtkType;
    public int Speed;
    public float BarHeight;
    public int Range;
    public string[] Propertys;
    public int[] Skill;
    public string[] LvPer;
    public string[] DiffAdd1;
    public string[] DiffAdd2;
    public int[] InitHaveBuff;

    public override void FillData(string data)
    {
        var array = data.Split('|');

        ResourceInitUtil.InitField(array[0],out this.ID);
        ResourceInitUtil.InitField(array[1],out this.Icon);
        ResourceInitUtil.InitField(array[2],out this.IconSimple);
        ResourceInitUtil.InitField(array[3],out this.Card);
        ResourceInitUtil.InitField(array[4],out this.Type);
        ResourceInitUtil.InitField(array[5],out this.Name);
        ResourceInitUtil.InitField(array[6],out this.Desc);
        ResourceInitUtil.InitField(array[7],out this.Model);
        ResourceInitUtil.InitField(array[8],out this.AtkType);
        ResourceInitUtil.InitField(array[9],out this.Speed);
        ResourceInitUtil.InitField(array[10],out this.BarHeight);
        ResourceInitUtil.InitField(array[11],out this.Range);
        ResourceInitUtil.InitShortArray(array[12],out this.Propertys);
        ResourceInitUtil.InitShortArray(array[13],out this.Skill);
        ResourceInitUtil.InitShortArray(array[14],out this.LvPer);
        ResourceInitUtil.InitShortArray(array[15],out this.DiffAdd1);
        ResourceInitUtil.InitShortArray(array[16],out this.DiffAdd2);
        ResourceInitUtil.InitShortArray(array[17],out this.InitHaveBuff);

    }

public new static bool HasKey {get{return true;}}
}
