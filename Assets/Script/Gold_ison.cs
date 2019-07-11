using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold_ison : MonoBehaviour {

    [SerializeField]
    GameObject gold;

    private void OnEnable()
    {

        Static.Instance.planNamber++;
        if (Static.Instance.planNamber != 0)
            gold.SetActive(false);
    }

    private void OnDisable()
    {
        Static.Instance.planNamber--;
        if (Static.Instance.planNamber <= 0)
        {
            gold.SetActive(true);
            Static.Instance.planNamber = 0;
        }
    }
}
