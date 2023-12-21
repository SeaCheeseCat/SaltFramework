using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeLight : MonoBehaviour
{
    public float shadowHight;
    public Color shadowColor;
    public Vector4 shadowDir;

    // Start is called before the first frame update
    void Start()
    {
        Shader.SetGlobalFloat("_shadowHight", shadowHight);
        Shader.SetGlobalColor("_shadowColor", shadowColor);
        Shader.SetGlobalVector("_shadowDir", shadowDir);
    }

    // Update is called once per frame
    void Update()
    {
        Shader.SetGlobalFloat("_shadowHight", shadowHight);
        Shader.SetGlobalColor("_shadowColor", shadowColor);
        Shader.SetGlobalVector("_shadowDir", shadowDir);

    }
}
