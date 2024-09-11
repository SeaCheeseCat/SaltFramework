using System;

/// <summary>
/// buff配置
/// </summary>
public class BuffCfg : BaseResData
{
    public int Name;
    public int Desc;
    public string Path;
    public bool Bad;

    public override void FillData(string data)
    {
        var array = data.Split('|');

        ResourceInitUtil.InitField(array[0],out this.ID);
        ResourceInitUtil.InitField(array[1],out this.Name);
        ResourceInitUtil.InitField(array[2],out this.Desc);
        ResourceInitUtil.InitField(array[3],out this.Path);
        ResourceInitUtil.InitField(array[4],out this.Bad);

    }

public new static bool HasKey {get{return true;}}
}
