using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button StartButton;
    [SerializeField] private Button HowToPlayButton;

    // Start is called before the first frame update
    void Start()
    {
        StartButton.onClick.AddListener(LoadLevels);
        HowToPlayButton.onClick.AddListener(LoadHowToPlay);
    }

    private void LoadLevels()
    {
        SceneManager.LoadScene("LevelSelector");
    }

    private void LoadHowToPlay()
    {
        SceneManager.LoadScene("HowToPlay");
    }
}
