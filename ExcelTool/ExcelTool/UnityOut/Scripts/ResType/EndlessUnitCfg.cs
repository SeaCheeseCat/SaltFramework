using System;

/// <summary>
/// 配置数量
/// </summary>
public class EndlessUnitCfg : BaseResData
{
    public int Name;
    public string Num;
    public int[] EndlessUnits;
    public int[] UnitNum;
    public string Text;
    public float StartTime_Minus;
    public int WaveType;

    public override void FillData(string data)
    {
        var array = data.Split('|');

        ResourceInitUtil.InitField(array[0],out this.ID);
        ResourceInitUtil.InitField(array[1],out this.Name);
        ResourceInitUtil.InitField(array[2],out this.Num);
        ResourceInitUtil.InitShortArray(array[3],out this.EndlessUnits);
        ResourceInitUtil.InitShortArray(array[4],out this.UnitNum);
        ResourceInitUtil.InitField(array[5],out this.Text);
        ResourceInitUtil.InitField(array[6],out this.StartTime_Minus);
        ResourceInitUtil.InitField(array[7],out this.WaveType);

    }

public new static bool HasKey {get{return true;}}
}
