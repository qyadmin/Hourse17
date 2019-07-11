using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using LitJson;
using System;
public class HttpImage : MonoBehaviour {
	
    [SerializeField]
    string LoadURL;
	[SerializeField]
	Image GetImage;

	void Start()
	{
		StartCoroutine ("LoadImage");
	}


    IEnumerator LoadImage()
	{
		string url= LoadURL;
		WWW www = new WWW(url);
		yield return www;
		if (www.error == null) 
		{
			Texture2D texture = www.texture;
			Sprite sprites = Sprite.Create(texture,new Rect(0,0,texture.width,texture.height),new Vector2(0.5f,0.5f));
			GetImage.sprite = sprites;
		}
		else
			Debug.Log ("Fail"+www.error);
	}





    void CreateFile(string path, string name, string info)
	{
		//文件流信息
		StreamWriter sw;
		FileInfo t = new FileInfo(path + "//" +Static.Instance.SaveName+ name);
		if (!t.Exists)
		{
			//如果此文件不存在则创建
			sw = t.CreateText();
		}
		else
		{
			//如果此文件存在则打开
			sw = t.AppendText();
		}
		//以行的形式写入信息
		sw.WriteLine(info);
		//关闭流
		sw.Close();
		//销毁流
		sw.Dispose();
	}

	/**
     * path：读取文件的路径
     * name：读取文件的名称
     */
	ArrayList LoadFile(string path, string name)
	{
		//使用流的形式读取
		StreamReader sr = null;
		try
		{
			sr = File.OpenText(path + "//" +Static.Instance.SaveName+ name);
		}
		catch (Exception e)
		{
			//路径与名称未找到文件则直接返回空
			return null;
		}
		string line;
		ArrayList arrlist = new ArrayList();
		while ((line = sr.ReadLine()) != null)
		{
			//一行一行的读取
			//将每一行的内容存入数组链表容器中
			arrlist.Add(line);
		}
		//关闭流
		sr.Close();
		//销毁流
		sr.Dispose();
		//将数组链表容器返回
		return arrlist;
	}

	/**
     * path：删除文件的路径
     * name：删除文件的名称
     */
	void DeleteFile(string path, string name)
	{
		File.Delete(path + "//" + Static.Instance.SaveName+name);

	}
		

}
