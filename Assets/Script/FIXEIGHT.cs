using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FIXEIGHT : MonoBehaviour {


	[SerializeField]
	private int SingleLine;
	[SerializeField]
	private int WITH;

	private Text mYsELF;
	private string str;
	private int lineWith;
	void Start()
	{
		mYsELF = GetComponent<Text> ();
		lineWith = mYsELF.fontSize;
	}

	void Update () 
	{
		str = mYsELF.text;
		int heght =(str.Length / SingleLine) * lineWith * 2;
		heght = heght > 30 ? heght : 30;
		mYsELF.rectTransform.sizeDelta=new Vector2 (WITH,heght);
	}
}
