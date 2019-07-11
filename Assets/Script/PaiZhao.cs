using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaiZhao : MonoBehaviour {

	public AudioClip play;

	void OnEnable()
	{
		GetComponent<Animator> ().SetTrigger("playe");
		GetComponent<AudioSource> ().PlayOneShot(play,1);
	}

}
