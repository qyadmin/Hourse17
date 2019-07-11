using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager Instance;

    [SerializeField]
    AudioSource racing;
    [SerializeField]
    AudioSource noracing;
    [SerializeField]
    AudioSource ma;

    private void Awake()
    {
        Instance = this;
    }

    public void Noracing()
    {
        racing.Stop();
        noracing.Play();
        ma.Stop();
    }
    public void Racing()
    {
        racing.Play();
        noracing.Stop();
        ma.Play();
    }


}
