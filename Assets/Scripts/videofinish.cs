using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
public class videofinish : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;

    bool videoPause = false;
    GameObject video;

    // Start is called before the first frame update
    void Start()
    {
        video = this.gameObject;
        videoPlayer = video.GetComponent<VideoPlayer>();
        videoPlayer.isLooping = true;
        videoPlayer.loopPointReached += FinishPlayingVideo;
        videoPlayer.Play();
    }

    // Update is called once per frame
    public void FinishPlayingVideo(VideoPlayer vp)
    {
        videoPlayer.Stop();
        video.SetActive(false);
    }
}