using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPopupScript : MonoBehaviour
{
    public GameObject popupAudio;
    public void OnClickPopupAudio()
    {
        popupAudio.SetActive(true);
    }

    public void OnClickClose()
    {
        popupAudio.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        popupAudio.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
