using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;  
using System.ComponentModel;  
using System.Text;  

public class ShowOrHit : MonoBehaviour {

    public void Show(GameObject Obj)
    {
        Obj.SetActive(true);
    }

    public void Hide(GameObject Obj)
    {
        Obj.SetActive(false);
    }

    public void SettextNull(Text GetText)
    {
        GetText.text = string.Empty;
    }

    public void SettextInPutNull(InputField GetText)
    {
        GetText.text = string.Empty;
    }

	//mm

	public void StopYzm(GameObject Obj)
	{
		Obj.SetActive(false);
		StopCoroutine("Hit");
		if(ss!=null)
		ss.text = "获取验证码";
	}

    public void ShowWaitHit(GameObject Obj)
    {
        Obj.SetActive(true);
		StartCoroutine("Hit",Obj);
    }
		
    private int Nub;
    IEnumerator Hit(GameObject obj)
    {
        Nub = 60;
        while (Nub > 0)
        {
            Nub--;
            if (ss != null)
                ss.text = Nub.ToString() + "s重新获取验证码";
            yield return new WaitForSeconds(1);
        }
        obj.SetActive(false);
        if (ss != null)
            ss.text = "获取验证码";
    }
    private Text ss;
    public void ShowTime(Text aaa)
    {
        ss = aaa;
    }
	//mm

	//jymm
	public void ShowWaitHitjy(GameObject Obj)
	{
		Obj.SetActive(true);
		StartCoroutine(Hitjy(Obj));
	}

	private int jyNub;
	IEnumerator Hitjy(GameObject obj)
	{
		jyNub = 60;
		while (jyNub > 0)
		{
			jyNub--;
			if (sss != null)
				sss.text = jyNub.ToString() + "s重新获取验证码";
			yield return new WaitForSeconds(1);
		}
		obj.SetActive(false);
		if (sss != null)
			sss.text = "获取验证码";
	}
	private Text sss;
	public void ShowTimejy(Text aaa)
	{
		sss = aaa;
	}
	//jymm



    public void ShowTimeFive(Text ttt)
    {
        StartCoroutine(hitoh(ttt));
    }

    IEnumerator hitoh(Text ttt)
    {
        yield return new WaitForSeconds(5);
        ttt.text = string.Empty;
    }


    public void SetJiFen(Text ShowJiFen)
    {
        ShowJiFen.text = Static.Instance.GetValue("jifen");
    }

    private bool open = true;
    public Sprite Open, close;
    public Image myself;
    public void FLMUSIC(AudioSource AA)
    {
		open = Static.Instance.MusicSwich;
        open = !open;
		Static.Instance.MusicSwich = open;
        AA.enabled = open ? true : false;
        myself.GetComponent<Image>().sprite = open ? Open : close;
    }
	public void SoundAwake(AudioSource AA)
	{
		open = Static.Instance.MusicSwich;
		AA.enabled = open ? true : false;
		myself.GetComponent<Image>().sprite = open ? Open : close;
	}


    public void SuoXiao(Transform aaa)
    {
        aaa.localScale = new Vector3(0, 0, 0);
    }

	public void FangDa(Transform aaa)
	{
		aaa.localScale = new Vector3(1, 1, 1);
	}

    public void ChangeText(Text showyzm)
    {
        StopAllCoroutines();
        showyzm.text = "获取验证码";
    }

	[SerializeField]
	private Text PageNubText;
	public void PageChange(int PageNub)
	{
		if (aLLlIST.transform.childCount <= 0)
			return;
		lASTpage = PageNubText.text;
		Nub = int.Parse (PageNubText.text)+PageNub;
		Nub = Nub > 0 ? Nub : 1;
		PageNubText.text = Nub.ToString ();
	}

	private string lASTpage="1";

	public void UpdatePageFL()
	{
		PageNubText.text = lASTpage;
	}
 

    [SerializeField]
	private Text PageNubShowText;
	public void ShowPAGE()
	{
		PageNubShowText.text = PageNubText.text;
	}
//	public void SetALLtYPE(Dropdown gETD)
//	{
//		GoSearch = true;
//		gETD.value = 0;
//	}


