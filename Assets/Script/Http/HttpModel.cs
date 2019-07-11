using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;
using UnityEngine.Events;
using System.IO;
using UnityEngine.UI;
using TypeClass;
public class HttpModel : MonoBehaviour {

	public TypeGo DataType;
	public NewMessageInfo Data;
	public string WarmText;
	public UnityEvent Suc, Fal;

	public bool IsLock=false;
	//public InputField inputField;
	private GameObject ShowLoad;
	private GameObject ShowError;

	public UnityEvent DoAction;
	public bool NoShow=false;

    public bool httpplan = true;
    void Awake()
	{
		ShowLoad = GameObject.Find ("HttpBack");
		ShowError=GameObject.Find ("ShowErrorGo");
		IsLock = Static.Instance.Lock;
	}


	private bool XtoY(string X,string Y)
	{
		string GETX = GetSendDataValue (X);
		string GetY = GetSendDataValue (Y);
		
		if (GETX != GetY||GETX==string.Empty||GetY==string.Empty)
			return false;
		return true;
	}

	private string GetSendDataValue(string name)
	{
		foreach (DataValue child in Data.SendData)
		{
			if (name == child.Name)
				return child.GetString ();
		}
		return string.Empty;
	}

	private void GetTagStr(string name)
	{
		foreach (GetBaseData child in Data.BaseDataList) 
		{
			if (child.Name == "msg") {
				child.UseTextGet.text = name;
				return;
			}
		}
		Debug.Log ("没有接受消息的text");
	}


	public void ActionOther(JsonData GetData)
	{
		foreach (HttpModel child in Data.ActionAdd)
			child.AddOtherAction (GetData);
	}

	public void AddOtherAction(JsonData GetJson)
	{
		Data.ShowMessage = GetJson.ToJson ();
		switch (DataType) {
		case TypeGo.GetTypeA:
			break;
		case TypeGo.GetTypeB:
			foreach (Transform child in Data.MyListMessage.FatherObj)
				Destroy(child.gameObject);
			Data.MyListMessage.SetVaule (GetJson[Data.DataName]);
			break;
		case TypeGo.GetTypeC:
			foreach (GameObject child in Data.MyListMessage.AllObj)
				Destroy (child);
			Data.MyListMessage.SetValueSingle (GetJson[Data.DataName]);
			break;
	      }
	}

	public void Get()
	{
		bool IsOk = true;
		foreach (DataValue child in Data.SendData) 
		{
				switch (child.IsEmpty) 
				{
			case WarmType.Empty:
				if (child.GetString () == string.Empty) {
					WarmText = child.EmptyTag;
					IsOk = false;
				}
					break;
			case WarmType.XandY:
				if (child.GetString()!=child.Y_Name.text) {
					WarmText = child.EmptyTag;
					IsOk = false;
				}
			break;
			case WarmType.Value:
				if (child.ValueName.Contains("#")) 
				{
					string[] str=child.ValueName.Split('#');
					switch (str.Length) 
					{
					case 2:
						int getvalue = int.Parse (child.GetString ());
						int maxnub = int.Parse (str [1]);
						int bilinub = int.Parse (str [0]);
						if (getvalue % bilinub == 0 && getvalue >= maxnub)
							IsOk = true;
						else
							IsOk = false;
						WarmText = "你输入的金额不符合提现要求";
						break;
					}
				} 
				else 
				{
					if (child.GetString () == child.ValueName) {
						WarmText = child.EmptyTag;
						IsOk = false;
					}
					if (child.GetString () == string.Empty) {
						WarmText = "你输入的金额不能为空";
						IsOk = false;
					}
				}
				break;
                case WarmType.Length:
                    if (child.GetString().Length < child.Length)
                    {
                        WarmText = child.EmptyTag;
                        IsOk = false;
                    }
                    break;
            }

			if (!IsOk)
				break;
		}
        if (!IsOk)
        {
            GetTagStr(WarmText);
            GameObject.Find("ShowMessage").transform.localScale = new Vector3(1, 1, 1);
            return;
        }
        Data.BackData.Clear ();
        if (ShowLoad != null && httpplan)
            ShowLoad.transform.localScale = new Vector3(1, 1, 1);
		if(ShowError!=null)
		ShowError.transform.localScale = new Vector3 (0, 0, 0);
		StopAllCoroutines();
		StartCoroutine ("GetMessage");
	}
		
