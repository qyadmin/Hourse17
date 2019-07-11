using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
public enum Dx
{
	none,
	Da,
	Xiao,
	Dan,
	Shuang
}
public class ScoreManager : MonoBehaviour {

	public static ScoreManager GetScoreManager;


	void Awake()
	{
		GetScoreManager = this;
	}

	public Color M_COLOR;
	public Color D_COLOR;


	public int AllScore=0;
	public int SingleScore=0;
	public int MaHao=1;
	public int Mingci=1;
	public bool DaXiao=false;

	public Text SinglrScoretext;
	public Text AllScoretext;
	public Text ZhuShu;

	List<int> SaveBianhao=new List<int>();
	public List<int> GetBianHao{get { return SaveBianhao;}}
	public string Get_BHJsonList()
	{
		string str = string.Empty;
		for (int i = 0; i < SaveBianhao.Count; i++) 
		{
			if (i != 0)
				str += ",";
			str +=SaveBianhao [i].ToString ();
		}
		return str;	
	}

	public string Get_MCJsonList()
	{
		string str = string.Empty;
		for (int i = 0; i < Savemingci.Count; i++) 
		{
			if (i != 0)
				str += ",";
			str +=Savemingci [i].ToString ();
		}
		return str;	
	}

	List<int> Savemingci=new List<int>();

	public Message<Color> ClearColor=new Message<Color>();

	//编号
	public bool AddBianHao(int GetNub)
	{
		if (DaXiao)
			return false;
		if (SaveBianhao.Contains (GetNub)) {
			SaveBianhao.Remove (GetNub);
			return false;
		} 
		else 
		{
			if (SaveBianhao.Count >= 9)
				return false;
			else {
				SaveBianhao.Add (GetNub);
				return true;
			}
		}
	}

	//名词
	public bool AddMingCi(int GetNub)
	{
		Static.Instance.AddValue ("playType", (System.Convert.ToInt32 (DaXiao) + 1).ToString ());
		if (Savemingci.Contains (GetNub)) {
			Savemingci.Remove (GetNub);
			return false;
		} 
		else 
		{
			if (Savemingci.Count >= 9)
				return false;
			else {
				Savemingci.Add (GetNub);
				return true;
			}
		}
	}

	//大小
	public Image[] aLLiMAGE;
	public void ClearDxColor()
	{
		foreach (Image CHILD in aLLiMAGE)
			CHILD.GetComponent<Image> ().color = new Color (1,1,1,1);
	}
	public Dx MyDx=Dx.none;
	public bool SetDaXiao(int Getdx)
	{
		if (SaveBianhao.Count > 0) {
			return false;
		}
		if (MyDx == (Dx)Dx.none) 
		{
			MyDx = (Dx)Getdx;
			DaXiao = true;
		} 
		else 
		{
			if ((Dx)Getdx == MyDx) 
			{
				MyDx = Dx.none;
				DaXiao = false;
			} 
			else 
			{
				MyDx = (Dx)Getdx;
				DaXiao = true;
			}
		}

		if(DaXiao)
			Static.Instance.AddValue ("typeHorses",Getdx.ToString());
		else
			Static.Instance.AddValue ("typeHorses","-1");
		Static.Instance.AddValue ("playType",(System.Convert.ToInt32(DaXiao)+1).ToString());
		return DaXiao;
	}
		
	public void ClearALL()
	{
		DaXiao = false;
		SaveBianhao.Clear ();
		Savemingci.Clear ();
		Static.Instance.AddValue ("strHorses","");
		Static.Instance.AddValue ("strRankings","");
		Static.Instance.AddValue ("playType","0");
		Static.Instance.AddValue ("typeHorses","0");
		ClearDxColor ();
		ClearColor.Send (new Color(1,1,1,1));
		SinglrScoretext.text =string.Empty;
		AllScoretext.text = string.Empty;
		AllScore = 0;
		SingleScore = 0;
	}


	public void AddScore(int Nub)
	{
		SingleScore += Nub;
		SingleScore = SingleScore > 0 ? SingleScore : 0;
	}
		
	// Update is called once per frame
	void Update () 
	{
		MaHao = SaveBianhao.Count>0?SaveBianhao.Count:1;
		Mingci = Savemingci.Count>0?Savemingci.Count:1;

		AllScore = SingleScore * MaHao * Mingci;

		SinglrScoretext.text = SingleScore.ToString();
		AllScoretext.text = AllScore.ToString();
		ZhuShu.text = (MaHao * Mingci).ToString ();
	}
}
