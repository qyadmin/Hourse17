using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parPlay : MonoBehaviour {

    ParticleSystem par;

	// Use this for initialization
	void Awake () {
        par = this.gameObject.GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!par.isPlaying)
            Destroy(this.gameObject);
	}
}
