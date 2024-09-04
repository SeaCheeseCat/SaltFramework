using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugGame : MonoBehaviour
{
    
    /// <summary>
    /// Base: update
    /// </summary>
    void Update()
    {   
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.C))
        {
        }
#endif
    }

    
}
