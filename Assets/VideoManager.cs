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
    public GameObject[] texts;
    private int count=0;
    private int i;

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetActive(false);
        rightbutton.SetActive(false);
        leftbutton.SetActive(false);
        backbutton.SetActive(false);
        for (i = 0; i < 9; i++) texts[i].SetActive(false);
        videoplayer = Screen.gameObject.GetComponent<VideoPlayer>();

        
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
                rightbutton.SetActive(true);
                leftbutton.SetActive(true);
                backbutton.SetActive(true);
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
                count = 0;
                Screen.SetActive(false);
                for (i = 0; i < 9; i++) texts[i].SetActive(false);
                rightbutton.SetActive(false);
                leftbutton.SetActive(false);
                backbutton.SetActive(false);
                break;
        }
    }

    public void VideoManeger()
    {
        switch (count)
        {
            case 0:
                videoplayer.clip = videoclips[0];
                texts[0].SetActive(true);
                texts[1].SetActive(false);
                leftbutton.SetActive(false);
                rightbutton.SetActive(true);
                break;
            case 1:
                videoplayer.clip = videoclips[1];
                textManager();
                leftbutton.SetActive(true);
                break;
            case 2:
                videoplayer.clip = videoclips[2];
                textManager();
                break;
            case 3:
                videoplayer.clip = videoclips[3];
                textManager();
                break;
            case 4:
                videoplayer.clip = videoclips[4];
                textManager();
                break;
            case 5:
                videoplayer.clip = videoclips[5];
                textManager();
                break;
            case 6:
                videoplayer.clip = videoclips[6];
                textManager();
                break;
            case 7:
                videoplayer.clip = videoclips[7];
                textManager();
                rightbutton.SetActive(true);
                break;
            case 8:
                videoplayer.clip = videoclips[8];
                texts[7].SetActive(false);
                texts[8].SetActive(true);
                rightbutton.SetActive(false);
                break;
        }
        videoplayer.Play();
    }

    private void textManager()
    {
        texts[count-1].SetActive(false);
        texts[count].SetActive(true);
        texts[count+1].SetActive(false);
    }
}
