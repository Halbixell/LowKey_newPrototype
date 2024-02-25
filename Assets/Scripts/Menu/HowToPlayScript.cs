using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HowToPlayScript : MonoBehaviour
{

    public Button NextStepButton;

    public Image FirstStep_AnimateHel;
    private int StepCounter = 0;
    public GameObject[] AnimationSteps;
    public Image EndHowToPlay;

    public MovePlayerPreview _player;

    private float timer = 0;


    // Start is called before the first frame update
    void Start()
    {
        AnimationSteps[StepCounter].SetActive(true);
    }

    public void ButtonPressNext()
    {
        if (StepCounter + 1 < AnimationSteps.Length)
        {
            AnimationSteps[StepCounter].SetActive(false);
            StepCounter += 1;
            AnimationSteps[StepCounter].SetActive(true);

            switch (StepCounter)
            {
                case 1:
                    Vector2 targetPosition = new Vector2(-305, -260);
                    MoveImageToTarget(targetPosition);
                    break;
                case 3:
                    Vector2 targetPosition2 = new Vector2(336, -265);
                    MoveImageToTargetAndDelete(targetPosition2);
                    break;
                case 5:
                    _player.MovePlayer();
                    break;


            }
        }
        else
        {
            EndHowToPlay.enabled = true;
            EndHowToPlay.gameObject.SetActive(true);
        }
    }


    public int moveSpeed = 100000;

    private bool isMoving = false;

    public void MoveImageToTarget(Vector2 targetPosition)
    {
        if (!isMoving)
        {
            StartCoroutine(MoveCoroutine(targetPosition));
        }
    }

    private IEnumerator MoveCoroutine(Vector2 targetPosition)
    {
        yield return new WaitForSeconds(2f);
        RectTransform imageTransform = FirstStep_AnimateHel.rectTransform;
        isMoving = true;

        while (imageTransform.anchoredPosition != targetPosition)
        {
            imageTransform.localPosition = Vector3.MoveTowards(imageTransform.localPosition, targetPosition, moveSpeed * 5 * Time.deltaTime);
            yield return null;
        }

        
        isMoving = false;
    }


    public void MoveImageToTargetAndDelete(Vector2 targetPosition)
    {
        if (!isMoving)
        {
            StartCoroutine(MoveandDeleteCoroutine(targetPosition));
        }
    }

    private IEnumerator MoveandDeleteCoroutine(Vector2 targetPosition)
    {
        RectTransform imageTransform = FirstStep_AnimateHel.rectTransform;
        isMoving = true;

        while (imageTransform.anchoredPosition != targetPosition)
        {
            imageTransform.localPosition = Vector3.MoveTowards(imageTransform.localPosition, targetPosition, moveSpeed * 5 * Time.deltaTime);
            yield return null;
        }

        isMoving = false;
        yield return new WaitForSeconds(2f);
        Destroy(imageTransform.gameObject);
    }


    public void LoadLevelSelection()
    {
        SceneManager.LoadScene("LevelSelector");
    }
}


