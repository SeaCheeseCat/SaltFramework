using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 关卡的启动器
/// 从这里启动
/// </summary>
public class GameStart : MonoBehaviour
{
    private void Awake()
    {

    }

    private void Start()
    {
        GameManager.Instance.player = UnitManager.Instance.CreateNpc(1) as Player;

    }

    public void Update()
    {
       
    }

}
