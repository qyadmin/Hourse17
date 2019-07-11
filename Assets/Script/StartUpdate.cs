using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class StartUpdate : MonoBehaviour
{
    public static StartUpdate Instance;
	public UnityEvent StartEvent;
    public GameObject Uni;

    // Use this for initialization

    private void Awake()
    {
        Instance = this;
    }

    public void Start ()
    {
        
        //Uni.SetActive(true);
        //Uni.SetActive(false);
        StartEvent.Invoke ();
        
	}
}
