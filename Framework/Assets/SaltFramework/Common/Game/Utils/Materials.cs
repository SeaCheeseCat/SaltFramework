using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MaterialPair
{
    public MaterialPair (string name, Material material)
    {
        this.name = name;
        this.material = material;
    }

    public string name;
    public Material material;
}

public class Materials : MonoBehaviour
{
    public MaterialPair[] materials;
    private Dictionary<string, Material> m_materialDict = new Dictionary<string, Material> ();

    public void Setup ()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            m_materialDict.Add (materials[i].name, materials[i].material);
        }
    }

    public Material GetMaterial (string name)
    {
        m_materialDict.TryGetValue(name, out Material res);
        return res;
    }
}