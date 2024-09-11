using System;

/// <summary>
/// 科技配置
/// </summary>
public class TechCfg : BaseResData
{
    public int Name;
    public int Desc;
    public string Icon;
    public int Max;
    public int Stage;
    public int NeedTech;
    public int Group;
    public int Type;
    public int[] Limits;
    public int Value;
    public int Unlock;
    public int[] Values;
    public int[] Cost;
    public int Chapter;

    public override void FillData(string data)
    {
        var array = data.Split('|');

        ResourceInitUtil.InitField(array[0],out this.ID);
        ResourceInitUtil.InitField(array[1],out this.Name);
        ResourceInitUtil.InitField(array[2],out this.Desc);
        ResourceInitUtil.InitField(array[3],out this.Icon);
        ResourceInitUtil.InitField(array[4],out this.Max);
        ResourceInitUtil.InitField(array[5],out this.Stage);
        ResourceInitUtil.InitField(array[6],out this.NeedTech);
        ResourceInitUtil.InitField(array[7],out this.Group);
        ResourceInitUtil.InitField(array[8],out this.Type);
        ResourceInitUtil.InitShortArray(array[9],out this.Limits);
        ResourceInitUtil.InitField(array[10],out this.Value);
        ResourceInitUtil.InitField(array[11],out this.Unlock);
        ResourceInitUtil.InitShortArray(array[12],out this.Values);
        ResourceInitUtil.InitShortArray(array[13],out this.Cost);
        ResourceInitUtil.InitField(array[14],out this.Chapter);

    }

public new static bool HasKey {get{return true;}}
}
