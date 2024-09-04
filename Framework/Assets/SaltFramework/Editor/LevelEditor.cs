using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEditor : OdinMenuEditorWindow
{
    public static LevelLoadEditor loadeditor;
    public static BaseSetEditor baseseteditor;
    [MenuItem("▷ SaltFramework/关卡编辑器")]
    private static void OpenWindow()
    {
        GetWindow<LevelEditor>().Show();
    }


    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree();
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name != "Demo")
            return tree;
        MapConfig.Instance.InitData();
        tree.Selection.SupportsMultiSelect = false;
        loadeditor = new LevelLoadEditor();
        baseseteditor = new BaseSetEditor();
        var dataeditor = new LevelDataEditor();
        tree.Add("基础配置 □", baseseteditor);
        tree.Add("关卡列表 □", loadeditor);
        tree.Add("生成配置 □", new LevelBuildEditor());
        tree.Add("关卡数据 ▲", dataeditor);
        tree.Add("Document", new DocumentEditor());
        loadeditor.Init(baseseteditor);
        dataeditor.RefreshData();
        return tree;
    }

    public class BaseSetEditor
    {
        
        [EnumToggleButtons, LabelText("关卡类型")]
        public LevelType leveltype;

        [LabelText("章节"), FoldoutGroup("关卡配置"), InfoBox("请确保关卡编辑器运行之前\n所有关卡物体均在MapConfig物体下 (阴暗地飞行)")]
        public int Chapter;
        [LabelText("关卡"), FoldoutGroup("关卡配置")]
        public int Level;

        [GUIColor(1, 1, 1)]
        [Button("预加载",ButtonSizes.Large)]
        [ButtonGroup("功能区")]
        private void LoadEditData()
        {
            MapConfig.Instance.InitData();
          
        
        }

        [GUIColor(0, 0.8f, 0)]
        [Button("生成关卡配置", ButtonSizes.Large)]
        [ButtonGroup("功能区")]
        private void SaveEditData()
        {
            MapConfig.Instance.InitData();
            loadeditor.Init(baseseteditor);
            AssetDatabase.Refresh();
        }

        public void SetData(int chapter,int level,List<string> musicpaths) 
        {
            Chapter = chapter;
            Level = level;
            if (musicpaths == null || musicpaths.Count <= 0)
                return;
            foreach (var item in musicpaths)
            {
                var clip = Resources.Load<AudioClip>("Audio/Music/" + item);
                Musicclips.Add(clip);
            }
           
            //UnityEditor.EditorUtility.SetDirty(Musicclip);
        }


        /// <summary>
        /// 刷新数据区
        /// </summary>
        public void RefreshDatas(MapConfigData data) 
        {
            Npcs = new List<int>();
            foreach (var item in data.mapnpcdata)
            {
                Npcs.Add(item.ID);
            }

            Models = new List<int>();
            foreach (var item in data.mapmodeldatas)
            {
                Models.Add(item.ID);
            }

            Particles = new List<int>();
            foreach (var item in data.mapparticledatas)
            {
                Particles.Add(item.ID);
            }

            Lands = new Vector3((float)data.maplanddata.x, (float)data.maplanddata.y, (float)data.maplanddata.z);

        }

        [GUIColor(0.7f, 0, 0)]
        [Button("重置场景")]
        [ButtonGroup("功能区")]
        public void ResetScene()
        {
            MapConfig.Instance.InitData();
            DeleteChild(MapConfig.Instance.npcsTrans);
            DeleteChild(MapConfig.Instance.modelsTrans);
            DeleteChild(MapConfig.Instance.particleTrans);
            DeleteChild(MapConfig.Instance.landTran);
        }

        /// <summary>
        /// 在Eidt模式删除一个物体下面所有的子物体
        /// </summary>
        /// <param name="item"></param>
        private void DeleteChild(Transform item)
        {
            GameObject[] items = new GameObject[item.childCount];
            for (int i = 0; i < item.childCount; i++)
            {
                items[i] = item.GetChild(i).gameObject;
            }

            foreach (var obj in items)
            {
                DestroyImmediate(obj);
            }
        }

        [FoldoutGroup("详细配置")]
        [EnumToggleButtons, LabelText("文件储存类型")]
        public SaveDataType savedataEnum;

        [FoldoutGroup("详细配置"), LabelText("背景音乐")]
        public List<AudioClip> Musicclips;


        [FoldoutGroup("详细配置"), LabelText("是否包含过场")]
        public bool isCg;

        [FoldoutGroup("关卡任务"), LabelText("任务id")]
        public List<int> Tasks;


        [FoldoutGroup("数据"), InfoBox("关卡数据点击预加载后自动生成，不需要手动配置，检查数据使用x"), LabelText("Npc")]
        public List<int> Npcs;
        [FoldoutGroup("数据"), LabelText("模型")]
        public List<int> Models;
        [FoldoutGroup("数据"), LabelText("粒子")]
        public List<int> Particles;
        [FoldoutGroup("数据"), LabelText("地面数据")]
        public Vector3 Lands;

    }
    
    public class DocumentEditor
    {

        [BoxGroup("关卡编辑器 Document")]
        [DisplayAsString(false), HideLabel]
        public string Doc = "  阴暗地爬行.jpg\n  关于框架的更多信息可以访问 SaltFramework @Github  \n  (那里有我用md写的具体文档)";
        [BoxGroup("关卡编辑器 Document"),Button, LabelText("打开GitHub")]
        public void OpenGitHub()
        {
            Application.OpenURL("https://github.com/3382634691/CheeseFramework");

        }

        [HideLabel]
        [DisplayAsString(false)]
        [FoldoutGroup("如何使用关卡编辑器")]
        public string SomeText = 
            " -打开Unity关卡编辑器场景Edit\n" +
            "\n -找到场景下的MapConfig\n" +
            "\n -将你的预制体放到该物体下\n" +
            "\n -打开关卡编辑器 基础配置 列，配置好相应的章节与关卡  点击生成\n" +
            "\n -关卡就这么简单的配置好了！\n"+
             "\n -美美收官！去喝杯热茶！摸一把妮娜";


        [FoldoutGroup("需要注意什么"), DisplayAsString(false), HideLabel]
        public string SomeText2 =
           " -在编辑场景之前，先将MapConfig下的物体清理干净\n"+
           "\n -相同关卡配置一遍后，再次配置将覆盖原本的配置\n"+
           "\n -不要让妮娜跑到键盘上来！";
    }


    #region Hide Serialized
    public class YourAttributeProcessor<T> : OdinAttributeProcessor<T>
    {
        public override void ProcessChildMemberAttributes(InspectorProperty _, MemberInfo member, List<Attribute> attributes)
        {
            if (member.Name == "m_SerializedDataModeController")
            {
                attributes.Add(new HideInInspector());
            }
        }
    }
    #endregion*/


    // 自定义Drawer，用于在Odin Inspector中显示Emoji
    public class EmojiDrawer : OdinValueDrawer<Texture2D>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            Rect rect = EditorGUILayout.GetControlRect();

            if (ValueEntry.SmartValue != null)
            {
                GUI.DrawTexture(rect, ValueEntry.SmartValue);
            }

            CallNextDrawer(label);
        }
    }
}


public enum SaveDataType
{ 
    Json,
    Xml
}

public enum LevelType
{
     Normal,
     Dream,
     Cg
}
