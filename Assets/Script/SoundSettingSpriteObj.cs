using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Source
{
    public AudioSource audio;

    public float volume_Max;
}
[System.Serializable]
public class Sound
{
    [SerializeField]
    public Source[] sources;

    [SerializeField]
    int volume;

    public int Volume
    {
        get { return volume; }
        set
        {
            volume = value;
            Setting();
        }
    }

    public void Setting()
    {
        if(sources != null)
        foreach (Source i in sources)
        {
            if(i.audio != null)
            i.audio.volume = i.volume_Max * ( (float)Volume/100);
        }
    }

}


public class SoundSettingSpriteObj : ScriptableObject {

    

    [SerializeField]
    public Sound[] Allsound;

}
