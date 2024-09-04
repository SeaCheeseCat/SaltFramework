using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipBoxUI : TipBase
{
    internal override void OnOpen()
    {
        base.OnOpen();
        gameObject.transform.localScale = new Vector3(1, 1, 1);
    }

    public override void OnClose()
    {
        base.OnClose();
        yesBtn.onClick.RemoveAllListeners();
        cancelBtn.onClick.RemoveAllListeners();
    }

}
