using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct SpritePair
{
    public SpritePair(string name, Sprite sprite)
    {
        this.name = name;
        this.sprite = sprite;
    }

    public string name;
    public Sprite sprite;
}

public class Atlas : MonoBehaviour
{
    public List<SpritePair> sprites = new List<SpritePair>();

    private Dictionary<string, Sprite> m_spriteDict = new Dictionary<string, Sprite>();

    public void Init()
    {
        m_spriteDict.Clear();
        for (int k = 0; k < sprites.Count; k++)
        {
            m_spriteDict.Add(sprites[k].name, sprites[k].sprite);
        }
    }

    public Sprite GetSpriteByName(string name)
    {
        if (m_spriteDict.ContainsKey(name))
        {
            return m_spriteDict[name];
        }
        else
        {
            Debug.LogError("未找到图标" + name);
            return null;
        }
    }
}