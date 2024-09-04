using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerPrefsViewerEdit : EditorWindow
{
    private List<string> keys;
    private List<string> values;
    private ListView listView;
    private VisualTreeAsset prefsTemplate;

    [MenuItem("▷ SaltFramework/PlayerPrefs管理器")]
    public static void ShowExample()
    {
        PlayerPrefsViewerEdit wnd = GetWindow<PlayerPrefsViewerEdit>();
        wnd.titleContent = new GUIContent("PlayerPrefs管理器");
    }

    public void OnEnable()
    {
        PlayerPrefsManager.Instance.Add("Test","Yes");
        PlayerPrefsManager.Instance.Add("Test2", "what");
        PlayerPrefsManager.Instance.Add("Test3", 23);
        PlayerPrefsManager.Instance.Add("Test4", false);
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/UIToolKit/PlayerPrefsViewerEdit.uxml");
        visualTree.CloneTree(rootVisualElement);

        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/UIToolKit/PlayerPrefsViewerEdit.uss");
        rootVisualElement.styleSheets.Add(styleSheet);

        prefsTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/UIToolKit/PlayerPrefsItemEditor.uxml");

        LoadPlayerPrefs();

        listView = rootVisualElement.Q<ListView>("keyList");
        if (keys == null || keys.Count <= 0)
            return;
        listView.itemsSource = keys;
        listView.makeItem = MakePrefsItem();
        listView.bindItem = BindPrefsItem();

        var addButton = rootVisualElement.Q<Button>("newKeyBtn");
        addButton.clicked += AddNewPref;

        var deleteAllBtn = rootVisualElement.Q<Button>("deleteAllBtn");
        deleteAllBtn.clicked += DeleteAll;

        var refreshButton = rootVisualElement.Q<Button>("refreshBtn");
        refreshButton.clicked += LoadPlayerPrefs;
    }

    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        keys.Clear();
        values.Clear();
        listView.Rebuild();
    }

    private System.Action<VisualElement, int> BindPrefsItem()
    {
        return (ve, index) =>
        {
            var deleteBtn = ve.Q<Button>("deleteBtn");
            var keyLabel = ve.Q<Label>("keyLabel");
            var valueLabel = ve.Q<TextField>("valueText");
            keyLabel.text = keys[index];
            valueLabel.value = values[index];
            deleteBtn.clicked += DeleteOnclick(index);

            valueLabel.RegisterValueChangedCallback(evt =>
            {
                values[index] = evt.newValue;
            });

            valueLabel.RegisterCallback<KeyDownEvent>(evt =>
            {
                if (evt.keyCode == KeyCode.Space)
                {
                    PlayerPrefs.SetString(keys[index], valueLabel.value);
                    PlayerPrefs.Save();
                    evt.StopPropagation(); // 防止空格键的默认行为
                }
            });
        };
    }

    private System.Func<VisualElement> MakePrefsItem()
    {
        return () =>
        {
            VisualElement element = prefsTemplate.CloneTree();
            return element;
        };
    }

    private Action DeleteOnclick(int index)
    {
        return () =>
        {
            keys.RemoveAt(index);
            values.RemoveAt(index);
            listView.Rebuild();
        };
    }

    private void LoadPlayerPrefs()
    {
        keys = new List<string>();
        values = new List<string>();
        foreach (var key in PlayerPrefs.GetString("PlayerPrefsKeys").Split(';'))
        {
            DebugEX.Log(key);
            if (!string.IsNullOrEmpty(key))
            {
                keys.Add(key);
                values.Add(PlayerPrefs.GetString(key));
            }
        }
    }

    public string GetAsString(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            if (PlayerPrefs.GetString(key, null) != null)
            {
                return PlayerPrefs.GetString(key);
            }
            else if (PlayerPrefs.GetFloat(key, float.MinValue) != float.MinValue)
            {
                return PlayerPrefs.GetFloat(key).ToString();
            }
            else if (PlayerPrefs.GetInt(key, int.MinValue) != int.MinValue)
            {
                int intValue = PlayerPrefs.GetInt(key);
                if (intValue == 0 || intValue == 1)
                {
                    return (intValue == 1).ToString();
                }
                else
                {
                    return intValue.ToString();
                }
            }
        }
        return null;
    }

    private void SavePlayerPrefs()
    {
        PlayerPrefs.SetString("PlayerPrefsKeys", string.Join(";", keys));
        PlayerPrefs.Save();
    }

    private void AddNewPref()
    {
        var newKeyWindow = new AddKeyWindow();
        newKeyWindow.titleContent = new GUIContent("添加数据");
        newKeyWindow.OnKeySubmitted += (newKey) =>
        {
            if (!string.IsNullOrEmpty(newKey) && !keys.Contains(newKey))
            {
                PlayerPrefs.SetString(newKey, "New Value");
                keys.Add(newKey);
                values.Add("New Value");
                SavePlayerPrefs();
                listView.Rebuild();
            }
        };
        newKeyWindow.ShowModal();
    }

    public class AddKeyWindow : EditorWindow
    {
        public Action<string> OnKeySubmitted;
        private TextField keyTextField;


        public void OnEnable()
        {     
            minSize = new Vector2(300, 100);
            maxSize = new Vector2(300, 100);

            var root = rootVisualElement;
            root.style.flexDirection = FlexDirection.Column;

            keyTextField = new TextField("Key:");
            root.Add(keyTextField);

            var submitButton = new Button(() => SubmitKey())
            {
                text = "Add Key"
            };
            root.Add(submitButton);
        }

        private void SubmitKey()
        {
            OnKeySubmitted?.Invoke(keyTextField.value);
            Close();
        }
    }

}
