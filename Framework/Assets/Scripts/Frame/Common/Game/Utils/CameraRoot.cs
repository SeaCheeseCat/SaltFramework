using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 主摄像机管理类,挂载于战斗主摄像机父节点,用于移动/旋转主摄像机
/// </summary>
public class CameraRoot : MonoBehaviour {

    private void AWake()
    {
    }

    private void OnEnable()
    {
        AdjustCameraPositionByResolution();
    }

    /// <summary>
    /// 根据当前分辨率调整摄像机位置,用于保证可以显示完整的战斗场景
    /// </summary>
    private void AdjustCameraPositionByResolution()
    {
        Resolution resolution = Screen.currentResolution;
        //16:9的为1.7777,4:3为1.3333故取1.5作为阈值
        if((float)resolution.width / (float)resolution.height < 1.5f)
        {
            transform.position = new Vector3(6.7f, 10.6f, 5.42f);
        }
        else
        {
            transform.position = new Vector3(6.5f, 10f, 5.42f);
        }
    }

}
