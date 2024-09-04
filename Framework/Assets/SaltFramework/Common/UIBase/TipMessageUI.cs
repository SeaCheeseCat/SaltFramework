using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class TipMessageUI : MonoBehaviour
{
    public Text msgTxt;
    private float m_lifeTime = 1.5f;

    //bool isInit = false;

    private void Awake()
    {
    }

    void Update()
    {
        if (m_lifeTime > 0f)
        {
            m_lifeTime -= Time.unscaledDeltaTime;
            /*
            if (m_lifeTime <= 1f&& !isInit)
            {
                isInit = true;
                TipsManager.Instance.isShow = false;
                UIManager.Instance.ShowQueueUI();
            }*/
            if(m_lifeTime<0)
            {
                Destroy(gameObject);
            }
        }
    }
}
