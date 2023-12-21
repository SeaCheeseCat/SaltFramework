using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ai状态机基类
/// </summary>
public class AIStateBase
{
    protected UnitAI AI;

    public virtual void SetUp(UnitAI ai)
    {
        AI = ai;
    }

    /// <summary>
    /// 进入
    /// </summary>
    public virtual void OnEnter()
    {

    }

    /// <summary>
    /// 及时更新
    /// </summary>
    public virtual void Update()
    {

    }

    /// <summary>
    /// 退出
    /// </summary>
    public virtual void OnExit()
    {

    }
}