	IEnumerator GetMessage()
	{
		message=null;
		message += "?"; 
		string url=Static.Instance.URL+Data.URL;
		if (Data.SendData.Count > 0) 
		{
			foreach (DataValue child in Data.SendData)
			{
				message += "&" + child.Name + "=" +child.GetString();		        
			}      
		}
		message=EncryptDecipherTool.GetListOld(message,IsLock);
		if(IsLock)
			message +="&"+EncryptDecipherTool.UserMd5 (Data.SendData);
		url = url + message;
		Debug.Log (url);

		url = Uri.EscapeUriString(url);
		WWW www = new WWW(url);
		yield return www;

		if (www.error != null)
		{
			Data.ShowMessage="error code = " + www.error;
            if (ShowError != null && !NoShow)
            {
                //ShowError.transform.localScale = new Vector3(1, 1, 1);
                //ShowError.gameObject.GetComponentInChildren<Text>().text = "网络连接不稳定";
            }
            DoAction.Invoke ();
		}
		else
		{
			string jsondata = www.text; 

		    //System.Text.Encoding.UTF8.GetString(www.bytes, 3, www.bytes.Length - 3);
			jsondata = jsondata.Remove(0, Data.CutCount);
         
            DeleteFile(Application.persistentDataPath, "json.txt");
			CreateFile(Application.persistentDataPath, "json.txt", jsondata);
			ArrayList infoall = LoadFile(Application.persistentDataPath, "json.txt");
			String sr = null;
			foreach (string str in infoall)
			{
				sr += str;
			}
           

            JsonData jd = JsonMapper.ToObject (sr);
            Data.ShowMessage = jsondata + jd["msg"].ToString();
            Data.GetBase.code=jd.Keys.Contains("code")?jd["code"].ToString():"";
            Data.GetBase.systemtime = jd.Keys.Contains("now") ? jd["now"].ToString() : "";
            if (Data.GetBase.systemtime != "")
                Static.Instance.AddValue("system_time", Data.GetBase.systemtime);
            //			Data.GetBase.result=jd.Keys.Contains("result")?jd["result"].ToString():"";
            //			Data.GetBase.msg=jd.Keys.Contains("msg")?jd["msg"].ToString():"";
            //			Data.GetBase.url=jd.Keys.Contains("url")?jd["url"].ToString():"";
            //			if(Data.GetBase.codetext!=null)
            //			Data.GetBase.codetext.text = Data.GetBase.code;
            //			if(Data.GetBase.resulttext!=null)
            //			Data.GetBase.resulttext.text = Data.GetBase.result;
            //			if(Data.GetBase.msgtext!=null)
            //			Data.GetBase.msgtext.text = Data.GetBase.msg;
            //			if(Data.GetBase.urltext!=null)
            //				Data.GetBase.urltext.text = Data.GetBase.url;



            foreach (Value i in Data.GetBase.otherValue)
            {
                if (i.FuntionName != "" && i.SendObj != null)
                {
                    i.SendObj.SendMessage(i.FuntionName, jd[i.valueName]);
                }
                else
                {
                    if (i.valueText != null)
                        i.valueText.text = jd.Keys.Contains(i.valueName) ? jd[i.valueName].ToString() : "";
                    if (i.isSave)
                        Static.Instance.AddValue(i.valueName, jd.Keys.Contains(i.valueName) ? jd[i.valueName].ToString() : "");
                }
            }


            foreach (GetBaseData child in Data.BaseDataList) 
			{
				try{
				     switch (child.GetType) 
				     {
				       case GetBackType.Text:
					        child.UseTextGet.text =jd [child.Name].ToString ();
					   break;
				       case GetBackType.InputText:
					        child.UseInputGet.text = jd [child.Name].ToString ();
					   break;
				    }
					child.IsGet = true;
				 }
				catch
				{
					child.IsGet = false;
				}
			}
				

			if (Data.GetBase.code == "1") 
			{
				ActionOther (jd);

			 try{

                    List<string> Savename = new List<string>();
                    Dictionary<string, string> SaveMessage = new Dictionary<string, string>();
                    switch (DataType)
                    {
                        case TypeGo.GetTypeA:
                            break;
                        case TypeGo.GetTypeB:
                            foreach (Transform child in Data.MyListMessage.FatherObj)
                                Destroy(child.gameObject);
                            Data.MyListMessage.SetVaule(jd[Data.DataName]);
                            break;
                        case TypeGo.GetTypeC:
                            foreach (GameObject child in Data.MyListMessage.AllObj)
                                Destroy(child);
                            Data.MyListMessage.SetValueSingle(jd[Data.DataName]);
                            break;
                        case TypeGo.GetTypeD:
                            Data.SetData(jd);
                            break;
                    }

                }
                catch 
			  {
				Debug.Log ("无法解析");
			  }
			}
		}
			
		if (Data.GetBase.code == "1")
			Suc.Invoke ();
		else if (Data.GetBase.code == "0")
			Fal.Invoke ();
		
		if (BusinessInfoHelper.Instance !=  null) 
		{
			BusinessInfoHelper.Instance.isDone = true;
		}

		ShowLoad.transform.localScale=new Vector3(0,0,0);
	}


