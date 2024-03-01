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



    // Start is called before the first frame update
    void Start()
    {
        if(LevelSelectionMenuManager.currentLevel == 1)
        {
            AnimationSteps_Beginning[StepCounter].SetActive(true);
            AnimationSteps_Beginning_Canvas[StepCounter].SetActive(true);
            StartCoroutine(BeginningCutscene());
        }
        else
        {
            AnimationSteps_End[StepCounter].SetActive(true);
            AnimationSteps_End_Canvas[StepCounter].SetActive(true);
            StartCoroutine(EndingCutscene());
        }

    }


    private IEnumerator BeginningCutscene()
    {
        FindObjectOfType<SoundManager>().Play("Expansion_1");
        //Exposition[0].Play();
        yield return new WaitForSeconds(8f);
        FindObjectOfType<SoundManager>().Play("Expansion_2");
        //Exposition[1].Play();
        yield return new WaitForSeconds(12f);

        AnimationSteps_Beginning[StepCounter].SetActive(false);
        AnimationSteps_Beginning_Canvas[StepCounter].SetActive(false);

        StepCounter++;

        AnimationSteps_Beginning[StepCounter].SetActive(true);
        AnimationSteps_Beginning_Canvas[StepCounter].SetActive(true);


        FindObjectOfType<SoundManager>().Play("Expansion_3");
        //Exposition[2].Play();
        yield return new WaitForSeconds(7.6f);
        FindObjectOfType<SoundManager>().Play("Expansion_4");
        //Exposition[3].Play();
        yield return new WaitForSeconds(7f);
        FindObjectOfType<SoundManager>().Play("Expansion_5");
        //Exposition[4].Play();
        yield return new WaitForSeconds(7f);


        SceneManager.LoadScene("Level1");


        yield return null;
    }

    private IEnumerator EndingCutscene()
    {


        SceneManager.LoadScene("LevelSelector");
        yield return null;
    }

}
