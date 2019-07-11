using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;
using UnityEngine.Events;
using System.IO;
using UnityEngine.UI;
using System.Linq;
public class HouseMove : MonoBehaviour {

	public static HouseMove GetHouseMove;


    [System.Serializable]
    public class Camera
    {
        public Transform camera;
        public Animator cameraAnima;
        Vector3 Startpos = new Vector3();
        Quaternion Startrot = new Quaternion();
        bool movinglock = false;

        Transform followVec;

        public void Startpos_set()
        {
            cameraAnima = camera.gameObject.GetComponent<Animator>();
            Startpos = camera.localPosition;
            Startrot = camera.localRotation;
            cameraAnima.enabled = true;
        }
        public void Camera_setpos()
        {
            camera.localPosition = Startpos;
            camera.localRotation = Startrot;
        }
        public void lockSet(bool b)
        {

            movinglock = b;
        }

        public void follow_set(Transform obj)
        {
            followVec = obj;
        }

        public void Camera_Move()
        {
            if (followVec.localPosition.z > camera.localPosition.z && !movinglock)
                camera.localPosition = new Vector3(camera.localPosition.x, camera.localPosition.y, followVec.localPosition.z);
            if (movinglock)
                camera.position = new Vector3(camera.position.x, camera.position.y, Mathf.Lerp(camera.position.z, followVec.position.z, (60 / Vector3.Distance(camera.position, followVec.position)) * Time.deltaTime));
        }

        public void Camera_lerp(Transform end)
        {
            camera.position = new Vector3(camera.position.x, camera.position.y, Mathf.Lerp(camera.position.z, end.position.z, (60 / Vector3.Distance(camera.position, end.position)) * Time.deltaTime));
        }

    }

    [SerializeField]
    Camera camera3D;

    [SerializeField]
	private float speed;

	[SerializeField]
	private float MoveSpeed;

	[SerializeField]
	private Transform EndPos;

	[SerializeField]
	private Transform StartPos;

	public Action<bool> iSRun;

	public List<Run> AllHouse=new List<Run>();

	private float NowPos = 0;

	string[] AllHorsePositon=null;
	[SerializeField]
	private int IntTime = 500;
    public int time { get { return IntTime; } }
	public float AddDis=0;
	public bool IsTimeUp=false;
	public bool IsGet=true;
	public bool IsWait = false;
	public bool isPaiZhao = false;
	[TextArea(2,30)]
	public string ShowPosMessage;

	public UnityEvent OverEvent;

	public Vector3 RoadPos;
	private Vector3 endlineStart;

	private int Fristt;

	public Text ShowDisTime;
	public Transform markdis;
    [SerializeField]
    Transform MenFather;
    [SerializeField]
    List<MaMen> mapengmen = new List<MaMen>();

    [System.Serializable]
    public class MaMen
    {
        public Animator anim;
        public AudioSource source;
    }

	public void FristSet(int TimeGet)
	{
		//Debug.Log("sethsave****************************");
        //markdis.gameObject.SetActive(true);
		//Fristt = TimeGet-1;
		//if (!IsGet)
		//	WebSocketSimpet.GetWebSocketSimpet.SendToServer ();
		//Invoke ("SetLatePsotion",1.0f);
	}

	[SerializeField]
	private int LastMinute;

	void SetLatePsotion()
	{
		//transform.position -= new Vector3 ((20 - Fristt) * 20, 0, 0);
        //transform.Translate((20 - Fristt) * 5,0,0); 
        startspeed = startspeed + (20 - Fristt) * 50 * 0.0001f;

        if (Fristt < LastMinute)
        {
            for (int i = 0; i < AllAction.Count; i++)
            {
                AllHouse[i].UpdateNextPosition(AllAction[i].GetLastPos() * speedmax);
            }
            //endline.SetParent (transform);
        }
    }

    public AudioSource Music;
	public GameObject ClsoeButton;
	RunAction aa ;
    RunAction bb;


