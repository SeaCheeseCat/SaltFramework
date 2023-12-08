using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using RpcData;
using System;

/// <summary>
/// 账号数据扩展
/// </summary>
/*public static class AccountDataExtend
{
    /// <summary>
    /// 读取本地存档
    /// </summary>
    /// <param name="title"></param>
    /// <returns></returns>
    public static Account Load(string title)
    {
        var msg = GameDataManager.Instance.Load(title);
        if (msg == null)
        {
            Debug.Log("数据为空:" + title);
            return null;
        }
        else
        {
            Debug.Log("读取成功:" + title);
        }
        
        var rt = Account.Parser.ParseFrom(msg);
        rt.ReInitData();
        //rt.LoadSetting();
        //rt.LoadAds();
        return rt;
    }

    /// <summary>
    /// 创建账号
    /// </summary>
    /// <returns></returns>
    public static Account MakeAccount()
    {
        var account = new Account();

        var player = new Player()
        {
            Mark = new MarkData(),
            Chapter = new Chapter()
            {
                Hard = 1,
                LevelId = 0
            }
        };
        account.Player = player;

        //初始化
        account.ReInitData();

#if UNITY_EDITOR
        if (GameBase.Instance.PassFirst)
        {
            account.Player.FirstGame = false;
        }

        if (GameBase.Instance.CountlessItems)
        {
            account.ChangeRes(eResType.Coin, 10000000);
            account.ChangeRes(eResType.Diamond, 100000);
        }
#endif

        return account;
    }

    /// <summary>
    /// 重载数据，用于版本更新加新数据的时候初始化
    /// </summary>
    /// <param name="account"></param>
    public static void ReInitData(this Account account)
    {
        account.InitSetting();
        account.InitAdsData();
        account.InitCardData();
        account.InitCardData();
    }

    /// <summary>
    /// 初始化设置
    /// </summary>
    /// <param name="account"></param>
    public static void InitSetting(this Account account)
    {
        if (account.Setting == null)
        {
            var setting = new Setting();
            setting.Music = true;
            setting.Audio = true;
            setting.Fps = false;
            setting.DmgTip = false;
            switch (GameBase.Instance.GetCountry())
            {
                case "CN":
                    setting.Language = RpcData.LanguageType.Cn;
                    break;
                case "JP":
                    setting.Language = RpcData.LanguageType.Jp;
                    break;
                case "TW":
                    setting.Language = RpcData.LanguageType.Tcn;
                    break;
                case "HK":
                    setting.Language = RpcData.LanguageType.Tcn;
                    break;
                default:
                    setting.Language = RpcData.LanguageType.En;
                    break;

            }

            
           
            account.Setting = setting;
        }
    }

    /// <summary>
    /// 初始化广告
    /// </summary>
    /// <param name="account"></param>
    public static void InitAdsData(this Account account)
    {
        if (account.AdsData == null)
        {
            var data = new AdsData()
            {
                LoadCloud = 5,
                PutCloud = 5
            };
            data.Date = MathG.ConvertDateTimeToLong(GameBase.Instance.serverTime);
            account.AdsData = data;

        }
    }

    /// <summary>
    /// 初始化卡牌资源
    /// </summary>
    /// <param name="account"></param>
    public static void InitCardData(this Account account)
    {
        foreach (var v in ConfigManager.GetConfigList<CardCfg>())
        {
            if (v.UnockType != 0) continue;
            if (account.Player.Cards.ContainsKey(v.ID))
            {
                continue;
            }
            account.Player.Cards.Add(v.ID, new CardData()
            {
                Id = v.ID,
                Level = v.InitLevel
            });
        }

        for (int i = 0; i < 3; i++)
        {
            if (!account.Player.CardGroup.ContainsKey(i))
            {
                var cards = new CardGroup();
                foreach (var v in ConfigManager.GetConfigList<CardCfg>())
                {
                    if (v.UnockType == 0) cards.Cards.Add(v.ID);
                }
                account.Player.CardGroup.Add(i, cards);
            }
        }
    }

    /// <summary>
    /// 获取资源
    /// </summary>
    /// <param name="account"></param>
    /// <returns></returns>
    public static int GetRes(this Account account, eResType resType)
    {
        if (!account.Player.Resources.ContainsKey((int)resType))
        {
            account.Player.Resources[(int)resType] = 0;
        }
        return account.Player.Resources[(int)resType];
    }

    /// <summary>
    /// 获取卡牌升级所需单位卡牌的数量
    /// </summary>
    /// <param name="card"></param>
    /// <returns></returns>
    public static int GetCardUpNeed(CardData card)
    {
        var cfg = ConfigManager.GetConfigByID<CardCfg>(card.Id);
        if (cfg == null)
        {
            Debug.LogError("错误未找到配置" + card.Id);
        }
        //var goldCost = ConfigManager.GetConfigByID<StarCostCfg>(card.Level + 1);
        var star = ConfigManager.GetConfigByID<StarCostCfg>(card.Level + 1);
        if (star == null)
        {
            Debug.LogError("错误，未找到升级配置" + (card.Level + 1) + " 卡片等级" + card.Level);
        }
        var numCost = ConfigManager.GetConfigByID<StarCostCfg>(card.Level + 1 - cfg.InitLevel).Card;
        return numCost;
    }

    /// <summary>
    /// 获取卡牌数据
    /// </summary>
    /// <param name="account"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public static CardData GetCard(this Account account, int id)
    {
        CardData rt = null;
        if (account.Player.Cards.ContainsKey(id))
        {
            return account.Player.Cards[id];
        }
        return rt;
    }

    /// <summary>
    /// 获取卡牌
    /// </summary>
    public static void AddCard(this Account account, int id, int count = 1)
    {
        if (!account.Player.Cards.ContainsKey(id))
        {
            var cfg = ConfigManager.GetConfigByID<CardCfg>(id);
            var card = new CardData()
            {
                Id = id,
                Level = cfg.InitLevel
            };
            account.Player.Cards.Add(id, card);
        }
        account.Player.Cards[id].Count += count;
    }

    /// <summary>
    /// 修改资源
    /// </summary>
    /// <param name="account"></param>
    /// <param name="resType"></param>
    /// <param name="num"></param>
    public static void ChangeRes(this Account account, eResType resType, int num)
    {
        if (!account.Player.Resources.ContainsKey((int)resType))
        {
            account.Player.Resources[(int)resType] = 0;
        }
        account.Player.Resources[(int)resType] += num;
        //MsgManager.Instance.SendMessage(MsgDefine.FRESH_RES);
    }

    /// <summary>
    /// 获取大厅宝箱数据
    /// </summary>
    public static Google.Protobuf.Collections.RepeatedField<LobbyChest> GetLobbyChest(this Account account)
    {
        var Unlock = new List<int> { 1, 3, 6, 10 };
        //初始化大厅宝箱
        if (account.Player.Chests.Count == 0)
        {
            var index = 0;
            for (int i = 0; i < 4; i++)
            {
                var chest = new LobbyChest();
                chest.Id = GetChestId(index);
                chest.Unlock = account.HasPassChapter(Unlock[i], 1);
                chest.Hard = account.Player.Chapter.Hard;
                chest.Chapter = account.Player.Chapter.LevelId;
                chest.Time = GetChestTime(index);
                account.Player.Chests.Add(chest);
                index++;
            }
        }
        else
        {
            var index = 0;
            foreach (var v in account.Player.Chests)
            {
                if (v.Unlock)
                {
                    index++;
                    continue;
                }
                v.Unlock = account.HasPassChapter(Unlock[index], 1);
                //宝箱解锁时重置章节和难度加成
                if (v.Unlock)
                {
                    v.Hard = account.Player.Chapter.Hard;
                    v.Chapter = account.Player.Chapter.LevelId;
                }
                index++;
            }
        }
        return account.Player.Chests;
    }

    /// <summary>
    /// 根据概率获取宝箱id
    /// </summary>
    public static int GetChestId()
    {
        var Id = 0;
        var index = UnityEngine.Random.Range(0, 8);
        if (index < 1)
        {
            Id = 3;
        }
        else if (index < 3)
        {
            Id = 2;
        }
        else Id = 1;
        return Id;
    }

    /// <summary>
    /// 获取大厅宝箱id
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public static int GetChestId(int index)
    {
        switch (index)
        {
            case 0:
                return 1;
            case 1:
                return UnityEngine.Random.Range(0, 10) < 9 ? 1 : 2;
            case 2:
                return UnityEngine.Random.Range(0, 10) < 8 ? 1 : 2;
            case 3:
                var num = UnityEngine.Random.Range(0, 10);
                var rt = 1;
                if (num >= 7 && num < 9)
                {
                    rt = 2;
                }
                if (num == 9) rt = 3;
                return rt;
            default:
                return 1;
        }
    }

    /// <summary>
    /// 获取大厅宝箱刷新时间
    /// </summary>
    /// <returns></returns>
    public static int GetChestTime(int index)
    {
        var time = new List<int> { 3600, 10800, 18000, 28800 };
        return time[index];
    }

    /// <summary>
    /// 检查是否通关
    /// </summary>
    /// <param name="account"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public static bool HasPassChapter(this Account account, int id, int dif)
    {
        if (dif != account.Player.Chapter.Hard) return dif < account.Player.Chapter.Hard;
        return account.Player.Chapter.LevelId >= id;
    }

    /// <summary>
    /// 通关章节
    /// </summary>
    /// <param name="account"></param>
    /// <param name="id"></param>
    /// <param name="diff"></param>
    public static void PassChapter(this Account account, int id, int diff)
    {
        var count = ConfigManager.GetConfigList<ChapterCfg>().Length;
        if (GameBase.Instance.Account.Player.Chapter.Hard > diff) return;
        if (id == count || GameBase.Instance.Account.Player.Chapter.Hard < diff)
        {
            diff++;
            var hard = Mathf.Clamp(diff, 1, 3);
            GameBase.Instance.Account.Player.Chapter.Hard = hard;
            if (diff == 4)
            {

                GameBase.Instance.Account.Player.Chapter.LevelId = id >= 21 ? 21 : id;
            }
            else
            {
                GameBase.Instance.Account.Player.Chapter.LevelId = 0;
            }
        }
        else
        {
            if (id > GameBase.Instance.Account.Player.Chapter.LevelId)
            {
                GameBase.Instance.Account.Player.Chapter.LevelId = id;
            }
        }
        GamerLeve leve = new GamerLeve(GameBase.Instance.Account.Player.Chapter.Hard,GameBase.Instance.Account.Player.Chapter.LevelId);
        DBMgr.Instance.TrackEvent("Gamer_Leve", leve);
        GameBase.Instance.LocalSave();
    }

    /// <summary>
    /// 加载设置
    /// </summary>
    /// <param name="account"></param>
    public static void LoadSetting(this Account account)
    {
        if (account.Setting == null)
        {
            account.InitSetting();
        }

       *//* if (account.Setting.Fps) UIManager.OpenUI<FPSUI>();*//*
        AudioManager.Instance.SetMusicVolume(account.Setting.Music ? 1 : 0);
        AudioManager.Instance.SetAudioVolume(account.Setting.Audio ? 1 : 0);
    }

    /// <summary>
    /// 加载刷新广告数据  /*每天刷新数据
    /// </summary>
    /// <param name="account"></param>
    public static void LoadAds(this Account account)
    {
        DebugEX.Log("每天刷新");
        if (account.AdsData == null)
        {
            account.InitAdsData();
        }

        var timeNow = GameBase.Instance.serverTime;
        var timeLast = MathG.ConvertLongToDateTime(account.AdsData.Date);
        if (timeNow.Year > timeLast.Year || timeNow.Month > timeLast.Month || timeNow.Day > timeLast.Day)
        {
            //刷新每日广告次数
            account.AdsData.Date = MathG.ConvertDateTimeToLong(timeNow);
            foreach (var v in account.AdsData.AdsDailyRecord)
            {
                v.Value.Count = 0;
            }
            GameBase.Instance.isGetMonthCardEveryDay = false;
            //刷新存档信息
            account.AdsData.LoadCloud = 5;
            account.AdsData.PutCloud = 5;
            //PlayerPrefs.DeleteKey(ShopManger.Instance.GameEndRewardKey);
            //刷新大厅宝箱id
            account.Player.AdChest = UnityEngine.Random.Range(1, 3);
        }
    }



    /// <summary>
    /// 获取卡牌稀有度背景
    /// </summary>
    public static Sprite GetCardRareBg(int rare)
    {
        switch (rare)
        {
            case 0:
                return AtlasManager.GetSprite("CardBg/CardBg_1");
            case 1:
                return AtlasManager.GetSprite("CardBg/CardBg_2");
            case 2:
                return AtlasManager.GetSprite("CardBg/CardBg_3");
            default:
                return AtlasManager.GetSprite("CardBg/CardBg_1");
        }
    }

    /// <summary>
    /// 获取技能类型文本
    /// </summary>
    /// <param name="Type"></param>
    /// <returns></returns>
    public static string GetSkillType(int Type)
    {
        return TextManager.GetText(141 + Type);
    }

    /// <summary>
    /// 获取属性名
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static String GetProTitle(int id)
    {
        return TextManager.GetText(1000 + id);
    }

    /// <summary>
    /// 获取装备等级背景框
    /// </summary>
    /// <returns></returns>
    public static Sprite GetEquipBg(int lv)
    {
        switch (lv)
        {
            case 0:
                return AtlasManager.GetSprite("EquipBg/Bg0");
            case 1:
                return AtlasManager.GetSprite("EquipBg/Bg1");
            case 2:
                return AtlasManager.GetSprite("EquipBg/Bg2");
            case 3:
                return AtlasManager.GetSprite("EquipBg/Bg3");
            case 4:
                return AtlasManager.GetSprite("EquipBg/Bg4");
            case 5:
                return AtlasManager.GetSprite("EquipBg/Bg5");
            case 6:
                return AtlasManager.GetSprite("EquipBg/Bg6");
            default:
                return AtlasManager.GetSprite("EquipBg/Bg5");
        }
    }

    /// <summary>
    /// 获取科技等级
    /// </summary>
    /// <param name=""></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public static int GetTechLevel(this Account account, int id)
    {
        if (account.Player.Techs.ContainsKey(id)) return account.Player.Techs[id];
        return 0;
    }

    /// <summary>
    /// 获取随机提示文本
    /// </summary>
    /// <returns></returns>
    public static string GetTipText()
    {
        var list = new List<string>();
        for (int i = 3800; i < 3999; i++)
        {
            var cfg = ConfigManager.GetConfigByID<TextCfg>(i);
            if (cfg == null) continue;
            var str = TextManager.GetText(i);
            list.Add(str);
        }
        return list[UnityEngine.Random.Range(0, list.Count)];
    }

    public static int GetCompletedDiff(this Account account)
    {
        return account.Player.Chapter.Hard;
    }

}*/
