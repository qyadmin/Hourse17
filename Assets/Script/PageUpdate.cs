using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PageUpdate : MonoBehaviour {


	[SerializeField]
	private Text PageNub;

	public void PageChange(int Nub)
	{
		Debug.Log (PageShow.text.Substring(0,1));
		string[] page = PageShow.text.Split (new char[1]{'/'});
		int a = int.Parse(page[0]) + Nub;
		a = a >= 0 ? a : 0;
		PageNub.text = a.ToString ();
	}

	[SerializeField]
	private Text PageShow;
	public void UodatePage()
	{
		//PageShow.text = PageNub.text;
	}
}