	IEnumerator GetMessageB()
	{
		string url=Static.Instance.URL+Data.URL;

		if (Data.SendData.Count > 0)
		{
			foreach (DataValue child in Data.SendData)
			{
				message += "&" + child.Name + "=" +child.GetString();		        
			}        
		}
		message=EncryptDecipherTool.GetListOld(message,IsLock);
		url = url + message;
		url = Uri.EscapeUriString(url);
		Debug.Log (url);
		WWW www = new WWW(url);
		yield return www;

		if (www.error != null)
		{
			Data.ShowMessage = "error code = " + www.error;
            if (ShowError != null && !NoShow)
            {
                //ShowError.transform.localScale = new Vector3(1, 1, 1);
                //ShowError.gameObject.GetComponentInChildren<Text>().text = "网络连接不稳定";
            }
        }
		else
		{
			string jsondata = System.Text.Encoding.UTF8.GetString(www.bytes, 3, www.bytes.Length - 3);
			jsondata = jsondata.Remove(0, Data.CutCount);
			int a = 0;
			//CreateFile(Application.streamingAssetsPath, "json.txt", jsondata);
			DeleteFile(Application.persistentDataPath, "json.txt");
			CreateFile(Application.persistentDataPath, "json.txt", jsondata);
			ArrayList infoall = LoadFile(Application.persistentDataPath, "json.txt");
			String sr = null;
			foreach (string str in infoall)
			{
				sr += str;
			}
			JsonData jd = JsonMapper.ToObject(sr);

			Data.ShowMessage = jsondata;
			Debug.Log(jsondata);
			Data.GetBase.code = jd.Keys.Contains("code") ? jd["code"].ToString() : "";
			Data.GetBase.result = jd.Keys.Contains("result") ? jd["result"].ToString() : "";
			Data.GetBase.msg = jd.Keys.Contains("msg") ? jd["msg"].ToString() : "";
            Data.GetBase.systemtime = jd.Keys.Contains("now") ? jd["now"].ToString() : "";
            if (Data.GetBase.systemtime != "")
                Static.Instance.AddValue("system_time", Data.GetBase.systemtime);
            if (Data.GetBase.msgInputtext!=null)
				Data.GetBase.msgInputtext.text = System.Math.Floor(float.Parse(Data.GetBase.msg)).ToString();
			if(Data.GetBase.msgtext!=null)
				Data.GetBase.msgtext.text = Data.GetBase.msg;

            foreach (Value i in Data.GetBase.otherValue)
            {
                if (i.FuntionName != "" && i.SendObj != null)
                {
                    i.SendObj.SendMessage(i.FuntionName, jd[i.valueName]);
                }
                else
                {
                    if (i.valueText != null)
                        i.valueText.text = jd.Keys.Contains(i.valueName) ? jd[i.valueName].ToString() : "";
                    if (i.isSave)
                        Static.Instance.AddValue(i.valueName, jd.Keys.Contains(i.valueName) ? jd[i.valueName].ToString() : "");
                }
            }

            if (Data.GetBase.code == "1")
			{
				List<string> Savename = new List<string>();
				Dictionary<string, string> SaveMessage = new Dictionary<string, string>();

                foreach (Transform child in Data.MyListMessage.FatherObj)
                {
                    Destroy(child.gameObject);
                }
                Data.MyListMessage.SetVaule (jd[Data.DataName]);
					
				if (Data.Action)
				{
					Data.GetData (SaveMessage);
				}
			}

			if (Data.GetBase.code == "2")
				GameObject.Find ("ErrorRestart").SendMessage("Restart",Data.GetBase.msg);
			if (Data.GetBase.code == "1")
				Suc.Invoke();
			else
				Fal.Invoke();
			
		}
		if (BusinessInfoHelper.Instance != null) 
		{
			BusinessInfoHelper.Instance.isDone = true;
		}
		ShowLoad.transform.localScale=new Vector3(0,0,0);
	}


