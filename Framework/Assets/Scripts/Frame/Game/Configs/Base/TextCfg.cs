using System;
using System.Diagnostics;

/// <summary>
/// 文本配置
/// </summary>
public class TextCfg : BaseResData
{
    public string cn;
    public string en;
    public string gcn;
    public string tcn;
    public string jp;

    public override void FillData(string data)
    {
        var array = data.Split('|');

        try
        {
            ResourceInitUtil.InitField(array[0], out this.ID);
            ResourceInitUtil.InitField(array[1], out this.cn);
            ResourceInitUtil.InitField(array[2], out this.en);
            ResourceInitUtil.InitField(array[3], out this.tcn);
            //ResourceInitUtil.InitField(array[4], out this.gcn);
            ResourceInitUtil.InitField(array[4], out this.jp);
        }
        catch (Exception)
        {
            UnityEngine.Debug.Log("错误文本"+data);
        }
    }

    public new static bool HasKey { get { return true; } }
}
