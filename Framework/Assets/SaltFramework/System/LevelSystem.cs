using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 关卡管理器
/// </summary>
public class LevelSystem : Manager<LevelSystem>
{
    /// <summary>
    /// Init:
    /// 初始化
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override IEnumerator Init(MonoBehaviour obj)
    {
        return base.Init(obj);
    }

    /// <summary>
    /// Load:
    /// 载入
    /// </summary>
    /// <param name="Chapter"></param>
    /// <param name="Level"></param>
    public void Load(int Chapter, int Level)
    {
        MapConfig.Instance.InitData();
        DeleteChild(MapConfig.Instance.npcsTrans);
        DeleteChild(MapConfig.Instance.modelsTrans);
        DeleteChild(MapConfig.Instance.particleTrans);
        DeleteChild(MapConfig.Instance.landTran);
    }

    /// <summary>
    /// Delete：
    /// 在Eidt模式删除一个物体下面所有的子物体
    /// </summary>
    /// <param name="item"></param>
    private void DeleteChild(Transform item)
    {
        GameObject[] items = new GameObject[item.childCount];
        for (int i = 0; i < item.childCount; i++)
        {
            items[i] = item.GetChild(i).gameObject;
        }

        foreach (var obj in items)
        {
            GameObject.Destroy(obj);
        }
    }

    /// <summary>
    /// Get:
    /// 获取配置表内容
    /// </summary>
    /// <param name="Chapter"></param>
    /// <param name="Level"></param>
    /// <returns></returns>
    public MapConfigData GetConfig(int Chapter, int Level) 
    {
        var data = ArchiveManager.Instance.LoadMapConfigFromJson<MapConfigData>(Chapter, Level);
        if (data == null)
        {
            DebugEX.LogError("读取关卡配置数据失败");
            return null;
        }
        return data;
    }
}
