using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSetting : MonoBehaviour {
    public static SoundSetting Instance;

    [System.Serializable]
    public class SoundVolume
    {
        public AudioSource obj;

        float volume;

        [HideInInspector]
        public float MaxVolume;

        public float Volume
        {
            get { return volume; }
            set
            {
                volume = value;
                setting();
            }
            
        }

        public void setting()
        {
            obj.volume = volume;
        }
    }


    [System.Serializable]
    public class Sound
    {
        public string SourcesType;
        
        public SoundVolume[] Sources;


        public void SoundStart()
        {
            foreach (SoundVolume i in Sources)
            {
                i.MaxVolume = i.obj.volume;
            }
        }
    }

    public Sound[] newSound;

    [HideInInspector]
    public bool swich = true;
    [SerializeField]
    Sprite close, Open;
    [SerializeField]
    Button Onclick;


    private void Start()
    {
        Instance = this;
        Onclick.onClick.AddListener(delegate {
            funtion();
        });

        foreach (Sound i in newSound)
        {
            i.SoundStart();
        }
        swich = Static.Instance.MusicSwich;
        Reload();
    }


    public void funtion()
    {
        if (swich)
        {
            foreach (Sound i in newSound)
            {
                foreach (SoundVolume j in i.Sources)
                {
                    j.Volume = 0;
                }
            }
            swich = false;
            Onclick.GetComponent<Image>().sprite = close;
        }
        else
        {
            foreach (Sound i in newSound)
            {
                foreach (SoundVolume j in i.Sources)
                {
                    j.Volume = j.MaxVolume;
                }
            }
            swich = true;
            Onclick.GetComponent<Image>().sprite = Open;
        }
        Static.Instance.MusicSwich = swich;
    }
    public void Reload()
    {
        if (swich)
        {
            foreach (Sound i in newSound)
            {
                foreach (SoundVolume j in i.Sources)
                {
                    j.Volume = j.MaxVolume;
                }
            }
            Onclick.GetComponent<Image>().sprite = Open;
        }
        else
        {
            foreach (Sound i in newSound)
            {
                foreach (SoundVolume j in i.Sources)
                {
                    j.Volume = 0;
                }
            }
            Onclick.GetComponent<Image>().sprite = close;
        }
    }
}
