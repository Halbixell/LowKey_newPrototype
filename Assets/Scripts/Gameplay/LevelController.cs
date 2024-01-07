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
    [HideInInspector] public bool StopMovement;
    [SerializeField] private Button RestartLevelWhilePlaying;

    [Header("Win UI")]
    [SerializeField] private GameObject WinTile;
    [SerializeField] private Canvas LevelWonCanvas;
    [SerializeField] private CanvasGroup LevelWonCanvasGroup;

    [SerializeField] TMP_Text WinText;
    [SerializeField] Button WinMainMenuButton;
    [SerializeField] Button WinRestartLevelButton;

    [Header("Game Over UI")]
    [SerializeField] private Canvas GameOverCanvas;
    [SerializeField] private CanvasGroup GameOverCanvasGroup;
    public float fadeDuration = 0.5f;

    [SerializeField] TMP_Text GameOverText;
    [SerializeField] Button LoseMainMenuButton;
    [SerializeField] Button LoseRestartButton;



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
        RestartLevelWhilePlaying.onClick.AddListener(RestartLevel);
        WinMainMenuButton.onClick.AddListener(LoadMainMenu);
        WinRestartLevelButton.onClick.AddListener(RestartLevel);
    }

    void Update()
    {

    }

    //################################################################################################

    void StartButtonClicked()
    {
        StopMovement = false;
        MoveOptions = _listOfInventorySlots.CollectMoveOptions();
        _player.MovePlayer(MoveOptions, Enemies);
    }

    public void LevelLost()
    {
        StopMovement = true;
        Debug.Log("Level is lost!");
        GameOverCanvas.gameObject.SetActive(true);
        StartCoroutine(FadeInScreen(GameOverCanvasGroup, GameOverText));
    }

    public void LevelWon()
    {
        StopMovement = true;
        Debug.Log("Level is won");
        LevelWonCanvas.gameObject.SetActive(true);
        StartCoroutine(FadeInScreen(LevelWonCanvasGroup, WinText));

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
        SceneManager.LoadScene("Main Menu");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("Demo Level");
    }
}