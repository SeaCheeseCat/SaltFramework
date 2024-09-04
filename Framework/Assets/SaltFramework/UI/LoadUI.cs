using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class LoadUI : UIBase
{
    //Tip: 进度值的文本显示
    public Text progressText;
    //Tip: 进度值的图片显示
    public Image progressImage;
    //Tip: 进度值的最大限制宽度
    public float maxProgressWidthVal;
    //Tip: 平滑移动时间
    private float animationDuration = 0.01f;
    /// <summary>
    /// To:
    /// 前往一个目标值
    /// </summary>
    /// <param name="progress"></param>
    public void ToTargetValue(float targetProgress) 
    {
        DOTween.To(() => progressImage.rectTransform.sizeDelta.x,
                   x => UpdateProgressBar(x),
                   targetProgress * maxProgressWidthVal,
                   animationDuration)
            .SetEase(Ease.InOutQuad)
            .OnUpdate(() => UpdateProgressText(targetProgress))
            .OnComplete(() => UpdateProgressText(targetProgress));
    }

    /// <summary>
    /// Update:
    /// 更新ProgressBar进度
    /// </summary>
    /// <param name="width"></param>
    private void UpdateProgressBar(float width)
    {
        RectTransform rt = progressImage.rectTransform;
        rt.sizeDelta = new Vector2(width, rt.sizeDelta.y);
    }
    
    /// <summary>
    /// Update:
    /// 更新ProgressBar文本框的进度 
    /// </summary>
    /// <param name="targetProgress"></param>
    private void UpdateProgressText(float targetProgress)
    {
        float currentProgress = progressImage.rectTransform.sizeDelta.x / maxProgressWidthVal;
        float progress = Mathf.Lerp(currentProgress, targetProgress, progressImage.fillAmount);
        progressText.text = Mathf.RoundToInt(progress * 100) + " %";
    }
}
