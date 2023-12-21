using System;

/// <summary>
/// 信件任务
/// </summary>
public class LetterTaskCfg : BaseResData
{
    public string[] Contents;
    public int TargetLetterId;
    public int TargetStateId;
    public int OwnLetter;
    public string Emotion;

    public override void FillData(string data)
    {
        var array = data.Split('|');

        ResourceInitUtil.InitField(array[0],out this.ID);
        ResourceInitUtil.InitShortArray(array[1],out this.Contents);
        ResourceInitUtil.InitField(array[2],out this.TargetLetterId);
        ResourceInitUtil.InitField(array[3],out this.TargetStateId);
        ResourceInitUtil.InitField(array[4],out this.OwnLetter);
        ResourceInitUtil.InitField(array[5],out this.Emotion);

    }

public new static bool HasKey {get{return true;}}
}
