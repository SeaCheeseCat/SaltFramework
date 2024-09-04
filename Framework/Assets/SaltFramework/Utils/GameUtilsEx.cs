using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public  class GameUtilsEx
{
    public static string GetUUID()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        return uuid();
#elif UNITY_IPHONE && !UNITY_EDITOR
        return GetOnlyDeviceID.Get_OnlyID_By_KeyChain();
#else
        return "test";
#endif
    }

    private static string uuid() 
    {
        /*switch (GameBase.Instance.Platform) 
        {
            case Platform.TapTap:
                return GameBase.Instance.User.ObjectId;
            case Platform.HuaWei:
                return "huaiweitest";
            case Platform.HaoYou:
                return GameBase.Instance.HyPlayerID.ToString();

        }*/
        return "test";
    }



    public static string GetPlayerID() 
    {

#if UNITY_ANDROID && !UNITY_EDITOR
        return playerid();
#elif UNITY_IPHONE && !UNITY_EDITOR
        return GetOnlyDeviceID.Get_OnlyID_By_KeyChain();
#else
        return "test";
#endif
    }
    
    /// <summary>
    /// »ñÈ¡TapÃû×Ö
    /// </summary>
    /// <returns></returns>
    public static string GetPlayerName()
    {

#if UNITY_ANDROID && !UNITY_EDITOR
        return name();
#elif UNITY_IPHONE && !UNITY_EDITOR
        return GetOnlyDeviceID.Get_OnlyID_By_KeyChain();
#else
        return "test";
#endif
    }

    public static string name() 
    {
        /*switch (GameBase.Instance.Platform)
        {
            case Platform.TapTap:
                return GameBase.Instance.User["nickname"].ToString();
            case Platform.HuaWei:
                return HuaWeiLogin.Instance.name;
            case Platform.HaoYou:
                return GameBase.Instance.HaoYouName;
        }*/
        return "test";
    }








}
public class GetOnlyDeviceID
{
#if UNITY_IPHONE && !UNITY_EDITOR
    public static string Get_OnlyID_By_KeyChain() 
    {
        return Get_UUID_By_KeyChain();
    }

    [DllImport("__Internal")]
    private static extern string Get_UUID_By_KeyChain();
#endif
}
