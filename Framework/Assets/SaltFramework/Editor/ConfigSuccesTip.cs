using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ConfigSuccesTip : OdinEditorWindow
{

    [Title("配置生成成功！")]
    public string path = "";
    

    [Button(ButtonSizes.Large)]
    [ButtonGroup("功能区"), LabelText("打开路径")]
    private void OpenPath()
    {
        DirectoryInfo direction = new DirectoryInfo(path);
        System.Diagnostics.Process.Start(direction.FullName);
    }

    public void InitData(string path)
    {
        this.path = Application.dataPath + path;
    }

    [Button(ButtonSizes.Large)]
    [ButtonGroup("功能区"), LabelText("关闭")]
    private void CloseWinds()
    {
        Close();
    }

    public static string PersistentDataPath
    {
        get
        {
            string path =
#if UNITY_ANDROID
         Application.persistentDataPath;
#elif UNITY_IPHONE && !UNITY_EDITOR
         Application.persistentDataPath;
#elif UNITY_STANDLONE_WIN || UNITY_EDITOR
         Application.persistentDataPath;
#else
        string.Empty;
#endif
            return path;
        }
    }
}