    int randomtime;
	void UpdateRunTime(int GetTime)
	{
        
        IntTime = GetTime;
        //Debug.Log(IntTime + "秒");
        if (IntTime < 60)
        {
            ClsoeButton.SetActive(true);
            
            ceb.Shouhui();
        }           
        else
        {
            ClsoeButton.SetActive(false);
            
            ceb.Dakai();
        }

        if(IntTime == 25)
            ReStart();

        if (IntTime == 300)
        {
            randomtime = UnityEngine.Random.Range(25, 300);
            camera3D.cameraAnima.enabled = true;
            
        }

        if(IntTime == randomtime)
            StartUpdate.Instance.Start();

        if (IntTime <= 25) 
		{
            //if (!IsGet)
            //    WebSocketSimpet.GetWebSocketSimpet.SendToServer();
            if (AlloldAction.Count == 0)
            {
                markdis.gameObject.SetActive(true);
                EndTime_text.gameObject.SetActive(false);

                if (IntTime <= 20 && !IsTimeUp)
                {
                    markdis.gameObject.SetActive(true);
                }
            }
            else
            {
                markdis.gameObject.SetActive(false);
                
                if (IntTime == 20)
                {
                    //Music.Play();
                    AudioManager.Instance.Racing();
                    Openmen();
                    IsTimeUp = true;
                    
                    //CancelInvoke("CheckClose");
                    //InvokeRepeating("CheckClose",2,2);
                }
                if (IntTime <= 20 && !IsTimeUp)
                {
                    markdis.gameObject.SetActive(true);
                }
                if (IntTime <= 10 && IsTimeUp && AllAction.Count == 0)
                {
                    WebSocketSimpet.GetWebSocketSimpet.Realod();
                }
            }
            
            ShowDisTime.text = GetTime.ToString();
        } 
		else 
		{
            markdis.transform.gameObject.SetActive(false);
            camera3D.cameraAnima.enabled = true;
        }
		if (IsRunGo()) 
		{
			if (iSRun != null)
				iSRun.Invoke (true);
		} 
		else 
		{
			if (iSRun != null)
				iSRun.Invoke (false);
		}
		if (IntTime <= 1) 
		{
			
		}
//		if (IntTime > 30 && IntTime < 250)
//			Invoke ("ReStart",2.0f);
		//if (IntTime == 35)
		//	ReStart ();
        if (IntTime < LastMinute)
        {
            //endline.SetParent (transform);
        }
			
	}

	public void GameOver()
	{
		//Music.Pause ();
		PaoZhaoObj.SetActive (true);
		StartCoroutine ("StartJs");
//
        foreach (Run child in AllHouse)
			child.SetFree ();
		Time.timeScale = 0;
	}


	IEnumerator StartJs()
	{
		float i = 0;
		while (i < 60f) 
		{
			i ++;
			yield return 0;
		}
		//Music.Pause();
		Time.timeScale = 1;
		yield return 0;
	}


	public bool IsRunGo()
	{
		if (IsTimeUp && IsGet && IsWait&&!isPaiZhao)
			return true;
		else
			return false;
	}


	[SerializeField]
	private int speedmax=1;
	public string mc;
	public void SetRunLoop(string GetMC)
	{
        Debug.Log(GetMC + "名次----");
        AllAction.Clear ();
		mc = GetMC;
		
		string[] allpos = GetMC.Split (new char[1]{','});

		Dictionary<int,int> ALLMC = new Dictionary<int,int> ();
		for(int i=0;i<allpos.Length;i++)
		{
			ALLMC.Add (int.Parse (allpos [i]), i + 1);
		}
		Dictionary<int,int> bymc=ALLMC.OrderBy(o=>o.Key).ToDictionary(o=>o.Key,p=>p.Value);
		foreach(int child in bymc.Values)
		{
			 aa = new RunAction (child);
			AllAction.Add (aa);
			Debug.Log (child+"循序名次");
		}
        //		for(int i=0;i<allpos.Length;i++)
        //		{
        //			RunAction aa = new RunAction (int.Parse(allpos[i]),allpos);
        //			AllAction.Add (aa);
        //			Debug.Log (int.Parse(allpos[i])+"循序名次");
        //		}
        IsGet = true;
    }

