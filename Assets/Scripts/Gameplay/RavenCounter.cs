using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RavenCounter : MonoBehaviour
{
    public LevelController _levelController;
    public int AmountOfRavens = 0;
    [SerializeField] private Image[] RavenImages;
    [SerializeField] private Sprite notCollected;
    [SerializeField] private Sprite Collected;

    [SerializeField] private CollectibleRaven[] Ravens;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < RavenImages.Length; i++)
        {
            RavenImages[i].sprite = notCollected;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CollectRaven(int Ravenindex)
    {
        if(RavenImages[Ravenindex].sprite != notCollected)
        {
            AmountOfRavens += 1;
        }
        RavenImages[Ravenindex].sprite = Collected;
        
    }
}
