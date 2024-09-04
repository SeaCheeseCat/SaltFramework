using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    //Tip: 物体id
    public int ID;

    //Tip: 语言字典
    public Dictionary<Language, string> languageDic = new Dictionary<Language, string>();
    //Tip: 语言数组字典
    public Dictionary<Language, string[]> languagearrayDic = new Dictionary<Language, string[]>();

    /// <summary>
    /// Init:
    /// 初始化多语言
    /// </summary>
    public void InitLanguage(Dictionary<Language, string> languageDic) 
    {
        this.languageDic = languageDic;
    }

    /// <summary>
    /// Init:
    /// 初始化多语言
    /// </summary>
    public void InitLanguage(Dictionary<Language, string[]> languagearrayDic)
    {
        this.languagearrayDic = languagearrayDic;
    }
    
    /// <summary>
    /// Base:start
    /// </summary>
    public virtual void Start()
    {
        MsgManager.Instance.AddObserver(MSGInfo.SWITCHLANGUAGE, OnSwitchLanguageEvent);
    }

    /// <summary>
    /// Callback:
    /// 当语言切换时执行
    /// </summary>
    /// <param name="msgData"></param>
    public virtual void OnSwitchLanguageEvent(MsgData msgData) 
    { 
       
    }

    /// <summary>
    /// Set:
    /// 设置多语言
    /// </summary>
    /// <param name="contentText"></param>
    /// <param name="val"></param>
    public void SetTextLanguage(Text contentText, string val) 
    {
        if (GameBaseData.language == Language.Chinese)
        {
            contentText.text = val;
            return;
        }
        contentText.text = GetLanguageValue(GameBaseData.language);
    }

    /// <summary>
    /// Get:
    /// 获取不同语言的文本值
    /// </summary>
    /// <param name="language"></param>
    public  string GetLanguageValue(Language language)
    {
        if (languageDic.ContainsKey(language))
            return languageDic[language];
        return "";
    }

    /// <summary>
    /// Get:
    /// 获取不同语言的文本值
    /// </summary>
    /// <param name="language"></param>
    public string[] GetLanguageArrayValue(Language language)
    {
        if (languagearrayDic.ContainsKey(language))
            return languagearrayDic[language];
        return new string[0];
    }

}
