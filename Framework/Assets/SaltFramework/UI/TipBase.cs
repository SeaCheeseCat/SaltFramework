using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipBase : UIBase
{
    //Tip: 内容文本框
    public Text contentText;
    //Tip: 标题文本框
    public Text titleText;
    //Tip: 确定按钮
    public Button yesBtn;
    //Tip: 取消按钮
    public Button cancelBtn;
    //Tip: 确定按钮文本
    public Text yesText;
    //Tip: 取消按钮文本
    public Text cancelText;
    //Tip: 关闭按钮
    public Button closeBtn;
    //Tip: 关闭按钮的物体
    public GameObject closeObj;

    /// <summary>
    /// Init:
    /// 初始化
    /// </summary>
    /// <param name="dialogArgs">自定义参数</param>
    internal override void Init(params object[] dialogArgs)
    {
        base.Init(dialogArgs);
        closeBtn.onClick.AddListener(() => {
            Close();
        });
    }

    /// <summary>
    /// Init:
    /// 初始化数据
    /// </summary>
    /// <param name="title">标题</param>
    /// <param name="content">内容</param>
    /// <param name="yesAction">确定事件</param>
    /// <param name="cancelAction">取消事件</param>
    public void Init(string title, string content, Action yesAction, Action cancelAction = null, bool hasCloseBtn = false)
    {
        titleText.text = title;
        contentText.text = content;
        yesBtn.onClick.RemoveAllListeners();
        cancelBtn.onClick.RemoveAllListeners();
        closeBtn.onClick.RemoveAllListeners();
        yesBtn.onClick.AddListener(() => { yesAction(); Close(); });
        closeObj.SetActive(hasCloseBtn);
        cancelBtn.onClick.AddListener(() => {
            if (cancelAction == null)
            {
                Close();
            }
            else
            {
                Close();
                cancelAction?.Invoke();
            }
               
        });
        closeBtn.onClick.AddListener(() => {
            Close();
        });
    }

    /// <summary>
    /// Init初始化数据 复杂版（可自定义确定按钮和取消按钮文本呢）
    /// </summary>
    /// <param name="title"></param>
    /// <param name="content"></param>
    /// <param name="yesContent"></param>
    /// <param name="cancelContent"></param>
    /// <param name="yesAction"></param>
    /// <param name="cancelAction"></param>
    public void Init(string title, string content, string yesContent, string cancelContent, Action yesAction, Action cancelAction = null, bool hasCloseBtn = false)
    {
        Init(title, content, yesAction, cancelAction, hasCloseBtn);
        yesText.text = yesContent;
        cancelText.text = cancelContent;
    }

    
    /// <summary>
    /// Add:
    /// 添加标题
    /// </summary>
    /// <returns></returns>
    public TipBase AddTitle(string title) 
    {
        titleText.text = title;
        return this;
    }


    /// <summary>
    /// Add:
    /// 添加内容
    /// </summary>
    /// <returns></returns>
    public TipBase AddContent(string content)
    {
        contentText.text = content;
        return this;
    }

    /// <summary>
    /// Add:
    /// 添加确定点击事件
    /// </summary>
    /// <param name="action">事件</param>
    /// <returns></returns>
    public TipBase AddYesClickEvent(Action action)
    {
        yesBtn.onClick.AddListener(() => { action(); });
        return this;
    }

    /// <summary>
    /// Add:
    /// 添加No点击事件
    /// </summary>
    /// <param name="action">事件</param>
    /// <returns></returns>
    public TipBase AddNoClickEvent(Action action)
    {
        cancelBtn.onClick.AddListener(() => { action(); });
        return this;
    }

    /// <summary>
    /// Add:
    /// 添加确定文本
    /// </summary>
    /// <param name="text">文本内容</param>
    /// <returns></returns>
    public TipBase AddYesText(string text) 
    {
        yesText.text = text;
        return this;
    }

    /// <summary>
    /// Add:
    /// 添加No文本
    /// </summary>
    /// <param name="text">文本内容</param>
    /// <returns></returns>
    public TipBase AddNoText(string text) 
    {
        cancelText.text = text;
        return this;
    }

    /// <summary>
    /// Enable:
    /// 是否启用Close按钮
    /// </summary>
    /// <param name="enable"></param>
    /// <returns></returns>
    public TipBase EnableClose(bool enable)
    {
        closeObj.SetActive(enable);
        return this;
    }

    /// <summary>
    /// Set:
    /// 设置字体大小
    /// </summary>
    /// <param name="tipUi">UI类型</param>
    /// <param name="size">字体大小</param>
    public TipBase FontSize(TipBaseUI tipUi, int size) 
    {
        if (tipUi == TipBaseUI.Title)
            titleText.fontSize = size;
        else if (tipUi == TipBaseUI.Content)
            contentText.fontSize = size;
        else if (tipUi == TipBaseUI.Yes)
            yesText.fontSize = size;
        else if(tipUi == TipBaseUI.No)
            cancelText.fontSize = size;
        return this;
    }


    /// <summary>
    /// Set:
    /// 设置字体加粗
    /// </summary>
    /// <param name="tipUi">UI类型</param>
    /// <param name="size">字体大小</param>
    public TipBase FontBold(TipBaseUI tipUi)
    {
        if (tipUi == TipBaseUI.Title)
            titleText.fontStyle = FontStyle.Bold;
        else if (tipUi == TipBaseUI.Content)
            contentText.fontStyle = FontStyle.Bold;
        else if (tipUi == TipBaseUI.Yes)
            yesText.fontStyle = FontStyle.Bold;
        else if (tipUi == TipBaseUI.No)
            cancelText.fontStyle = FontStyle.Bold;
        return this;
    }

}

public enum TipBaseUI
{ 
    Title,
    Content,
    Yes,
    No
}
