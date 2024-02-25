using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlayScript : MonoBehaviour
{

    public Button NextStepButton;

    public Image FirstStep_AnimateHel;
    private int StepCounter = 0;
    public GameObject[] AnimationSteps;
    public Image EndHowToPlay;


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
                    MoveImageToTarget();
                    break;
                case 2:
                    break;


            }
        }
        else
        {
            EndHowToPlay.enabled = true;
            EndHowToPlay.gameObject.SetActive(true);
        }
    }

    //public void AnimateHel()
    //{
    //    RectTransform Hel = FirstStep_AnimateHel.rectTransform;
    //    Vector3 target = new Vector3(-305, -260, 0);
    //    float distance = Vector3.Distance(Hel.localPosition, target);
    //    float duration = distance / 0.1f;
    //    Debug.Log(duration);
    //    Debug.Log(distance);
    //    float elapsedTime = 0f;
    //    float t = 0;

    //    for(elapsedTime = 0; elapsedTime<duration; elapsedTime+= Time.deltaTime)
    //    {
    //        // Calculate the interpolation factor
    //        t = elapsedTime / duration;
    //        //Debug.Log(t);

    //        // Lerp between the start and target positions
    //        Hel.anchoredPosition = Vector3.Lerp(Hel.localPosition, target, t);

    //        // Update the elapsed time
    //        elapsedTime += Time.deltaTime;
    //    }

 

    //}

    public float moveSpeed = 10000000f;

    private bool isMoving = false;

    public void MoveImageToTarget()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveCoroutine());
        }
    }

    private IEnumerator MoveCoroutine()
    {
        RectTransform imageTransform = FirstStep_AnimateHel.rectTransform;
        Vector2 targetPosition = new Vector2(-305, -260);
        isMoving = true;
        Vector2 initialPosition = imageTransform.anchoredPosition;
        float distance = Vector2.Distance(initialPosition, targetPosition);
        float startTime = Time.time;

        while (imageTransform.anchoredPosition != targetPosition)
        {
            float coveredDistance = (Time.time - startTime) * moveSpeed;
            float fracJourney = coveredDistance / distance;
            imageTransform.anchoredPosition = Vector2.Lerp(initialPosition, targetPosition, fracJourney);
            yield return null;
        }

        isMoving = false;
    }
}
