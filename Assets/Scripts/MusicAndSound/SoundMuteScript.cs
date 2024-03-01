using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundMuteScript : MonoBehaviour
{
    public static bool muted = false;


    [SerializeField] public Image _musicImage;
    [SerializeField] private Sprite _mutedSound;
    [SerializeField] private Sprite _unmutedSound;
    public SoundManager _soundManager;
    public Slider sliderSound;
    [HideInInspector] static float gemerkt_sound;
    [HideInInspector] static float gemerkt_sound_PP;

    public void OnClickMuteSound()
    {
        if (muted == false)
        {
            foreach (Sounds s in _soundManager.sounds)
            {
                
                s.volume = 0.0f;
                s.source.volume = 0.0f;
                
            }
            sliderSound.value = 0.0f;
            muted = true;
            _musicImage.sprite = _mutedSound;
        }
        else
        {
            foreach (Sounds s in _soundManager.sounds)
            {
                
                s.volume = gemerkt_sound;
                s.source.volume = gemerkt_sound;
            }
            sliderSound.value = gemerkt_sound;
            muted = false;
            _musicImage.sprite = _unmutedSound;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        gemerkt_sound = PlayerPrefs.GetFloat("gemerkt_sound", 1);
        gemerkt_sound_PP = PlayerPrefs.GetFloat("gemerkt_sound_PP", 1);
        if (muted == false)
        {
            foreach (Sounds s in _soundManager.sounds)
            {
                
                s.volume = gemerkt_sound_PP;
                s.source.volume = gemerkt_sound_PP;

            }
            sliderSound.value = gemerkt_sound_PP;
            muted = false;
            _musicImage.sprite = _unmutedSound;
        }
        else
        {
            foreach (Sounds s in _soundManager.sounds)
            {
                
                s.volume = 0.0f;
                s.source.volume = 0.0f;

            }
            sliderSound.value = 0.0f;
            muted = true;
            _musicImage.sprite = _mutedSound;
        }

    }

    public void SoundVolume(float volume)
    {
        if (volume != 0)
        {
            gemerkt_sound = volume;
        }
        PlayerPrefs.SetFloat("gemerkt_sound", gemerkt_sound);
        PlayerPrefs.SetFloat("gemerkt_sound_PP", volume);
        foreach (Sounds s in _soundManager.sounds)
        {

            s.volume = volume;
            s.source.volume = volume;

        }
        if (volume == 0.0f)
        {
            _musicImage.sprite = _mutedSound;
            muted = true;
        }
        else
        {
            _musicImage.sprite = _unmutedSound;
            muted = false;
        }

    }

    public void SliderVolume()
    {
        SoundVolume(sliderSound.value);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
