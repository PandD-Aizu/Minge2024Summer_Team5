using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piano_ChangeSpeedAndJumpPower : MonoBehaviour{
    public Player Player;
    public int pianochange_speed = 0;/*切り替えスイッチ*/
    public int pianochange_jumppower = 0;
    public float speed_p = 100f;    /*変換後の速度*/
    public int jumppower_p = 500;  /*変換後のジャンプ力*/
    public float countdown_speed = 10.0f;/*速度upの効果時間*/
    public float countdown_speed_define = 10.0f;
    public float countdown_jumppower = 10.0f;/*ジャンプ力upの効果時間*/
    public float countdown_jumppower_define = 10.0f;
    public int CommandChecker_speed = 0;/*ピアノのコマンド判定*/
    public int CommandChecker_jumppower = 0;
    public int[] CommandInput = { 0, 0, 0 };
    public int KeyUpChecker = 0;
    public int Count_CommandMiss = 0;/*仮*/
    int i, j;
    

    private void Start()
    {
        Player = GameObject.FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))/*仮*/
        {
            Debug.Log("test1");
            CommandChecker_speed = 1;
            //CommandChecker_jumppower = 1;
            //Player.jumppower = 350;/*仮*/
        }
        
        if (CommandInput[0] == 0 && CommandInput[1] == 0 && CommandInput[2] == 0 && Input.GetKey(KeyCode.U) && KeyUpChecker == 0)/*コマンド1つ目のキー判定*/ /*自分の知識不足で長いコードになってしまった反省*/
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

        if (CommandInput[0] != 0 && CommandInput[1] == 0 && CommandInput[2] == 0 && Input.GetKey(KeyCode.U) && KeyUpChecker == 1)/*コマンド2つ目のキー判定*/
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

        if (CommandInput[0] != 0 && CommandInput[1] != 0 && CommandInput[2] == 0 && Input.GetKey(KeyCode.U) && KeyUpChecker == 2)/*コマンド3つ目のキー判定*/
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


        if (CommandInput[0] == 1 && CommandInput[1] == 2 && CommandInput[2] == 3 && KeyUpChecker == 3)/*スピードアップコマンド(U I O)*/
        {
            CommandChecker_speed = 1;
            KeyUpChecker = 0;
            for (i = 0; i < 3; i++)/*CommandInputの初期化*/
            {
                CommandInput[i] = 0;
            }
        }else if (CommandInput[0] == 3 && CommandInput[1] == 1 && CommandInput[2] == 2 && KeyUpChecker == 3)/*ジャンプ強化コマンド(O U I)*/
        {
            CommandChecker_jumppower = 1;
            KeyUpChecker = 0;
            for (i = 0; i < 3; i++)/*CommandInputの初期化*/
            {
                CommandInput[i] = 0;
            }

        }else if (KeyUpChecker == 3)/*コマンド間違っているときのリセット*/
        {
            Count_CommandMiss += 1;
            KeyUpChecker = 0;
            for (i = 0; i < 3; i++)/*CommandInputの初期化*/
            {
                CommandInput[i] = 0;
            }
        }


        if (CommandChecker_speed == 1 && countdown_speed == countdown_speed_define)/*スピードアップ*/
        {
            pianochange_speed = 1;
        }

        if (CommandChecker_jumppower == 1 && countdown_jumppower == countdown_jumppower_define)/*ジャンプ強化*/
        {
            pianochange_jumppower = 1;
        }


        if (pianochange_speed == 1)
        {
            Player.speed = speed_p;
            Debug.Log("test_speed");
            countdown_speed -= Time.deltaTime;/*タイマー*/
            if (countdown_speed <= 0)/*時間経過*/
            {
                pianochange_speed = 0;/*戻す*/
                countdown_speed = countdown_speed_define;/*タイマー初期化*/
                CommandChecker_speed = 0;/*コマンド戻す*/
            }

        }
        else
        {
            Player.speed = 10f;/*戻す*/
        }

        if (pianochange_jumppower == 1)
        {
            Player.jumppower = jumppower_p;
            Debug.Log("test_jump");
            countdown_jumppower -= Time.deltaTime;/*タイマー*/
            if (countdown_jumppower <= 0)/*時間経過*/
            {
                pianochange_jumppower = 0;/*戻す*/
                countdown_jumppower = countdown_jumppower_define;/*タイマー初期化*/
                CommandChecker_jumppower = 0;/*コマンド戻す*/
            }
        }
        else
        {
            Player.jumppower = 250;/*戻す*/
        }
    }
    

}

