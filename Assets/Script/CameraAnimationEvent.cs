using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraAnimationEvent : MonoBehaviour {

    [SerializeField]
    Image Plan;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void CameraAnimationEvnet()
    {
        StopAllCoroutines();
        IEnumerator ie = animationEvent();
        StartCoroutine(ie);
    }

    IEnumerator animationEvent()
    {
        float a = Plan.color.a;
        while (a != 1)
        {
            Plan.color = new Color(Plan.color.r,Plan.color.g,Plan.color.b, a);
            a += Time.deltaTime * 1.2f;
            if (a >= 1)
                a = 1;
            yield return null;
        }
        
        while (a != 0)
        {
            Plan.color = new Color(Plan.color.r, Plan.color.g, Plan.color.b, a);
            a -= Time.deltaTime * 0.5f;
            if (a <= 0)
                a = 0;
            yield return null;
        }
    }

}
