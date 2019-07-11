using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public  class Con_scorllBar : MonoBehaviour
{
    public Transform m_transform;
    public float aa;
    void  Start ()
    {
        transform.localPosition = new Vector3(-520, 0, 0);
	}
    private void Update()
    {
        if (transform.localPosition.x != 0)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, m_transform.localPosition, aa * Time.deltaTime);
        }
    }
}
