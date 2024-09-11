using System;

/// <summary>
/// 内购配置
/// </summary>
public class ShopCfg : BaseResData
{
    public int Name;
    public string Icon;
    public int Type;
    public int Main;
    public int Sec;
    public string ShopId;
    public float Price;

    public override void FillData(string data)
    {
        var array = data.Split('|');

        ResourceInitUtil.InitField(array[0],out this.ID);
        ResourceInitUtil.InitField(array[1],out this.Name);
        ResourceInitUtil.InitField(array[2],out this.Icon);
        ResourceInitUtil.InitField(array[3],out this.Type);
        ResourceInitUtil.InitField(array[4],out this.Main);
        ResourceInitUtil.InitField(array[5],out this.Sec);
        ResourceInitUtil.InitField(array[6],out this.ShopId);
        ResourceInitUtil.InitField(array[7],out this.Price);

    }

public new static bool HasKey {get{return true;}}
}
