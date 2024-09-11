using System;

/// <summary>
/// 道具配置
/// </summary>
public class ItemCfg : BaseResData
{
    public int Name;
    public int Desc;
    public string Icon;

    public override void FillData(string data)
    {
        var array = data.Split('|');

        ResourceInitUtil.InitField(array[0],out this.ID);
        ResourceInitUtil.InitField(array[1],out this.Name);
        ResourceInitUtil.InitField(array[2],out this.Desc);
        ResourceInitUtil.InitField(array[3],out this.Icon);

    }

public new static bool HasKey {get{return true;}}
}
