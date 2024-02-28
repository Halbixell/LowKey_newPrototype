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

    public void OnClickMuteSound()
    {
        if (muted == false)
        {
            foreach (Sounds s in _soundManager.sounds)
            {
                
                s.volume = 0.0f;
                s.source.volume = 0.0f;
                
            }
            muted = true;
            _musicImage.sprite = _mutedSound;
        }
        else
        {
            foreach (Sounds s in _soundManager.sounds)
            {
                
                s.volume = 1.0f;
                s.source.volume = 1.0f;
            }
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
                
                s.volume = 1.0f;
                s.source.volume = 1.0f;

            }
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
            muted = true;
            _musicImage.sprite = _mutedSound;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
