using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class SoundManager : MonoBehaviour
{
    public Sounds[] sounds;


    // Start is called before the first frame update
    void Awake()
    {
        foreach(Sounds s in sounds)
        {
            s.source=gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;

        }
    }

    public void PlayMovesSound(string name)
    {
        foreach (Sounds k in sounds)
        {

            k.source.Stop();
        }

        if (SoundMuteScript.muted == false)
        {
            Sounds s = Array.Find(sounds, sounds => sounds.name == name);
            s.source.Play();
        }
        
    }


    public void Play(string name)
    {
        
        if (SoundMuteScript.muted == false)
        {
            Sounds s = Array.Find(sounds, sounds => sounds.name == name);
            s.source.Play();

        }

        
    }

    public void StopMusic(string name)
    {
        Sounds s = Array.Find(sounds, sounds => sounds.name == name);
        s.source.Stop();
    }

}