    public void SetRunLoop_old(string GetMC)
    {
        Debug.Log(GetMC + "假名次----");
        AlloldAction.Clear();
        mc = GetMC;

        string[] allpos = GetMC.Split(new char[1] { ',' });

        Dictionary<int, int> ALLMC = new Dictionary<int, int>();
        for (int i = 0; i < allpos.Length; i++)
        {
            ALLMC.Add(int.Parse(allpos[i]), i + 1);
        }
        Dictionary<int, int> bymc = ALLMC.OrderBy(o => o.Key).ToDictionary(o => o.Key, p => p.Value);
        foreach (int child in bymc.Values)
        {
            aa = new RunAction(child);
            AlloldAction.Add(aa);
            Debug.Log(child + "循序名次");
        }
        IsGet = true;
    }


    public class RunAction
	{
		public List<float> MyPos=new List<float>();

		public int MyNub;

		public float GetPos(int Nub)
		{
			return MyPos [Nub];
		}
		public float GetLastPos()
		{
			return MyPos.Last();
		}


		public  RunAction(int nubMC)
		{
            MyNub = nubMC;
            int A = (-MyNub + 11) % 10;
            int B = (-MyNub + 12) % 10;
            int C = (-MyNub + 13) % 10;
            int D = (-MyNub + 14) % 10;
            int E = (-MyNub + 15) % 10;
            int F = (-MyNub + 16) % 10;
            int G = (-MyNub + 17) % 10;
            int H = (-MyNub + 18) % 10;
            int I = (-MyNub + 19) % 10;
            int J = (-MyNub + 20) % 10;
            MyPos.Add(A);
            MyPos.Add(B);
            MyPos.Add(C);
            MyPos.Add(D);
            MyPos.Add(E);
            MyPos.Add(F);
            MyPos.Add(G);
            MyPos.Add(H);
            MyPos.Add(I);
            MyPos.Add(J);
            MyPos.Add(-(MyNub * 5) + 51);
        }
	}

	public List <RunAction> AllAction = new List<RunAction> ();

    public List<RunAction> AlloldAction = new List<RunAction>();

