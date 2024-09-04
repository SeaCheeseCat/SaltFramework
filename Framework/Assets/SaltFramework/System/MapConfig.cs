using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MapConfig : Manager<MapConfig>
{
    // Tip: NPC的Transform
    public Transform npcsTrans;

    // Tip: 模型的Transform
    public Transform modelsTrans;

    // Tip: 粒子效果的Transform
    public Transform particleTrans;

    //Tip: 地面坐标
    public Transform landTran;

    // Tip: NPC的字符串标识
    private const string npcTr = "Npc";

    // Tip: 场景模型的字符串标识
    private const string sceneTr = "SceneModel";

    // Tip: 粒子效果的字符串标识
    private const string particleTr = "Particle";

    //Tip: 地面的位置
    private const string landTrName = "Land";

    // Tip: 地图配置数据
    private MapConfigData configData;

    //Tip: 临时储存编辑器Npc的数据处，可以做到临时储存编辑器时数据
    private Dictionary<int, Transform> tempEditNpcs = new Dictionary<int, Transform>();

    //Tip: 临时储存编辑器Npc的数据处，可以做到临时储存编辑器时数据
    private Dictionary<int, Transform> tempEditModel = new Dictionary<int, Transform>();

    /// <summary>
    /// Init:
    /// 初始化数据
    /// </summary>
    public void InitData()
    {
        npcsTrans = GameObject.Find(npcTr)?.transform;
        modelsTrans = GameObject.Find(sceneTr)?.transform;
        particleTrans = GameObject.Find(particleTr)?.transform;
        landTran = GameObject.Find(landTrName)?.transform;
    }





    public MapDialogData Getmapdialogdatas(Transform item, ItemBase npc)
    {

        return null;
    }


    // 递归方法获取所有子物体的信息
    DecryptObjectData[] GetChildModels(Transform parent)
    {
        List<DecryptObjectData> childDataList = new List<DecryptObjectData>();

        foreach (Transform child in parent)
        {
            var childData = new DecryptObjectData
            {
                x = child.localPosition.x,
                y = child.localPosition.y,
                z = child.localPosition.z,
                scalex = child.localScale.x,
                scaley = child.localScale.y,
                scalez = child.localScale.z,
                rotatex = child.localEulerAngles.x,
                rotatey = child.localEulerAngles.y,
                rotatez = child.localEulerAngles.z,
            };

            childDataList.Add(childData);
        }

        return childDataList.ToArray();
    }


    /// <summary>
    /// Save:
    /// 保存场景粒子数据
    /// </summary>
    private void SavePartitleData()
    {
        var data = Getmapparticledatas();
        if (data == null)
            return;
        configData.mapparticledatas = data;
    }

    /// <summary>
    /// Save:
    /// 保存场景地面数据
    /// </summary>
    private void SaveLandData()
    {
        var data = Getmaplanddata();
        if (data == null)
            return;
        configData.maplanddata = data;
    }

    /// <summary>
    /// Get:
    /// 获取场景粒子数据
    /// </summary>
    /// <returns></returns>
    public List<MapParticleData> Getmapparticledatas()
    {
        var datas = new List<MapParticleData>();
        if (particleTrans == null)
        {
            Debug.LogError("Npc Transform not found.");
            return null;
        }
        for (int i = 0; i < particleTrans.childCount; i++)
        {
            var item = particleTrans.GetChild(i);
            var npc = item.GetComponent<ParticleBase>();
            var data = new MapParticleData();
            data.x = item.localPosition.x;
            data.y = item.localPosition.y;
            data.z = item.localPosition.z;
            data.scalex = item.localScale.x;
            data.scaley = item.localScale.y;
            data.scalez = item.localScale.z;
            data.rotatex = item.localEulerAngles.x;
            data.rotatey = item.localEulerAngles.y;
            data.rotatez = item.localEulerAngles.z;
            data.ID = npc?.ID ?? 0;
            datas.Add(data);
        }
        return datas;
    }




    /// <summary>
    /// Get:
    /// 获取场景地面数据
    /// </summary>
    /// <returns></returns>
    public MapLandData Getmaplanddata()
    {
        var data = new MapLandData();
        if (landTran == null)
        {
            Debug.LogError("Land Transform not found.");
            return null;
        }
        data.x = landTran.localPosition.x;
        data.y = landTran.localPosition.y;
        data.z = landTran.localPosition.z;
        data.scalex = landTran.localScale.x;
        data.scaley = landTran.localScale.y;
        data.scalez = landTran.localScale.z;
        data.rotatex = landTran.localEulerAngles.x;
        data.rotatey = landTran.localEulerAngles.y;
        data.rotatez = landTran.localEulerAngles.z;

        var decryptObjs = landTran.GetChildFromName("DecryptObject", true);
        if (decryptObjs != null && decryptObjs.Length > 0)
        {
            var list = new DecryptObjectData[decryptObjs.Length];
            data.decryptObjectdatas = list; 
        }

        return data;
    }

}
