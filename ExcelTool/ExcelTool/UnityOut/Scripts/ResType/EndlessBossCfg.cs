using System;

/// <summary>
/// 配置数量
/// </summary>
public class EndlessBossCfg : BaseResData
{
    public int Name;
    public string Num;
    public int[] EndlessUnits;
    public int[] UnitNum;
    public string Text;
    public float Start;
    public float StartTime_Min;
    public float StartTime_Minus;
    public float Curve;
    public float Curve_Min;
    public float Curve_Minus;

    public override void FillData(string data)
    {
        var array = data.Split('|');

        ResourceInitUtil.InitField(array[0],out this.ID);
        ResourceInitUtil.InitField(array[1],out this.Name);
        ResourceInitUtil.InitField(array[2],out this.Num);
        ResourceInitUtil.InitShortArray(array[3],out this.EndlessUnits);
        ResourceInitUtil.InitShortArray(array[4],out this.UnitNum);
        ResourceInitUtil.InitField(array[5],out this.Text);
        ResourceInitUtil.InitField(array[6],out this.Start);
        ResourceInitUtil.InitField(array[7],out this.StartTime_Min);
        ResourceInitUtil.InitField(array[8],out this.StartTime_Minus);
        ResourceInitUtil.InitField(array[9],out this.Curve);
        ResourceInitUtil.InitField(array[10],out this.Curve_Min);
        ResourceInitUtil.InitField(array[11],out this.Curve_Minus);

    }

public new static bool HasKey {get{return true;}}
}
