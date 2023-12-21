using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 索敌阶段
/// </summary>
public class AIStateChase : AIStateBase
{
    //SkillMono skill;    //准备释放的技能
    Vector3 direct;
    NavMeshPath navMeshPath;
    List<Vector3> path; //路径
    float findPathCache;    //重新寻路延迟
    NavMeshAgent agent;

    public override void SetUp(UnitAI ai)
    {
        base.SetUp(ai);
        agent = ai.GetComponent<NavMeshAgent>();
        navMeshPath = new NavMeshPath();
        path = new List<Vector3>();
    }

    public void SetSkill(/*SkillMono skill*/)
    {
       // this.skill = skill;
    }

    public override void OnExit()
    {
        base.OnEnter();
        //skill = null;
    }

    public override void Update()
    {
        base.Update();
        //if (Battle.Instance.Pause) return;
        if (findPathCache > 0f)
            findPathCache -= Time.deltaTime;

        //skill.CheckTarget();
        //if (skill.Target == null)
        //{
        //    skill.FindTarget();
        //}
        /*if (skill.Target != null)
        {
            direct = skill.Target.transform.position - AI.transform.position;
            if (direct.magnitude<= AI.owner.GetProperty(RpcData.PropertyType.Range) / 100f||skill.Config.InitCast==1)
            {
                //Debug.Log("距离够进");
                //施法距离内，直接施法
                var skill = this.skill;
                var state = AI.ChangeState(AIState.action) as AIStateAction;
                state.SetSkill(skill);
            }
            else
            {
                //判定是否能够移动
                if (AI.owner.UnitCfg.Speed <= 0f)
                {
                    skill.ClearTarget();
                    AI.ChangeState(AIState.search); //重新寻敌
                }
                else
                {
                    //可以移动
                    //首先进行寻路
                    if (findPathCache <= 0||path.Count<=0)
                    {
                        findPathCache = 0.5f;
                        path.Clear();
                        navMeshPath.ClearCorners();
                        if (agent.CalculatePath(skill.Target.transform.position, navMeshPath))
                        {
                            //写入寻路结果
                            //Debug.Log("路径长度:" + navMeshPath.corners.Length);
                            if(navMeshPath.status == NavMeshPathStatus.PathComplete)
                            {
                                foreach (var v in navMeshPath.corners)
                                {
                                    path.Add(v);
                                }
                            }
                        }
                    }

                    //存在寻路路径
                    if (path.Count > 0)
                    {
                        direct = path[0] - AI.transform.position;
                        if (direct.magnitude <= 0.1f)
                        {
                            path.RemoveAt(0);
                        }
                        else
                        {
                            AI.owner.Move(direct);
                        }   
                    }
                }
            }
        }
        else
        {
            AI.ChangeState(AIState.search); //重新寻敌
        }*/
    }
}
