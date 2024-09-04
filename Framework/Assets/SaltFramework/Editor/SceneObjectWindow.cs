using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SceneObjectWindow : EditorWindow
{
    public ObjectField objectField;
    public ListView listView;
    public Button button;
    GameObject[] sceneObjects;
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    [MenuItem("Window/UI Toolkit/SceneObjectWindow")]
    public static void ShowExample()
    {
        SceneObjectWindow wnd = GetWindow<SceneObjectWindow>();
        wnd.titleContent = new GUIContent("SceneObjectWindow");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        /*  VisualElement label = new Label("Hello World! From C#");
          root.Add(label);*/

        // Instantiate UXML
        /* VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
         root.Add(labelFromUXML);*/
     /*   VisualElement label = new Label("Hello World! From C#");
        root.Add(label);*/

        // Instantiate UXML
        VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
        root.Add(labelFromUXML);
        var helpBox = new HelpBox("≤‚ ‘1", HelpBoxMessageType.None);
        var helpBox2 = new HelpBox("≤‚ ‘1", HelpBoxMessageType.Info);
        var helpBox3 = new HelpBox("≤‚ ‘1", HelpBoxMessageType.Warning);
        var helpBox4 = new HelpBox("≤‚ ‘1", HelpBoxMessageType.Error);
        var rightVe = root.Q<VisualElement>("right");
        rightVe.Add(helpBox);
        rightVe.Add(helpBox2);
        rightVe.Add(helpBox3);
        rightVe.Add(helpBox4);

        objectField = root.Q<ObjectField>("ObjectField");
        objectField.objectType = typeof(GameObject);
        objectField.allowSceneObjects = false;

        button = root.Q<Button>("RefreshBtn");
        button.clicked += OnRefresh();

        listView = root.Q<ListView>("ListView");
        listView.makeItem = MakeItem();
        listView.bindItem = BindItem();
        listView.onSelectionChange += OnListSelect();
    }

    private Action<IEnumerable<object>> OnListSelect()
    {
        return (val) =>
        {
            foreach (var item in val)
            {
                Selection.activeGameObject = item as GameObject;
            }
        };
    }



    private Action OnRefresh()
    {
        return () =>
        {
            Scene scene = SceneManager.GetActiveScene();
            sceneObjects = scene.GetRootGameObjects();
            listView.itemsSource = sceneObjects;
        };
    }

    private Func<VisualElement> MakeItem()
    {
        return () =>
        {
            Label label = new Label();
            label.style.unityTextAlign = TextAnchor.MiddleLeft;
            label.style.marginLeft = 5;
            return label;
        };
    }

    private Action<VisualElement, int> BindItem()
    {
        return (ve,index) =>
        {
            Label label = ve as Label;
            var go = sceneObjects[index];
            label.text = go.name;
        };
    }

}
