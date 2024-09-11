using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageConfigData
{
    //public List<LanguageItemData> languageItemDatas;
    public Dictionary<int, List<LanguageData>> languageItemDatas;
}

public class LanguageItemData
{
    public int id;
    public List<LanguageData> languageDatas;
}

public class LanguageData
{
    public Language language;
    public string value;
}