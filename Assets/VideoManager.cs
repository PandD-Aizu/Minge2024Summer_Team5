using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{

    public GameObject Screen;
    public GameObject rightbutton;
    public GameObject leftbutton;
    public GameObject backbutton;
    public VideoPlayer videoplayer;
    public VideoClip[] videoclips;

    public GameObject text0;
    public GameObject text1;
    public GameObject text2;
    public GameObject text3;
    public GameObject text4;
    public GameObject text5;
    private int count=0;

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetActive(false);

        videoplayer = Screen.gameObject.GetComponent<VideoPlayer>();
        //videoplayer.clip = videoclips[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonClick(int num)
    {
        switch (num)
        {
            case 0:
                Debug.Log("push");
                Screen.SetActive(true);
                VideoManeger();
                break;
            case 1:
                count++;
                VideoManeger();
                break;
            case 2:
                count--;
                VideoManeger();
                break;
            case 3:
                Screen.SetActive(false);
                break;
        }
    }

    public void VideoManeger()
    {
        switch (count)
        {
            case 0:
                videoplayer.clip = videoclips[1];
                text0.SetActive(true);
                text1.SetActive(false);
                leftbutton.SetActive(false);
                rightbutton.SetActive(true);
                break;
            case 1:
                videoplayer.clip = videoclips[2];
                text1.SetActive(true);
                text0.SetActive(false);
                text2.SetActive(false);
                leftbutton.SetActive(true);
                break;
            case 2:
                videoplayer.clip = videoclips[3];
                text2.SetActive(true);
                text1.SetActive(false);
                text3.SetActive(false);
                break;
            case 3:
                videoplayer.clip = videoclips[4];
                text3.SetActive(true);
                text2.SetActive(false);
                text4.SetActive(false);
                break;
            case 4:
                videoplayer.clip = videoclips[5];
                text4.SetActive(true);
                text3.SetActive(false);
                text5.SetActive(false);
                rightbutton.SetActive(true);
                break;
            case 5:
                videoplayer.clip = videoclips[6];
                text5.SetActive(true);
                text4.SetActive(false);
                rightbutton.SetActive(false);
                break;
        }
        videoplayer.Play();
    }
}
