using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class CreateEdit : MonoBehaviour
{
    [MenuItem("GameObject/UI/ButtonTemplate")]
    [MenuItem("GameObject/▷ SaltFramework/ButtonTemplate", false,0)]
    static void CreateCloseTemplate(MenuCommand menuCommand)
    {
        var obj = EditTool.CreatePrefabToScene("System/ButtonTemplate", "Button");
        GameObjectUtility.SetParentAndAlign(obj, menuCommand.context as GameObject);
        Selection.activeGameObject = obj;
    }


    static GameObject createdObject;
    static bool isPrefabCreated = false;
    static string newScriptName = "";
    [MenuItem("GameObject/UI/UIBase")]
    [MenuItem("GameObject/▷ SaltFramework/UIBase", false, -1)]
    static void CreatePrefabWithScript(MenuCommand menuCommand)
    {
        GameObject obj = Resources.Load<GameObject>("System/UIBaseTemplate");
        obj = GameObject.Instantiate(obj);
        obj.name = "UIBase";
        GameObjectUtility.SetParentAndAlign(obj, menuCommand.context as GameObject);
        Selection.activeGameObject = obj;
        EditorUtility.FocusProjectWindow();
        createdObject = obj;
        isPrefabCreated = true;
        EditorApplication.hierarchyChanged += OnHierarchyChanged;
    }


    [MenuItem("GameObject/▷ SaltFramework/DecryptObject", false, 1)]
    static void CreateDecryptObjectTemplate(MenuCommand menuCommand)
    {
        var obj = EditTool.CreatePrefabToScene("System/DecryptObject", "DecryptObject");
        GameObjectUtility.SetParentAndAlign(obj, menuCommand.context as GameObject);
        Selection.activeGameObject = obj;
    }


    static void OnHierarchyChanged()
    {
        if (isPrefabCreated && createdObject != null && createdObject.name != "UIBase")
        {
            //CreateScriptFile(createdObject.name);
            createdObject.name = createdObject.name;
            EditorApplication.hierarchyChanged -= OnHierarchyChanged;
        }
    }


    [MenuItem("Assets/▷ SaltFramework/Create Material from Texture", true)]
    private static bool ValidateCreateMaterial()
    {
        // 确保选择的是纹理
        return Selection.activeObject is Texture2D;
    }

    [MenuItem("Assets/▷ SaltFramework/Create Material from Texture")]
    private static void CreateMaterial()
    {
        // 获取选择的纹理
        Texture2D selectedTexture = Selection.activeObject as Texture2D;
        if (selectedTexture == null)
        {
            Debug.LogError("Selected object is not a texture.");
            return;
        }

        // 创建一个新的材质
        Material material = new Material(Shader.Find("Shader Graphs/Gray"));
        if (material == null)
        {
            Debug.LogError("Shader 'ShaderGraphs/Gray' not found.");
            return;
        }

        // 将MainTex设置为选择的纹理
        material.SetTexture("_MainTex", selectedTexture);

        // 确定材质文件的路径
        string texturePath = AssetDatabase.GetAssetPath(selectedTexture);
        string materialPath = texturePath.Replace(".png", ".mat").Replace(".jpg", ".mat");

        // 保存材质
        AssetDatabase.CreateAsset(material, materialPath);
        AssetDatabase.SaveAssets();

        // 选择新创建的材质
        Selection.activeObject = material;

        DebugEX.LogFrameworkMsg("Material created successfully: " + materialPath);
    }
}

