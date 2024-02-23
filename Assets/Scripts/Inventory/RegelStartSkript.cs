using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegelStartSkript : MonoBehaviour
{

    public GameObject[] ListeVonVideos;
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i < ListeVonVideos.Length; i++)
        {
            ListeVonVideos[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
