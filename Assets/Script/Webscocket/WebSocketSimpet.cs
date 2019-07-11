using System;
using UnityEngine;
using BestHTTP;
using BestHTTP.WebSocket;  
using UnityEngine.UI;
using LitJson;
using UnityEngine.Events;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class DataSend
{
	public string type;
	public DataSend(string StartValue)
	{
		type = StartValue;
	}
}
	
public class Message<T>
{
	public Action<T> Listener;

	public void AddLintner(Action<T> GetLintener)
	{
		Listener += GetLintener;
	}

	public void RemoveLintner(Action<T> GetLintener)
	{
		Listener -= GetLintener;
	}

	public void Send(T GetValue)
	{
		Listener.Invoke (GetValue);
	}
}
	
	
public class WebSocketSimpet : MonoBehaviour
{

	public static WebSocketSimpet GetWebSocketSimpet;

	void Awake()
	{
		GetWebSocketSimpet = this;
	}

	#region Private Fields

	/// <summary>  
	/// The WebSocket address to connect  
	/// </summary>  
	string _address;  

	/// <summary>  
	/// Default text to send  
	/// </summary>  
	string _msgToSend;

	/// <summary>  
	/// Debug text to draw on the gui  
	/// </summary>  
	//string _text;

	/// <summary>  
	/// GUI scroll position  
	/// </summary>  
	Vector2 _scrollPos;

	private WabData _wabData;

	#endregion

	#region Unity Events

