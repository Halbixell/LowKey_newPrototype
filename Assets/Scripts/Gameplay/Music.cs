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


    [Header("Music")]
    [SerializeField] public AudioSource _hintergrundmusik;


    void Start()
    {
        if (musicMuted == 0)
        {
            _hintergrundmusik.mute = true;
            _musicImage.sprite = _muted;
            musicMuted = 0;
        }
        else
        {
            _hintergrundmusik.mute = false;
            _musicImage.sprite = _unmuted;
            musicMuted = 1;
        }
        _musicButton.onClick.AddListener(MusicMute);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MusicMute()
    {
        if (musicMuted == 1)
        {
            _hintergrundmusik.mute = true;
            _musicImage.sprite = _muted;
            musicMuted = 0;
        }
        else
        {
            _hintergrundmusik.mute = false;
            _musicImage.sprite = _unmuted;
            musicMuted = 1;
        }
    }




}
