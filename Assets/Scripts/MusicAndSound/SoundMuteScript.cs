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
    [HideInInspector] static float gemerkt_sound=1.0f;

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
        if (muted == false)
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
