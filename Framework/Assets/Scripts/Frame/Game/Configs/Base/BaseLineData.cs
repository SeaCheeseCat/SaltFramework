using System;

public class BaseLineData : BaseResData
{
    public string Content;
    public int LinkeType;
    public float[] Emo;
    public int[] Next;
    public int Chat;

    public override void FillData(string data)
    {
        var array = data.Split('|');
        ResourceInitUtil.InitField(array[0], out this.ID);
        ResourceInitUtil.InitField(array[1], out this.Content);
        ResourceInitUtil.InitField(array[2], out this.LinkeType);
        ResourceInitUtil.InitShortArray(array[3], out this.Emo);
        ResourceInitUtil.InitShortArray(array[4], out this.Next);
        ResourceInitUtil.InitField(array[5], out this.Chat);
    }

    public new static bool HasKey { get { return true; } }
}
