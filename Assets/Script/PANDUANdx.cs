using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PANDUANdx : MonoBehaviour {

	public Transform Showerror;
	public Text showerror;
	public HttpModel http;
	public Text money;
	public Text ChsoeMoney;
	public void NULLpp()
	{
		if (float.Parse (money.text) < float.Parse (ChsoeMoney.text))
		{
			Showerror.localScale = new Vector3 (1,1,1);
			showerror.text = "下注金额不足！";
			return;
		}
		if(!ScoreManager.GetScoreManager.DaXiao&&ScoreManager.GetScoreManager.GetBianHao.Count<=0)
		{
			Showerror.localScale = new Vector3 (1,1,1);
			showerror.text = "选择马号或者大小单双！";
			return;
		}

		http.Get ();
	}
}
