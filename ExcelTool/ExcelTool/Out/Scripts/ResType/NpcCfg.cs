using System;
using System.Collections.Generic;
/// <summary>
/// 
/// </summary>
public class NpcCfg 
{
    public int ID;
    public string Dialog;
    public string Path;
    public bool Islens;

    public static string configName = "NpcCfg";
    public static NpcCfg config { get; set; }
	public static LanguageConfigData languageConfigData;
	public string version { get; set; }
    public List<NpcCfg> datas { get; set; }

	public static NpcCfg Get(int id)
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

	public static List<NpcCfg> GetList()
	{
		if (config == null)
		{
			config = ConfigManager.Instance.Readjson<NpcCfg>(configName);
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