using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pianotimer : MonoBehaviour
{

    public int pianochange_speed = 0;/*?????????X?C?b?`*/
    public int pianochange_jumppower = 0;
    public float speed_p = 20f;    /*???????????x*/
    public int jumppower_p = 500;  /*?????????W?????v??*/
    public float countdown_speed = 10.0f;/*???xup??????????*/
    public float countdown_speed_define = 10.0f;
    public float countdown_jumppower = 10.0f;/*?W?????v??up??????????*/
    public float countdown_jumppower_define = 10.0f;

    public Player Player;
    Piano_ChangeSpeedAndJumpPower piano;

    GameObject timer1;
    GameObject timer2;
    GameObject timericon1;
    GameObject timericon2;
    private Slider gauge1;
    private Slider gauge2;
    GameObject canvas;

    public bool changespeed = false;
    public bool changejump = false;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindObjectOfType<Player>();
        piano = GameObject.FindObjectOfType<Piano_ChangeSpeedAndJumpPower>();

        canvas = GameObject.Find("pianotimer");
        if (canvas == null)
        {
            Debug.LogWarning("pianotimer????????????????");
        }
        timer1 = GameObject.Find("slider1");
        if (timer1 == null)
        {
            Debug.LogWarning("slider1????????????????");
        }
        else
        {
            timer1.SetActive(false);
            gauge1 = timer1.GetComponent<Slider>();
            gauge1.value = 1f;
        }
        timericon1 = GameObject.Find("speed");
        if (timericon1 == null)
        {
            Debug.LogWarning("speed????????????????");
        }
        else
        {
            timericon1.SetActive(false);
        }
        timer2 = GameObject.Find("slider2");
        if (timer2 == null)
        {
            Debug.LogWarning("slider2????????????????");
        }
        else
        {
            timer2.SetActive(false);
            gauge2 = timer2.GetComponent<Slider>();
            gauge2.value = 1f;
        }
        timericon2 = GameObject.Find("jump");
        if (timericon1 == null)
        {
            Debug.LogWarning("speed????????????????");
        }
        else
        {
            timericon2.SetActive(false);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (changespeed)
        {
            speed();
        }

        if (changejump)
        {
            jump();
        }
    }

    public void speed()
    {
            Player.speed = speed_p;
            Debug.Log("test_speed");
            timer1.SetActive(true);
            timericon1.SetActive(true);
            gauge1.value = countdown_speed / countdown_speed_define;
            countdown_speed -= Time.deltaTime;/*?^?C?}?[*/

            if (countdown_speed <= 0)
            {
                piano.Pianoaudio.PlayOneShot(piano.PianoFinish);
                changespeed = false;
                countdown_speed = countdown_speed_define;
                timer1.SetActive(false);
                timericon1.SetActive(false);
                piano.CommandChecker_speed = 0;
                piano.pianochange_speed = 0;
                Player.speed = piano.defaultspeed;
            }
    }

    public void jump()
    {
        Player.jumppower = jumppower_p;
        Debug.Log("test_jump");
        timer2.SetActive(true);
        timericon2.SetActive(true);
        gauge2.value = countdown_jumppower / countdown_jumppower_define;
        countdown_jumppower -= Time.deltaTime;

        if (countdown_jumppower <= 0)/*?????o??*/
        {
            piano.Pianoaudio.PlayOneShot(piano.PianoFinish);
            changejump = false;
            countdown_jumppower = countdown_jumppower_define;
            timer2.SetActive(false);
            timericon2.SetActive(false);      
            piano.CommandChecker_jumppower = 0;
            piano.pianochange_jumppower = 0;
            Player.jumppower = piano.defaultjump;
        }
    }
}
