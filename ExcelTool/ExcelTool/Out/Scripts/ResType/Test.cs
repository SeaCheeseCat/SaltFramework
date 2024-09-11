using System;
using System.Collections.Generic;
/// <summary>
/// 
/// </summary>
public class Test 
{
    public int ID;
    public string Args;

    public static string configName = "Test";
    public static Test config { get; set; }
	public static LanguageConfigData languageConfigData;
	public string version { get; set; }
    public List<Test> datas { get; set; }

	public static Test Get(int id)
	{
		if (config == null)
		{
			config = ConfigManager.Instance.Readjson<SetCfg>(configName);
			languageConfigData = ConfigManager.Instance.ReadLanguageJson(configName);;
		}

		foreach (var item in config.datas)
		{
			if (item.ID == id)
			{
				return item;
			}
		}
		return null;
	}

	public static List<Test> GetList()
	{
		if (config == null)
		{
			config = ConfigManager.Instance.Readjson<Test>(configName);
		}
		return config.datas;
	}

	public static string GetLangugeText(int id, Language language)
	{
		if (config == null)
		{
			config = ConfigManager.Instance.Readjson<TextCfg>(configName);
			languageConfigData = ConfigManager.Instance.ReadLanguageJson(configName); ;
		}

		if (languageConfigData.languageItemDatas.ContainsKey(id))
		{
			foreach (var data in languageConfigData.languageItemDatas[id])
			{
				if (data.language == language)
					return data.value;
			}
		}
		return "";
	}
}