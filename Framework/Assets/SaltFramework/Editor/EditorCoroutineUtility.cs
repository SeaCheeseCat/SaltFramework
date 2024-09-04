using UnityEngine;
using UnityEditor;
using System.Collections;

public static class EditorCoroutineUtility
{
    public static IEnumerator ExecuteAfterDelay(System.Action action, float delaySeconds)
    {
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + delaySeconds)
        {
            yield return null;
        }
        action();
    }

    public static void StartEditorCoroutine(IEnumerator routine)
    {
        void EditorUpdate()
        {
            if (routine.MoveNext() == false)
            {
                EditorApplication.update -= EditorUpdate;
            }
        }
        EditorApplication.update += EditorUpdate;
    }
}

public class ExampleEditorScript
{
    [MenuItem("Tools/Execute After Delay")]
    public static void ExecuteWithDelay()
    {
        Debug.Log("Starting delay...");
        EditorCoroutineUtility.StartEditorCoroutine(
            EditorCoroutineUtility.ExecuteAfterDelay(() =>
            {
                Debug.Log("This message is delayed by 2 seconds.");
            }, 2.0f)
        );
        Debug.Log("Delay set.");
    }
}
