using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : Manager<SystemManager>
{
    public bool isInit;

    /// <summary>
    /// 创建音效物体
    /// </summary>
    public void CreateAudioSystem() 
    {
        if (isInit)
            return;
        var obj = SpawnPrefab("AudioManager");
        var audio = obj.GetComponent<AudioManager>();
        audio.InitCompentConfig();
        audio.Init();
        isInit = true;
    }

    /// <summary>
    /// 创建长按管理
    /// </summary>
    public void CreateLongPressDetectorSystem()
    {
        SpawnPrefab("LongPressDetectorManager");
    }

    public void CreateCameraSystem()
    {
        var canvs = GameObject.Find("Canvas").transform;
        var unit = SpawnPrefab("CameraSystem");
        unit.transform.SetParent(canvs,false);
        
    }

    /// <summary>
    /// 创建长按管理
    /// </summary>
    public void CreateInputKeySystem()
    {
        SpawnPrefab("InputKeyManager");
    }
    GameObject SpawnPrefab(string prefabName)
    {
        GameObject prefab = Resources.Load<GameObject>("System/" + prefabName);
        if (prefab != null)
        {
            // 在场景中创建物体
            GameObject spawnedObject = GameObject.Instantiate(prefab);
            return spawnedObject;
        }
        Debug.LogError("Prefab not found: " + prefabName);
        return null;
    }
}
