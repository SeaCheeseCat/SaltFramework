using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using UnityEngine;
using UnityEngine.SceneManagement;
/*using UnityEngine.UI;*/

/// <summary>
/// 场景加载管理器
/// </summary>
public class LoadManager : Manager<LoadManager>
{
    /// <summary>
    /// 加载场景
    /// </summary>
    public void LoadScene(string scene, Func<IEnumerator> loadCoro = null,Action action = null)
    {
        GameBase.Instance.StartCoroutine(LoadLevel(scene, loadCoro,action));
       
    }

    /// <summary>
    /// 加载场景
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    private IEnumerator LoadLevel(string scene,Func<IEnumerator> LoadCoro,Action action)
    {
        //var loadUI= UIManager.OpenUI<LoadingUI>();
        yield return new WaitForSeconds(0.5f);  //等待遮罩
        SceneManager.LoadScene("Load"); //加载一个空场景
        AsyncOperation op = SceneManager.LoadSceneAsync(scene);    //加载目标场景
        float loadPerc;
        do
        {
            loadPerc = op.progress * 0.3f;
            //loadUI.UpdateProgress(loadPerc);
            yield return null;
        } while (!op.isDone);

        GameBase.Instance.FreshUICam();
        //UIManager.CloseUI<CardGroupUI>();

        if (LoadCoro != null)
        {
            yield return LoadCoro.Invoke();
        }

        //loadUI.UpdateProgress(1f);
        /*do
        {
            yield return null;
        } while (loadUI.loadBarSlider.value < 1);*/
        yield return new WaitForSeconds(0.1f);

      /*  if (loadUI != null)
        {
            yield return loadUI.CloseCoro();
        }
*/
        action?.Invoke();
    }
}