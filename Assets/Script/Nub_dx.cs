using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Nub_dx : MonoBehaviour {


	public Text text;
	public void SetChange(string Nub)
	{
		if (Nub == "1")
			text.text = "大";
		if (Nub == "2")
			text.text = "小";
		if (Nub == "3")
			text.text = "单";
		if (Nub == "4")
			text.text = "双";
	}

}
