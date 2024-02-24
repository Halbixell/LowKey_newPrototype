using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CompleteLevelScript : MonoBehaviour
{
    public void OnClickBack()
    {
        this.gameObject.SetActive(false);
    }

    public void OnClickComplete()
    {
        this.gameObject.SetActive(true);
    }

    public void OnCompleteLevel(int stars)
    {
        
        if (LevelSelectionMenuManager.currentLevel-1 == LevelSelectionMenuManager.unlockedLevels)
        {
            LevelSelectionMenuManager.unlockedLevels++;
            PlayerPrefs.SetInt("unlockedLevels", LevelSelectionMenuManager.unlockedLevels);

        }
        int richtigeZahl = LevelSelectionMenuManager.currentLevel - 1;
        if (stars > PlayerPrefs.GetInt("stars" + richtigeZahl.ToString(),0))
        {
            PlayerPrefs.SetInt("stars" + richtigeZahl.ToString(), stars);
        }
        SceneManager.LoadScene("LevelSelector");
        
    }

    public void OnNotComplete()
    {
        SceneManager.LoadScene("LevelSelector");
    }



    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
