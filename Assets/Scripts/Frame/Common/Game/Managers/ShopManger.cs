
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManger : Manager<ShopManger>
{/*
    public List<PurchaseItems> CoinItems = new List<PurchaseItems>();
    public List<PurchaseItems> MoneyItems = new List<PurchaseItems>();
    public List<PurchaseItems> monthCardItems = new List<PurchaseItems>();
    public Dictionary<int, adItemData> getAditem = new Dictionary<int, adItemData>();
    public Dictionary<string, int> GameEndRewardNum = new Dictionary<string, int>();

    public string GameEndRewardKey = "gameendrewardkey";
    public void AddPurchaseItem(PurchaseItems data)
    {
        switch (data.product_type)
        {
            case 0:
                CoinItems.Add(data);
                CoinItems.Sort(SortCompare);
                break;
            case 1:
                MoneyItems.Add(data);
                MoneyItems.Sort(SortCompare);
                break;
            case 1000:
                monthCardItems.Add(data);
                monthCardItems.Sort(SortCompare);
                break;
        }
    }
    private static int SortCompare(PurchaseItems info1, PurchaseItems info2)
    {
        return info2.price.CompareTo(info1.price);
    }


    //public static void SetAddLeveReward(int level, int diff) 
    //{
    //    LevelReward level1 = new LevelReward();
    //    level1.level = level;
    //    level1.diff=
    //}

    public void AddGameEndRewardNum(int level, int diff)
    {
        string name = string.Format("{0}_{1}", level, diff);

        if (GameEndRewardNum.ContainsKey(name))
        {
            GameEndRewardNum[name] = GameEndRewardNum[name] + 1;
        }
        else
        {
            GameEndRewardNum.Add(name, 1);
        }
        //List<RewardList> rea = new List<RewardList>();
        //foreach (var a in GameEndRewardNum) 
        //{
        //    RewardList rewardList = new RewardList();
        //    rewardList.diff= a.Value
        //}
        PlayerPrefs.SetString(GameEndRewardKey, JsonConvert.SerializeObject(GameEndRewardNum));
    }

    public bool CheckHasShowReward(int level, int diff)
    {
        if (PlayerPrefs.HasKey(GameEndRewardKey)) 
        {
            GameEndRewardNum = JsonConvert.DeserializeObject<Dictionary<string, int>>(PlayerPrefs.GetString(GameEndRewardKey));
        }

        string name = string.Format("{0}_{1}", level, diff);
        if (GameEndRewardNum.ContainsKey(name))
        {
            return GameEndRewardNum[name] <3;
        }
        else
        {
            return true;
        }
    }
*/

}
public class RewardList 
{
    public string name;
    public int diff;
    public int level;
    public int num;
}
