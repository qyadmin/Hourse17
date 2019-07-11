using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using UnityEngine.Events;
using System;
using UnityEngine.UI;
using System.Linq;
public class SaveZH : MonoBehaviour {

	public InputField Name;
	public InputField Password;

	void Start()
	{
		try
		{
		SaveZP.isOn = Convert.ToBoolean(LaodToggle ());
		}
		catch 
		{
			SaveZP.isOn = false;
			Debug.Log ("没有记录");
		}
		if (SaveZP.isOn) 
		{
			Debug.Log (SaveZP.isOn.ToString()+"读取了信息");
			Name.text = LoadName ();
			Password.text = Loadpassword ();
		}
		//      if(LoadURL()!=string.Empty)
		//Static.Instance.URL = LoadURL ();
	}

	public UnityEvent Loagin;
	public void SetName(Dropdown URLList)
	{
//		string aa=Static.Instance.GetValue ("tel");
//		string bb=Static.Instance.GetValue ("password");
//		SaveName (aa);
//		password (bb);
//		SaveURL (URLList);
//		SaveNameLsit (Static.Instance.GetNeedNameList(Name.text,Password.text));
	}



	public void SaveNameLsit(string Namelist)
	{
		DeleteFile(Application.persistentDataPath, "nameList.txt");
		CreateFile(Application.persistentDataPath, "nameList.txt", Namelist);
	}
		

	[SerializeField]
	private Toggle SaveZP;
	public void SaveNameAndPassword()
	{
		if (SaveZP.isOn) {
			DeleteFile (Application.persistentDataPath, "name.txt");
			CreateFile (Application.persistentDataPath, "name.txt", Name.text);
			DeleteFile (Application.persistentDataPath, "password.txt");
			CreateFile (Application.persistentDataPath, "password.txt", Password.text);
		} 
		else 
		{
			ClearZH ();
		}

		Loagin.Invoke ();
	}

	public void SaveZPON()
	{
		DeleteFile (Application.persistentDataPath, "Toggle.txt");
		CreateFile (Application.persistentDataPath, "Toggle.txt", SaveZP.isOn.ToString());
		Debug.Log ("存了状态"+SaveZP.isOn.ToString());
		if (!SaveZP.isOn)
			ClearZH ();
	}


	public void ClearZH()
	{
		DeleteFile (Application.persistentDataPath, "name.txt");
		DeleteFile (Application.persistentDataPath, "password.txt");
	}


	public void password()
	{
		
	}
		



	public string  LaodToggle()
	{
		ArrayList infoall = LoadFile(Application.persistentDataPath, "Toggle.txt");
		String sr = null;
		if (infoall == null)
			return string.Empty;
		foreach (string str in infoall)
		{
			sr += str;
		}
		Debug.Log (sr);
		return sr;
	}





	public string  LoadName()
	{
		ArrayList infoall = LoadFile(Application.persistentDataPath, "name.txt");
		String sr = null;
		if (infoall == null)
			return string.Empty;
		foreach (string str in infoall)
		{
			sr += str;
		}
		Debug.Log (sr);
		return sr;
	}

	public string  Loadpassword()
	{
		ArrayList infoall = LoadFile(Application.persistentDataPath, "password.txt");
		String sr = null;
		if (infoall == null)
			return string.Empty;
		foreach (string str in infoall)
		{
			sr += str;
		}
		Debug.Log (sr);
		return sr;
	}


	public string  LoadNameList()
	{
		ArrayList infoall = LoadFile(Application.persistentDataPath, "nameList.txt");
		String sr = null;
		if (infoall == null)
			return string.Empty;
		foreach (string str in infoall)
		{
			sr += str;
		}
		Debug.Log (sr);
		return sr;
	}


	public Transform NameListFather;
	public GameObject baseLsit;
	public void GetNameShow(GameObject ButtonChild)
	{
		if (baseLsit.activeSelf) 
		{
			baseLsit.SetActive (false);
			return;
		}
		else
			baseLsit.SetActive (true);
		foreach (Transform child in NameListFather)
			Destroy (child.gameObject);

		Dictionary<string ,string> NMAELSIT = Static.Instance.SetNameListBack (LoadNameList());

		foreach (string child in NMAELSIT.Keys) 
		{
			GameObject Newname = GameObject.Instantiate (ButtonChild);
			Newname.SetActive (true);
			Newname.GetComponentInChildren<Text> ().text = child;
			Newname.transform.SetParent (NameListFather);
			Newname.transform.localScale = new Vector3 (1,1,1);
		}
	}

	public void SetLogin(Text Nametext)
	{
		Name.text = Nametext.text;
		Password.text = Static.Instance.GetPassWord (Nametext.text);
		baseLsit.SetActive (false);
	}


	public void SaveURL(Dropdown URLList)
	{
		DeleteFile(Application.persistentDataPath, "URL.txt");
		CreateFile(Application.persistentDataPath, "URL.txt", URLList.value.ToString());
	}


	public string  LoadURL()
	{
		ArrayList infoall = LoadFile(Application.persistentDataPath, "URL.txt");
		String sr = null;
		if (infoall == null)
			return string.Empty;
		foreach (string str in infoall)
		{
			sr += str;
		}
		Debug.Log (sr);
		return sr;
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
			sr = File.OpenText(path + "//" + Static.Instance.SaveName+name);
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