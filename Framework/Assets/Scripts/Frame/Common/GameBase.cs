using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Globalization;
using System;
using System.Collections.Generic;
using Google.Protobuf;
//using RpcData;

/*
    #if UNITY_ANDROID
    using Plugins.AntiAddictionUIKit;
    #endif
*/

//我们如何成为自己
//我们必须成为自己
/// <summary>
/// 游戏入口,所有逻辑从这里开启
/// </summary>
public class GameBase : MonoBehaviour
{
    public static GameBase Instance { get; private set; }
   /* public Account Account
    {
        get { return account; }
        set { account = value; }
    }
    private Account account;*/
    public Platform Platform;
    [HideInInspector]
    public bool HasIAP = false;
    public bool LoadAb = false; //资源加载开关
   // public ShaderVariantCollection ShaderVariantCollection;
   
    public int Code;
    [HideInInspector]
    public string versionCode;
    [HideInInspector]
    public bool isLogin;
#if UNITY_EDITOR
    public bool ClearSaveData = false;  //清除本地存档
    /*public RpcData.LanguageType LanguageTypem;  //设置语言类型
    public static RpcData.LanguageType LanguageType;    //全局语言类型*/
    [HideInInspector]
    public bool UnlockAll = false;
    [HideInInspector]
    public bool CountlessItems = false;
    [HideInInspector]
    public bool PassFirst;
#endif
    [HideInInspector]
    public string CloudToken;
    [Header("是否使用服务器数据")]
    public bool isCloudData;

    /*public TapGameSave TapGameSave = null;
    public TDSUser User = null;*/
    [HideInInspector]
    public string HyPlayerID;
    [HideInInspector]
    public string HaoYouName;
    [HideInInspector]
    public int monthCardDayNum = -1;

    public Action updateShopMonthCar;
    [HideInInspector]
    public DateTime serverTime;
    [HideInInspector]
    public string HyGameID = "25360";

    /// <summary>
    /// false 打开购买月卡按钮
    /// </summary>
    [HideInInspector]
    public bool isPayVip;
    [HideInInspector]
    public bool IsVip;
    [HideInInspector]
    public string userid;
    [HideInInspector]
    public int serverTimeNum;
    [HideInInspector]
    public int vipTime;
    [HideInInspector]
    public bool isCheckPayVip = true;
    public int RandomSeed;
    [HideInInspector]
    public string contact_text;

    [HideInInspector]
    public GamePlayState gamePlayState;


    public InitPoolEnum InitPoolMode;
    public Camera UICamera;

    [Header("是否取消游戏开始的过场演出")]
    public bool CnacelBornAct;  
    
    public int Chapter { get; set; }  //章节

    public bool LoadPart2 = false;

    //public GameStart gameStart;

    public int Part = 0;

    public bool isGetMonthCardEveryDay
    {
        get { return PlayerPrefs.GetInt("GetEveryDayNum", 0) == 1; }
        set { PlayerPrefs.SetInt("GetEveryDayNum", value ? 1 : 0); }
    }
    private void Awake()
    {
        DebugEX.Log("Awake 启动");
        versionCode = string.Format("1.0.{0}", Code);
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            DebugEX.Log("销毁掉了");
            //CutUI.Instance.takecut.InitData();
            //gameStart.StartGame();
            return;
        }

#if UNITY_EDITOR
        //清理存档
        if (ClearSaveData)
        {
            GameDataManager.Instance.Delete("save");
        }
        //设置本地语言
      //  LanguageType = LanguageTypem;
#endif

        //QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 120;   //设置默认帧率

        DontDestroyOnLoad(gameObject);
        Instance = this;

        //游戏启动
        StartCoroutine(Init());
    }

   


    
    private void Start()
    {
#if UNITY_IPHONE
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            int curStatus = ATTAuth.GetAppTrackingAuthorizationStatus();
            if (curStatus == 0)
            {
                ATTAuth.RequestTrackingAuthorizationWithCompletionHandler((status) =>
                {
                    Debug.Log("ATT status :" + status);
                });
            }
        }
