using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneAnimationController : MonoBehaviour
{
    public List<GameObject> AnimationSteps_Beginning;
    public List<GameObject> AnimationSteps_End;
    private int StepCounter = 0;

    public bool isBeginning = true;


    // Start is called before the first frame update
    void Start()
    {
        
        if(isBeginning)
        {
            AnimationSteps_Beginning[StepCounter].SetActive(true);
            StartCoroutine(BeginningCutscene());
        }
        else
        {
            AnimationSteps_End[StepCounter].SetActive(true);
            StartCoroutine(EndingCutscene());
        }

    }


    private IEnumerator BeginningCutscene()
    {


        yield return null;
    }

    private IEnumerator EndingCutscene()
    {
        yield return null;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
