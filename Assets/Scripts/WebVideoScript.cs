using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Video;

public class WebVideoScript : MonoBehaviour
{

    VideoPlayer videoplayer;

    public string VideoURL;
    // Start is called before the first frame update
    void Start()
    {
        videoplayer = this.GetComponent<VideoPlayer>();

        videoplayer.url = Path.Combine(Application.streamingAssetsPath, VideoURL);
    }

}
