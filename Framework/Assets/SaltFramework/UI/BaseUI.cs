using System;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class BaseUI : UIBase
{
    public Joystick joystick;
    internal override void OnOpen()
    {
        base.OnOpen();
    }

    internal override void Init(params object[] dialogArgs)
    {
        base.Init(dialogArgs);
    }

}