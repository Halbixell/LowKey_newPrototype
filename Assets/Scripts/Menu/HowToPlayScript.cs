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

    public Image MoveImageToRotate;

    public MovePlayerPreview _player;
    public GameObject _direction;

    public SoundManager _soundManager;

    // Start is called before the first frame update
    void Start()
    {
        AnimationSteps[StepCounter].SetActive(true);
    }

    public void ButtonPressNext()
    {
        for(int s=0; s < _soundManager.sounds.Length; s++)
        {
            string name = _soundManager.sounds[s].name;
            _soundManager.StopMusic(name);

        }
       



        if (StepCounter + 1 < AnimationSteps.Length)
        {
            AnimationSteps[StepCounter].SetActive(false);
            StepCounter += 1;
            AnimationSteps[StepCounter].SetActive(true);

            switch (StepCounter)
            {
                case 1:
                    Vector2 targetPosition = new Vector2(-141, -193);
                    MoveImageToTarget(targetPosition);
                    break;
                case 2:
                    StartCoroutine(RotateImage(MoveImageToRotate));
                    break;
                case 4:
                    Vector2 targetPosition2 = new Vector2(210, -193);
                    MoveImageToTargetAndDelete(targetPosition2);
                    break;
                case 6:
                    _player.MovePlayer();
                    _direction.SetActive(false);
                    break;


            }
        }
        else
        {
            EndHowToPlay.enabled = true;
            EndHowToPlay.gameObject.SetActive(true);
        }
    }

    public void ButtonPressBefore()
    {
        for (int s = 0; s < _soundManager.sounds.Length; s++)
        {
            string name = _soundManager.sounds[s].name;
            _soundManager.StopMusic(name);

        }

        if (StepCounter >= 1)
        {
            AnimationSteps[StepCounter].SetActive(false);
            StepCounter -= 1;
            AnimationSteps[StepCounter].SetActive(true);

            switch (StepCounter)
            {
                case 1:
                    Vector2 targetPosition = new Vector2(-141, -193);
                    MoveImageToTarget(targetPosition);
                    break;
                case 2:
                    StartCoroutine(RotateImage(MoveImageToRotate));
                    break;
                case 4:
                    Vector2 targetPosition2 = new Vector2(210, -193);
                    MoveImageToTargetAndDelete(targetPosition2);
                    break;
                case 6:
                    _player.MovePlayer();
                    _direction.SetActive(false);
                    break;


            }
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

    private IEnumerator RotateImage(Image moveImage)
    {
        while(moveImage.transform.parent.gameObject.activeSelf)
        {
            moveImage.transform.Rotate(0f, 0f, 90f);
            yield return new WaitForSeconds(1f);
        }
        yield return null;
    }
}


