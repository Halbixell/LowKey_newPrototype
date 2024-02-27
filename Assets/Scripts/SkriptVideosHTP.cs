using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkriptVideosHTP : MonoBehaviour
{

    public GameObject[] VideoOfMove;
    public GameObject[] TextOfMove;


    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<VideoOfMove.Length; i++)
        {
            VideoOfMove[i].SetActive(false);
            TextOfMove[i].SetActive(false);
        }
        
    }

    public void OnClickEnemy(int i)
    {
        for (int j = 0; j < VideoOfMove.Length; j++)
        {
            VideoOfMove[j].SetActive(false);
            TextOfMove[j].SetActive(false);
        }


        VideoOfMove[i].SetActive(true);
        TextOfMove[i].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
