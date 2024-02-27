using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CompleteLevelScript : MonoBehaviour
{
    public GameObject PopUpCheat;
    public Button BackButton;

    public void OnClickBack()
    {
        PopUpCheat.gameObject.SetActive(false);
    }



    public void OnCompleteLevel(int stars)
    {
        
        if (LevelSelectionMenuManager.currentLevel-1 == LevelSelectionMenuManager.unlockedLevels)
        {
            Debug.Log("Schleife 1");
            LevelSelectionMenuManager.unlockedLevels++;
            PlayerPrefs.SetInt("unlockedLevels", LevelSelectionMenuManager.unlockedLevels);

        }
        int richtigeZahl = LevelSelectionMenuManager.currentLevel - 1;
        if (stars > PlayerPrefs.GetInt("stars" + richtigeZahl.ToString(),0))
        {
            Debug.Log("Schleife 2");
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
        PopUpCheat.gameObject.SetActive(false);
        BackButton.onClick.AddListener(OnClickBack);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            PopUpCheat.gameObject.SetActive(true);
        }
       
    }
}
