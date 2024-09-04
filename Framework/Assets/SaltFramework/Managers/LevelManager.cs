using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Manager<LevelManager>
{
    //Tip: 全部的关卡数据 
    public LevelCfg[] levelcfgs;

    /// <summary>
    /// Init:
    /// 初始化
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override IEnumerator Init(MonoBehaviour obj)
    {
        levelcfgs = LevelCfg.GetList().ToArray();
        //levelcfgs = ConfigManager.GetConfigList<LevelCfg>();
        return base.Init(obj);
    }

    public int GetChapterLevelCount(int chapter)
    {
        var count = 0;
        foreach (var item in levelcfgs)
        {
            if (item.Chapter == chapter)
                count++;
        }

        return count;
    
    }

    /// <summary>
    /// Get:
    /// 获取一个关卡的触发条件
    /// </summary>
    /// <param name="chapter">章节</param>
    /// <param name="level">关卡</param>
    /// <returns></returns>
    public int[] GetLevelTrigger(int chapter, int level) 
    {
        foreach (var item in levelcfgs)
        {
            if (item.Chapter == chapter && item.Level == level)
            {
                return item.Trigger;
            }
        }
        return new int[2];
    }

    /// <summary>
    /// Check:
    /// 检查全部可打开的关卡
    /// </summary>
    public List<LevelCfg> CheckLevelCompleteState(List<LevelData> datas) 
    {
        var result = new List<LevelCfg>();
        for (int i = 0; i < levelcfgs.Length; i++)
        {
            var item = levelcfgs[i];
            if (item.Trigger[0] == 0 && item.Trigger[1] == 0)
            {
                result.Add(item);
                continue;
            }
            foreach (var data in datas)
            {
                if (data.chapter == item.Trigger[0] && data.level == item.Trigger[1] && data.completed)
                    result.Add(item);
            }
        }
        return result;
    }



}
