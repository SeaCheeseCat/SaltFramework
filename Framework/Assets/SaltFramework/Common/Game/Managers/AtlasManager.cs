using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 图集管理器,游戏中动态使用的精灵图片从这里获取
/// </summary>
public class AtlasManager : Manager<AtlasManager>
{
    private const string ATLAS_PATH = "Atlases/";

    private Dictionary<string, Atlas> m_atlasDict = new Dictionary<string, Atlas>();

    /// <summary>
    /// 通过路径获取资源
    /// </summary>
    /// <returns>The sprite.</returns>
    /// <param name="spritePath">Sprite path.</param>
    public static Sprite GetSprite(string spritePath)
    {
        return Instance.GetSpriteInner(spritePath);
    }

    #region 内部方法
    /// <summary>
    /// 获取Sprite
    /// </summary>
    /// <param name="spritePath"></param>
    /// <returns></returns>
    private Sprite GetSpriteInner(string spritePath)
    {
        string[] path = spritePath.Split('/');
        return GetSpriteInner(path[0], path[1]);
    }

    /// <summary>
    /// 获取Sprite
    /// </summary>
    /// <param name="atlasName"></param>
    /// <param name="spriteName"></param>
    /// <returns></returns>
    private Sprite GetSpriteInner(string atlasName, string spriteName)
    {
        Atlas atlas;
        if (!m_atlasDict.TryGetValue(atlasName, out atlas))
        {
            GameObject obj = ResourceManager.LoadPrefabSync(ATLAS_PATH + atlasName) as GameObject;
            if (obj != null)
            {
                atlas = obj.GetComponent<Atlas>();
                atlas.Init();
                m_atlasDict.Add(atlasName, atlas);
            }
        }
        if (atlas != null)
        {
            return atlas.GetSpriteByName(spriteName);
        }
        else
        {
            Debug.Log("未找到图标" + atlasName + "/" + spriteName);
            return null;
        }
    }

    /// <summary>
    /// 获取图集
    /// </summary>
    /// <param name="atlasName"></param>
    /// <returns></returns>
    public List<string> GetSpriteList(string atlasName)
    {
        var rt = new List<string>();
        Atlas atlas;
        if (!m_atlasDict.TryGetValue(atlasName, out atlas))
        {
            GameObject obj = ResourceManager.LoadPrefabSync(ATLAS_PATH + atlasName) as GameObject;
            if (obj != null)
            {
                atlas = obj.GetComponent<Atlas>();
                atlas.Init();
                m_atlasDict.Add(atlasName, atlas);
            }
        }
        if (atlas != null)
        {
            foreach(var v in atlas.sprites)
            {
                rt.Add(v.name);
            }
        }
        return rt;
    }
    #endregion
}