    public float addspeed=3;
	[SerializeField]
	private Transform ScoreText;
    public Transform parent;
    public void CleanTitle()
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Destroy(parent.GetChild(i).gameObject);
        }
    }
    [SerializeField]
    Cebianlan ceb;

    [SerializeField]
    private XiaZhu_Model xiazhu;
    public void RealGameOver()
	{

        //Music.Stop ();
        AudioManager.Instance.Noracing();
        Closemen();
		IsTimeUp = false;
		Text[] allmc = ScoreText.GetComponentsInChildren<Text> ();
		//string[] strtext = WebSocketSimpet.GetWebSocketSimpet.GetMessageReceived.HorseOrder.Split(new char[] { ',' });
        string[] strtext = mc.Split(new char[] { ',' });

        for (int i = 0; i <strtext.Length; i++) 
		{
			allmc [i].text = strtext [i];
		}
		OverEvent.Invoke ();
        ceb.Dakai();
        CleanTitle();
        AllAction.Clear();
        AlloldAction.Clear();
        xiazhu.data.Clear();
        camera3D.cameraAnima.enabled = true;
        Invoke ("ReStart",2.0f);
        //CancelInvoke("CheckClose");
        //InvokeRepeating("CheckClose", 5.0f, 5.0f);
    }

	public float GetLastpos()
	{
		float getlast = 0;
		foreach (RunAction child in AllAction) 
		{
			if (child.MyNub == 1) {
				getlast = child.GetLastPos ();
				break;
			}
		}
	
		return getlast;
	}

    

	public Transform endline;
	void Move(int PosNub)
	{
        
		if (PosNub <= 25 && PosNub>21)
		//if (AllAction.Count == 0) {
        //        ReStart();
                //WebSocketSimpet.GetWebSocketSimpet.SendToServer ();
			return;
		//}
        //Debug.Log(PosNub+"Move");
        if (PosNub > 20)
            return;
        //if (PosNub == 20) 
        //{
        //	for (int i = 0; i <AllAction.Count; i++) 
        //    {
        //		AllHouse [i].UpdateFristPosition (AllAction[i].GetPos(0)*speedmax);
        //    }
        //}
        if (PosNub == 19)
        {
            for (int i = 0; i < AlloldAction.Count; i++)
            {
                AllHouse[i].UpdateFristPosition(AlloldAction[i].GetPos(7) * speedmax);
            }
        }
        if (PosNub == 18) 
		{
			for (int i = 0; i < AlloldAction.Count; i++) 
			{
				AllHouse [i].UpdateNextPosition (AlloldAction[i].GetPos(1)*speedmax);
			}
		}

		//if (PosNub == 17) 
		//{
		//	for (int i = 0; i <AllAction.Count; i++) 
		//	{
		//		AllHouse [i].UpdateNextPosition (AllAction[i].GetPos(6)*speedmax);
		//	}
		//}

		if (PosNub == 16) 
		{
			for (int i = 0; i < AlloldAction.Count; i++) 
			{
				AllHouse [i].UpdateNextPosition (AlloldAction[i].GetPos(2)*speedmax);
			}
		}

		if (PosNub == 15) 
		{
			for (int i = 0; i < AlloldAction.Count; i++) 
			{
				AllHouse [i].UpdateNextPosition (AlloldAction[i].GetPos(5)*speedmax);
			}
		}

		//if (PosNub == 14) 
		//{
		//	for (int i = 0; i <AllAction.Count; i++) 
		//	{
		//		AllHouse [i].UpdateNextPosition (AllAction[i].GetPos(3)*speedmax);
		//	}
		//}

		//if (PosNub == 13) 
		//{
		//	for (int i = 0; i <AllAction.Count; i++) 
		//	{
		//		AllHouse [i].UpdateNextPosition (AllAction[i].GetPos(9)*speedmax);
		//	}
		//}

		if (PosNub ==12) 
		{
			for (int i = 0; i <AllAction.Count; i++) 
			{
				AllHouse [i].UpdateNextPosition (AllAction[i].GetPos(9)*speedmax);
			}
		}
		//if (PosNub ==11) 
		//{
		//	for (int i = 0; i <AllAction.Count; i++) 
		//	{
		//		AllHouse [i].UpdateNextPosition (AllAction[i].GetPos(2)*speedmax);
		//	}
		//}

		//if (PosNub ==10) 
		//{
		//	for (int i = 0; i <AllAction.Count; i++) 
		//	{
		//		AllHouse [i].UpdateNextPosition (AllAction[i].GetPos(5)*speedmax);
		//	}
		//}

		//if (PosNub ==9) 
		//{
		//	for (int i = 0; i <AllAction.Count; i++) 
		//	{
		//		AllHouse [i].UpdateNextPosition (AllAction[i].GetPos(7)*speedmax);
		//	}
		//}
		if (PosNub ==8) 
		{
			for (int i = 0; i <AllAction.Count; i++) 
			{
				AllHouse [i].UpdateNextPosition (AllAction[i].GetPos(6)*speedmax);
			}
		}
		if (PosNub ==7) 
		{
			for (int i = 0; i <AllAction.Count; i++) 
			{
				AllHouse [i].UpdateNextPosition (AllAction[i].GetPos(3)*speedmax);
			}
		}

		if (PosNub ==6) 
		{
			for (int i = 0; i <AllAction.Count; i++) 
			{
				AllHouse [i].UpdateNextPosition (AllAction[i].GetPos(9)*speedmax);
			}
		}

		if (PosNub ==5) 
		{
			for (int i = 0; i <AllAction.Count; i++) 
			{
				AllHouse [i].UpdateNextPosition (AllAction[i].GetPos(6)* speedmax);
			}
		}

		if (PosNub ==4) 
		{
			for (int i = 0; i <AllAction.Count; i++) 
			{
				AllHouse [i].UpdateNextPosition (AllAction[i].GetPos(9)* speedmax * 2);
			}
            camera3D.lockSet(true);
            camera3D.follow_set(endline);
        }

		if (PosNub ==LastMinute) 
		{
			addspeed = 0.2f;
			float LSAST = GetLastpos ();
			Debug.Log (LSAST);
			for (int i = 0; i <AllAction.Count; i++) 
			{
				AllHouse [i].GetMyNub (AllAction[i].MyNub);
				AllHouse [i].UpdateLastPosition (AllAction[i].GetLastPos()*speedmax * 3);
			}
			//endline.SetParent (transform);
		}
	}



    [SerializeField]
    Text EndTime_text;
    int EndTime;
    float fontsize = 200;
    int endtime
    {
        get { return EndTime; }
        set
        {
            if (EndTime != value)
            {
                StopCoroutine("endtimepar");
                EndTime = value;
                EndTime_text.fontSize = 100;
                EndTime_text.color = new Color(EndTime_text.color.r, EndTime_text.color.g, EndTime_text.color.b, 0);
                EndTime_text.text = endtime.ToString();
                StartCoroutine("endtimepar");
            }

        }
    }

    void EndTimeProFuntion(int time)
    {
        EndTime_text.gameObject.SetActive(true);
        endtime = time;
    }

    void EndTimeReset(int time)
    {
        EndTime_text.gameObject.SetActive(false);
    }

    [SerializeField]
    GameObject miaoaudio;
    IEnumerator endtimepar()
    {
        Instantiate(miaoaudio);
        while (EndTime_text.color.a != 1)
        {
            EndTime_text.color = new Color(EndTime_text.color.r, EndTime_text.color.g, EndTime_text.color.b, Mathf.Lerp(EndTime_text.color.a, 1, Time.deltaTime * 2));
            EndTime_text.fontSize = (int)Mathf.Lerp(EndTime_text.fontSize, 200, Time.deltaTime * 2);
            yield return null;
        }
    }




    public void SetPos(string AllPosition)
	{
		ShowPosMessage = AllPosition;
		AllPosition= AllPosition.Replace("[[", string.Empty).Replace("],[","*");
		AllHorsePositon=AllPosition.Split(new char[1]{'*'});	
	}


    void CameraStart()
    {
        camera3D.cameraAnima.enabled = false;
        camera3D.Camera_setpos();
        camera3D.follow_set(transform);
        camera3D.lockSet(false);
    }


    public class aLLdATA
	{
		public string[] aaa;
	}
		
	public void AddRun(Run GetRun)
	{
		AllHouse.Add (GetRun);
	}
		
	void Awake()
	{
		GetHouseMove = this;
	}
		
	public Transform HorseRun;
	void Start()
	{

        WebSocketSimpet.GetWebSocketSimpet.HorsesEvent.AddLintner (SetPos);
        WebSocketSimpet.GetWebSocketSimpet.toRun_Delegate += CameraStart;
        WebSocketSimpet.GetWebSocketSimpet.RunTime.AddLintner(Move);
        WebSocketSimpet.GetWebSocketSimpet.EndTime.AddLintner(EndTimeProFuntion);
        WebSocketSimpet.GetWebSocketSimpet.resetEndTime.AddLintner(EndTimeReset);
        WebSocketSimpet.GetWebSocketSimpet.RunTime.AddLintner (UpdateRunTime);
		Run[] all = HorseRun.GetComponentsInChildren<Run>();
		foreach (Run child in all)
			AllHouse.Add (child);

        foreach (Transform i in MenFather)
        {
            MaMen men = new MaMen();
            men.anim  = i.GetComponent<Animator>();
            men.source = i.GetComponent<AudioSource>();
            mapengmen.Add(men);
        }
        //endlineStart = endline.position;
        AudioManager.Instance.Noracing();
        ceb.Dakai();
        camera3D.Startpos_set();

        
        Invoke ("Iswaitgo",0);
		InvokeRepeating ("CheckClose",5.0f, 5.0f);

	}

    public void Openmen()
    {
        foreach (MaMen i in mapengmen)
        {
            i.anim.Play("open");
            i.source.Play();
        }
    }
    public void Closemen()
    {
        foreach (MaMen i in mapengmen)
        {
            i.anim.Play("close");
        }
    }


	public Text timeget;
	private string LastTime;
	public void CheckClose()
	{
        //Debug.Log(timeget.text + "           " + LastTime);
        if (timeget.text == LastTime)
        {
            WebSocketSimpet.GetWebSocketSimpet.Realod();

        }
        else
        {
            LastTime = timeget.text;
        }
        
    }


	private void Iswaitgo()
	{
		IsWait = true;
	}

	void OnDisable()
	{
		WebSocketSimpet.GetWebSocketSimpet.HorsesEvent.RemoveLintner (SetPos);
	} 

    [SerializeField]
    private float startspeed = 0f;
    private float speedaddtimes = 0f;
    private float logx = -2f;
    private float a = 1.05f;
	void Update () 
	{
        
        //transform.Translate(Vector3.forward * startspeed * Time.deltaTime);
        if (IsRunGo ()) {
            //speedaddtimes += 0.005f;
            startspeed = Mathf.Log(logx, a);
            startspeed = 1/(1+Mathf.Exp(-0.5f*logx))*40f;
            logx += Time.deltaTime;
            //startspeed += speedaddtimes;
            startspeed = startspeed >= MoveSpeed ? MoveSpeed : startspeed;
			//transform.position = Vector3.Lerp (transform.position, EndPos.position, startspeed * Time.deltaTime);
            transform.Translate(Vector3.forward * startspeed * Time.deltaTime);
            camera3D.Camera_Move();
        }
        //startspeed = 1 / (1 + Mathf.Exp(-0.5f * logx)) * 60f;
        //Debug.Log(startspeed+"    "+logx);
        //logx += Time.deltaTime;

       
    }
		

	public Transform endlinefather;
	public void ReStart()
	{
        Closemen();
        transform.position = StartPos.position;
		foreach (Run child in AllHouse)
			child.ReStart ();
		AllHorsePositon=null;
		ShowPosMessage = string.Empty;
		//endline.position = endlineStart;
		//endline.SetParent (endlinefather);
		//IsGet = false;
		mc = string.Empty;
        startspeed = 0f;
        speedaddtimes = 0f;
        logx = -2f;
        markdis.gameObject.SetActive(false);
		addspeed = 3;
		PaoZhaoObj.SetActive (false);
		IsTimeUp = false;
	}
		
	public GameObject PaoZhaoObj;
	public void ShowScore()
	{
		Debug.Log (WebSocketSimpet.GetWebSocketSimpet.GetMessageReceived.HorseOrder);
    }


#if UNITY_ANDROID && UNITY_EDITOR

#endif

    void OnApplicationFocus(bool isFocus)
    {
#if !UNITY_EDITOR
        if (isFocus)
        {
        }
        else
        {
            //
            //WebSocketSimpet.GetWebSocketSimpet.Realod();
            //Debug.Log("离开游戏 激活推送");  //  返回游戏的时候触发     执行顺序 1
        }
#endif

    }


    void OnApplicationPause(bool isPause)
    {
        if (isPause)
        {
            //Debug.Log("游戏暂停 一切停止");  // 缩到桌面的时候触发
        }
        else
        {
            WebSocketSimpet.GetWebSocketSimpet.Realod();
            //Debug.Log("游戏开始  万物生机");  //回到游戏的时候触发 最晚
        }
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

}



