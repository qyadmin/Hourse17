using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System;

public class Run : MonoBehaviour
{

    public bool IsRun = false;
    //[SerializeField]
    //private Sprite[] RunAnimationIamge;
    [SerializeField]
    private float LoopTime;
    private Image MyImage;

    private Vector3 StartPos;
    //private Sprite StartImage;
    private bool IsStop;

    [System.Serializable]
    public class Hourse
    {
        GameObject Hourseobj;
        public Animator hourseAnim;
        public Animator playerAnim;

        public void start()
        {

        }
        void idelTime()
        {
            hourseAnim.SetBool("start", false);
            playerAnim.SetBool("start", false);
            int i = Random.Range(1, 3);
            hourseAnim.SetInteger("idel", i);
            playerAnim.SetInteger("idel", i);
        }
        void runTime(Run hourse)
        {
            hourseAnim.speed = 1 + ((10 - hourse.Mynub + 2) * 0.01f) * 5f;

            hourseAnim.SetInteger("idel", 0);
            playerAnim.SetInteger("idel", 0);
            hourseAnim.SetBool("start", true);
            playerAnim.SetBool("start", true);
        }
        public void AnimContral(Run hourse)
        {
            if (hourse.RunState)
                runTime(hourse);
            else
                idelTime();

        }

    }
    [SerializeField]
    Hourse obj = new Hourse();

    public void ReStart()
    {
        Mynub = 11;
        IsFree = false;
        //GetComponent<Image> ().sprite = StartImage;
        NowPos = StartPos.z;
        transform.localPosition = StartPos;
        speed = 0.07f;
        warmpos.localPosition = transform.localPosition;
    }
    // Use this for initialization
    void Start()
    {
        warmpos.localPosition = transform.localPosition;
        //StartImage = GetComponent<Image> ().sprite;
        //HouseMove.GetHouseMove.AddRun (this);
        HouseMove.GetHouseMove.iSRun += StartRun;
        MyImage = GetComponent<Image>();
        StartPos = transform.localPosition;

        //StartCoroutine ("RunGo");
    }

    [HideInInspector]
    public bool RunState = false;

    private void StartRun(bool GetValue)
    {
        RunState = GetValue;
    }

    //IEnumerator RunGo()
    //{
    //	int i = 0;
    //	while (true) 
    //	{
    //		if (!RunState) 
    //		{
    //			yield return 0;
    //			continue;
    //		}
    //		if (i > RunAnimationIamge.Length - 1)
    //			i = 0;
    //		MyImage.sprite = RunAnimationIamge [i];
    //		yield return new WaitForSeconds (LoopTime);
    //		i++;
    //	}
    //	yield return 0;
    //}
    private float NowPos;
    public void UpdatePosition(float GetNowPos)
    {
        NowPos = GetNowPos;
    }

    public Transform warmpos;


    public void UpdateFristPosition(float GetNowPos)
    {
        warmpos.localPosition = StartPos + new Vector3(0, 0, GetNowPos);
    }

    public void UpdateNextPosition(float GetNowPos)
    {
        warmpos.localPosition = StartPos + new Vector3(0, 0, GetNowPos);
    }
    public void UpdateLastPosition(float GetNowPos)
    {
        warmpos.localPosition = new Vector3(StartPos.x, StartPos.y, GetNowPos);
        //warmpos.SetParent (HouseMove.GetHouseMove.EndLine);
    }

    public int Mynub = 11;
    public void GetMyNub(int GetNub)
    {
        Mynub = GetNub;
    }
    private float speed = 0.07f;
    private bool IsFree = false;
    public void SetFree()
    {
        IsFree = true;
    }

    public void StopAnamtion()
    {
        //LoopTime = 1.5f;
        //IsStop = true;
        //StopCoroutine ("RunGo");
    }

    public void StartAnamtion()
    {
        //LoopTime = 0.05f;
        //IsStop = false;
        //StartCoroutine ("RunGo");
    }

    void Update()
    {
        obj.AnimContral(this);
        if (RunState)
        {
            if (IsFree)
            {
                transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0, 0, 50), UnityEngine.Time.deltaTime * speed * HouseMove.GetHouseMove.addspeed);
                if (Mynub == 10)
                {
                    if (transform.position.z >= HouseMove.GetHouseMove.endline.position.z)
                        HouseMove.GetHouseMove.RealGameOver();
                }
            }
            else
            {
                speed += (10 - Mynub + 1) * 0.0001f;
                transform.position = Vector3.Lerp(transform.position, warmpos.position, UnityEngine.Time.deltaTime * speed * HouseMove.GetHouseMove.addspeed);
                if (Mynub == 1)
                {
                    if (transform.position.z >= HouseMove.GetHouseMove.endline.position.z)
                        HouseMove.GetHouseMove.GameOver();
                }
            }
        }
    }
}
