using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RavenCounter : MonoBehaviour
{
    public LevelController _levelController;
    public int AmountOfRavens = 0;
    [SerializeField] public Image[] RavenImages;
    [SerializeField] public Sprite notCollected;
    [SerializeField] public Sprite Collected;

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
        RavenImages[Ravenindex].sprite = Collected;
        if (RavenImages[Ravenindex].sprite != notCollected)
        {
            Debug.Log("<color=green> JETZT!!!! </color>");
            AmountOfRavens = AmountOfRavens + 1;
        }
        
    }
}
