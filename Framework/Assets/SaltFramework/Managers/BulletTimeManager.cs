using UnityEngine;

//子弹时间控制器
public class BulletTimeManager:Manager<BulletTimeManager>
{
    //持续时间
    public float delayTime;
    public System.Action OnBulletEnd;

    //激活子弹时间
    public void SetBulletTime(float length, float rate = 0.5f)
    {
        delayTime = length;
        Time.timeScale = rate;
    }

    //子弹时间更新
    public void OnUpdate(float deltaTime)
    {
        if(delayTime>0)
        {
            delayTime -= deltaTime;
            if(delayTime<=0)
            {
                Time.timeScale = 1;
                OnBulletEnd?.Invoke();
                OnBulletEnd = null;
            }
        }
    }
}
