using System;
using System.Collections.Generic;
/// <summary>
/// 
/// </summary>
public class TaskCfg 
{
    public int ID;
    public int TaskID;
    public int[] Chapter;
    public string Content;
    public float[] Angles;
    public string Event;
    public string[] Args;

    public static string configName = "TaskCfg";
    public static TaskCfg config { get; set; }
	public static LanguageConfigData languageConfigData;
	public string version { get; set; }
    public List<TaskCfg> datas { get; set; }

	public static TaskCfg Get(int id)
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

	public static List<TaskCfg> GetList()
	{
		if (config == null)
		{
			config = ConfigManager.Instance.Readjson<TaskCfg>(configName);
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