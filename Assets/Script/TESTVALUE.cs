using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTVALUE : MonoBehaviour {

	public string gisoracle="NIHAO";

	void Start () 
	{
		Debug.Log(this.GetType ().GetField ("gisoracle").GetValue (this).ToString ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