	string message=null;
	IEnumerator GetMessageC()
	{
		string url=Static.Instance.URL+Data.URL;
		if (Data.SendData.Count > 0)
		{
			foreach (DataValue child in Data.SendData)
			{
				message += "&" + child.Name + "=" +child.GetString();		        
			}        
		}
		message=EncryptDecipherTool.GetListOld(message,IsLock);
		url = url + message;
		url = Uri.EscapeUriString(url);
		Debug.Log (url);
		WWW www = new WWW(url);
		yield return www;

		if (www.error != null)
		{
			Data.ShowMessage = "error code = " + www.error;
            if (ShowError != null && !NoShow)
            {
                //ShowError.transform.localScale = new Vector3(1, 1, 1);
                //ShowError.gameObject.GetComponentInChildren<Text>().text = "网络连接不稳定";
            }
        }
		else
		{
			string jsondata = System.Text.Encoding.UTF8.GetString(www.bytes, 3, www.bytes.Length - 3);
			jsondata = jsondata.Remove(0, Data.CutCount);
			int a = 0;
			//CreateFile(Application.streamingAssetsPath, "json.txt", jsondata);
			DeleteFile(Application.persistentDataPath, "json.txt");
			CreateFile(Application.persistentDataPath, "json.txt", jsondata);
			ArrayList infoall = LoadFile(Application.persistentDataPath, "json.txt");
			String sr = null;
			foreach (string str in infoall)
			{
				sr += str;
			}
			JsonData jd = JsonMapper.ToObject(sr);
			Data.ShowMessage = jsondata;
			Debug.Log(jsondata);
			Data.GetBase.code = jd.Keys.Contains("code") ? jd["code"].ToString() : "";
			Data.GetBase.result = jd.Keys.Contains("result") ? jd["result"].ToString() : "";
			Data.GetBase.msg = jd.Keys.Contains("msg") ? jd["msg"].ToString() : "";
            Data.GetBase.systemtime = jd.Keys.Contains("now") ? jd["now"].ToString() : "";
            if (Data.GetBase.systemtime != "")
                Static.Instance.AddValue("system_time", Data.GetBase.systemtime);
            if (Data.GetBase.msgInputtext!=null)
				Data.GetBase.msgInputtext.text = System.Math.Floor(float.Parse(Data.GetBase.msg)).ToString();


            foreach (Value i in Data.GetBase.otherValue)
            {
                if (i.FuntionName != "" && i.SendObj != null)
                {
                    i.SendObj.SendMessage(i.FuntionName, jd[i.valueName]);
                }
                else
                {
                    if (i.valueText != null)
                        i.valueText.text = jd.Keys.Contains(i.valueName) ? jd[i.valueName].ToString() : "";
                    if (i.isSave)
                        Static.Instance.AddValue(i.valueName, jd.Keys.Contains(i.valueName) ? jd[i.valueName].ToString() : "");
                }
            }


            if (Data.GetBase.code == "1")
			{
				List<string> Savename = new List<string>();
				Dictionary<string, string> SaveMessage = new Dictionary<string, string>();

				foreach (GameObject child in Data.MyListMessage.AllObj)
					Destroy (child);
				//Debug.Log (jd[Data.DataName]["name"]);
				Data.MyListMessage.SetValueSingle (jd[Data.DataName]);
				if (Data.Action)
				{
					Data.GetData (SaveMessage);
				}
			}

			if (Data.GetBase.code == "2")
				GameObject.Find ("ErrorRestart").SendMessage("Restart",Data.GetBase.msg);
			if (Data.GetBase.code == "1")
				Suc.Invoke();
			else
				Fal.Invoke();
		}
		if (BusinessInfoHelper.Instance != null) 
		{
			BusinessInfoHelper.Instance.isDone = true;
		}
		ShowLoad.transform.localScale=new Vector3(0,0,0);
	}


	void CreateFile(string path, string name, string info)
	{
		//文件流信息
		StreamWriter sw;
		FileInfo t = new FileInfo(path + "//" + Static.Instance.SaveName+name);
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
		File.Delete(path + "//" +Static.Instance.SaveName+ name);

	}
}
