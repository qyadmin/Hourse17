using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System;

public class ThreadingMessage
{
	public  static Action Begin;
}
	
public class LoopControl : MonoBehaviour {
	
	Thread athread;
	// Use this for initialization
	void Start () 
	{
		ThreadingMessage.Begin += StartGo;
		athread = new Thread(new ThreadStart(goThread));
		athread.IsBackground = true;
		athread.Start();
	}
	private bool IsStop;
		
	public void StartGo()
	{
		Time.timeScale = 1;
	}
		
	public void StartThreading()
	{
		IsStop = true;
	}
		
	void goThread()
	{
		while (true)
		{
			if (IsStop) {
				Thread.Sleep (1000);
				ThreadingMessage.Begin.Invoke ();
				Debug.Log ("打开了！");
			}
			IsStop = false;
		}
	}
}
