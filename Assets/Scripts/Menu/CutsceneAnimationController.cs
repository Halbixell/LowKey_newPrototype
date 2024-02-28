using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class CutsceneAnimationController : MonoBehaviour
{
    public List<GameObject> AnimationSteps_Beginning_Canvas;
    public List<GameObject> AnimationSteps_Beginning;
    public List<GameObject> AnimationSteps_End;
    public List<GameObject> AnimationSteps_End_Canvas;
    private int StepCounter = 0;

    public bool isBeginning = true;

    public List<AudioSource> Exposition;
    public List<AudioSource> Finale;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("1");
        //if(LevelSelectionMenuManager.currentLevel == 1)
        {
            Debug.Log("2");
            AnimationSteps_Beginning[StepCounter].SetActive(true);
            Debug.Log("3");
            AnimationSteps_Beginning_Canvas[StepCounter].SetActive(true);
            Debug.Log("4");
            StartCoroutine(BeginningCutscene());
        }
        //else
        //{
        //    AnimationSteps_End[StepCounter].SetActive(true);
        //    AnimationSteps_End_Canvas[StepCounter].SetActive(true);
        //    StartCoroutine(EndingCutscene());
        //}

    }


    private IEnumerator BeginningCutscene()
    {
        Debug.Log("5");
        Exposition[0].Play();
        yield return new WaitForSeconds(8f);
        Debug.Log("6");
        Exposition[1].Play();
        yield return new WaitForSeconds(12f);
        Debug.Log("7");

        AnimationSteps_Beginning[StepCounter].SetActive(false);
        AnimationSteps_Beginning_Canvas[StepCounter].SetActive(false);

        StepCounter++;

        AnimationSteps_Beginning[StepCounter].SetActive(true);
        AnimationSteps_Beginning_Canvas[StepCounter].SetActive(true);

        Exposition[2].Play();
        yield return new WaitForSeconds(7.6f);
        Exposition[3].Play();
        yield return new WaitForSeconds(7f);
        Exposition[4].Play();
        yield return new WaitForSeconds(7f);


        SceneManager.LoadScene("Level1");


        yield return null;
    }

    private IEnumerator EndingCutscene()
    {

        yield return null;
    }

}
