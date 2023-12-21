using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色身上的AI控制器
/// </summary>
public class UnitAI : MonoBehaviour
{
    //public UnitMono owner { get; private set; }  //持有对象
    Dictionary<AIState, AIStateBase> states = new Dictionary<AIState, AIStateBase>();    //状态池
    AIStateBase state;  //当前状态
    public AIState AIState;

   /* public void SetUp(UnitMono owner,AIState state)
    {
        this.owner = owner;
        ChangeState(state);

        var agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (agent != null)
        {
            agent.Warp(transform.position);
        }
    }*/

/*    protected virtual void Update()
    {
        if (Battle.Instance.Pause) return;
        if (owner == null || !owner.Alive) return;
        state?.Update();
    }*/

    /// <summary>
    /// 改变ai状态
    /// </summary>
    public virtual AIStateBase ChangeState(AIState type)
    {
        if (state != null)
        {
            state.OnExit();
        }
        if (!states.ContainsKey(type))
        {
            AIStateBase state = null;
            switch (type)
            {
                case AIState.search:
                    state = new AIStateSearch();
                    break;
                case AIState.action:
                    state = new AIStateAction();
                    break;
                case AIState.chase:
                    state = new AIStateChase();
                    break;
                case AIState.idle:
                    state = new AIStateIdle();
                    break;
            }
            state.SetUp(this);
            states.Add(type, state);
        }
        state = states[type];
        state.OnEnter();
        AIState = type;
        //Debug.Log("ai状态切换==>>" + type);
        return state;
    }
}

public enum AIState
{
    idle = 0,   //什么都不做
    search = 1, //寻敌阶段
    chase = 2,  //索敌阶段
    action = 3,   //行为阶段
}