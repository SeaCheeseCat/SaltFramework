using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class Md5Utils
{
    public static string MD5Encrypt (string password)
    {
        MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider ();
        byte[] hashedDataBytes;
        hashedDataBytes = md5Hasher.ComputeHash (Encoding.GetEncoding ("gb2312").GetBytes (password));
        StringBuilder tmp = new StringBuilder ();
        foreach (byte i in hashedDataBytes)
        {
            tmp.Append (i.ToString ("x2"));
        }
        return tmp.ToString ();
    }
}