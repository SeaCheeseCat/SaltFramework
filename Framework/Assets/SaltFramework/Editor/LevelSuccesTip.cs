using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelSuccesTip : OdinEditorWindow
{

    [Title("关卡配置生成成功！")]
    public string path = "";

    [LabelText("章节"), FoldoutGroup("数据"), InfoBox("生成成功后数据保存在路径下面，此处仅预览"), ReadOnly]
    public int Chapter;
    [LabelText("关卡"), FoldoutGroup("数据"), ReadOnly]
    public int Level;

    [FoldoutGroup("数据"), LabelText("Npc")]
    public List<int> Npcs;
    [FoldoutGroup("数据"), LabelText("模型")]
    public List<int> Models;
    [FoldoutGroup("数据"), LabelText("地面数据")]
    public Vector3 Land;

    [Button(ButtonSizes.Large)]
    [ButtonGroup("功能区"), LabelText("打开路径")]
    private void OpenPath()
    {
        DirectoryInfo direction = new DirectoryInfo(path);
        System.Diagnostics.Process.Start(direction.FullName);
    }

    public void InitData(MapConfigData data)
    {
        Npcs = new List<int>();
        Chapter = data.Charpter;
        Level = data.Level;

        foreach (var item in data.mapnpcdata)
        {
            Npcs.Add(item.ID);
        }

        Land = new Vector3((float)data.maplanddata.x, (float)data.maplanddata.y, (float)data.maplanddata.z);
        path = Application.dataPath + "/Resources/Config/Map";
    }
    [Button(ButtonSizes.Large)]
    [ButtonGroup("功能区"), LabelText("关闭")]
    private void CloseWinds()
    {
        Close();
    }

    public static string PersistentDataPath
    {
        get
        {
            string path =
#if UNITY_ANDROID
         Application.persistentDataPath;
#elif UNITY_IPHONE && !UNITY_EDITOR
         Application.persistentDataPath;
#elif UNITY_STANDLONE_WIN || UNITY_EDITOR
         Application.persistentDataPath;
#else
        string.Empty;
#endif
            return path;
        }
    }
}
