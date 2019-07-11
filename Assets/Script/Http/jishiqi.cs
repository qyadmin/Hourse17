using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class jishiqi : MonoBehaviour 
{
    
	public HttpModel ObjModel;
	void Start()
	{
        
    }
	public void StartJishi()
	{
		StartCoroutine (TimeGo1());
		StartCoroutine (TimeGo2());
		StartCoroutine (TimeGo3());
	}
	IEnumerator TimeGo1()
	{
		yield return new WaitForSeconds (120f);
		ObjModel.Get ();
	}
	IEnumerator TimeGo2()
	{
		yield return new WaitForSeconds (180f);
		ObjModel.Get ();
	}
	IEnumerator TimeGo3()
	{
		yield return new WaitForSeconds (300f);
		ObjModel.Get ();
	}
}
