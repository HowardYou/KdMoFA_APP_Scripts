using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerCon : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject Player;
    public GameObject Video;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Cam");
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        Player.GetComponent<ClickObj>().notPass = false;
        Player.GetComponent<ClickObj>().Moving = true;
        Video.SetActive(false);
    }
}
