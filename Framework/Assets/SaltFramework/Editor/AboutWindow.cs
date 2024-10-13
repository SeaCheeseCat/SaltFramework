using System.IO;
using UnityEditor;
using UnityEngine;
using Markdig;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

public class AboutWindow : OdinEditorWindow
{
    [Title("Version: 1.01"),BoxGroup("Version"),DisplayAsString(false), HideLabel]
    public string Version = "版本信息: 1.0.1";
    [BoxGroup("Version"), DisplayAsString(false), HideLabel]
    public string updateLog = "更新记录:\n" +
                               "1.0.0 - 初始版本\n" +
                               "1.0.1 - 修复了已知问题\n";

    private Vector2 scrollPosition;

    private GameObject uiTextObject;

    [MenuItem("▷ SaltFramework/About")]
    private static void OpenWindow()
    {
        var window = GetWindow<AboutWindow>("About");
        
    }

    [BoxGroup("Framework Document")]
    [DisplayAsString(false), HideLabel]
    public string Doc = "  ▷ SaltFramework\n  关于框架的更多信息可以访问 CheeseFramework @Github  \n  (那里有我用md写的具体文档)";

    [BoxGroup("Document"), Button, LabelText("打开GitHub")]
    public void OpenGitHub()
    {
        Application.OpenURL("https://github.com/SeaCheeseCat/SaltFramework");
    }

   

   

   






   
}
