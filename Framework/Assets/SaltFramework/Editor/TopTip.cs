using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TopTip : OdinEditorWindow
{
    [HideInInlineEditors][HideInEditorMode]
    public Action yesAction;
    [HideInInlineEditors][HideInEditorMode]
    public Action noAction;

    [ReadOnly]
    public string Title;

    [ReadOnly]
    public string Content;

    public void InitData(string Title, string Content,Action yesAction = null, Action noAction = null)
    {
        this.Title = Title;
        this.Content = Content;
        this.yesAction = yesAction;
        this.noAction = noAction;
    }


    [Button("确定",ButtonSizes.Large)]
    [ButtonGroup("功能区")]
    private void YesWinds()
    {
        yesAction?.Invoke();
        Close();
    }

    [Button("关闭",ButtonSizes.Large)]
    [ButtonGroup("功能区")]
    private void CloseWinds()
    {
        noAction?.Invoke();
        Close();
    }


}
