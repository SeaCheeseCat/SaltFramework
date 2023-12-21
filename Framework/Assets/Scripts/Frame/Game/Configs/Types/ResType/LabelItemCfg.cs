using System;

/// <summary>
/// 第一章主线对话配置
/// </summary>
public class LabelItemCfg : BaseResData
{
    public string Title;
    public string[] Think;
    public string[] Args;

    public override void FillData(string data)
    {
        var array = data.Split('|');

        ResourceInitUtil.InitField(array[0],out this.ID);
        ResourceInitUtil.InitField(array[1],out this.Title);
        ResourceInitUtil.InitShortArray(array[2],out this.Think);
        ResourceInitUtil.InitShortArray(array[3],out this.Args);

    }

public new static bool HasKey {get{return true;}}
}
