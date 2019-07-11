using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miaoPerfrab : MonoBehaviour {

    AudioSource audio;

    // Update is called once per frame
    private void Awake()
    {
        audio = this.GetComponent<AudioSource>();
    }

    void Update () {
        if (!audio.isPlaying)
            Destroy(this.gameObject);
	}
}
