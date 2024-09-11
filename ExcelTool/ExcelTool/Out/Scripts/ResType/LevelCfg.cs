using System;
using System.Collections.Generic;
/// <summary>
/// 
/// </summary>
public class LevelCfg 
{
    public int ID;
    public int Chapter;
    public int Level;
    public int[] Trigger;
    public bool Storymode;

    public static string configName = "LevelCfg";
    public static LevelCfg config { get; set; }
	public static LanguageConfigData languageConfigData;
	public string version { get; set; }
    public List<LevelCfg> datas { get; set; }

	public static LevelCfg Get(int id)
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

	public static List<LevelCfg> GetList()
	{
		if (config == null)
		{
			config = ConfigManager.Instance.Readjson<LevelCfg>(configName);
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