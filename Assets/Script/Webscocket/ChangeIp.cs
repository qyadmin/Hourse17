using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ChangeIp : MonoBehaviour
{
    public Text iptext;
    public UnityEvent M_start;

    // Use this for initialization
    private void OnEnable()
    {
        M_start.Invoke();
    }
    void Start ()
    {
        //InvokeRepeating("RepeatUpdate", 0, 5);
    }
	public void SetIp()
    {
        WabData.m_ipaddress =iptext.text ;
        //WabData.m_ipaddress = "39.107.99.82";
    }


	// Update is called once per frame
	void Update ()
    {
        
    }
    void RepeatUpdate()
    {

    }
}
