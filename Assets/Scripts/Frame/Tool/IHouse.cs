using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IHouse : MonoBehaviour 
{

    public float DoorX; //门
    public Transform OutPos;
    public Transform InPos;
    public Collider2D LimitCamaera;
   
    //打开门
    public virtual void OpenDoor() { 
    }

    public virtual void CloseDoor()
    {
    }

    //场景所携带的信息
    public string Content1 { get; set; }     //我低头看了看手表， 显示的时间是凌晨三点半

    public string Conten2 { get; set; }   //咖啡馆   在漆黑的雨夜中    只有微弱的灯光暗示它的存在


}
