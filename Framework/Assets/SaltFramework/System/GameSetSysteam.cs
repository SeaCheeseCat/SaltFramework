using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetSysteam : Manager<GameSetSysteam>
{
    public bool isFullScreen = true;  // 默认全屏
    public int resolutionIndex = 0;  // 默认分辨率索引

    public override IEnumerator Init(MonoBehaviour obj)
    {
        LoadSettings();
        return base.Init(obj);
    }

    // 保存游戏设置
    public void SaveSettings()
    {
        PlayerPrefsManager.Instance.Add("FullScreen", isFullScreen ? 1 : 0);
        PlayerPrefsManager.Instance.Add("ResolutionIndex", resolutionIndex);
    }

    // 加载游戏设置
    public void LoadSettings()
    {
        isFullScreen = PlayerPrefsManager.Instance.Get("FullScreen", 1) == 1;  // 默认全屏
        resolutionIndex = PlayerPrefsManager.Instance.Get("ResolutionIndex", 0);  // 默认第一个分辨率
    }

}
