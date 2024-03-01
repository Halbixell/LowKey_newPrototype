using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour
{

    static int musicMuted = 1;
    [Header("Button")]
    [SerializeField] public Button _musicButton;
    [SerializeField] public Image _musicImage;
    [SerializeField] private Sprite _muted;
    [SerializeField] private Sprite _unmuted;

    public Slider musicSlider;
    [HideInInspector] static float gemerkt;
    [HideInInspector] static float gemerkt_playerpref;


    [Header("Music")]
    [SerializeField] public AudioSource _hintergrundmusik;


    void Awake()
    {
        gemerkt = PlayerPrefs.GetFloat("gemerkt", 0.5f);
        gemerkt_playerpref = PlayerPrefs.GetFloat("gemerkt_playerpref", 0.5f);
        if (musicMuted == 0)
        {
            _hintergrundmusik.volume = 0.0f;
            _musicImage.sprite = _muted;
            musicMuted = 0;
            musicSlider.value = 0.0f;
        }
        else
        {
            _hintergrundmusik.volume = gemerkt_playerpref;
            _musicImage.sprite = _unmuted;
            musicMuted = 1;
            musicSlider.value = gemerkt_playerpref;
        }
        _musicButton.onClick.AddListener(MusicMute);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MusicMute()
    {
        Debug.Log(gemerkt);
        if (musicMuted == 1)
        {
            _hintergrundmusik.volume = 0.0f;
            _musicImage.sprite = _muted;
            musicMuted = 0;
            musicSlider.value = 0.0f;
        }
        else
        {
            _hintergrundmusik.volume = gemerkt;
            _musicImage.sprite = _unmuted;
            musicMuted = 1;
            musicSlider.value = gemerkt;
        }
    }

    public void MusicVolume(float volume)
    {
        if (volume != 0)
        {
            gemerkt = volume;
        }
        PlayerPrefs.SetFloat("gemerkt", gemerkt);
        PlayerPrefs.SetFloat("gemerkt_playerpref", volume);
        _hintergrundmusik.volume = volume;
        if (_hintergrundmusik.volume == 0.0f)
        {
            
            _musicImage.sprite = _muted;
            musicMuted = 0;
            
        }
        else
        {
            
            _musicImage.sprite = _unmuted;
            musicMuted = 1;
            
        }
    }

    public void SliderMusic()
    {
        MusicVolume(musicSlider.value);
    }



}
