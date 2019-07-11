using UnityEngine;
using System.Collections;
using System.Text;
using System.Security.Cryptography;
using System;
using System.IO;
using System.Collections.Generic;
public class EncryptDecipherTool {

	private static string key = "ABCDEFGHABCDEFGHABCDEFGHABCDEFGH";
	//private static string keyLock = "!@#$%^&*$#";//@#$$#%^&*//@#$%^&*$#//&*$#@#$%^

	public static string GetList(string AAAA,bool Ifmake)
	{
		//return	Encrypt (AAAA, key);
		return AAAA;
	}

	public static string GetListSS(string AAAA)
	{
		//return Encrypt (AAAA, key);
		return AAAA;
	}



	public static string GetListOld(string AAAA,bool Ifmake)
	{
//		if (Ifmake)
//			return Encrypt (AAAA, key);
//		else
			return AAAA;
	}


	public static string Encrypt(string content)
	{
		return Encrypt(content, key);
	}

	//加密
	public static string Encrypt(string content, string k )
	{
		byte[] keyBytes = UTF8Encoding.UTF8.GetBytes(k);
		RijndaelManaged rm = new RijndaelManaged();
		rm.Key = keyBytes;
		rm.Mode = CipherMode.ECB;
		rm.Padding = PaddingMode.PKCS7;
		ICryptoTransform ict = rm.CreateEncryptor();
		byte[] contentBytes = UTF8Encoding.UTF8.GetBytes(content);
		byte[] resultBytes = ict.TransformFinalBlock(contentBytes, 0, contentBytes.Length);

		string AAA=Convert.ToBase64String(resultBytes, 0, resultBytes.Length);
		Debug.Log(Decipher (AAA,key));
		return AAA;
	}

	public static string InsertFormat(string input, int interval, string value)  
	{  
		for (int i = interval; i < input.Length; i += interval + 1)  
			input = input.Insert(i, value);  
		return input;  
	} 
		
	public string GetValueGo(char getv)
	{
		string newv = null;
		int a = ((int)getv) % 3;
			newv+=key[a];
		 a = ((int)getv) % 5;
		newv+=key[a];
		a = ((int)getv) % 7;
		newv+=key[a];
		a = ((int)getv) % 9;
		newv+=key[a];

		return newv;
	}


//	public static string Decipher(string content)
//	{
//		return Decipher(content, key);
//	}

	//解密
	public static string Decipher(string content, string k)
	{
		byte[] keyBytes = UTF8Encoding.UTF8.GetBytes(k);
		RijndaelManaged rm = new RijndaelManaged();
		rm.Key = keyBytes;
		rm.Mode = CipherMode.ECB;
		rm.Padding = PaddingMode.PKCS7;
		ICryptoTransform ict = rm.CreateDecryptor();
		byte[] contentBytes = Convert.FromBase64String(content);
		byte[] resultBytes = ict.TransformFinalBlock(contentBytes, 0, contentBytes.Length);
		return UTF8Encoding.UTF8.GetString(resultBytes);
	}






	private static string strKey="myHippodrome";
	//MD5加密

	public static string UserMd5(List<DataValue> GetDataValue)
	{
		string message = string.Empty;
		GetDataValue = SortMessage (GetDataValue);
		if (GetDataValue.Count > 0) 
		{
			foreach (DataValue child in GetDataValue)
			{
				message += child.Name + "=" +child.GetString()+"&";		        
			}      
		}
		string time =UnityEngine.Random.Range(0,10000).ToString();
		string input =message+ "time"+"="+time+"&"+"key"+"="+strKey;
		//Debug.Log (input);
		System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();  
		byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(input);  
		byte[] hash = md5.ComputeHash(inputBytes);  
		StringBuilder sb = new StringBuilder();  
		for (int i = 0; i < hash.Length; i++)  
		{  
			sb.Append(hash[i].ToString("x2"));//大  "X2",小"x2"    
		} 
		string Md5OK =  "time" + "=" + time+ "&" +"sign" + "=" + sb.ToString();
		return Md5OK;   
	}
		
	private  static List<DataValue> SortMessage(List<DataValue> GetDataValue)
	{
		GetDataValue.Sort((x, y) => string.Compare(x.Name, y.Name));
		return GetDataValue;
	}

	//be bfoprt
}
