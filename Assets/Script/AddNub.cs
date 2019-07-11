using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AddNub : MonoBehaviour {


	void ClearColor(Color GetColor)
	{
		if(GetComponent<Image> ())
		GetComponent<Image> ().color = GetColor;
	}

	void Start()
	{
		ScoreManager.GetScoreManager.ClearColor.AddLintner (ClearColor);
	}


	public void AddNubGo(int nub)
	{
		ScoreManager.GetScoreManager.AddScore (nub);
	}

	public void AddDaXiao(int nub)
	{
		ScoreManager.GetScoreManager.ClearDxColor ();
		if (ScoreManager.GetScoreManager.SetDaXiao (nub))
			GetComponent<Image> ().color = ScoreManager.GetScoreManager.D_COLOR;
		else
			GetComponent<Image> ().color = new Color(1,1,1,1);
		
	}

	public void AddMC(int nub)
	{
		if (ScoreManager.GetScoreManager.AddMingCi (nub))
			GetComponent<Image> ().color = ScoreManager.GetScoreManager.M_COLOR;
		else
			GetComponent<Image> ().color = new Color(1,1,1,1);

		Static.Instance.AddValue ("strRankings",ScoreManager.GetScoreManager.Get_MCJsonList());
	}

	public void AddMH(int nub)
	{
		if (ScoreManager.GetScoreManager.AddBianHao (nub))
			GetComponent<Image> ().color = ScoreManager.GetScoreManager.M_COLOR;
		else
			GetComponent<Image> ().color = new Color(1,1,1,1);

		Static.Instance.AddValue ("strHorses",ScoreManager.GetScoreManager.Get_BHJsonList());
	}
		
}
