using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Globalization;
using System;
using System.Collections.Generic;
using Google.Protobuf;
using Sirenix.OdinInspector;

/// <summary>
/// 游戏入口,所有逻辑从这里开启
/// </summary>
public class GameBase : MonoBehaviour
{
    //Tip: GameBase单例模式
    public static GameBase Instance { get; private set; }
    //Tip: 游戏平台
    [EnumToggleButtons]
    public Platform Platform;
    //Tip: 对象池初始化方式
    [EnumPaging]
    public InitPoolEnum InitPoolMode;
    //Tip: 游戏加载资源方式,是否采用AB包方式
    public bool LoadAb;
    //Tip: 存档数据
    public SaveData saveData;
    //Tip: 当前关卡数据
    public LevelData currentLevelData;
    //Tip: 当前的游戏场景
    public GameScene gameScene;
    //Tip: 当前的语言类型
    public Language languageType = Language.Chinese;
    //Tip: 调试模式
    public bool debugModel;
    /// <summary>
    /// Base:awake
    /// </summary>
    private void Awake()
    {
        UIManager.Instance.Init();
        InitGameData();
        InstanceSaveData();
        StartGame();
    }

    /// <summary>
    /// Init:
    /// 初始化游戏数据
    /// </summary>
    public void InitGameData()
    {
        if (Instance != null && Instance != this)
        {
            GameEnter();
            Destroy(gameObject);
            return;
        }
        Application.targetFrameRate = 120;   //设置默认帧率
        Instance = this;
        GameBaseData.language = this.languageType;
    }

    public void OnBackMainScene()
    {
        AudioManager.Instance.PlayMainMusic();
    }

    /// <summary>
    /// Start:
    /// 游戏启动器 游戏中这里开启
    /// </summary>
    public void StartGame()
    {
        StartCoroutine(Init());
    }

    /// <summary>
    /// 进入游戏  游戏逻辑开始执行
    /// </summary>
    public void GameEnter()
    {
        SystemManager.Instance.CreateCameraSystem();
        DebugEX.LogFrameworkMsg("游戏初始化完成！");
    }

    /// <summary>
    /// Instance:
    /// 实例化一个存档数据
    /// </summary>
    public void InstanceSaveData()
    {
        if (ArchiveManager.Instance.ExistDataFile())
        {
            saveData = ArchiveManager.Instance.LoadDataFromJson<SaveData>();
            DebugEX.LogFrameworkMsg("存档SaveData已载入！");
        }
        else
        {
            saveData = new SaveData();
            saveData.baseData = new BaseData();
            saveData.levelData = new List<LevelData>();
            saveData.baseData.firstEntry = true;
            DebugEX.LogFrameworkMsg("存档SaveData已创建数据！");
        }
    }

    /// <summary>
    /// Init:
    /// 初始化System数据
    /// </summary>
    /// <returns></returns>
    IEnumerator Init()
    {
        yield return LanguageManager.Instance.Init(this);
        yield return PoolManager.Instance.Init(this);
        yield return GameSetSysteam.Instance.Init(this);
        SystemManager.Instance.CreateAudioSystem();
        SystemManager.Instance.CreateLongPressDetectorSystem();
        SystemManager.Instance.CreateInputKeySystem();
        GameEnter();
    }

    /// <summary>
    /// Make:
    /// 创建唯一uid
    /// </summary>
    /// <returns></returns>
    public int MakeUid()
    {
        var buffer = System.Guid.NewGuid().ToByteArray();
        var uid = System.BitConverter.ToInt32(buffer, 0);
        return uid;
    }

    /// <summary>
    /// Get:
    /// 获取国家名称
    /// </summary>
    /// <returns></returns>
    public string GetCountry()
    {
        var str = RegionInfo.CurrentRegion.Name;
        Debug.Log("当前地区：" + str);
        return str;
    }

    /// <summary>
    /// Load:
    /// 读取存档
    /// </summary>
    /// <returns></returns>
    public IEnumerator LoadSave()
    {
        yield return null;
    }

    /// <summary>
    /// Quit:
    /// 退出游戏
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Get:
    /// 获取关卡数据
    /// </summary>
    public List<LevelData> GetLevelData()
    {
        return saveData.levelData;
    }

    /// <summary>
    /// Get:
    /// 获取章节关卡数据
    /// </summary>
    /// <param name="chatper">章节</param>
    /// <returns></returns>
    public List<LevelData> GetLevelData(int chatper)
    {
        var datas = new List<LevelData>();
        foreach (var item in saveData.levelData)
        {
            if (item.chapter == chatper)
            {
                datas.Add(item);
            }
        }
        return datas;
    }

    /// <summary>
    /// Is:
    /// 查看一个关卡是否已经完成
    /// </summary>
    /// <param name="chapter"></param>
    /// <param name="level"></param>
    /// <returns></returns>
    public bool IsLevelComplete(int chapter, int level)
    {
        foreach (var item in saveData.levelData)
        {
            if (item.chapter == chapter && item.level == level)
            {
                return item.completed;
            }
        }
        return false;
    }

    /// <summary>
    /// Get:
    /// 获取所有已经完成的关卡
    /// </summary>
    /// <returns></returns>
    public List<(int, int)> GetCompleteLevels() 
    {
        List<(int, int)> completes = new List<(int, int)>();
        foreach (var item in saveData.levelData)
        {
            if (item.completed)
                completes.Add((item.chapter, item.level));
        }
        return completes;
    }

    /// <summary>
    /// Load:
    /// 载入指定关卡数据
    /// </summary>
    /// <param name="chapter"></param>
    /// <param name="level"></param>
    /// <returns></returns>
    public void LoadLevelData(int chapter, int level) 
    {
        foreach (var item in saveData.levelData)
        {
            if (item.chapter == chapter && item.level == level)
            {
                currentLevelData = item;
                DebugEX.Log("初始化完毕关卡数据了 的数据是", item.completedTask.Count, item.chapter, item.level);
                return;
            }
        }
        var data = new LevelData();
        data.chapter = chapter;
        data.level = level;
        data.completedTask = new List<int>();
        data.completed = false;
        data.levelWhithDialogDatas = new List<LevelWhithDialogData>();
        data.levelWithModePositionDatas = new List<LevelWithModePositionData>();
        currentLevelData = data;
        saveData.levelData.Add(currentLevelData);
        saveData.baseData.currentChapter = chapter;
        saveData.baseData.currentLevel = level;
    }

    /// <summary>
    /// Reset:
    /// 重置关卡数据
    /// </summary>
    /// <param name="chapter">章节</param>
    /// <param name="level">关卡</param>
    public void ResetLevelData(int chapter, int level)
    {
         
    }
}

public enum Platform
{ 
    PC
}

public enum GameScene
{ 
    Init,
    GameLevel
}

public enum Language
{
    English,
    Chinese,
    French,
    Japanese,
}
