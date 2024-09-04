using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class ConfigManager : Manager<ConfigManager>
{
    public const string RESOURCE_BASE_PATH = "Config/Table/";

    public T Readjson<T>(string name)
    {
        UnityEngine.Object res = ResourceManager.LoadPrefabSync(RESOURCE_BASE_PATH + name);
        TextAsset textAsset = res as TextAsset;
        string str = textAsset.text;
        return JsonConvert.DeserializeObject<T>(str);
    }

    public LanguageConfigData ReadLanguageJson(string name)
    {
        UnityEngine.Object res = ResourceManager.LoadPrefabSync(RESOURCE_BASE_PATH + name);
        TextAsset textAsset = res as TextAsset;
        string str = textAsset.text;
        languageJson languagejson = new languageJson();
        //List<LanguageJsonCfgData> cfgDatas = new List<LanguageJsonCfgData>();
        languagejson = JsonConvert.DeserializeObject<languageJson>(str);
        var result = new LanguageConfigData();
        result.languageItemDatas = new Dictionary<int, List<LanguageData>>();

        for (int i = 0; i < languagejson.language.Length; i++)
        {
            var item = languagejson.language[i];
            var data = new List<LanguageData>();
            LanguageData languagedataCN = new LanguageData();
            languagedataCN.language = Language.Chinese;
            languagedataCN.value = item.CN;

            LanguageData languagedataEN = new LanguageData();
            languagedataEN.language = Language.English;
            languagedataEN.value = item.EN;

            LanguageData languagedataJP = new LanguageData();
            languagedataJP.language = Language.Japanese;
            languagedataJP.value = item.JP;


            data.Add(languagedataCN);
            data.Add(languagedataEN);
            data.Add(languagedataJP);
            result.languageItemDatas.Add(item.ID,data);
            //result.languageItemDatas[item.ID].AddRange(data);
        }
        

        return result;
    }


}

public class languageJson
{
    public LanguageJsonCfgData[] language;
}

public class LanguageJsonCfgData
{
    
    public int ID;
    public string CN;
    public string EN;
    public string JP;
}