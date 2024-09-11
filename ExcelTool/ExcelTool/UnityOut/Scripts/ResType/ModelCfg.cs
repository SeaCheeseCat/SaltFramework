using System;

/// <summary>
/// 模型配置
/// </summary>
public class ModelCfg : BaseResData
{
    public string Path;
    public int AtkLength;
    public int AnimAtk;
    public int CastLength;
    public int AnimCast;
    public string[] DeadAudio;
    public string[] AttackAudio;
    public int Projectile;

    public override void FillData(string data)
    {
        var array = data.Split('|');

        ResourceInitUtil.InitField(array[0],out this.ID);
        ResourceInitUtil.InitField(array[1],out this.Path);
        ResourceInitUtil.InitField(array[2],out this.AtkLength);
        ResourceInitUtil.InitField(array[3],out this.AnimAtk);
        ResourceInitUtil.InitField(array[4],out this.CastLength);
        ResourceInitUtil.InitField(array[5],out this.AnimCast);
        ResourceInitUtil.InitShortArray(array[6],out this.DeadAudio);
        ResourceInitUtil.InitShortArray(array[7],out this.AttackAudio);
        ResourceInitUtil.InitField(array[8],out this.Projectile);

    }

public new static bool HasKey {get{return true;}}
}
