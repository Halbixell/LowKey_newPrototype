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
    [SerializeField] private Button ViewEndingButton;

    public void ViewStory()
    {
        currentLevel = 1;
        PlayerPrefs.Save();
        SceneManager.LoadScene("AlphaundOmega");
    }


    public void ViewEnding()
    {
        currentLevel = 10;
        PlayerPrefs.Save();
        SceneManager.LoadScene("AlphaundOmega");
    }


    public void OnClickLevel(int NumberOfLevel)
    {
        currentLevel = NumberOfLevel;
        if (currentLevel > 10)
        {
            currentLevel = 10;
        }
        
       
        SceneManager.LoadScene("Level" + currentLevel);
       

    }

    public void OnCurrentLevel()
    {
        int richtigesLevel = PlayerPrefs.GetInt("unlockedLevels", 0)+1;
        currentLevel = richtigesLevel;
        PlayerPrefs.Save();

        if (richtigesLevel > 10)
        {
            richtigesLevel = 10;
        }

        if (richtigesLevel != 1)
        {
            SceneManager.LoadScene("Level" + richtigesLevel);
        }
        else if (richtigesLevel == 1)
        {
            SceneManager.LoadScene("AlphaundOmega");
        }
            
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
        ViewEndingButton.interactable = false;
        
        PlayerPrefs.Save();
        
    }




    // Start is called before the first frame update
    void Start()
    {
        ViewEndingButton.interactable = false;
        HowToPlayButton.onClick.AddListener(LoadHowToPlay);
        currentLevel = 0;
        unlockedLevels = PlayerPrefs.GetInt("unlockedLevels", 0);
        for (int i=0; i<levelObjects.Length; i++)
        {//unlockedLevels >= i
            if (unlockedLevels >= i)
            {
                levelObjects[i].LevelButton.interactable = true;
                int stars = PlayerPrefs.GetInt("stars" + i.ToString(), 0);
                Debug.Log("In Level " + i + " sind die Sterne: " + stars);
                for(int j=0; j < stars; j++)
                {
                    levelObjects[i].Stars[j].sprite = completedStarSprite;
                }
            }
            
        }
        if (unlockedLevels>=10)
        {
            ViewEndingButton.interactable = true;

        }
    }


    private void LoadHowToPlay()
    {
        SceneManager.LoadScene("HowToPlay");
    }

}
