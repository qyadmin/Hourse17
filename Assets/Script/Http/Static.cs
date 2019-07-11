using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.IO;
using System;
public class Static
{
    private static Static instance;

    public static Static Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Static();
            }
            return instance;
        }
    }

    public Logininfo CurrentAccount = new Logininfo();
	private Dictionary<string,string> SaveNameMessage = new Dictionary<string, string> ();

	public void ClearName()
	{
		SaveNameMessage.Clear ();
	}
	public string GetPassWord(string Name)
	{
		string passWord = null;
		SaveNameMessage.TryGetValue (Name, out passWord);
		return passWord;
	}

	public void AddName(string Name,string PassWord)
	{
		SaveNameMessage.Add (Name, PassWord);
	}
		
	public Dictionary<string,string> SetNameListBack(string GetList)
	{
		SaveNameMessage.Clear ();
		if (GetList != string.Empty) {
			string[] ZPArray = GetList.Split ('#');
			string NameList = ZPArray [0];
			string PassList = ZPArray [1];
			string[] NameArray = NameList.Split ('*');
			string[] PassArray = PassList.Split ('*');
			for (int i = 0; i < NameArray.Length; i++) 
			{
				if (NameArray [i] == string.Empty)
					continue;
				if (SaveNameMessage.ContainsKey (NameArray [i]))
					SaveNameMessage.Remove (NameArray [i]);
				SaveNameMessage.Add (NameArray [i], PassArray [i]);
			}
		}

		return SaveNameMessage;
	}


	public string GetNeedNameList(string NowName,string NowPassWord)
	{
		string Name = string.Empty;
		string password = string.Empty;
		Name += "*"+NowName;
		password +="*"+ NowPassWord;
		foreach (KeyValuePair<string,string> child in SaveNameMessage) 
		{
			if (child.Key == NowName)
				continue;
			Name += "*"+child.Key;
			password +="*"+ child.Value;
		}

		string AllString = Name + "#" + password;
		return AllString;
	}
		

	public string Level="1.0.1";
	public bool Lock=true;
	public bool MusicSwich = true;
	public string SaveName ="3DPaoMa17";
    public int planNamber = 0;
    //替换芯域名
    public string URL = "http://ycdtm.cn/horse17/index.php/Interface/"; //"http://nebulagroup.net/tpHorse/index.php/"; //http://47.94.41.248/tpHorse/index.php/       "http://psmin.cn/tpHorse/index.php/";//http://straw.mmykw.cn//http://test.mmykw.cn/    http://paoma.gulutea.com/index.php/      http://www.wenvbo.top/tpHorse/index.php/
                                                                    //public string URLold = "http://www.782pay.cn";http://www.pb6x.cn/    "http://www.c8m6tu.cn/tpHorse/index.php/"  "http://www.trf2.cn/tpHorse/index.php/"
    public Logininfo LoginAccount = new Logininfo();

    public MessageInfo Info = new MessageInfo();
    Dictionary<string, string> SaveMessage = new Dictionary<string, string>();


    public string GetValue(string Name)
    {
        string ValueGet = null;
		bool a= SaveMessage.TryGetValue(Name, out ValueGet);
		if (a)
			return ValueGet;
		else
			return "";
    }
    public void AddValue(string Name, string ValueAdd)
    {
        string a = GetValue(Name);
        if (a == null)
            SaveMessage.Add(Name, ValueAdd);
        else
        {
            SaveMessage.Remove(Name);
            SaveMessage.Add(Name, ValueAdd);
        }
    }


    Dictionary<string, string> SaveBackMessage = new Dictionary<string, string>();

    public string GetBackValue(string Name)
    {
        string ValueGet = null;
        SaveMessage.TryGetValue(Name, out ValueGet);
        return ValueGet;
    }
    public void AddBackValue(string Name, string ValueAdd)
    {
        string a = GetValue(Name);
        if (a == null)
            SaveMessage.Add(Name, ValueAdd);
    }

	public Dictionary<string, Dic> SaveTuDi = new Dictionary<string, Dic>();

	public void AddTuDi(string name,Dic GetDic)
	{
		if (SaveTuDi.ContainsKey (name))
			SaveTuDi.Remove (name);
		SaveTuDi.Add (name, GetDic);
	}

	public Dictionary<string, Dic> SaveFriend = new Dictionary<string, Dic>();

	public void AddFriend(string aaa,Dic bbb)
	{
		if (SaveFriend.ContainsKey (aaa))
			SaveFriend.Remove (aaa);
		SaveFriend.Add (aaa, bbb);
	}

    public Dictionary<string, Dic> SaveGrownInfo = new Dictionary<string, Dic>();

	public void SaveGrown(string aaa,Dic bbb)
	{
		if (SaveGrownInfo.ContainsKey (aaa))
			SaveGrownInfo.Remove (aaa);
		SaveGrownInfo.Add (aaa, bbb);
	}
		
    public Dictionary<string, Dic> SaveTradingInfo = new Dictionary<string, Dic>();
    public Dictionary<string, string> SaveData = new Dictionary<string, string>();

    public string GetData(string Name)
    {
        string ValueGet = null;
        SaveData.TryGetValue(Name, out ValueGet);
        return ValueGet;
    }
    public void AddData(string Name, string ValueAdd)
    {
        string a = GetData(Name);
        if (a == null)
            SaveData.Add(Name, ValueAdd);
    }

    public delegate void UpdateAllData();
    public UpdateAllData ClearData;
    public void UpdateData()
    {
        SaveTuDi.Clear();
        SaveData.Clear();
        SaveBackMessage.Clear();
        //ClearData.Invoke();
    }

	public void UpdateAllObj()
	{
		UpdateData ();
		if(BusinessInfoHelper.Instance!=null)
		BusinessInfoHelper.Instance.UpdateDate();
	}

    public void ClearAll()
    {
        UpdateData();
        SaveMessage.Clear();
    }




    private DateTime GetDateTime(string timeStamp)
    {
        DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        long lTime = long.Parse(timeStamp + "0000000");
        TimeSpan toNow = new TimeSpan(lTime);
        //DateTime targetDt = dtStart.Add(toNow);
        return dtStart.Add(toNow);
    }

    private int GetTimeStamp(DateTime dt)
    {
        DateTime dateStart = new DateTime(1970, 1, 1, 8, 0, 0);
        int timeStamp = Convert.ToInt32((dt - dateStart).TotalSeconds);
        return timeStamp;
    }


    public string Gettime(string time)
    {
        DateTime dt = GetDateTime(time);
        
        return dt.ToString("yyyy-MM-dd HH:mm:ss");
    }

    public string GetTime(string time)
    {
        DateTime dt = GetDateTime(time);

        return dt.ToString("yyyy-MM-dd hh:mm:ss");
    }

    public DateTime getdatetime(string time)
    {
        DateTime dt = GetDateTime(time);

        return dt;
    }


    public string Getstamp(string dtStart)
    {
        DateTime dt = Convert.ToDateTime(dtStart);
        string m_timestamp = GetTimeStamp(dt).ToString();
        return m_timestamp;
    }
}
