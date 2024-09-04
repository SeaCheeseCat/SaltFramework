using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class EditorListener
{
    /*static EditorListener()
    {
        // 注册 SceneView.duringSceneGui 事件
        //SceneView.duringSceneGui += OnSceneGUI;
        EditorApplication.update += Update;
    }

    private static void Update()
    {
        Event e = Event.current;
        //DebugEX.Log(e == null);
        //DebugEX.Log("update");
        if (Event.current != null && Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.A)
        {
            DebugEX.Log("AAAA");
            //ExecuteYourFunction();
            Event.current.Use(); // 使用 Event.current.Use() 来防止事件被其他地方使用
        }

        // 使用普通的 Input.GetKeyDown 也可以检测按键
        if (Input.GetKeyDown(KeyCode.F8))
        {
            //ExecuteYourFunction();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            DebugEX.Log("检测A按下了");
        }
        *//*if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.A)
        {
            Debug.Log("键盘A键在Window面板中按下....");
        }*//*
    }

    *//* private static void ExecuteYourFunction()
     {
         // 在这里添加你需要执行的代码
         Debug.Log("F8 键被按下，执行操作...");
         // 可以在这里执行你需要的任何操作
     }*//*

     private static void OnSceneGUI(SceneView sceneView)
     {
         Event e = Event.current;
         if (e.type == EventType.KeyDown && e.keyCode == KeyCode.F8)
         {
             // 执行你需要的操作
             //ExecuteYourFunction();
             // 使用 e.Use() 来防止事件被其他地方使用
             e.Use();
         }

         if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.A)
         {
             //Debug.Log("键盘A键在Window面板中按下....");
         }
     }

     private static void ExecuteYourFunction()
     {
         // 在这里添加你需要执行的代码
         Debug.Log("F8 键被按下，执行操作...");
         // 比如可以执行某个功能函数
         // YourFunction();
     }*/
}
