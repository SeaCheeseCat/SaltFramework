using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using static LevelEditor;

public class LevelLoadEditor : OdinMenuEditorWindow
{
    private BaseSetEditor baseSetEditor;
    public void Init(BaseSetEditor baseSetEditor)
    {
        this.baseSetEditor = baseSetEditor;
        LoadEditData();
    }
    [Title("关卡列表")]
    [ReadOnly]
    public List<string> LevelList;

    [LabelText("章节"), FoldoutGroup("加载"), InfoBox("请确保该关卡已经配置完成（可以看看上面的关卡列表中有没有此关） (阴暗地飞行)")]
    public int Chapter;
    [LabelText("关卡"), FoldoutGroup("加载")]
    public int Level;

    [GUIColor(0, 1, 0)]
    [Button("加载关卡", ButtonSizes.Large)]
    [FoldoutGroup("加载")]
    private void LoadMap()
    {
        baseSetEditor.ResetScene();
        MapConfig.Instance.InitData();
        var data = ArchiveManager.Instance.LoadMapConfigFromJson<MapConfigData>(Chapter, Level);
        if (data == null)
        {
            DebugEX.LogError("读取关卡配置数据失败");
            return;
        }
      
        baseseteditor.SetData(data.Charpter, data.Level, data.aduioPaths);
        DebugEX.LogFrameworkMsg("关卡加载成功！");
    }

    [GUIColor(1, 0f, 0f)]
    [Button("清空场景", ButtonSizes.Medium)]
    [FoldoutGroup("加载")]
    private void RestScene()
    {
        baseSetEditor.ResetScene();
    }

    [GUIColor(1, 0f, 0)]
    [Button("删除关卡", ButtonSizes.Medium)]
    [FoldoutGroup("加载")]
    private void DeleteLevel()
    {
        var window = GetWindow<TopTip>();
        window.InitData("删除关卡","确定删除关卡吗？配置文件将被删除",()=> {
            ArchiveManager.Instance.DeleteMapConfig(Chapter, Level);
            LoadEditData();
            DebugEX.Log("删除关卡成功: "+Chapter+" - "+Level);
        });
        window.Show();
    }


    [GUIColor(1, 1, 1)]
    [Button("刷新关卡列表", ButtonSizes.Large)]
    [ButtonGroup("功能区")]
    private void LoadEditData()
    {
        LevelList = new List<string>();
        var list = ArchiveManager.Instance.LaodMapConfigDic();
        foreach (var item in list)
        {
            if (item.Contains("meta"))
                continue;
            var name = ParseAndPrintLevelInfo(Path.GetFileName(item)) + " [" + Path.GetFileName(item) + "]";
            LevelList.Add(name);
        }
    }

    /// <summary>
    /// Parse:
    /// 解析文件名字
    /// 成为关卡信息的状态
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    string ParseAndPrintLevelInfo(string fileName)
    {
        // 使用正则表达式解析文件名
        Match match = Regex.Match(fileName, @"mapconfig_(\d+)_(\d+).json");
       
        if (match.Success)
        {
            int chapter = int.Parse(match.Groups[1].Value);
            int level = int.Parse(match.Groups[2].Value);
            return "第 " + chapter + " 章 " + " 第" + level + "关";
        }
        return "关卡解析失败";
    }

    protected override OdinMenuTree BuildMenuTree()
    {
        throw new System.NotImplementedException();
    }
}
