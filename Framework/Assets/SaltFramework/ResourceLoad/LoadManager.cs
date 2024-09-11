using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 场景加载管理器
/// </summary>
public class LoadManager : Manager<LoadManager>
{
    // 定义加载进度回调委托
    public delegate void LoadProgressCallback(float progress);

    /// <summary>
    /// Load:
    /// 加载场景
    /// </summary>
    /// <param name="scenename"></param>
    public void LoadScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }

    /// <summary>
    /// Load:
    /// 拥有加载页面的异步加载场景
    /// </summary>
    public void LoadSceneAsync(string scene, LoadProgressCallback loadcallback = null,Action action = null)
    {
        GameBase.Instance.StartCoroutine(LoadSceneAsyncCoroutine(scene, loadcallback, action));
    }

    /// <summary>
    /// Load:
    /// 加载异步场景
    /// 该方法适用于 自写加载方法，当加载完成会返回，但是不直接进入，当需要进入时可调控
    /// </summary>
    /// <param name="scene"></param>
    public void LoadAsyncScene(string sceneName, LoadProgressCallback loadcallback, Action<AsyncOperation> loadCompleteAction)
    {
        GameBase.Instance.StartCoroutine(LoadSceneAsyncNoInCoroutine(sceneName, loadcallback, loadCompleteAction));
    }



    /// <summary>
    /// Load:
    /// 异步加载场景
    /// </summary>
    /// <param name="sceneName">目标场景名字</param>
    /// <param name="loadProgressCallback">加载进度回调</param>
    /// <param name="loadCompleteAction">加载完成后执行的操作</param>
    /// <returns></returns>
    public IEnumerator LoadSceneAsyncCoroutine(string sceneName, LoadProgressCallback loadProgressCallback, Action loadCompleteAction)
    {
        var laodUi = UIManager.Instance.OpenUI<LoadUI>();
        yield return new WaitForSeconds(0.5f); 
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        // 设置加载完成后不能自动跳转场景
        op.allowSceneActivation = false;
        do
        {
            // 报告加载进度
            if (loadProgressCallback != null)
            {
                float progress = Mathf.Clamp01(op.progress / 0.9f); // 获取异步加载进度
                loadProgressCallback(progress);
                laodUi.ToTargetValue(progress);
            }
            
            if (op.progress >= 0.9f)
            {
                yield return new WaitForSeconds(0.1f);
                DebugEX.Log("进度加载完毕");
                UIManager.Instance.CloseUI<LoadUI>();
                // 执行加载完成后的操作
                loadCompleteAction?.Invoke();
                // 加载完成后手动激活场景
                op.allowSceneActivation = true;
            }
            yield return null;
        } while (!op.isDone);
    }

    /// <summary>
    /// Load:
    /// 异步加载场景
    /// </summary>
    /// <param name="sceneName">目标场景名字</param>
    /// <param name="loadProgressCallback">加载进度回调</param>
    /// <param name="loadCompleteAction">加载完成后执行的操作</param>
    /// <returns></returns>
    public IEnumerator LoadSceneAsyncNoInCoroutine(string sceneName, LoadProgressCallback loadProgressCallback, Action<AsyncOperation> loadCompleteAction)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        // 设置加载完成后不能自动跳转场景
        op.allowSceneActivation = false;
        do
        {
            // 报告加载进度
            if (loadProgressCallback != null)
            {
                float progress = Mathf.Clamp01(op.progress / 0.9f); // 获取异步加载进度
                loadProgressCallback(progress);
                
            }

            if (op.progress >= 0.9f)
            {
                yield return new WaitForSeconds(0.1f);
                // 执行加载完成后的操作
                loadCompleteAction?.Invoke(op);
            }
            yield return null;
        } while (!op.isDone);
    }
}