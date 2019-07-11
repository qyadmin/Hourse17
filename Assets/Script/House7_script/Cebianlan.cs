using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cebianlan : MonoBehaviour
{
    private Animator mAni;
    [SerializeField]
    private bool m_bool = false;
    [SerializeField]
    private XiaZhu_Model GetXiaZhu_Model;
    public Sprite image1;
    public Sprite image2;
    private GameObject ObjectChangeimage;

    public bool m_switch =false;
	// Use this for initialization
	void Awake ()
    {
        ObjectChangeimage = transform.GetChild(0).gameObject;
        mAni = GetComponentInChildren<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
       
	}

    public void OpenTitle()
    {

            if (!m_bool)
            {
                //Debug.Log("dakaile......");
                ObjectChangeimage.GetComponent<Image>().sprite = image1;
                mAni.Play("LableAni");
                m_bool = true;
                GetXiaZhu_Model.CanSend = true;
            }
            else if (m_bool)
            {
                //Debug.Log("guanbile......");
                ObjectChangeimage.GetComponent<Image>().sprite = image2;
                mAni.Play("LableAni2");
                m_bool = false;
                GetXiaZhu_Model.CanSend = false;
            }
        
    }
    [SerializeField]
    Button swich;
    public void Shouhui()
    {
        //Debug.Log("guanbile......");
        if (!m_switch)
            return;
        swich.interactable = false;
        ObjectChangeimage.GetComponent<Image>().sprite = image2;
        mAni.Play("LableAni2");
        m_bool = false;
        GetXiaZhu_Model.CanSend = false;
        m_switch = false;
        Debug.Log("Shouhuizhixing......");
    }
    public void Dakai()
    {
        //Debug.Log("dakaile......");
        if (m_switch)
            return;
        swich.interactable = true;
        ObjectChangeimage.GetComponent<Image>().sprite = image1;
        mAni.Play("LableAni");
        m_bool = true;
        GetXiaZhu_Model.CanSend = true;
        m_switch = true;
    }
}
