using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(UIBaseInspector))]
public class UIBaseInspectorEdit : Editor
{
    public TextField textfield;
    public Button ModelBtn;
    public Button createBtn;
    UIBaseInspector uiBaseInspector;
    static bool isCreate = false;
    static string uiName = "UIBase";
    public override VisualElement CreateInspectorGUI()
    {
        var root = new VisualElement();
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/Inspector/UIToolKit/UibaseTemplateKit.uxml");
        visualTree.CloneTree(root);
        uiBaseInspector = target as UIBaseInspector;
        textfield = root.Q<TextField>("uiName");
        ModelBtn = root.Q<Button>("ModelBtn");
        ModelBtn.clickable.clicked += ModelOnclick();
        createBtn = root.Q<Button>("createBtn");
        createBtn.clickable.clicked += CreateOnClick();
        textfield.RegisterValueChangedCallback(OnTextFieldValueChanged);
        Init();
        
        EditorApplication.hierarchyChanged += OnHierarchyChanged;
        return root;
    }

    private void OnTextFieldValueChanged(ChangeEvent<string> evt)
    {
        uiBaseInspector.name = evt.newValue;
    }

    public void OnHierarchyChanged()
    {
        Init();
    }


    private Action CreateOnClick()
    {
        return () =>{
            if (textfield.value != null && textfield.value != "")
            {
                CreateScriptFile(textfield.value);
                //SaveAsPrefab(uiBaseInspector.gameObject, textfield.value);
            }
        };
    }

    private Action ModelOnclick()
    {
        return () =>
        {
            GameObject obj = Resources.Load<GameObject>("System/UIBaseModel");
            obj = GameObject.Instantiate(obj);
            obj.name = "UIBaseModel";
            GameObjectUtility.SetParentAndAlign(obj, uiBaseInspector.gameObject);
        };
    }

    public void Init()
    {
        textfield.value = uiBaseInspector.name;
        uiName = uiBaseInspector.name;
    }

    public void CreateScriptFile(string newName)
    {
        string scriptPath = "Assets/Scripts/UI/" + newName + ".cs";
        uiName = newName;
        EditorPrefs.SetString("UIBaseData", uiName);
        if (!File.Exists(scriptPath))
        {
            string templatePath = "Assets/Scripts/Framework/Common/UIBase/UIBaseTemplate.text";
            string templateContent = File.ReadAllText(templatePath);
            templateContent = templateContent.Replace("{0}", newName);
            File.WriteAllText(scriptPath, templateContent);
            AssetDatabase.Refresh();
        }
        else
        {
            Debug.LogWarning("脚本文件已存在：" + scriptPath);
        }
    }

    [DidReloadScripts]
    static void OnDidReloadScripts()
    {
        var bindScripts = EditorPrefs.GetString("UIBaseData");
        if (string.IsNullOrEmpty(bindScripts))
            return;
        System.Type newScriptType = GetScriptType(bindScripts);
        GameObject createObj = null;
        createObj = GameObject.Find(bindScripts);
        if (createObj != null)
        {
            EditorPrefs.SetString("UIBaseData", "");
            if (createObj.GetComponent(newScriptType) == null)
                createObj.AddComponent(newScriptType);
            SaveAsPrefab(createObj, bindScripts);
        }
        else
        {
            DebugEX.Log("Not Find", bindScripts);
        }
    }

    static void SaveAsPrefab(GameObject obj, string uiName)
    {
        // 选择保存路径
        string prefabPath = "Assets/Resources/UI/" + uiName + ".prefab";
        // 检查是否已存在同名的预制体
        if (AssetDatabase.LoadAssetAtPath(prefabPath, typeof(GameObject)))
        {
            // 如果已存在同名预制体，提示并退出
            if (!EditorUtility.DisplayDialog("提示", "已存在同名预制体，是否替换？", "是", "否"))
            {
                return;
            }
        }
        obj.name = uiName;
        if(obj.GetComponent<UIBaseInspector>() != null)
            DestroyImmediate(obj.GetComponent<UIBaseInspector>());
        // 保存为预制体
        GameObject prefab = PrefabUtility.SaveAsPrefabAssetAndConnect(obj, prefabPath, InteractionMode.UserAction);
        //PrefabUtility.ReplacePrefab(obj, prefab, ReplacePrefabOptions.ConnectToPrefab);

        DebugEX.LogFrameworkMsg("创建UI成功", uiName);
        //DestroyImmediate(obj);
        // 刷新AssetDatabase，使新创建的预制体在Unity中可见
        AssetDatabase.Refresh();
    }

  

    static System.Type GetScriptType(string scriptName)
    {
        // 获取已加载的程序集
        Assembly[] assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
        foreach (var assembly in assemblies)
        {
            // 在每个程序集中查找指定名称的类型
            System.Type type = assembly.GetType(scriptName);
            if (type != null)
            {
                // 如果找到类型，则返回
                return type;
            }
        }
        return null;
    }
}
