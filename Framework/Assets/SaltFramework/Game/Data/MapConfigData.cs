using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapConfigData
{
    //Tip: 章节
    public int Charpter;
    //Tip: 关卡
    public int Level;
    //Tip: 地图的NPC数据
    public List<MapNpcData> mapnpcdata;
    //Tip: 地图的模型数据
    public List<MapModelData> mapmodeldatas;
    //Tip: 地图的粒子数据
    public List<MapParticleData> mapparticledatas;
    //Tip: 地图的对话框数据
    public List<MapDialogData> mapdialogdatas;
    //Tip: 地图的地面数据
    public MapLandData maplanddata;
    //Tip: 地图的背景音效
    public List<string> aduioPaths;
}
