using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetEditor : OdinMenuEditorWindow
{
    [Title("SaltFramework","欢迎使用框架！先给自己打个气！一定可以做完的,阴暗地爬行.jpg")]
    [LabelText("游戏名称"), BoxGroup("Tool")]
    public string gameName;
    [LabelText("默认语言"), BoxGroup("Tool")]
    public Language defalutLanguage = Language.Chinese;

    [Button("生成配置",ButtonSizes.Large),GUIColor(35f/225f, 219f/225f, 66f/225f)]
    public void BuildConfig() 
    {
        SaveConfig();
        var window = GetWindow<ConfigSuccesTip>();
        var path = "/Resources/Config/GameConfig";
        window.InitData(path);
        window.Show();
    }

    /// <summary>
    /// Save:
    /// 保存设置
    /// </summary>
    public void SaveConfig()
    {
        GameConfigData data = new GameConfigData();
        data.gamename = gameName;
        data.defalutlanguage = defalutLanguage;
        ArchiveManager.Instance.SaveGameConfigToJsonFile<GameConfigData>(data);
    }

    public void OnCreate()
    {
        Load();
    }

    public void Load()
    {
        var data = ArchiveManager.Instance.LoadGameConfigFromJson<GameConfigData>();
        if (data == null)
            return;
        gameName = data.gamename;
        defalutLanguage = data.defalutlanguage;
    }

    protected override OdinMenuTree BuildMenuTree()
    {
       
        throw new System.NotImplementedException();
    }
}