	public Text TypeValue;

	public void SetPageStart(Dropdown getD)
	{
		TypeValue.text = GetTyPEid(getD.value.ToString());
		PageNubText.text = "1";
	}

	Dictionary<string,string> DropList=new Dictionary<string,string>();
	public Dictionary<string ,string> TypeMessage=new Dictionary<string, string>();//ID,NAME
	string GetTyPEid(string Name)
	{
		string id = string.Empty;
		TypeMessage.TryGetValue (Name, out id);
		return id;
	}

	public void SetTypeMessage()
	{
		
	}
		
	public Dropdown GetD;
	public void AddOpelns(Transform AllType)
	{
		TypeMessage.Clear ();
		DropList.Clear ();
		int i = 0;

		foreach (Transform child in AllType) 
		{
			TypeMessage.Add (i.ToString(), child.GetChild (0).name);
			if (DropList.ContainsKey (child.GetChild (1).name))
				DropList.Remove (child.GetChild (1).name);
			DropList.Add (child.GetChild (1).name,child.GetChild (0).name);
			i++;
		}
		GetD.options.Clear ();
	
		List<string> allname = new List<string> ();
		foreach (string child in DropList.Keys)
			allname.Add (child);
		GetD.AddOptions (allname);

	}

	public void ClearFather(Transform AllType)
	{
		foreach (Transform child in AllType)
		{
			Destroy(child.gameObject);
		}

	}
		
	[SerializeField]
	HttpModel listSHop;
	public void Search()
	{
		if (aLLlIST.transform.childCount > 0)
			listSHop.Get ();
	}

	public Transform aLLlIST;
	public void SetAll()
	{
		foreach (Transform child in aLLlIST)
			Destroy (child.gameObject);
	}

	public void ShowFail(Text text)
	{
		text.text = "获取数据失败!";
	}


	[SerializeField]
	private Text Now;
	[SerializeField]
	private GameObject XiaZhuMark,mark;
	public int SatrtTime;
	public int EndTime;
    public Cebianlan cebianlan;
    [HideInInspector]
    public bool isFengPan = true;
    public void CheckFP()
	{
        string S_time = Static.Instance.GetValue("config_stop_start");
        string E_time = Static.Instance.GetValue("config_stop_end");
        string[] S_times = S_time.Split(':');
        string[] E_times = E_time.Split(':');

        SatrtTime = int.Parse(S_times[0]);
        EndTime = int.Parse(E_times[0]);

        DateTime ts = GetTime (Convert.ToInt64(Static.Instance.GetValue("system_time")));
		Debug.Log (ts.Day+"天"+ts.Hour+"小时"+ts.Minute+"分钟"+ts.Second);
		if (ts.Hour > SatrtTime && ts.Hour < EndTime) 
		{
            
			XiaZhuMark.SetActive (true);
			Debug.Log ("封盘时间");
            isFengPan = true;
        } 
		else 
		{
            
            XiaZhuMark.SetActive (false);
			mark.SetActive (false);
            isFengPan = false;
            //Debug.Log(SatrtTime+"   "+ EndTime);
        }
	}
    private string DateDiff(DateTime DateTime1, DateTime DateTime2)
	{
		string dateDiff = null;
		TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
		TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
		TimeSpan ts = ts1.Subtract(ts2).Duration();
		dateDiff = ts.Days.ToString()+"天"+ ts.Hours.ToString()+"小时"+ ts.Minutes.ToString()+"分钟"+ ts.Seconds.ToString()+"秒";
		return dateDiff;
	}


	public DateTime GetTime(long unixTimeStamp)
	{
		System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
		DateTime dt = startTime.AddSeconds(unixTimeStamp);
		return dt;
	}

    [SerializeField]
    HttpModel kefu;
    private void Start()
    {
        if (kefu)
            InvokeRepeating("KefuGet",2,900);
    }
    void KefuGet()
    {
       
        kefu.Get();

    }
}
