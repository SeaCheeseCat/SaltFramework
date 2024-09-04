using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class UibaseTemplateKit : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;
    
    public static void ShowExample()
    {
        UibaseTemplateKit wnd = GetWindow<UibaseTemplateKit>();
        wnd.titleContent = new GUIContent("UibaseTemplateKit");
    }

    public void CreateGUI()
    {
        VisualElement root = rootVisualElement;
        VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
        root.Add(labelFromUXML);
    }
}
