using System;

/// <summary>
/// 波次配置
/// </summary>
public class WaveCfg : BaseResData
{
    public int Chapter;
    public int Index;
    public int[] Units;
    public int Num;
    public int Start;
    public float Curve;
    public int Diff;

    public override void FillData(string data)
    {
        var array = data.Split('|');

        ResourceInitUtil.InitField(array[0],out this.ID);
        ResourceInitUtil.InitField(array[1],out this.Chapter);
        ResourceInitUtil.InitField(array[2],out this.Index);
        ResourceInitUtil.InitShortArray(array[3],out this.Units);
        ResourceInitUtil.InitField(array[4],out this.Num);
        ResourceInitUtil.InitField(array[5],out this.Start);
        ResourceInitUtil.InitField(array[6],out this.Curve);
        ResourceInitUtil.InitField(array[7],out this.Diff);

    }

public new static bool HasKey {get{return true;}}
}
