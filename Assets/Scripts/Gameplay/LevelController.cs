using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [Header("GamePlay")]
    [SerializeField] private ListOfMoveSlots _listOfInventorySlots;
    public Button StartButton;
    [SerializeField] private PlayerController _player;
    [SerializeField] List<EnemyController> Enemies;

    [Header("Level UI")]
    [SerializeField] private Canvas UICanvas;
    [HideInInspector] public List<Item> MoveOptions;
    [HideInInspector] public List<ItemEntry> MoveOptionsAndRotations;
    [HideInInspector] public bool StopMovement;
    [SerializeField] private Button RestartLevelWhilePlaying;

    [Header("Win UI")]
    [SerializeField] private GameObject WinTile;
    [SerializeField] private Canvas LevelWonCanvas;
    [SerializeField] private CanvasGroup LevelWonCanvasGroup;

    [SerializeField] TMP_Text WinText;
    [SerializeField] Button WinMainMenuButton;
    [SerializeField] Button WinRestartLevelButton;
    [SerializeField] Button NextLevelButton;

    [Header("Game Over UI")]
    [SerializeField] private Canvas GameOverCanvas;
    [SerializeField] private CanvasGroup GameOverCanvasGroup;
    public float fadeDuration = 0.5f;

    [SerializeField] TMP_Text GameOverText;
    [SerializeField] Button LoseMainMenuButton;
    [SerializeField] Button LoseRestartButton;

    [Header("Ravens")]
    public RavenCounter _ravenCounter;

    [Header("How to Play")]
    [SerializeField] Button HowToPlayButton;



    //################################################################################################

    void Start()
    {
        UICanvas.gameObject.SetActive(true);
        _player = FindObjectOfType<PlayerController>();
        StartButton.onClick.AddListener(StartButtonClicked);
        GameOverCanvas.gameObject.SetActive(false);
        LevelWonCanvas.gameObject.SetActive(false);

        LevelWonCanvasGroup.alpha = 0f;
        WinText.canvasRenderer.SetAlpha(0f);
        GameOverCanvasGroup.alpha = 0f;
        GameOverText.canvasRenderer.SetAlpha(0f);
        LoseMainMenuButton.onClick.AddListener(LoadMainMenu);
        LoseRestartButton.onClick.AddListener(RestartLevel);
        RestartLevelWhilePlaying.onClick.AddListener(LoadMainMenu);
        WinMainMenuButton.onClick.AddListener(LoadMainMenu);
        WinRestartLevelButton.onClick.AddListener(RestartLevel);
        NextLevelButton.onClick.AddListener(LoadNextLevel);
        HowToPlayButton.onClick.AddListener(LoadHowToPlay);
    }

    void Update()
    {

    }

    //################################################################################################

    void StartButtonClicked()
    {
       
        FindObjectOfType<SoundManager>().Play("Knarksen");
        
        
        StopMovement = false;
        MoveOptionsAndRotations = _listOfInventorySlots.CollectMoveOptionsandRotation();
        _player.MovePlayer(MoveOptionsAndRotations, Enemies);
    }

    public void LevelLost()
    {
      
        FindObjectOfType<SoundManager>().StopMusic("Knarksen");
        FindObjectOfType<SoundManager>().Play("Spotted");

        StopMovement = true;
        GameOverCanvas.gameObject.SetActive(true);
        StartCoroutine(FadeInScreen(GameOverCanvasGroup, GameOverText));
    }

    public void LevelWon()
    {
       
        FindObjectOfType<SoundManager>().StopMusic("Knarksen");

        FindObjectOfType<SoundManager>().Play("Gewonnen");
        StopMovement = true;
       
        LevelWonCanvas.gameObject.SetActive(true);
        StartCoroutine(FadeInScreen(LevelWonCanvasGroup, WinText));
        int stars = _ravenCounter.AmountOfRavens + 1 ;
        

        if (LevelSelectionMenuManager.currentLevel-1  == LevelSelectionMenuManager.unlockedLevels)
        {
           
            LevelSelectionMenuManager.unlockedLevels++;
            PlayerPrefs.SetInt("unlockedLevels", LevelSelectionMenuManager.unlockedLevels);

        }
        int richtigeZahl = LevelSelectionMenuManager.currentLevel - 1;
        if (stars > PlayerPrefs.GetInt("stars" + richtigeZahl.ToString(), 0))
        {
            PlayerPrefs.SetInt("stars" + richtigeZahl.ToString(), stars);
        }


    }

   

    private IEnumerator FadeInScreen(CanvasGroup Screen, TMP_Text Text)
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            Screen.alpha = alpha;
            timer += Time.deltaTime;
            yield return null;
        }

        if (Text != null)
        {
            yield return new WaitForSeconds(0.3f); // Delay before fading in text
            StartCoroutine(FadeInUIElement(Text));
        }

    }

    private IEnumerator FadeInUIElement(Graphic uiElement)
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            uiElement.canvasRenderer.SetAlpha(alpha);
            timer += Time.deltaTime;
            yield return null;
        }

    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("LevelSelector");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LoadNextLevel()
    {
        if (LevelSelectionMenuManager.currentLevel != 10)
        {

            LevelSelectionMenuManager.currentLevel++;
            if (LevelSelectionMenuManager.currentLevel > 10)
            {
                LevelSelectionMenuManager.currentLevel = 10;
            }
            PlayerPrefs.SetInt("currentLevel", LevelSelectionMenuManager.currentLevel);
            PlayerPrefs.Save();
           
            
            SceneManager.LoadScene("Level" + LevelSelectionMenuManager.currentLevel);
        }
        else
        {
            SceneManager.LoadScene("AlphaundOmega");
        }
    }

   public void LoadHowToPlay()
    {
        SceneManager.LoadScene("HowToPlay");
    }


}