	void Start()
	{
        //_wabData = new WabData();
        //_address = _wabData.Address;
        //_msgToSend = _wabData.MsgToSend;
        //_text = _wabData.Text;
        //_wabData.MsgQueueStr.Clear ();
        //_wabData.OpenWebSocket();
        
        RunTime.AddLintner(GetFriestTime);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
		

	public Text ShowTime;
	public Text Minute;
	private bool IsSend;

	public enum MessageType
	{
		timer,
		sendHorseRun,
		sendHorseRanking

	}
	[System.Serializable]
	public class MessageReceived
	{
		public int Time;
		public string StrTime;
		public int IntTimer;
		public string HorseMessage;
		public string HorseOrder;
		public MessageType Type;
	}


	public MessageReceived GetMessageReceived=new MessageReceived();

	public Message<string> HorsesEvent=new Message<string>();
	public Message<int> RunTime=new Message<int>();

    public Message<int> EndTime = new Message<int>();
    public Message<int> resetEndTime = new Message<int>();

    public delegate void ReadytoRun();
    public ReadytoRun toRun_Delegate;


    public void SendToServer()
	{
		DataSend SendData=new DataSend("sendGetHorseRanking");
		string str = JsonMapper.ToJson (SendData);
		_wabData.WebSocket.Send(str);
	}
	private bool HAVES = false;
	public void GetFriestTime(int Timeget)
	{
		if (HAVES)
			return;
        if (Timeget <= 20)
        {
            HouseMove.GetHouseMove.FristSet(Timeget);
        }
        HAVES = true;
	}


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            locktime.time = 30;
        }
    }

    public void GetJson(JsonData obj)
	{
        JsonData getjson = obj["intTimer"];
        Debug.Log(JsonMapper.ToJson(getjson));
        //if (Input.GetKeyDown (KeyCode.Escape))
        //	TC ();
        //if (_wabData!=null&&_wabData.WebSocket == null) {
        //	Realod ();
        //}
        //Debug.Log(_wabData.MsgQueueStr.Count+"队列长度");
        //      if (_wabData.MsgQueueStr.Count > 10)
        //          _wabData.MsgQueueStr.Clear();

        //      if (_wabData.MsgQueueStr.Count > 0)
        //      {
        //	string info = _wabData.MsgQueueStr.Dequeue ();
        //	string jsondata = info; 
        //	//System.Text.Encoding.UTF8.GetString(www.bytes, 3, www.bytes.Length - 3);
        //	DeleteFile (Application.persistentDataPath, "json.txt");
        //	CreateFile (Application.persistentDataPath, "json.txt", jsondata);
        //	ArrayList infoall = LoadFile (Application.persistentDataPath, "json.txt");
        //	String sr = null;
        //	foreach (string str in infoall) {
        //		sr += str;
        //	}
        //	JsonData AllPosition = JsonMapper.ToObject (sr);
        //          //Debug.Log(JsonMapper.ToJson(sr));
        //	//Debug.Log (sr);
        //	try {
        //		GetMessageReceived.Type = (MessageType)Enum.Parse (typeof(MessageType), AllPosition ["type"].ToString ());
        //		switch (GetMessageReceived.Type) {
        //		case MessageType.timer:
        //			UpdateData (AllPosition);
        //			break;
        //		case MessageType.sendHorseRun:
        //			UpdateDataRun (AllPosition);
        //			break;
        //		case MessageType.sendHorseRanking:
        //			UpdateDataMC (AllPosition);
        //			break;
        //		}

        //	} catch {
        //		//Debug.LogError ("无用的数据" + sr + GetMessageReceived.Type);
        //	}
        //}
    }

	public GameObject webview;
	public void TC()
	{
		//Destroy (webview);
		SceneManager.LoadScene ("MeunScreen");
	}

	public void Realod()
	{
		//if(_wabData.WebSocket!=null)
		//_wabData.WebSocket.Close(1000, "Bye!");
        StopAllCoroutines();

        //SceneManager.LoadScene ("SyncScene");
        StartUpdate.Instance.Start();
        HouseMove.GetHouseMove.ReStart();
        AudioManager.Instance.Noracing();
    }


    public void RealoadGame()
    {
        SceneManager.LoadScene("SyncScene");
    }

	private int TimeInt=0;
	private string TimeIntFen="00";
	private string TimeIntMiao="00";
	public Text FengPan;
	public GameObject XiaZhu;
	public GameObject PersonPfb;

    public class LockTime
    {
        int locktime;
        int systimetime;
        public bool locktimeLock = false;
        public int time
        {
            get { return locktime; }
            set
            {

                locktime = value;
            }
        }
        public int systemtime
        {
            get { return systimetime; }
            set
            {

                systimetime = value;
            }
        }



    }
    LockTime locktime = new LockTime();

    int randomtime;

    IEnumerator Daojishi()
    {
        locktime.locktimeLock = true;
        while (true)
        {
            yield return new WaitForSeconds(1);
            locktime.time--;
            locktime.systemtime++;
            if (locktime.time <= 1)
            {
                locktime.time = 300;
                randomtime = UnityEngine.Random.Range(25, 15);
            }
                




            ShowTime.text = Static.Instance.Gettime(locktime.systemtime.ToString());
            if (locktime.time > 20)
            {
                TimeInt = locktime.time - 20;
                TimeIntFen = (TimeInt / 60).ToString();
                TimeIntMiao = (TimeInt % 60).ToString();
                TimeIntFen = TimeIntFen.Length > 1 ? TimeIntFen : "0" + TimeIntFen;
                TimeIntMiao = TimeIntMiao.Length > 1 ? TimeIntMiao : "0" + TimeIntMiao;
                Minute.text = TimeIntFen + ":" + TimeIntMiao;
            }
            else
            {
                Minute.text = "00:00";
            }

            if (locktime.time > 60)
            {
                TimeInt = locktime.time - 60;
                TimeIntFen = (TimeInt / 60).ToString();
                TimeIntMiao = (TimeInt % 60).ToString();
                TimeIntFen = TimeIntFen.Length > 1 ? TimeIntFen : "0" + TimeIntFen;
                TimeIntMiao = TimeIntMiao.Length > 1 ? TimeIntMiao : "0" + TimeIntMiao;
                FengPan.text = TimeIntFen + ":" + TimeIntMiao;
            }
            else
            {
                FengPan.text = "00:00";
                XiaZhu.SetActive(false);
                PersonPfb.SetActive(false);
            }


            //if (locktime.locktimeLock)
            //{
            if (locktime.time <= 25 && locktime.time > 20)
            {
                EndTime.Send(locktime.time - 20);
            }
            else
                resetEndTime.Send(locktime.time - 20);
            //}


            //if (locktime.locktimeLock)
            if (locktime.time == 25)
            {
                //GetMessageReceived.HorseMessage = AllPosition ["jsonHorses"].ToString ();
                //GetMessageReceived.HorseOrder=AllPosition ["horseOrder"].ToString();
                //HouseMove.GetHouseMove.SetRunLoop (GetMessageReceived.HorseOrder);
                //Debug.Log (GetMessageReceived.HorseOrder+"名次");
                //HorsesEvent.Send (GetMessageReceived.HorseMessage);
               
                if (toRun_Delegate != null)
                    toRun_Delegate();
            }


            if(locktime.time == randomtime)
                mchttp.Get();
            //if(locktime.locktimeLock)
            //    RunTime.Send(locktime.time);
            //else
            RunTime.Send(locktime.time);

            //if (GetMessageReceived.IntTimer == 1) 
            //{
            //	GetMessageReceived.HorseOrder=AllPosition ["horseOrder"].ToString();
            //	HouseMove.GetHouseMove.SetRunLoop (GetMessageReceived.HorseOrder);
            //	Debug.Log (GetMessageReceived.HorseOrder+"名次");
            //}



        }

    }
    IEnumerator ie;
    public void UpdateData()
	{
        //Debug.Log(JsonMapper.ToJson(AllPosition));
        GetMessageReceived.Time = int.Parse(Static.Instance.GetValue("system_time"));
		//GetMessageReceived.StrTime = AllPosition ["strTimer"].ToString ();
		GetMessageReceived.IntTimer = (60-Static.Instance.getdatetime(GetMessageReceived.Time.ToString()).Second)+ (4-Static.Instance.getdatetime(GetMessageReceived.Time.ToString()).Minute%5)*60;

        locktime.locktimeLock = false;
        StopAllCoroutines();
        if(GetMessageReceived.IntTimer>25)
            randomtime = UnityEngine.Random.Range(25, 15);
        if(GetMessageReceived.IntTimer <= 25 && GetMessageReceived.IntTimer > 15)
            randomtime = UnityEngine.Random.Range(GetMessageReceived.IntTimer, 15);
        if (GetMessageReceived.IntTimer <= 300)
        {
            if (!locktime.locktimeLock)
            {
                locktime.time = GetMessageReceived.IntTimer;
                locktime.systemtime = GetMessageReceived.Time;

                ie = Daojishi();
                StartCoroutine(ie);
                
            }
        }  
    }



	private void UpdateDataRun(JsonData AllPosition)
	{
		//GetMessageReceived.HorseMessage = AllPosition ["jsonHorses"].ToString ();
		//HorsesEvent.Send (GetMessageReceived.HorseMessage);
	}

	private void UpdateDataMC(JsonData AllPosition)
	{
		GetMessageReceived.HorseOrder=AllPosition ["horseOrder"].ToString();
		HouseMove.GetHouseMove.SetRunLoop (GetMessageReceived.HorseOrder);
        Debug.Log(GetMessageReceived.HorseOrder + "名次");
        //RunTime.Send(GetMessageReceived.IntTimer);
    }

    [SerializeField]
    HttpModel mchttp;
    public void GetMC(Text mc)
    {
        HouseMove.GetHouseMove.SetRunLoop(mc.text);
        Debug.Log(mc.text + "名次");
    }


	void OnDestroy()
	{
		//if (_wabData.WebSocket != null)
		//	_wabData.WebSocket.Close();
	}

	void OnGUI()
	{
//		_address = GUILayout.TextField(_address);
//
//		if (_wabData.WebSocket == null && GUI.Button(new Rect(10,50,100,50),"OpenWebsocket"))
//		{
//			_wabData.OpenWebSocket();
//
//			_text += "Opening Web Socket...\n";
//		}
//
//		if (_wabData.WebSocket != null && _wabData.WebSocket.IsOpen)
//		{
//			if (GUI.Button(new Rect(10,50,100,50),"Send"))
//			{
//				_text += "Sending message...\n";
//				// Send message to the server  
//				DataSend SendData=new DataSend("sendGetHorseRun");
//				string str = JsonMapper.ToJson (SendData);
//				_wabData.WebSocket.Send(str);
//				//_wabData.WebSocket.Send(_msgToSend);
//			}
//
//			if (GUI.Button(new Rect(10,100,100,50),"Close"))
//			{
//				// Close the connection  
//				_wabData.WebSocket.Close(1000, "Bye!");
//			}
//		}
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
		File.Delete(path + "//" + Static.Instance.SaveName+name);

	}

	#endregion
}
