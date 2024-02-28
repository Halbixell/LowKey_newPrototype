using UnityEngine.Audio;
using UnityEngine;


[System.Serializable]
public class Sounds
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;
    public bool loop;


    [HideInInspector]public AudioSource source;

}
