using System;

/// <summary>
/// 飞行道具配置
/// </summary>
public class ProjectileCfg : BaseResData
{
    public string Path;
    public int Speed;
    public string[] HitAudio;
    public string HitVFX;

    public override void FillData(string data)
    {
        var array = data.Split('|');

        ResourceInitUtil.InitField(array[0],out this.ID);
        ResourceInitUtil.InitField(array[1],out this.Path);
        ResourceInitUtil.InitField(array[2],out this.Speed);
        ResourceInitUtil.InitShortArray(array[3],out this.HitAudio);
        ResourceInitUtil.InitField(array[4],out this.HitVFX);

    }

public new static bool HasKey {get{return true;}}
}
