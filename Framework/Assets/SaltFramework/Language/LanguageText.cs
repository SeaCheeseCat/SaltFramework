using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageText : MonoBehaviour
{
    //Tip: 语言文本的ID
    public int id;
    //Tip：语言数据
    public LanguageItem languageItem;
    //Tip: 文本框
    private Text mText;
    //Tip: 中文
    public string chineseValue;

    /// <summary>
    /// Base:awake
    /// </summary>
    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// Base:init
    /// 初始化数据
    /// </summary>
    public void Init()
    {
        if (mText == null)
            mText = GetComponent<Text>();
        MsgManager.Instance.AddObserver(MSGInfo.SWITCHLANGUAGE, (data) =>
        {
            var language = (Language)data.arg1;
            UpadteLanguage(language);
        });
    }

    /// <summary>
    /// Base:start
    /// </summary>
    private void Start()
    {
        MsgManager.Instance.AddObserver(MSGInfo.LanguageComplete, (val) =>
        {
            languageItem = LanguageManager.Instance.AddLanguagerText(this);
            if (languageItem == null)
                return;
            chineseValue = languageItem.ChineseLanguage;
        });
      
    }

    /// <summary>
    /// Update:
    /// 刷新语言
    /// </summary>
    /// <param name="language"></param>
    public void UpadteLanguage(Language language) 
    {
        if (languageItem == null)
            return;

        foreach (var item in languageItem.languages)
        {
            if (item.language == language)
            {
                mText.text = item.content;
            }
        }
    }

}
