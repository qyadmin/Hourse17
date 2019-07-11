using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReconnectMast : MonoBehaviour
{
    private Text m_text;
    public GameObject Reconnect;
	// Use this for initialization
	void Start ()
    {
        m_text = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (m_text.text==string.Empty)
        {
            Reconnect.SetActive(true);
        }
        else
        {
            Reconnect.SetActive(false);
        }
	}
}
