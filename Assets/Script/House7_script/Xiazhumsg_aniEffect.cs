using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xiazhumsg_aniEffect : MonoBehaviour
{
    private Animator EffAni;
	// Use this for initialization
	void Start ()
    {
        EffAni.Play("mask");
        Debug.Log("Effectani");
    }
    private void OnEnable()
    {
        EffAni = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
