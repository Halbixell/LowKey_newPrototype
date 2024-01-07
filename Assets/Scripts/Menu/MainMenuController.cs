using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button StartButton;

    // Start is called before the first frame update
    void Start()
    {
        StartButton.onClick.AddListener(LoadLevels);
    }

    private void LoadLevels()
    {
        SceneManager.LoadScene("Demo Level");
    }
}
