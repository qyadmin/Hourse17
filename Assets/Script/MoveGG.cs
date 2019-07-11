using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoveGG : MonoBehaviour {

	[SerializeField]
	private Text gg;
	private Vector3 StartPos;
	public int leg;
	public int AddDis=310;
	void Start () {
		StartPos = gg.transform.localPosition;
	}
		
	public void SeTLEBGH()
	{
		leg = gg.text.Length;
	}

	void Update()
	{
		gg.transform.Translate (-200*Time.deltaTime,0,0);
		if (gg.transform.position.x <-AddDis-leg/10) 
		{
			gg.transform.localPosition = StartPos;
		}
	}
		

}
