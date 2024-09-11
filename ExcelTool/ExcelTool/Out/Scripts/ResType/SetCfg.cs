using System;
using System.Collections.Generic;
/// <summary>
/// 
/// </summary>
public class SetCfg 
{
    public int ID;
    public string Args;

    public static string configName = "SetCfg";
    public static SetCfg config { get; set; }
    public string version { get; set; }
    public List<SetCfg> datas { get; set; }

	public static SetCfg Get(int id)
	{
		if (config == null)
		{
			config = ConfigManager.Instance.Readjson<SetCfg>(configName);
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

	public static List<SetCfg> GetList()
	{
		if (config == null)
		{
			config = ConfigManager.Instance.Readjson<SetCfg>(configName);
		}
		return config.datas;
	}
}