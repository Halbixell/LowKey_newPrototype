using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RavenCounter : MonoBehaviour
{
    public LevelController _levelController;
    public int AmountOfRavens = 0;
    [SerializeField] private Image[] RavenImages;

    [SerializeField] private CollectibleRaven[] Ravens;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < RavenImages.Length; i++)
        {
            RavenImages[i].color = new Color(0f, 0f, 0f, 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CollectRaven(int Ravenindex)
    {
        if(RavenImages[Ravenindex].color != new Color(1f,1f,1f,1f))
        {
            AmountOfRavens += 1;
        }
        RavenImages[Ravenindex].color = new Color(1f,1f,1f,1f);
        
    }
}
