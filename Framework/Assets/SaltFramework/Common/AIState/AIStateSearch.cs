using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 寻敌阶段
/// </summary>
public class AIStateSearch : AIStateBase
{
    float cache;    //寻敌间隔

    public override void OnEnter()
    {
        base.OnEnter();
        cache = 0.1f;
    }

    public override void Update()
    {
        base.Update();
        cache -= Time.deltaTime;
        if (cache <= 0f)
        {
            cache += 0.1f;

            //遍历查询可用技能并进行寻敌
            /*foreach(var v in AI.owner.Skills)
            {
                if (v.CanCast())
                {
                    //Debug.Log(AI.owner.ID+ "存在可释放技能" + v.ID);
                    var state = AI.ChangeState(AIState.chase) as AIStateChase;
                    state.SetSkill(v);
                    break;
                }
            }*/

            //Debug.Log("未找到可释放技能" + AI.owner.Skills.Count);
        }
    }
}
