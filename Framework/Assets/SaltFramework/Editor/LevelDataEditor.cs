using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDataEditor
{
    [LabelText("当前章节"), ReadOnly]
    public int chapter;
    [LabelText("当前关卡"), ReadOnly]
    public int level;

    [FoldoutGroup("场景数据")]
    [LabelText("Npc数据"), ReadOnly]
    public List<int> Npcs;

    [FoldoutGroup("场景数据")]
    [LabelText("模型数据"), ReadOnly]
    public List<int> Modes;

    [FoldoutGroup("场景数据")]
    [LabelText("粒子数据"), ReadOnly]
    public List<int> Particles;

    [FoldoutGroup("文件数据")]
    [LabelText("文件列表"), ReadOnly]
    public List<string> Files;


    [Button(ButtonSizes.Large), LabelText("更新数据")]
    public void RefreshData()
    {
        MapConfig.Instance.InitData();
        Npcs = new List<int>();
        Modes = new List<int>();
        Particles = new List<int>();
        chapter = GameBaseData.Chapter;
        level = GameBaseData.Level;

        

        var particledatas = MapConfig.Instance.Getmapparticledatas();
        foreach (var item in particledatas)
        {
            Particles.Add(item.ID);
        }


    }
}
