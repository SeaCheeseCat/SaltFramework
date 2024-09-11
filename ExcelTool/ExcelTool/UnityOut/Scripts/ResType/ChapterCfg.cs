using System;

/// <summary>
/// 关卡配置
/// </summary>
public class ChapterCfg : BaseResData
{
    public int Name;
    public int Desc;
    public string Icon;
    public int Boss;
    public int BossTime;
    public int KillTime;
    public int BaseP;
    public int WaveP;
    public string Map;
    public string Music;
    public string Nav;

    public override void FillData(string data)
    {
        var array = data.Split('|');

        ResourceInitUtil.InitField(array[0],out this.ID);
        ResourceInitUtil.InitField(array[1],out this.Name);
        ResourceInitUtil.InitField(array[2],out this.Desc);
        ResourceInitUtil.InitField(array[3],out this.Icon);
        ResourceInitUtil.InitField(array[4],out this.Boss);
        ResourceInitUtil.InitField(array[5],out this.BossTime);
        ResourceInitUtil.InitField(array[6],out this.KillTime);
        ResourceInitUtil.InitField(array[7],out this.BaseP);
        ResourceInitUtil.InitField(array[8],out this.WaveP);
        ResourceInitUtil.InitField(array[9],out this.Map);
        ResourceInitUtil.InitField(array[10],out this.Music);
        ResourceInitUtil.InitField(array[11],out this.Nav);

    }

public new static bool HasKey {get{return true;}}
}
