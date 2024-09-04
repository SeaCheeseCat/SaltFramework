using System;

public class BaseLetterData : BaseResData
{
    public string[] Contents;
    public int[] Types;
    public int[] Itemid;
    public int Page;

    public override void FillData(string data)
    {
        var array = data.Split('|');
        ResourceInitUtil.InitField(array[0], out this.ID);
        ResourceInitUtil.InitShortArray(array[1], out this.Contents);
        ResourceInitUtil.InitShortArray(array[2], out this.Types);
        ResourceInitUtil.InitShortArray(array[3], out this.Itemid);
        ResourceInitUtil.InitField(array[4], out this.Page);
    }

    public new static bool HasKey { get { return true; } }
}
