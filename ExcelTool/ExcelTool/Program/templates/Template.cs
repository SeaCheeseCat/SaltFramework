using System;
using System.Collections.Generic;
/// <summary>
/// {0}
/// </summary>
public class {1} 
{
{2}
    public static string configName = {3};
    public static {4} config { get; set; }
	public static LanguageConfigData languageConfigData;
	public string version { get; set; }
    public List<{4}> datas { get; set; }

	public static {4} Get(int id)
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

	public static List<{4}> GetList()
	{
		if (config == null)
		{
			config = ConfigManager.Instance.Readjson<{4}>(configName);
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