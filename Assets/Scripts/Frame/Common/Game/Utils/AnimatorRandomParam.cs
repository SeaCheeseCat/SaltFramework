using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorRandomParam : StateMachineBehaviour
{
    public string ValueName;
    public int ValueMax;

    bool isDebug;   //打印开关
    private List<int> countList = new List<int>();    //随机列表
    private int curIndex;   //当前序号
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state

    override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //创建随机列表
        if (countList.Count <= 0)
        {
            for(int i = 0; i < ValueMax; i++)
            {
                countList.Add(i);
            }
        }

        if (countList.Count > 0)
        {
            //获取随机序号
            curIndex = MathUtils.RandomInList(countList);
            countList.Remove(curIndex);

            if (isDebug)
            {
                Debug.Log(ValueName + ":" + curIndex);
            }

            //设置随机动画
            animator.SetFloat(ValueName, curIndex);

            //animator.GetComponent<Model>().OnActiveRandom(ValueName, curIndex);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}