#endif
        Chapter = 1;
        //ShaderVariantCollection.WarmUp();
    }



    /// <summary>
    /// 本地存储
    /// </summary>
    public void LocalSave()
    {
        DebugEX.LogError("save上报数据");
        /*if (User != null)
        {
            Account.UserId = User.ObjectId;
        }
        if (isCloudData)
        {
            GameDataManager.Instance.SavaCloudAccount(Account, (success, data) =>
            {
                if (success)
                {
                    DebugEX.Log("save上报成功");
                }
            });
        }
        else
        {
            GameDataManager.Instance.Save(Account, "save");
        }*/


    }

    /// <summary>
    /// 云存档
    /// </summary>
    public async void SaveCloud(Action<bool> action)
    {
        /* LoginControl.Instance.ShowLoading();

         LocalSave();
         if ( Platform == Platform.TapTap )
         {
             if (TapGameSave != null)
             {
                 Debug.Log("移除旧云存档");
                 await TapGameSave.Delete();
             }

             Debug.LogWarning("创建新的云存档对象");
             TapGameSave = new TapGameSave
             {
                 Name = "miracle",
                 Summary = "miracle",
                 ModifiedAt = DateTime.Now.ToLocalTime(),
                 PlayedTime = 0,
                 ProgressValue = 0,
                 GameFilePath = GameDataManager.Instance.FilePath
             };

             try
             {
                 await TapGameSave.Save();
                 Debug.Log("存储成功");
                 action?.Invoke(true);
             }
             catch (Exception e)
             {
                 if (e is TapException tapError)
                 {
                     Debug.Log($"encounter exception:{tapError.code} message:{tapError.message}");
                 }
                 Debug.LogError("存储失败:" + e.Message);
                 action.Invoke(false);
             }
         }
         else if(GameBase.Instance.Platform == Platform.HaoYou){

             CloudMrg.Instance.SaveArchives(action);  

         }
         LoginControl.Instance.CloseLoading();*/

    }
    
    float time = 0;//记录已经经过多少秒
    private void Update()
    {
        if (isCheckPayVip)
        {
            time += Time.deltaTime;
            if (time >= 1f)//一秒
            {
                time = 0;
                serverTimeNum++;
                isPayVip = vipTime < serverTimeNum;
                IsVip = vipTime > serverTimeNum;
            }//
        }

        //AudioManager.Instance.OnUpdate(Time.deltaTime);
        //if (Map.Instance.Pause) return;
        BulletTimeManager.Instance.OnUpdate(Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.A))
        {
            //UIManager.OpenUI<AbnormalUI>();
            //var str = "购买失败!请联系游戏管理员!点击关闭";
            //MsgManager.Instance.SendMessage(MsgDefine.ACTIVE_INFO_SEND_POPINFO_TXT, str);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            
        }
    }
    
    IEnumerator Init() 
    {
        DebugEX.Log("开始初始化");
        //Input.multiTouchEnabled = false; //禁用多点触摸
        yield return ResourceManager.Instance.Init(this);
        yield return PoolManager.Instance.Init(this);
        //yield return AudioManager.Instance.Init(this);
        yield return TextManager.Instance.Init(this);
        yield return GameDataManager.Instance.Init(this);
        yield return LoadManager.Instance.Init(this);
        yield return BulletTimeManager.Instance.Init(this);
        yield return MsgManager.Instance.Init(this);
        DebugEX.Log("创建音频管理器");
        /*var audio = Instantiate(ResourceManager.LoadPrefabSync("System/AudioManager"));;
        DontDestroyOnLoad(audio);*/
        //yield return new WaitForSeconds(1f);
        var audio = Instantiate(ResourceManager.LoadPrefabSync("System/AudioManager"));
        DontDestroyOnLoad(audio);
        DebugEX.Log("创建音频");

        //LoginManger.Instance.Init();
        //yield return PlayerManager.Instance.Init(this);
        /*  //国内用taptap的包
          //初始化内购
          Debug.Log("开始初始化内购");
  #if UNITY_IPHONE
                          IAPManager.Instance.InitializeIAPManager(InitializeResultCallback);
  #elif UNITY_ANDROID

  #endif

          yield return null;

          UIManager.OpenUI<LoginInfoUI>();
          UIManager.OpenUI<StartUI>();*/

        //UIManager.OpenUI<InitUI>();


        //FreshUICam();

        /*var firstCover = GameObject.Find("FirstCover");
        if (firstCover != null)
        {
            var img = firstCover.GetComponentInChildren<Image>();
            var l = img.DOFade(0f, 1f);
            l.onComplete += delegate
            {
                Destroy(firstCover);
            };
        }*/
    }


   

    /// <summary>
    /// 初始化游戏数据
    /// </summary>
    public void InitGameData() 
    { 
        
        
    }


    /// <summary>
    //    ============================  一些后台相关的  ====================
    /// </summary>
    /// <param name="focus"></param>
    private void OnApplicationPause(bool focus)
    {
        if (focus)
        {
            /*if (!DebugMode && !GameMain.Instance.isloadarchiveing)
            {
                AccountDataExtend.SaveGameData();
            }*/
            /*ArchiveManager.SaveToJosn();
            ArchiveManager.SaveToJosnFormGameData();*/

        }
    }


    /// <summary>
    /// 程序退出 保存
    /// </summary>
    public void OnApplicationQuit()
    {
        

    }


    // =================================================  结束 ================================================
    /// <summary>
    /// 刷新摄像机数据
    /// </summary>
    public void FreshUICam()
    {
        UICamera = GameObject.FindGameObjectWithTag("UICam").GetComponent<Camera>();
        /*var uiCam = GameObject.FindGameObjectWithTag("UICam").GetComponent<Camera>();
        var uiCamData = uiCam.GetComponent<UniversalAdditionalCameraData>();
        var mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponentInChildren<UniversalAdditionalCameraData>();
        uiCamData.renderType = CameraRenderType.Overlay;
        mainCam.cameraStack.Add(uiCam);*/
    }

    /// <summary>
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
    /// 是否为中国
    /// </summary>
    public bool IsChina
    {
        get
        {
            return true;
        }
    }

    /// <summary>
    /// 读取存档
    /// </summary>
    /// <returns></returns>
    public IEnumerator LoadSave()
    {
        
        yield return null;

    }

    /// <summary>
    /// 初始化回调
    /// </summary>
    /// <param name="status"></param>
    /// <param name="message"></param>
    /// <param name="shopProducts"></param>
    /*private void InitializeResultCallback(IAPOperationStatus status, string message, List<StoreProduct> shopProducts)
    {
        if (status == IAPOperationStatus.Success)
        {
            //IAP was successfully initialized
            Debug.Log("IAP初始化成功");
            HasIAP = true;

            //loop through all products
            for (int i = 0; i < shopProducts.Count; i++)
            {
                if (shopProducts[i].productName == "YourProductName")
                {
                    //if active variable is true, means that user had bought that product
                    //so enable access
                    if (shopProducts[i].active)
                    {
                        Debug.LogWarning("玩家已购买过" + shopProducts[i].productName);
                    }
                }
            }
        }
        else
        {
            Debug.Log("初始化支付错误" + message);
        }
    }

    /// <summary>
    /// 内购回调
    /// </summary>
    /// <param name="status"></param>
    /// <param name="message"></param>
    /// <param name="product"></param>
    public void ProductBoughtCallback(IAPOperationStatus status, string message, StoreProduct product)
    {
        if (status == IAPOperationStatus.Success)
        {
            //Map.Instance.Purchase.Play(0.1f);
            //each consumable gives coins in this example
            AudioManager.Instance.PlayOnceSingle("UI/purchase");
            AudioManager.Instance.PlayOnceSingle("UI/coin");
            if (product.productType == ProductType.Consumable)
            {
                ShopCfg cfg = null;
                foreach (var v in ConfigManager.GetConfigList<ShopCfg>())
                {
                    if (v.ShopId == product.productName)
                    {
                        cfg = v;
                        break;
                    }
                }
                if (cfg == null)
                {
                    Debug.LogError("错误，未找到返回奖励");
                }
                Account.ChangeRes((eResType)cfg.Main, product.value);
                var str = TextManager.GetText(TextNameToID.Get) + "<color=#FDBD00>" + TextManager.GetText(cfg.Name) + "*" + product.value + "</color>";
                MsgManager.Instance.SendMessage(MsgDefine.INFO_SEND_POPINFO_TXT, str);
                LocalSave();
            }
            //non consumable Unlock Level 1 -> unlocks level 1 so we set the corresponding bool to true
            if (product.productName == "UnlockLevel1")
                unlockLevel1 = true;
            //subscription has been bought so we set our subscription variable to true
            if (product.productName == "Subscription")
                subscription = true;
        }
        else
        {
            //an error occurred in the buy process, log the message for more details
            //Map.Instance.Refuse.Play(0.1f);
            Debug.Log("购买失败: " + message);
            AudioManager.Instance.PlayOnceSingle("UI/refuse");
            MsgManager.Instance.SendMessage(MsgDefine.INFO_SEND_POPINFO_ID, TextNameToID.Shop_PurchaseFail);
        }
        AudioManager.Instance.RevertMuteMusic();
        UIManager.CloseUI<WaitingUI>();
    }
*/


    /*  public void AliProductBoughtCallback(bool success)
      {
          if (success)
          {
              //Map.Instance.Purchase.Play(0.1f);
              //each consumable gives coins in this example
              AudioManager.Instance.PlayOnceSingle("UI/purchase");
              AudioManager.Instance.PlayOnceSingle("UI/coin");
              var cfg = PaySystem.cfg;
              if (cfg == null)
              {
                  Debug.LogError("错误，未找到返回奖励");
              }
              Account.ChangeRes((eResType)cfg.product_type, cfg.reward_value);
              string name = cfg.product_type == 0 ? "银币" : "钻石";
              var str = TextManager.GetText(TextNameToID.Get) + "<color=#FDBD00>" + name + "*" + cfg.reward_value + "</color>";
              MsgManager.Instance.SendMessage(MsgDefine.INFO_SEND_POPINFO_TXT, str);
              LocalSave();
          }
          else
          {
              Debug.Log("购买失败: ");
              AudioManager.Instance.PlayOnceSingle("UI/refuse");
              MsgManager.Instance.SendMessage(MsgDefine.INFO_SEND_POPINFO_ID, TextNameToID.Shop_PurchaseFail);
          }
          AudioManager.Instance.RevertMuteMusic();
          UIManager.CloseUI<WaitingUI>();
          PaySystem.cfg = null;
      }*/

    /// <summary>
    /// 购买月卡结束
    /// </summary>
    /// <param name="success"></param>
    /*  public void AliPayMonthCardCallback(bool success)
      {
          UIManager.OpenUI<WaitingUI>();
          if (success)
          {
              //Map.Instance.Purchase.Play(0.1f);
              //each consumable gives coins in this example
              AudioManager.Instance.PlayOnceSingle("UI/purchase");
              AudioManager.Instance.PlayOnceSingle("UI/coin");
              var cfg = PaySystem.Purchasecfg;
              if (cfg == null)
              {
                  Debug.LogError("错误，未找到返回奖励");
              }
              monthCardDayNum = cfg.reward_value;

              LocalSave();
              CloudMrg.Instance.savePurchase_items(cfg.product_id, cfg.order_id, (success, data) =>
              {
                  if (success)
                  {
                      var dt = LitJson.JsonMapper.ToObject<purchaseData>(data);
                      if (dt != null && dt.status == 0)
                      {
                          var str = "购买成功!";
                          MsgManager.Instance.SendMessage(MsgDefine.INFO_SEND_POPINFO_TXT, str);
                          GameBase.Instance.isPayVip = false;
                          updateShopMonthCar?.Invoke();
                      }
                      else
                      {
                          UIManager.OpenUI<AbnormalUI>();
                      }

                  }
                  else
                  {
                      UIManager.OpenUI<AbnormalUI>();
                  }
                  UIManager.CloseUI<WaitingUI>();
              });

          }
          else
          {
              Debug.Log("购买失败: ");
              AudioManager.Instance.PlayOnceSingle("UI/refuse");
              MsgManager.Instance.SendMessage(MsgDefine.INFO_SEND_POPINFO_ID, TextNameToID.Shop_PurchaseFail);
          }
          AudioManager.Instance.RevertMuteMusic();


      }*/




    private IEnumerator IEBackMainThread(Action ac)
    {
        yield return null;
        ac?.Invoke();
    }

    public void BackMainThread(Action ac)
    {
        StartCoroutine(IEBackMainThread(ac));
    }


    /// <summary>
    /// 重置shader
    /// </summary>
    /// <param name="obj"></param>
    public void ShaderRecover(GameObject obj)
    {
        Renderer[] meshSkinRenderer = obj.GetComponentsInChildren<Renderer>();
        for (int i = 0; i < meshSkinRenderer.Length; i++)
        {
            for (int j = 0; j < meshSkinRenderer[i].materials.Length; j++)
            {
                meshSkinRenderer[i].materials[j].shader = Shader.Find(meshSkinRenderer[i].materials[j].shader.name);
            }
        }
    }

    /// <summary>
    /// 重置界面shader
    /// </summary>
    /// <param name="obj"></param>
    public void ShaderRecoverUI(GameObject obj)
    {
      /*  foreach (var v in obj.GetComponentsInChildren<Image>())
        {
            //Debug.Log("恢复shader："+v.material.shader.name);
            v.material.shader = Shader.Find(v.material.shader.name);
        }*/
    }






    /// <summary>
    /// 退出游戏
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadCloud()
    {

    }




/*    //这个是用来检测玩家后台关闭应用（移动端）    //按键home 退出
    private void OnApplicationPause(bool pause)
    {
        print("游戏退出"+pause);

    }
   
    private void OnApplicationQuit()
    {
        print("游戏now退出");
    }*/

    }


/// <summary>
/// 游戏位置
/// </summary>
public enum GamePlayState { 
    Init,  //初始化界面
    MainCity, //主城
}


public enum Platform
{ 
    PC
}