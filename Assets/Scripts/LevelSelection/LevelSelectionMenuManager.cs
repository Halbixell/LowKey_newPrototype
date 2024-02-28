using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelSelectionMenuManager : MonoBehaviour
{
    public Sprite completedStarSprite;
    public Sprite notCompletedStarSprite;
    public static int currentLevel;
    public static int unlockedLevels;
    public LevelObjectScript[] levelObjects;

    [SerializeField] private Button HowToPlayButton;

    public void OnClickLevel(int NumberOfLevel)
    {
        currentLevel = NumberOfLevel;
        SceneManager.LoadScene("Level"+currentLevel);

    }

    public void OnCurrentLevel()
    {
        int richtigesLevel = PlayerPrefs.GetInt("unlockedLevels", 0)+1;
        currentLevel = richtigesLevel;
        PlayerPrefs.Save();
        SceneManager.LoadScene("Level" + richtigesLevel);
    }

    public void OnClickMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void OnClickLevelSelector()
    {
        SceneManager.LoadScene("LevelSelector");
    }

    public void ResetPlayerPref()
    {// Setze die Anzahl der Sterne auf 0 für jedes Level
        for (int i = 0; i < levelObjects.Length; i++)
        {
            PlayerPrefs.SetInt("stars" + i.ToString(), 0);
        }
        UnlockedLevelsScript.freigeschaltenesLevel = 1;
        Debug.Log("Freigeschalten bei Reset:" + UnlockedLevelsScript.freigeschaltenesLevel);
        unlockedLevels = 0;
        PlayerPrefs.SetInt("unlockedLevels", unlockedLevels);
        PlayerPrefs.Save();

        for (int i = 0; i < levelObjects.Length; i++)
        {
            if (i == 0)
            {
                levelObjects[i].LevelButton.interactable = true;
            }
            else
            {
                levelObjects[i].LevelButton.interactable = false;
            }

            //aktualisiere Bild von Sternen
            for (int j = 0; j < levelObjects[i].Stars.Length; j++)
            {
                levelObjects[i].Stars[j].sprite = notCompletedStarSprite;
            }

        }
        
        PlayerPrefs.Save();
        
    }




    // Start is called before the first frame update
    void Start()
    {
        
        HowToPlayButton.onClick.AddListener(LoadHowToPlay);
        currentLevel = 0;
        unlockedLevels = PlayerPrefs.GetInt("unlockedLevels", 0);
        Debug.Log("freigeschalten bei Start: " + UnlockedLevelsScript.freigeschaltenesLevel);
        for (int i=0; i<levelObjects.Length; i++)
        {//unlockedLevels >= i
            if (UnlockedLevelsScript.freigeschaltenesLevel-1 >= i)
            {
                levelObjects[i].LevelButton.interactable = true;
                int stars = PlayerPrefs.GetInt("stars" + i.ToString(), 0);
                if (stars >= 3)
                {
                    stars = 3;
                }
                Debug.Log("In Level " + i + " sind die Sterne: " + stars);
                for(int j=0; j < stars; j++)
                {
                    levelObjects[i].Stars[j].sprite = completedStarSprite;
                }
            }
        }
    }


    private void LoadHowToPlay()
    {
        SceneManager.LoadScene("HowToPlay");
    }

}
