using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class LanguageManager : Manager<LanguageManager>
{
    //Tip: 存在的各种语言文本
    public List<LanguageText> languageTexts = new List<LanguageText>();
    //Tip：全部的语言数据
    public List<LanguageItem> languageDatas = new List<LanguageItem>();
    /// <summary>
    /// Base:init
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override IEnumerator Init(MonoBehaviour obj)
    {
        languageDatas = ArchiveManager.Instance.LoadLanguageConfigFromJson<List<LanguageItem>>();
        MsgManager.Instance.SendMessage(MSGInfo.LanguageComplete);
        return base.Init(obj);
    }

    /// <summary>
    /// Add:
    /// 添加一个语言文本
    /// </summary>
    public LanguageItem AddLanguagerText(LanguageText val)
    {
        DebugEX.Log("添加文本",val.id);
        if (!languageTexts.Contains(val))
        { 
            languageTexts.Add(val);
        }
        foreach (var item in languageDatas)
        {
            if (item.id == val.id)
                return item;
        }
        return null;
    }

    /// <summary>
    /// Update:
    /// 更新语言
    /// </summary>
    /// <param name="language"></param>
    public void UpdateLanguage(Language language) 
    {
        foreach (var item in languageTexts)
        {
            item.UpadteLanguage(language);
        }
    }


    /// <summary>
    /// Get:
    /// 处理Config配置表中的多语言设置
    /// {[en:NPc Not Rotate]  [jp:ssd]}  (示例:传入的语言文本示例  应该是这样)
    /// </summary>
    public Dictionary<Language, string> GetConfigLanguageString(string val)
    {
        var value = val;
        if (!value.Contains("{") || !value.Contains("{"))
            return null;

        // 提取 en 和 jp 的内容
        Dictionary<Language, string> extractedContents = ExtractContents(value);
        return extractedContents;

    }

    /// <summary>
    /// Get:
    /// 获取处理Config配置表中关于多语言的设置 
    /// {[en:a&b][jp:c&d]}
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    public Dictionary<Language, string[]> GetConfigLanguageArray(string val)
    {
        var value = val;
        if (!value.Contains("{") || value.Contains("{"))
            return null;

        // 提取 en 和 jp 的内容
        Dictionary<Language, string> extractedContents = ExtractContents(value);
        Dictionary<Language, string[]> results = new Dictionary<Language, string[]>();
        foreach (var item in extractedContents)
        {
            string[] arr = item.Value.Split('&');
            var res = new string[arr.Length];

            for (int k = 0; k < arr.Length; k++)
            {
                res[k] = arr[k];
            }

            results[item.Key] = res;
        }


        return results;

    }


    /// <summary>
    /// Extract:
    /// 将字符替换成需要的
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    static Dictionary<Language, string> ExtractContents(string input)
    {
        Dictionary<Language, string> extractedContents = new Dictionary<Language, string>();

        // 匹配 [en:...] 或 [jp:...]
        Regex regex = new Regex(@"\[(en|jp|fr):(.*?)\]");

        // 在输入文本中查找匹配项
        MatchCollection matches = regex.Matches(input);

        // 处理每个匹配项
        foreach (Match match in matches)
        {
            // 获取标签和内容
            string tag = match.Groups[1].Value;
            string content = match.Groups[2].Value;

            // 添加到提取的内容字典
            if (tag == "en")
            {
                extractedContents[Language.English] = content;
            }
            else if (tag == "jp")
            {
                extractedContents[Language.Japanese] = content;
            }
            else if (tag == "fr")
            {
                extractedContents[Language.French] = content;
            }
           
        }

        return extractedContents;
    }

}

[Serializable]
public class LanguageItem
{
    public int id;
    public string ChineseLanguage;
    [TableList]
    public List<languaItems> languages = new List<languaItems>()
    {

    };
}

[Serializable]
public class languaItems
{
    public Language language;
    public string content;
}

public class LanguageTextValue
{
    public string content;
    public Dictionary<Language, string> dic;
}

public class LanguageTextArrayValue
{
    public string[] content;
    public Dictionary<Language, string[]> dics;
}