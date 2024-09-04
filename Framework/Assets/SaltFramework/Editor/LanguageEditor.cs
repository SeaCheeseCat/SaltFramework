using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LanguageEditor
{
    [LabelText("语言选择"),BoxGroup("Preview")]
    public Language language = Language.Chinese;
    [TableList, LabelText("多语言编辑器:预览词库"),BoxGroup("Preview")]
    public List<LanguageItem> languageList = new List<LanguageItem>()
    {

    };

    [Button("重新加载",30), BoxGroup("Preview")]
    public void PreviewOnclick()
    {
        LoadConfig();
        UpdateLanguageTexts(language);
    }

    [Button("生成配置", 30), BoxGroup("Preview")]
    public void BuildOnclick()
    {
        ArchiveManager.Instance.SaveLanguageConfigToJsonFile<List<LanguageItem>>(languageList);
        AssetDatabase.Refresh();
    }

    private void UpdateLanguageTexts(Language language)
    {
        LanguageText[] languageTexts = GameObject.FindObjectsOfType<LanguageText>();

        foreach (LanguageText languageText in languageTexts)
        {
            // 获取Text组件
            Text textComponent = languageText.GetComponent<Text>();

            if (textComponent != null)
            {
                // 设置文本内容，这里假设你有一个获取文本内容的方法，你需要根据实际情况修改
                string newText = GetTextFromYourSource(languageText.id, language);

                textComponent.text = newText;

                // 打印日志以便在控制台中查看
                Debug.Log("Updated LanguageText: " + languageText.gameObject.name);
            }
        }
    }

    private string GetTextFromYourSource(int id, Language language)
    {
        foreach (var item in languageList)
        {
            if (item.id == id)
            {
                if (language == Language.Chinese)
                {
                    return item.ChineseLanguage;
                }
                foreach (var val in item.languages)
                {
                    if (val.language == language)
                        return val.content;
                }
            }
        }
        return "";
    }

    public void LoadConfig() 
    {
        var data = ArchiveManager.Instance.LoadLanguageConfigFromJson<List<LanguageItem>>();
        languageList = data;
    }
}

