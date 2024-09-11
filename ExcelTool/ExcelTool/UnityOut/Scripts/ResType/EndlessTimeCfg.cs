using System;

/// <summary>
/// 启动时间
/// </summary>
public class EndlessTimeCfg : BaseResData
{
    public int Name;
    public string Num;
    public float 列1;
    public float Start;
    public float StartTime_Min;
    public float Curve;
    public float Curve_Min;
    public float Curve_reduce_value;
    public int WaveReduceNum;

    public override void FillData(string data)
    {
        var array = data.Split('|');

        ResourceInitUtil.InitField(array[0],out this.ID);
        ResourceInitUtil.InitField(array[1],out this.Name);
        ResourceInitUtil.InitField(array[2],out this.Num);
        ResourceInitUtil.InitField(array[3],out this.列1);
        ResourceInitUtil.InitField(array[4],out this.Start);
        ResourceInitUtil.InitField(array[5],out this.StartTime_Min);
        ResourceInitUtil.InitField(array[6],out this.Curve);
        ResourceInitUtil.InitField(array[7],out this.Curve_Min);
        ResourceInitUtil.InitField(array[8],out this.Curve_reduce_value);
        ResourceInitUtil.InitField(array[9],out this.WaveReduceNum);

    }

public new static bool HasKey {get{return true;}}
}
