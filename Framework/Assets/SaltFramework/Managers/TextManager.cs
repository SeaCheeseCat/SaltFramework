using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LanguageType
{
    cn = 1, //简体中文
    en = 2, //英文
    tcn =3, //繁体中文（台湾）
    gcn =4,  //繁体中文（香港）
    jp = 5 //日语
}

public class TextManager : Manager<TextManager>
{
    //public LanguageType LanguageType { get; private set; } = LanguageType.cn;

    public override IEnumerator Init(MonoBehaviour obj)
    {
       /* ConfigManager.GetConfigList<TextCfg>();
        if (PlayerPrefs.HasKey("LanType"))
        {
            //SetLanguageType(PlayerPrefs.GetInt("LanType"));
            yield return null;
        }*/
        /*
        string languageStr = Application.systemLanguage.ToString();
        if (languageStr.CompareTo("ChineseSimplified") == 0 ||
            languageStr.CompareTo("Chinese") == 0)
        {
            SetLanguageType(LanguageType.cn);
        }
        else if (languageStr.CompareTo("ChineseTraditional") == 0)
        {
            SetLanguageType(LanguageType.tcn);
        }
        else if (languageStr.CompareTo("Japanese") == 0)
        {
            SetLanguageType(LanguageType.jp);
        }
        else
        {
            SetLanguageType(LanguageType.en);
        }
        */
#if UNITY_EDITOR
        //SetLanguageType(GameBase.LanguageType);
#endif

        yield return base.Init(obj);
    }

    /// <summary>
    /// 设置语言类型
    /// </summary>
    /// <param name="newType">New type.</param>
    /*public static void SetLanguageType(RpcData.LanguageType newType)
    {
        if (GameBase.Instance.Account != null)
            GameBase.Instance.Account.Setting.Language = newType;
        PlayerPrefs.SetInt("LanType", (int)newType);
        Debug.Log("设置当前语言类型" + newType);
        ResetText();
    }*/

    /// <summary>
    /// 设置语言类型
    /// </summary>
    /// <param name="newType">New type.</param>
    public static void SetLanguageType(int newType)
    {
        /*if (GameBase.Instance.Account != null)
            GameBase.Instance.Account.Setting.Language = (RpcData.LanguageType)newType;*/
        PlayerPrefs.SetInt("LanType", newType);
        //ResetText();
    }

    /// <summary>
    /// 获取文本资源,静态方法版本
    /// </summary>
    /// <returns>The text.</returns>
    /// <param name="id">Identifier.</param>
  /*  public static string GetText (int id)
    {
        return Instance.GetTextInner (id).Replace("\\n", "\n").Replace(" ", "\u00A0");
    }*/

/*    public static string GetMitiLineText (int id)
    {
        return GetText (id).Replace ("\\n", "\n");
    }*/

    /// <summary>
    /// 获取文本资源,内部非静态方法版本
    /// </summary>
    /// <returns>The text inner.</returns>
    /// <param name="id">Identifier.</param>
    /*private string GetTextInner (int id)
    {
        var cfg = ConfigManager.GetConfigByID<TextCfg>(id);
        if (cfg != null)
        {
            switch (GetLanguageType())
            {
                case RpcData.LanguageType.Cn:
                    return cfg.cn;
                case RpcData.LanguageType.En:
                    return cfg.en;
                case RpcData.LanguageType.Tcn:
                    return cfg.tcn;
                case RpcData.LanguageType.Jp:
                    return cfg.jp;
                default:
                    return cfg.en;
            }
        }
        else
        {
            return "缺少文本:" + id;
        }
        return "";
    }*/

   /* public static void ResetText()
    {
        var texts = Object.FindObjectsOfType<UITextBinder>();
        foreach (var i in texts)
        {
            i.SetText();
        }
    }*/

/*    public RpcData.LanguageType GetLanguageType()
    {
        var account = GameBase.Instance.Account;
        if (account != null)
            return account.Setting.Language;

        if (PlayerPrefs.HasKey("LanType"))
        {
            var type = PlayerPrefs.GetInt("LanType");
            return (RpcData.LanguageType)type;
        }
        switch (GameBase.Instance.GetCountry())
        {
            case "CN":
                return RpcData.LanguageType.Cn;
            case "JP":
                return RpcData.LanguageType.Jp;
            case "TW":
                return RpcData.LanguageType.Tcn;
            case "HK":
                return RpcData.LanguageType.Tcn;
            default:
                return RpcData.LanguageType.En;

        }
    }*/
}