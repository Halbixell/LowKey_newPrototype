using UnityEngine.Audio;
using UnityEngine;


[System.Serializable]
public class Sounds
{
    public string name;
    public AudioClip clip;
    public float volume;
    public bool loop;


    [HideInInspector]public AudioSource source;

}