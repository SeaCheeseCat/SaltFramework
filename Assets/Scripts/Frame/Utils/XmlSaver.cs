using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System;
public class XmlSaver
{   
	//内容加密
	private string Encrypt(string toE)
	{
		//加密和解密采用相同的key,具体自己填，但是必须为32位//
		byte[] keyArray = UTF8Encoding.UTF8.GetBytes("12348578902223367877723456789012");
		RijndaelManaged rDel = new RijndaelManaged();
		rDel.Key = keyArray;
		rDel.Mode = CipherMode.ECB;
		rDel.Padding = PaddingMode.PKCS7;
		ICryptoTransform cTransform = rDel.CreateEncryptor();
		
		byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toE);
		byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray,0,toEncryptArray.Length);

		return Convert.ToBase64String(resultArray,0,resultArray.Length);
	}

	private string Encrypt(byte[] data)
	{
		//加密和解密采用相同的key,具体自己填，但是必须为32位//
		byte[] keyArray = UTF8Encoding.UTF8.GetBytes("12348578902223367877723456789012");
		RijndaelManaged rDel = new RijndaelManaged();
		rDel.Key = keyArray;
		rDel.Mode = CipherMode.ECB;
		rDel.Padding = PaddingMode.PKCS7;
		ICryptoTransform cTransform = rDel.CreateEncryptor();

		byte[] resultArray = cTransform.TransformFinalBlock(data, 0, data.Length);
		return Convert.ToBase64String(resultArray, 0, resultArray.Length);
	}

	//内容解密
	private string Decrypt(string toD)
	{
		//加密和解密采用相同的key,具体值自己填，但是必须为32位//
		byte[] keyArray = UTF8Encoding.UTF8.GetBytes("12348578902223367877723456789012");

		RijndaelManaged rDel = new RijndaelManaged();
		rDel.Key = keyArray;
		rDel.Mode = CipherMode.ECB;
		rDel.Padding = PaddingMode.PKCS7;
		ICryptoTransform cTransform = rDel.CreateDecryptor();

		byte[] toEncryptArray = Convert.FromBase64String(toD);
		byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray,0,toEncryptArray.Length);

		return UTF8Encoding.UTF8.GetString(resultArray);
	}

	//内容解密
	private byte[] DecryptBytes(string toD)
	{
		//加密和解密采用相同的key,具体值自己填，但是必须为32位//
		byte[] keyArray = UTF8Encoding.UTF8.GetBytes("12348578902223367877723456789012");

		RijndaelManaged rDel = new RijndaelManaged();
		rDel.Key = keyArray;
		rDel.Mode = CipherMode.ECB;
		rDel.Padding = PaddingMode.PKCS7;
		ICryptoTransform cTransform = rDel.CreateDecryptor();

		byte[] toEncryptArray = Convert.FromBase64String(toD);
		byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

		return resultArray;
	}

	/// <summary>
	/// 序列化对象
	/// </summary>
	/// <param name="pObject"></param>
	/// <param name="ty"></param>
	/// <returns></returns>
	public string SerializeObject(object pObject,System.Type ty)
	{
        string jsonStr = JsonUtility.ToJson(pObject);
        return jsonStr;
        //string XmlizedString = null;
        //MemoryStream memoryStream = new MemoryStream();
        //XmlSerializer xs = new XmlSerializer(ty);
        //XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
        //xs.Serialize(xmlTextWriter, pObject);
        //memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
        //XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());
        //return XmlizedString;
    }

    /// <summary>
    /// 反序列化对象
    /// </summary>
    /// <param name="serializedString"></param>
    /// <param name="ty"></param>
    /// <returns></returns>
	public object DeserializeObject(string serializedString, System.Type ty)
	{
        //XmlSerializer xs  = new XmlSerializer(ty);
        //MemoryStream memoryStream  = new MemoryStream(StringToUTF8ByteArray(pXmlizedString));
        ////XmlTextWriter xmlTextWriter   = new XmlTextWriter(memoryStream, Encoding.UTF8);
        //return xs.Deserialize(memoryStream);
        return JsonUtility.FromJson(serializedString, ty);
    }

	//创建XML文件
	public void CreateXML(string fileName,string thisData)
	{
		string xxx = Encrypt(thisData);
		StreamWriter writer;
		writer = File.CreateText(fileName);
		writer.Write(xxx);
		writer.Close();
	}

	public void CreateXMLNoEn(string fileName, string thisData)
	{
		StreamWriter writer;
		writer = File.CreateText(fileName);
		writer.Write(thisData);
		writer.Close();
	}

	//创建XML文件64
	public void CreateXML(string fileName, byte[] thisData)
	{
		string xxx = Encrypt(thisData);
		StreamWriter writer;
		writer = File.CreateText(fileName);
		writer.Write(xxx);
		writer.Close();
	}

	//读取XML文件
	public string LoadXML(string fileName)
	{
		StreamReader sReader = File.OpenText(fileName);
		string dataString = sReader.ReadToEnd();
		sReader.Close();
		string xxx = Decrypt(dataString);
		return xxx;
	}

	public byte[] LoadXmlBytes(string fileName)
    {
		StreamReader sReader = File.OpenText(fileName);
		string dataString = sReader.ReadToEnd();
		sReader.Close();
		var xxx = DecryptBytes(dataString);
		return xxx;
	}
	public byte[] GetBytes(string data) 
	{
		var xxx = DecryptBytes(data);
		return xxx;
	}
	public string GetString(byte[] data) 
	{
		return Encrypt(data);
	}


	//判断是否存在文件
	public bool HasFile(String fileName)
	{
		return File.Exists(fileName);
	}

	private string UTF8ByteArrayToString(byte[] characters  )
	{    
		UTF8Encoding encoding  = new UTF8Encoding();
		string constructedString  = encoding.GetString(characters);
		return (constructedString);
	}

	private byte[] StringToUTF8ByteArray(String pXmlString )
	{
		UTF8Encoding encoding  = new UTF8Encoding();
		byte[] byteArray  = encoding.GetBytes(pXmlString);
		return byteArray;
	}
}
