using System;

/// <summary>
/// 技能配置
/// </summary>
public class SkillCfg : BaseResData
{
    public int Name;
    public int Desc;
    public string Icon;
    public string Path;
    public int Type;
    public int InitCast;
    public int Level;
    public int CD;
    public float[] Values;
    public float[] Values2;
    public bool IsPerc;
    public string Audio;
    public string Effect;

    public override void FillData(string data)
    {
        var array = data.Split('|');

        ResourceInitUtil.InitField(array[0],out this.ID);
        ResourceInitUtil.InitField(array[1],out this.Name);
        ResourceInitUtil.InitField(array[2],out this.Desc);
        ResourceInitUtil.InitField(array[3],out this.Icon);
        ResourceInitUtil.InitField(array[4],out this.Path);
        ResourceInitUtil.InitField(array[5],out this.Type);
        ResourceInitUtil.InitField(array[6],out this.InitCast);
        ResourceInitUtil.InitField(array[7],out this.Level);
        ResourceInitUtil.InitField(array[8],out this.CD);
        ResourceInitUtil.InitShortArray(array[9],out this.Values);
        ResourceInitUtil.InitShortArray(array[10],out this.Values2);
        ResourceInitUtil.InitField(array[11],out this.IsPerc);
        ResourceInitUtil.InitField(array[12],out this.Audio);
        ResourceInitUtil.InitField(array[13],out this.Effect);

    }

public new static bool HasKey {get{return true;}}
}
