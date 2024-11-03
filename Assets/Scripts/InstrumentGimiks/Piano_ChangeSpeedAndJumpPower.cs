
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Piano_ChangeSpeedAndJumpPower : MonoBehaviour{
    public Player Player;
    public int pianochange_speed = 0;/*?????????X?C?b?`*/
    public int pianochange_jumppower = 0;
    public float speed_p = 20f;    /*???????????x*/
    public int jumppower_p = 500;  /*?????????W?????v??*/
    public float countdown_speed = 10.0f;/*???xup??????????*/
    public float countdown_speed_define = 10.0f;
    public float countdown_jumppower = 10.0f;/*?W?????v??up??????????*/
    public float countdown_jumppower_define = 10.0f;
    public int CommandChecker_speed = 0;/*?s?A?m???R?}???h????*/
    public int CommandChecker_jumppower = 0;
    public int[] CommandInput = { 0, 0, 0 };
    public int KeyUpChecker = 0;
    public int Count_CommandMiss = 0;
    int i, j;

    public AudioSource Pianoaudio;
    public AudioClip Pianosound;

    GameObject timer1;
    GameObject timer2;
    GameObject timericon1;
    GameObject timericon2;
    private Slider gauge1;
    private Slider gauge2;
    GameObject canvas;

    private void Start()
    {
        Player = GameObject.FindObjectOfType<Player>();
        Pianoaudio = GetComponent<AudioSource>();
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
        if(timer2 == null)
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

    private void Update()
    {
        
        
        if (CommandInput[0] == 0 && CommandInput[1] == 0 && CommandInput[2] == 0 && Input.GetKey(KeyCode.U) && KeyUpChecker == 0)/*?R?}???h1???????L?[????*/ /*???????m???s?????????R?[?h????????????????????*/
        {
            CommandInput[0] = 1;
        }
        if (CommandInput[0] == 1 && Input.GetKeyUp(KeyCode.U) && KeyUpChecker == 0)
        {
            KeyUpChecker = 1;
        }

        if (CommandInput[0] == 0 && CommandInput[1] == 0 && CommandInput[2] == 0 && Input.GetKey(KeyCode.I) && KeyUpChecker == 0)
        {
            CommandInput[0] = 2;
        }
        if (CommandInput[0] == 2 && Input.GetKeyUp(KeyCode.I) && KeyUpChecker == 0)
        {
            KeyUpChecker = 1;
        }

        if (CommandInput[0] == 0 && CommandInput[1] == 0 && CommandInput[2] == 0 && Input.GetKey(KeyCode.O) && KeyUpChecker == 0)
        {
            CommandInput[0] = 3;
        }
        if (CommandInput[0] == 3 && Input.GetKeyUp(KeyCode.O) && KeyUpChecker == 0)
        {
            KeyUpChecker = 1;
        }

        if (CommandInput[0] != 0 && CommandInput[1] == 0 && CommandInput[2] == 0 && Input.GetKey(KeyCode.U) && KeyUpChecker == 1)/*?R?}???h2???????L?[????*/
        {
            CommandInput[1] = 1;
        }
        if (CommandInput[1] == 1 && Input.GetKeyUp(KeyCode.U) && KeyUpChecker == 1)
        {
            KeyUpChecker = 2;
        }

        if (CommandInput[0] != 0 && CommandInput[1] == 0 && CommandInput[2] == 0 && Input.GetKey(KeyCode.I) && KeyUpChecker == 1)
        {
            CommandInput[1] = 2;
        }
        if (CommandInput[1] == 2 && Input.GetKeyUp(KeyCode.I) && KeyUpChecker == 1)
        {
            KeyUpChecker = 2;
        }

        if (CommandInput[0] != 0 && CommandInput[1] == 0 && CommandInput[2] == 0 && Input.GetKey(KeyCode.O) && KeyUpChecker == 1)
        {
            CommandInput[1] = 3;
        }
        if (CommandInput[1] == 3 && Input.GetKeyUp(KeyCode.O) && KeyUpChecker == 1)
        {
            KeyUpChecker = 2;
        }

        if (CommandInput[0] != 0 && CommandInput[1] != 0 && CommandInput[2] == 0 && Input.GetKey(KeyCode.U) && KeyUpChecker == 2)/*?R?}???h3???????L?[????*/
        {
            CommandInput[2] = 1;
        }
        if (CommandInput[2] == 1 && Input.GetKeyUp(KeyCode.U) && KeyUpChecker == 2)
        {
            KeyUpChecker = 3;
        }

        if (CommandInput[0] != 0 && CommandInput[1] != 0 && CommandInput[2] == 0 && Input.GetKey(KeyCode.I) && KeyUpChecker == 2)
        {
            CommandInput[2] = 2;
        }
        if (CommandInput[2] == 2 && Input.GetKeyUp(KeyCode.I) && KeyUpChecker == 2)
        {
            KeyUpChecker = 3;
        }

        if (CommandInput[0] != 0 && CommandInput[1] != 0 && CommandInput[2] == 0 && Input.GetKey(KeyCode.O) && KeyUpChecker == 2)
        {
            CommandInput[2] = 3;
        }
        if (CommandInput[2] == 3 && Input.GetKeyUp(KeyCode.O) && KeyUpChecker == 2)
        {
            KeyUpChecker = 3;
        }


        if (CommandInput[0] == 1 && CommandInput[1] == 2 && CommandInput[2] == 3 && KeyUpChecker == 3)/*?X?s?[?h?A?b?v?R?}???h(U I O)*/
        {
            CommandChecker_speed = 1;
            KeyUpChecker = 0;
            for (i = 0; i < 3; i++)/*CommandInput????????*/
            {
                CommandInput[i] = 0;
            }
        }else if (CommandInput[0] == 3 && CommandInput[1] == 1 && CommandInput[2] == 2 && KeyUpChecker == 3)/*?W?????v?????R?}???h(O U I)*/
        {
            CommandChecker_jumppower = 1;
            KeyUpChecker = 0;
            for (i = 0; i < 3; i++)/*CommandInput????????*/
            {
                CommandInput[i] = 0;
            }

        }else if (KeyUpChecker == 3)/*?R?}???h?????????????????????Z?b?g*/
        {
            Count_CommandMiss += 1;
            KeyUpChecker = 0;
            for (i = 0; i < 3; i++)/*CommandInput????????*/
            {
                CommandInput[i] = 0;
            }
        }


        if (CommandChecker_speed == 1 && countdown_speed == countdown_speed_define)/*?X?s?[?h?A?b?v*/
        {
            Pianoaudio.PlayOneShot(Pianosound);
            pianochange_speed = 1;
        }

        if (CommandChecker_jumppower == 1 && countdown_jumppower == countdown_jumppower_define)/*?W?????v????*/
        {
            Pianoaudio.PlayOneShot(Pianosound);
            pianochange_jumppower = 1;
        }


        if (pianochange_speed == 1)
        {
            Player.speed = speed_p;
            Debug.Log("test_speed");
            timer1.SetActive(true);
            timericon1.SetActive(true);
            gauge1.value = countdown_speed / countdown_speed_define;
            countdown_speed -= Time.deltaTime;/*?^?C?}?[*/

            if (countdown_speed <= 0)/*?????o??*/
            {
                pianochange_speed = 0;/*????*/
                countdown_speed = countdown_speed_define;/*?^?C?}?[??????*/
                timer1.SetActive(false);
                timericon1.SetActive(false);
                CommandChecker_speed = 0;/*?R?}???h????*/              
            }
        }
        else
        {
            Player.speed = 10f;/*????*/
        }

        if (pianochange_jumppower == 1)
        {
            Player.jumppower = jumppower_p;
            Debug.Log("test_jump");
            timer2.SetActive(true);
            timericon2.SetActive(true);
            gauge2.value = countdown_jumppower / countdown_jumppower_define;
            countdown_jumppower -= Time.deltaTime;/*?^?C?}?[*/

            if (countdown_jumppower <= 0)/*?????o??*/
            {
                pianochange_jumppower = 0;/*????*/
                countdown_jumppower = countdown_jumppower_define;/*?^?C?}?[??????*/
                timer2.SetActive (false);
                timericon2.SetActive(false);
                CommandChecker_jumppower = 0;/*?R?}???h????*/
            }
        }
        else
        {
            Player.jumppower = 350;/*????*/
        }
    }
}

