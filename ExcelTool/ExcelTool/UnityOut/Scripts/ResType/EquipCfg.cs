using System;

/// <summary>
/// 装备配置
/// </summary>
public class EquipCfg : BaseResData
{
    public int Name;
    public int Desc;
    public string Icon;
    public string[] Propertys;
    public int Buff;
    public int DescType;
    public int Value;
    public int[] Values;

    public override void FillData(string data)
    {
        var array = data.Split('|');

        ResourceInitUtil.InitField(array[0],out this.ID);
        ResourceInitUtil.InitField(array[1],out this.Name);
        ResourceInitUtil.InitField(array[2],out this.Desc);
        ResourceInitUtil.InitField(array[3],out this.Icon);
        ResourceInitUtil.InitShortArray(array[4],out this.Propertys);
        ResourceInitUtil.InitField(array[5],out this.Buff);
        ResourceInitUtil.InitField(array[6],out this.DescType);
        ResourceInitUtil.InitField(array[7],out this.Value);
        ResourceInitUtil.InitShortArray(array[8],out this.Values);

    }

public new static bool HasKey {get{return true;}}
}
